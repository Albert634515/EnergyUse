using EnergyUse.Core.Context;
using EnergyUse.Core.Import;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using Microsoft.Data.Sqlite;
using Xunit;

namespace EnergyUse.Tests;

public class MeterReadingImportTests : IDisposable
{
    private readonly string _directory = Path.Combine(Path.GetTempPath(), $"energy-import-{Guid.NewGuid():N}");
    private string database => Path.Combine(_directory, "test.db");
    private MeterReadingImport importer => new(database);
    private static readonly DateTime day = new(2026, 1, 10);

    public MeterReadingImportTests()
    {
        Directory.CreateDirectory(_directory);
        using var context = new EnergyUseContext(database);
        context.Addresses.Add(new Address { Id = 1, Description = "Import address", DefaultTariffGroupId = 1, GeneralTariffGroupId = 1 });
        context.Meters.Add(new Meter { Id = 1, Description = "Meter one", AddressId = 1, EnergyTypeId = 1, ActiveFrom = day.AddYears(-1) });
        context.SaveChanges();
    }

    [Fact]
    public async Task PreviewClassifiesRowsWithoutChangingDatabase()
    {
        add(0, 100);
        add(1, 110);
        var preview = await prepare(row(0, 100), row(1, 115), row(2, 120));
        Assert.Equal(1, preview.NewCount);
        Assert.Equal(1, preview.ChangedCount);
        Assert.Equal(1, preview.UnchangedCount);
        Assert.Equal(0, preview.ConflictCount);
        using var verify = new EnergyUseContext(database);
        Assert.Equal(2, verify.MeterReadings.Count());
        Assert.Equal(110, verify.MeterReadings.Single(r => r.RegistrationDate == day.AddDays(1)).RateNormal);
    }

    [Fact]
    public async Task ConflictingCsvRowsRequireExplicitChoiceAndSaveChosenValuesOnly()
    {
        var preview = await prepare(row(0, 100), row(0, 200));
        var conflict = Assert.Single(preview.Rows);
        Assert.Equal(ImportStatus.Conflict, conflict.Status);
        Assert.Equal("2, 3", conflict.SourceRows);
        Assert.Equal(1, preview.UnresolvedCount);
        await Assert.ThrowsAsync<InvalidOperationException>(() => importer.SaveAsync(preview));
        conflict.SelectedChoice = conflict.Choices[2];
        Assert.Equal(1, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        var saved = Assert.Single(verify.MeterReadings);
        Assert.Equal(200, saved.RateNormal);
        Assert.Equal(1, saved.MeterId);
        Assert.Equal(1, saved.EnergyTypeId);
    }

    [Fact]
    public async Task IdenticalCsvRowsAreVisibleButInsertedOnlyOnceAndReimportIsUnchanged()
    {
        var preview = await prepare(row(0, 100), row(0, 100));
        Assert.Equal(2, preview.SourceCount);
        Assert.Equal(1, preview.ExtraSourceRows);
        Assert.Equal(1, preview.NewCount);
        Assert.Equal(1, await importer.SaveAsync(preview));
        var repeated = await prepare(row(0, 100), row(0, 100));
        Assert.Equal(1, repeated.UnchangedCount);
        Assert.Equal(0, await importer.SaveAsync(repeated));
        using var verify = new EnergyUseContext(database);
        Assert.Single(verify.MeterReadings);
    }

    [Fact]
    public async Task SkippedChangesPreserveStoredReadings()
    {
        add(0, 100);
        var preview = await prepare(row(0, 200), row(1, 300), row(1, 400));
        foreach (var item in preview.Rows) item.SelectedChoice = item.Choices[0];
        Assert.Equal(0, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.Equal(100, Assert.Single(verify.MeterReadings).RateNormal);
    }

    [Fact]
    public async Task ExistingDuplicateDayIsBlockedAndNeverSilentlyOverwritten()
    {
        add(0, 100);
        add(0, 200);
        var preview = await prepare(row(0, 300), row(2, 400));
        Assert.Equal(2, preview.ConflictCount);
        Assert.All(preview.Rows, item => Assert.Single(item.Choices));
        foreach (var item in preview.Rows) item.SelectedChoice = item.Choices[0];
        Assert.Equal(0, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.Equal(new[] { 100m, 200m }, verify.MeterReadings.OrderBy(item => item.Id).Select(item => item.RateNormal));
    }

    [Fact]
    public async Task HistoricalCorrectionRecalculatesNeighboursBeyondCsvRangeAndAllCounters()
    {
        add(-1, 90);
        add(0, 100);
        add(1, 130);
        var preview = await prepare(row(0, 110));
        Assert.Equal(1, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        var stored = verify.MeterReadings.OrderBy(item => item.RegistrationDate).ToList();
        Assert.Equal(new[] { 20m, 20m, 0m }, stored.Select(item => item.DeltaNormal));
        Assert.Equal(new[] { 40m, 40m, 0m }, stored.Select(item => item.DeltaLow));
        Assert.Equal(new[] { 60m, 60m, 0m }, stored.Select(item => item.ReturnDeliveryDeltaNormal));
        Assert.Equal(new[] { 80m, 80m, 0m }, stored.Select(item => item.ReturnDeliveryDeltaLow));
    }

    [Theory]
    [InlineData("changed")]
    [InlineData("inserted")]
    [InlineData("meter period")]
    public async Task DatabaseChangesSincePreviewAbortSave(string change)
    {
        add(0, 100);
        var preview = await prepare(row(0, 200), row(1, 300));
        using (var other = new EnergyUseContext(database))
        {
            if (change == "changed") other.MeterReadings.Single().RateNormal = 999;
            else if (change == "inserted") other.MeterReadings.Add(storedRow(1, 999));
            else other.Meters.Single().ActiveTill = day;
            other.SaveChanges();
        }
        await Assert.ThrowsAsync<InvalidOperationException>(() => importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.DoesNotContain(verify.MeterReadings, item => item.RateNormal == 200 || item.RateNormal == 300);
    }

    [Fact]
    public async Task MissingAndOverlappingMetersAppearAsConflicts()
    {
        using (var context = new EnergyUseContext(database))
        {
            context.Meters.Single().ActiveTill = day.AddDays(1);
            context.Meters.Add(new Meter { Id = 2, Description = "Overlap", AddressId = 1, EnergyTypeId = 1, ActiveFrom = day, ActiveTill = day.AddDays(1) });
            context.SaveChanges();
        }
        var preview = await prepare(row(0, 100), row(3, 200));
        Assert.Equal(2, preview.ConflictCount);
        Assert.All(preview.Rows, item => Assert.Single(item.Choices));
    }

    [Fact]
    public async Task FileAcrossMeterReplacementUsesCorrectMeterAndDoesNotLinkDeltasAcrossMeters()
    {
        using (var context = new EnergyUseContext(database))
        {
            context.Meters.Single().ActiveTill = day;
            context.Meters.Add(new Meter { Id = 2, Description = "Replacement", AddressId = 1, EnergyTypeId = 1, ActiveFrom = day.AddDays(1) });
            context.SaveChanges();
        }
        var preview = await prepare(row(0, 1000), row(1, 10));
        Assert.Equal(new long?[] { 1, 2 }, preview.Rows.Select(item => item.MeterId));
        Assert.Equal(2, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.All(verify.MeterReadings, item => Assert.Equal(0, item.DeltaNormal));
    }

    [Fact]
    public async Task SaveCannotBeRepeatedAndNegativeCountersAreRejected()
    {
        var preview = await prepare(row(0, 100));
        await importer.SaveAsync(preview);
        await Assert.ThrowsAsync<InvalidOperationException>(() => importer.SaveAsync(preview));
        await Assert.ThrowsAsync<InvalidDataException>(() => prepare(row(1, -100)));
    }

    [Fact]
    public async Task TransactionRollsBackAllRowsIfDatabaseRejectsOneInsert()
    {
        using (var db = new SqliteConnection($"Data Source={database}"))
        {
            db.Open();
            using var command = db.CreateCommand();
            command.CommandText = "CREATE TRIGGER RejectImport BEFORE INSERT ON MeterReadings WHEN NEW.RateNormal > 150 BEGIN SELECT RAISE(ABORT, 'test rejection'); END";
            command.ExecuteNonQuery();
        }
        var preview = await prepare(row(0, 100), row(1, 200));
        await Assert.ThrowsAnyAsync<Exception>(() => importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.Empty(verify.MeterReadings);
        Assert.False(preview.IsSaved);
    }

    [Theory]
    [InlineData("\n")]
    [InlineData("\r\n")]
    [InlineData("\r")]
    public async Task CsvKeepsBlankLinePositionsAndReportsMalformedRow(string newline)
    {
        var file = Path.Combine(_directory, "import.csv");
        File.WriteAllText(file, string.Join(newline, "Date;Normal;Low;ReturnNormal;ReturnLow", "2026-01-10;100;0;0;0", ";;;;", "2026-01-11;110;0;0;0", ""));
        var source = LibEpplus.ReadCsvFile(file, new EnergyType { Id = 1 }, new Meter { Id = 1 });
        Assert.Equal(2, source.Count);
        var preview = await prepare(source.ToArray());
        Assert.Equal(new[] { "2", "4" }, preview.Rows.Select(item => item.SourceRows));
        File.WriteAllText(file, "Date;Normal\n2026-01-10;invalid\n");
        Assert.Contains("2", Assert.Throws<InvalidDataException>(() => LibEpplus.ReadCsvFile(file, new EnergyType(), new Meter())).Message);
    }

    [Fact]
    public async Task RecalculationPreservesUnrelatedHistoricalDeltas()
    {
        add(-10, 50);
        add(-1, 90);
        add(0, 100);
        using (var context = new EnergyUseContext(database))
        {
            context.MeterReadings.Single(r => r.RegistrationDate == day.AddDays(-10)).DeltaNormal = 777;
            context.SaveChanges();
        }
        await importer.SaveAsync(await prepare(row(0, 110)));
        using var verify = new EnergyUseContext(database);
        Assert.Equal(777, verify.MeterReadings.Single(r => r.RegistrationDate == day.AddDays(-10)).DeltaNormal);
        Assert.Equal(20, verify.MeterReadings.Single(r => r.RegistrationDate == day.AddDays(-1)).DeltaNormal);
    }

    [Fact]
    public void CsvParsesDutchDecimalValuesAndAcceptsEmptyFile()
    {
        var file = Path.Combine(_directory, "decimal.csv");
        var culture = System.Globalization.CultureInfo.CurrentCulture;
        try
        {
            System.Globalization.CultureInfo.CurrentCulture = new System.Globalization.CultureInfo("nl-NL");
            File.WriteAllText(file, "Date;Normal;Low;ReturnNormal;ReturnLow\r\n2026-01-10;123,45;6,7;8,9;0\r\n");
            var reading = Assert.Single(LibEpplus.ReadCsvFile(file, new EnergyType(), new Meter()));
            Assert.Equal(123.45m, reading.RateNormal);
            Assert.Equal(6.7m, reading.RateLow);
            File.WriteAllText(file, "");
            Assert.Empty(LibEpplus.ReadCsvFile(file, new EnergyType(), new Meter()));
        }
        finally { System.Globalization.CultureInfo.CurrentCulture = culture; }
    }

    [Fact]
    public async Task DuplicateDayAcrossPhysicalMetersForSameAddressAndEnergyTypeIsBlocked()
    {
        add(0, 100);
        using (var context = new EnergyUseContext(database))
        {
            context.Meters.Add(new Meter { Id = 2, Description = "Old meter", AddressId = 1, EnergyTypeId = 1,
                ActiveFrom = day.AddYears(-2), ActiveTill = day.AddYears(-1).AddDays(-1) });
            var duplicate = storedRow(0, 200);
            duplicate.MeterId = 2;
            duplicate.RegistrationDate = day.AddHours(12);
            context.MeterReadings.Add(duplicate);
            context.SaveChanges();
        }
        var preview = await prepare(row(0, 300), row(1, 400));
        Assert.Equal(2, preview.ConflictCount);
        Assert.Contains("meter 1", preview.Rows[0].ExistingValues);
        Assert.Contains("meter 2", preview.Rows[0].ExistingValues);
        Assert.All(preview.Rows, item => Assert.Single(item.Choices));
        await Assert.ThrowsAsync<InvalidOperationException>(() => importer.SaveAsync(preview));
        foreach (var item in preview.Rows) item.SelectedChoice = item.Choices[0];
        Assert.Equal(0, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.Equal(2, verify.MeterReadings.Count());
    }

    [Fact]
    public async Task ExistingDayOnDifferentPhysicalMeterCannotBeInsertedAgainOrSilentlyReassigned()
    {
        using (var context = new EnergyUseContext(database))
        {
            context.Meters.Add(new Meter { Id = 2, Description = "Old meter", AddressId = 1, EnergyTypeId = 1,
                ActiveFrom = day.AddYears(-2), ActiveTill = day.AddYears(-1).AddDays(-1) });
            var existing = storedRow(0, 100);
            existing.MeterId = 2;
            context.MeterReadings.Add(existing);
            context.SaveChanges();
        }
        var preview = await prepare(row(0, 200));
        var conflict = Assert.Single(preview.Rows);
        Assert.Equal(ImportStatus.Conflict, conflict.Status);
        Assert.Contains("another meter", conflict.Reason);
        conflict.SelectedChoice = Assert.Single(conflict.Choices);
        Assert.Equal(0, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.Equal(2, Assert.Single(verify.MeterReadings).MeterId);
    }

    [Fact]
    public async Task SameDayAtOtherAddressOrForOtherEnergyTypeIsNotDuplicate()
    {
        using (var context = new EnergyUseContext(database))
        {
            context.Addresses.Add(new Address { Id = 2, Description = "Other address", DefaultTariffGroupId = 1, GeneralTariffGroupId = 1 });
            context.Meters.Add(new Meter { Id = 2, Description = "Other address meter", AddressId = 2, EnergyTypeId = 1, ActiveFrom = day.AddYears(-1) });
            context.Meters.Add(new Meter { Id = 3, Description = "Other energy type", AddressId = 1, EnergyTypeId = 3, ActiveFrom = day.AddYears(-1) });
            var otherAddress = storedRow(0, 100);
            otherAddress.MeterId = 2;
            var otherEnergy = storedRow(0, 200);
            otherEnergy.MeterId = 3;
            otherEnergy.EnergyTypeId = 3;
            context.MeterReadings.AddRange(otherAddress, otherEnergy);
            context.SaveChanges();
        }
        var preview = await prepare(row(0, 300));
        Assert.Equal(1, preview.NewCount);
        Assert.Equal(0, preview.ConflictCount);
        Assert.Equal(1, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.Equal(3, verify.MeterReadings.Count());
    }

    [Fact]
    public async Task CombinedOverviewKeepsStoredHistoryAndUsesOriginalImportConflictChoices()
    {
        add(-1, 90);
        add(0, 100);
        var preview = await prepare(row(0, 110), row(1, 120), row(1, 130));
        using var context = new EnergyUseContext(database);
        var stored = context.MeterReadings.ToList();
        var overview = ImportOverview.Merge(stored, preview.Rows);
        Assert.Equal(3, overview.Count);
        Assert.Equal(2, preview.Rows.Count);
        Assert.Single(overview.Where(item => item.Date == day));
        var history = Assert.Single(overview.Where(item => item.Date == day.AddDays(-1)));
        Assert.Equal("Stored", history.StatusText);
        Assert.False(history.CanChoose);
        var conflict = Assert.Single(overview.Where(item => item.Status == ImportStatus.Conflict));
        Assert.Same(preview.Rows[1], conflict);
        Assert.Null(conflict.RateNormal);
        var changed = Assert.Single(overview.Where(item => item.Status == ImportStatus.Changed));
        Assert.Equal(110m, changed.RateNormal);
        changed.SelectedChoice = changed.Choices[0];
        Assert.Equal(100m, changed.RateNormal);
        changed.SelectedChoice = changed.Choices[1];
        await Assert.ThrowsAsync<InvalidOperationException>(() => importer.SaveAsync(preview));
        conflict.SelectedChoice = conflict.Choices[2];
        Assert.Equal(130m, conflict.RateNormal);
        Assert.Equal(0, preview.UnresolvedCount);
        Assert.Equal(2, await importer.SaveAsync(preview));
        using var verify = new EnergyUseContext(database);
        Assert.Equal(3, verify.MeterReadings.Count());
        Assert.Equal(90, verify.MeterReadings.Single(item => item.RegistrationDate == day.AddDays(-1)).RateNormal);
        Assert.Equal(130, verify.MeterReadings.Single(item => item.RegistrationDate == day.AddDays(1)).RateNormal);
    }

    [Fact]
    public void CombinedOverviewPreservesExistingDuplicateRecordsAsVisibleConflicts()
    {
        var first = storedRow(0, 100);
        first.Id = 10;
        var second = storedRow(0, 200);
        second.Id = 20;
        second.MeterId = 2;
        var overview = ImportOverview.Merge([first, second], []);
        Assert.Equal(2, overview.Count);
        Assert.All(overview, item =>
        {
            Assert.Equal("Conflict", item.StatusText);
            Assert.False(item.CanChoose);
        });
    }

    private Task<ImportPreview> prepare(params MeterReading[] source) => importer.PrepareAsync(source, 1, 1);
    private static MeterReading row(int offset, decimal normal) => new()
    {
        RegistrationDate = day.AddDays(offset), RateNormal = normal, RateLow = normal * 2,
        ReturnDeliveryNormal = normal * 3, ReturnDeliveryLow = normal * 4
    };
    private static MeterReading storedRow(int offset, decimal normal)
    {
        var reading = row(offset, normal);
        reading.MeterId = 1;
        reading.EnergyTypeId = 1;
        return reading;
    }
    private void add(int offset, decimal normal)
    {
        using var context = new EnergyUseContext(database);
        context.MeterReadings.Add(storedRow(offset, normal));
        context.SaveChanges();
    }
    public void Dispose()
    {
        SqliteConnection.ClearAllPools();
        Directory.Delete(_directory, recursive: true);
    }
}
