using System.Globalization;
using EnergyUse.Core.Context;
using EnergyUse.Models;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Import;

public sealed class MeterReadingImport(string databaseFile)
{
    private readonly string _databasePath = Path.GetFullPath(databaseFile);

    public async Task<ImportPreview> PrepareAsync(IEnumerable<MeterReading> source, long addressId, long energyTypeId)
    {
        // No tracked database entities are exposed to the preview.
        using var context = new EnergyUseContext(_databasePath, initializeDatabase: false);
        using var transaction = await context.Database.BeginTransactionAsync();
        var meters = await context.Meters.AsNoTracking()
            .Where(m => m.AddressId == addressId && m.EnergyTypeId == energyTypeId).OrderBy(m => m.Id).ToListAsync();
        var stored = await readings(context, addressId, energyTypeId).AsNoTracking().ToListAsync();
        var indexed = source.Select((reading, index) => (Reading: copy(reading),
            Line: reading is CsvMeterReading csv ? csv.SourceRow : index + 2)).ToList();
        if (indexed.Any(item => item.Reading.RateNormal < 0 || item.Reading.RateLow < 0 ||
            item.Reading.ReturnDeliveryNormal < 0 || item.Reading.ReturnDeliveryLow < 0))
            throw new InvalidDataException("Meter readings cannot be negative.");
        // The query already scopes readings to one address and energy type.
        // A physical meter replacement does not create a separate daily reading key.
        var existingByDay = stored.ToLookup(row => row.RegistrationDate.Date);
        var hasDuplicateDays = existingByDay.Any(group => group.Count() > 1);
        var rows = new List<ImportPreviewRow>();
        foreach (var group in indexed.GroupBy(item => item.Reading.RegistrationDate.Date).OrderBy(item => item.Key))
        {
            var matches = meters.Where(m => m.ActiveFrom.Date <= group.Key &&
                (m.ActiveTill == null || m.ActiveTill.Value.Date >= group.Key)).ToList();
            var meter = matches.Count == 1 ? matches[0] : null;
            var existing = existingByDay[group.Key].ToList();
            var differentMeter = existing.Count == 1 && existing[0].MeterId != meter?.Id;
            var candidates = group.DistinctBy(item => values(item.Reading)).ToList();
            var reason = meter == null ? (matches.Count == 0 ? "No active meter on this day." : "Multiple active meters on this day.") :
                existing.Count > 1 ? "Multiple stored readings for this address, energy type and day, including across physical meters. Review these separately; only Skip is available." :
                hasDuplicateDays ? "This address and energy type have duplicate days elsewhere in the database. Review these separately; only Skip is available." :
                differentMeter ? "A reading for this address, energy type and day already exists on another meter. Review the meter assignment separately; only Skip is available." :
                candidates.Count > 1 ? "Source rows contain different readings for the same day." : "";
            var status = reason != "" ? ImportStatus.Conflict : existing.Count == 0 ? ImportStatus.New :
                values(existing[0]) == values(candidates[0].Reading) ? ImportStatus.Unchanged : ImportStatus.Changed;
            var choices = new List<ImportChoice> { new("Skip / keep stored data", null) };
            if (meter != null && !hasDuplicateDays && !differentMeter)
            {
                foreach (var candidate in candidates)
                {
                    var reading = copy(candidate.Reading);
                    reading.Id = existing.SingleOrDefault()?.Id;
                    reading.MeterId = meter.Id;
                    reading.EnergyTypeId = energyTypeId;
                    choices.Add(new ImportChoice($"Source row {candidate.Line}: {describe(reading)}", reading));
                }
            }
            var row = new ImportPreviewRow
            {
                Date = group.Key, MeterId = meter?.Id, MeterName = meter?.Description ?? "Unknown / ambiguous",
                ExistingId = existing.Count == 1 ? existing[0].Id : null,
                StoredReading = existing.Count == 1 ? existing[0] : null,
                Status = status, Reason = reason, SourceRows = string.Join(", ", group.Select(item => item.Line)),
                ExistingValues = string.Join("; ", existing.Select(item => $"#{item.Id} (meter {item.MeterId}): {describe(item)}")),
                ExistingConsumption = string.Join("; ", existing.Select(item => $"#{item.Id}: {ImportOverview.Consumption(item)}")),
                IncomingValues = string.Join("; ", candidates.Select(item => $"Row {item.Line}: {describe(item.Reading)}")),
                Choices = choices
            };
            row.SelectedChoice = status == ImportStatus.Conflict ? null : status == ImportStatus.Unchanged ? choices[0] : choices[1];
            rows.Add(row);
        }
        return new ImportPreview
        {
            Rows = rows, SourceCount = indexed.Count, AddressId = addressId, EnergyTypeId = energyTypeId,
            DatabasePath = _databasePath,
            OriginalReadings = stored.Select(ReadingSnapshot.From).ToList(),
            OriginalMeters = meters.Select(MeterSnapshot.From).ToList()
        };
    }

    public async Task<int> SaveAsync(ImportPreview preview)
    {
        if (preview.IsSaved || !string.Equals(preview.DatabasePath, _databasePath, StringComparison.OrdinalIgnoreCase))
            throw new InvalidOperationException("Prepare a new import preview before saving.");
        if (preview.UnresolvedCount > 0)
            throw new InvalidOperationException("Resolve every conflict or explicitly choose Skip.");
        using var context = new EnergyUseContext(_databasePath, initializeDatabase: false);
        // Acquire the SQLite write transaction before rechecking the preview and making changes.
        using var transaction = await context.Database.BeginTransactionAsync();
        var current = await readings(context, preview.AddressId, preview.EnergyTypeId).ToListAsync();
        var meters = await context.Meters.AsNoTracking()
            .Where(m => m.AddressId == preview.AddressId && m.EnergyTypeId == preview.EnergyTypeId).OrderBy(m => m.Id).ToListAsync();
        if (!current.Select(ReadingSnapshot.From).SequenceEqual(preview.OriginalReadings) ||
            !meters.Select(MeterSnapshot.From).SequenceEqual(preview.OriginalMeters))
            throw new InvalidOperationException("The database has changed since this preview. Reload the file and review the changes.");

        if (preview.SelectedCount > 0 && current.GroupBy(row => row.RegistrationDate.Date).Any(group => group.Count() > 1))
            throw new InvalidOperationException("This address and energy type have duplicate days. Review them before importing.");

        var affected = new HashSet<long?>();
        var changedDays = new HashSet<(long? MeterId, DateTime Date)>();
        var count = 0;
        foreach (var row in preview.Rows)
        {
            var selected = row.SelectedChoice;
            if (selected == null || !row.Choices.Contains(selected))
                throw new InvalidOperationException("Invalid import choice.");
            if (selected.Reading == null || row.Status == ImportStatus.Unchanged) continue;
            var incoming = selected.Reading;
            var existing = row.ExistingId == null ? null : current.Single(item => item.Id == row.ExistingId);
            if (existing == null)
            {
                existing = copy(incoming);
                existing.Id = null;
                context.MeterReadings.Add(existing);
                current.Add(existing);
            }
            else
            {
                existing.RateNormal = incoming.RateNormal;
                existing.RateLow = incoming.RateLow;
                existing.ReturnDeliveryNormal = incoming.ReturnDeliveryNormal;
                existing.ReturnDeliveryLow = incoming.ReturnDeliveryLow;
            }
            existing.WeekNo = ISOWeek.GetWeekOfYear(existing.RegistrationDate);
            affected.Add(existing.MeterId);
            changedDays.Add((existing.MeterId, existing.RegistrationDate.Date));
            count++;
        }
        // Include neighbours outside the CSV range so historical corrections keep their deltas consistent.
        foreach (var meterId in affected)
        {
            var meterRows = current.Where(item => item.MeterId == meterId).OrderBy(item => item.RegistrationDate).ToList();
            if (meterRows.GroupBy(item => item.RegistrationDate.Date).Any(group => group.Count() > 1))
                throw new InvalidOperationException($"Meter {meterId} has duplicate days. Review them before importing this meter.");
            for (var index = 0; index < meterRows.Count; index++)
            {
                var row = meterRows[index];
                var next = index + 1 < meterRows.Count ? meterRows[index + 1] : null;
                if (!changedDays.Contains((meterId, row.RegistrationDate.Date)) &&
                    (next == null || !changedDays.Contains((meterId, next.RegistrationDate.Date)))) continue;
                row.DeltaNormal = next == null ? 0 : Math.Max(0, next.RateNormal - row.RateNormal);
                row.DeltaLow = next == null ? 0 : Math.Max(0, next.RateLow - row.RateLow);
                row.ReturnDeliveryDeltaNormal = next == null ? 0 : Math.Max(0, next.ReturnDeliveryNormal - row.ReturnDeliveryNormal);
                row.ReturnDeliveryDeltaLow = next == null ? 0 : Math.Max(0, next.ReturnDeliveryLow - row.ReturnDeliveryLow);
            }
        }
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        preview.IsSaved = true;
        return count;
    }

    private static IQueryable<MeterReading> readings(EnergyUseContext context, long addressId, long energyTypeId) =>
        context.MeterReadings.Where(row => row.EnergyTypeId == energyTypeId && row.Meter != null && row.Meter.AddressId == addressId)
            .OrderBy(row => row.Id);

    private static (decimal, decimal, decimal, decimal) values(MeterReading row) =>
        (row.RateNormal, row.RateLow, row.ReturnDeliveryNormal, row.ReturnDeliveryLow);

    private static string describe(MeterReading row) =>
        $"N {row.RateNormal} / L {row.RateLow} / return N {row.ReturnDeliveryNormal} / L {row.ReturnDeliveryLow}";

    private static MeterReading copy(MeterReading row) => new()
    {
        Id = row.Id, MeterId = row.MeterId, EnergyTypeId = row.EnergyTypeId,
        RegistrationDate = row.RegistrationDate.Date, RateNormal = row.RateNormal, RateLow = row.RateLow,
        ReturnDeliveryNormal = row.ReturnDeliveryNormal, ReturnDeliveryLow = row.ReturnDeliveryLow
    };
}
