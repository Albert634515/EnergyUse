using EnergyUse.Models;

namespace EnergyUse.Core.Import;

public static class ImportOverview
{
    // Display rows only: stored-only rows never become import choices or changes to save.
    public static IReadOnlyList<ImportPreviewRow> Merge(IEnumerable<MeterReading> stored, IEnumerable<ImportPreviewRow> imported)
    {
        var preview = imported.ToList();
        var importedDays = preview.Select(row => row.Date.Date).ToHashSet();
        var readings = stored.ToList();
        var duplicateDays = readings.GroupBy(row => row.RegistrationDate.Date)
            .Where(group => group.Count() > 1).Select(group => group.Key).ToHashSet();
        return preview.Concat(readings.Where(row => !importedDays.Contains(row.RegistrationDate.Date)).Select(row => new ImportPreviewRow
        {
            IsImported = false,
            StoredReading = row,
            Date = row.RegistrationDate.Date,
            MeterId = row.MeterId,
            MeterName = row.Meter?.Description ?? $"Meter {row.MeterId}",
            Status = duplicateDays.Contains(row.RegistrationDate.Date) ? ImportStatus.Conflict : ImportStatus.Unchanged,
            Reason = duplicateDays.Contains(row.RegistrationDate.Date) ? "Multiple stored readings for this address, energy type and day. Review these separately." : "",
            ExistingValues = $"#{row.Id} (meter {row.MeterId}): N {row.RateNormal} / L {row.RateLow} / return N {row.ReturnDeliveryNormal} / L {row.ReturnDeliveryLow}",
            ExistingConsumption = Consumption(row)
        })).OrderByDescending(row => row.Date).ThenBy(row => row.MeterId).ToList();
    }

    internal static string Consumption(MeterReading row) =>
        $"N {row.DeltaNormal} / L {row.DeltaLow} / return N {row.ReturnDeliveryDeltaNormal} / L {row.ReturnDeliveryDeltaLow}";
}
