using EnergyUse.Common.Enums;
using EnergyUse.Core.Context;
using EnergyUse.Core.Manager;
using EnergyUse.Core.Repositories;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using Microsoft.Data.Sqlite;
using Xunit;

namespace EnergyUse.Tests;

public class CalculationTests
{
    private static readonly DateTime day = new(2024, 2, 15);

    [Fact]
    public void MissingRateIncludesCategoryGroupAndDate()
    {
        var error = Assert.Throws<RateValidationException>(() => select([]));
        Assert.Contains("Delivery", error.Message);
        Assert.Contains("tariff group 1", error.Message);
        Assert.Contains("15-02-2024", error.Message);
    }

    [Fact]
    public void FutureRateDoesNotFillMissingPastRate()
    {
        Assert.Throws<RateValidationException>(() => select([rate(1, 0.3m, day.AddDays(1), day.AddDays(10))]));
    }

    [Fact]
    public void OverlappingRatesAreRejectedIncludingSharedBoundaryDay()
    {
        var error = Assert.Throws<RateValidationException>(() => select([
            rate(1, 0.3m, day.AddDays(-1), day),
            rate(2, 0.4m, day, day.AddDays(1))]));
        Assert.Contains("Overlapping", error.Message);
        Assert.Contains("1, 2", error.Message);
    }

    [Fact]
    public void DateComparisonIgnoresTimeAndIncludesBothEndpoints()
    {
        var result = select([rate(1, 0.3m, day.AddHours(12), day.AddHours(18))]);
        Assert.Equal(0.3m, result.Rate);
        Assert.False(result.LastRateUsed);
    }

    [Fact]
    public void OtherCategoryEnergyAndGroupDoNotCauseOverlapOrSupplyMissingRate()
    {
        var correct = rate(1, 0.3m, day, day);
        var otherCategory = rate(2, 9m, day, day);
        otherCategory.CostCategoryId = 2;
        var otherGroup = rate(3, 9m, day, day);
        otherGroup.TariffGroupId = 2;
        var otherEnergy = rate(4, 9m, day, day);
        otherEnergy.EnergyTypeId = 2;
        Assert.Equal(0.3m, select([correct, otherCategory, otherGroup, otherEnergy]).Rate);
        Assert.Throws<RateValidationException>(() => select([otherCategory, otherGroup, otherEnergy]));
    }

    [Fact]
    public void ExplicitZeroIsAValidRate()
    {
        Assert.Equal(0m, select([rate(1, 0m, day, day)]).Rate);
    }

    [Fact]
    public void HistoricalEstimateIsMarkedAndKeepsUnitPricePrecision()
    {
        var previous = rate(1, 0.12345m, day.AddDays(-10), day.AddDays(-1));
        previous.ExpectedPriceChange = 3m;
        var result = select([previous]);
        Assert.True(result.LastRateUsed);
        Assert.Equal(1.03m, result.PriceAdjustmentFactor);
        Assert.Equal(0.1271535m, result.Rate);
        Assert.Equal(1, result.RateId);
    }

    [Fact]
    public void MostRecentlyEndedHistoricalRateIsUsed()
    {
        Assert.Equal(0.4m, select([
            rate(1, 0.3m, day.AddDays(-20), day.AddDays(-5)),
            rate(2, 0.4m, day.AddDays(-10), day.AddDays(-1))]).Rate);
    }

    [Fact]
    public async Task SameSubtypeCategoriesKeepTheirOwnRatesInSettlement()
    {
        using var db = new TestDatabase();
        var second = db.AddCategory(2, 1, 1);
        db.AddRate(1, 0.25m);
        db.AddRate(2, 0.40m);
        using var prices = new LibPriceRate(db.Path);
        var firstPrice = await prices.GetRate(1, db.Category, day, 1);
        var secondPrice = await prices.GetRate(1, second, day, 1);
        var periodic = new PeriodicData
        {
            ValueXDate = day, ValueYNormal = 10m, RateNormal = 99m,
            OtherCosts = [
                new OtherCost { CostCategoryId = 1, RateId = firstPrice.RateId, Rate = firstPrice.Rate },
                new OtherCost { CostCategoryId = 2, RateId = secondPrice.RateId, Rate = secondPrice.Rate }]
        };
        var lines = await new RepoCostCategories(db.Context).MapCostCategories([periodic]);
        Assert.Equal(2.50m, lines.Single(line => line.CostCategory.Id == 1).Value);
        Assert.Equal(4.00m, lines.Single(line => line.CostCategory.Id == 2).Value);
        Assert.Equal(0.65m, (await prices.GetCalculatedRate(1, day, SubEnergyType.Normal, 1)).Rate);
    }

    [Fact]
    public async Task FullCalculationUsesBothDeliveryCategoriesAndTheirOwnRates()
    {
        using var db = new TestDatabase();
        db.AddCategory(2, 1, 1);
        db.AddRate(1, 0.25m);
        db.AddRate(2, 0.40m);
        db.AddReading(10m, 0m);
        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        var lines = await new RepoCostCategories(db.Context).MapCostCategories(periodic);
        Assert.Equal(2.50m, lines.Single(line => line.CostCategory.Id == 1).Value);
        Assert.Equal(4.00m, lines.Single(line => line.CostCategory.Id == 2).Value);
        Assert.Equal(6.50m, periodic.Sum(item => item.ValueMonetaryY));
    }

    [Theory]
    [InlineData(0, "-0.40")]
    [InlineData(50, "-0.80")]
    [InlineData(100, "-1.20")]
    public async Task ReturnCreditKeepsNettingAndCorrectSign(int nettingPercent, string expected)
    {
        using var db = new TestDatabase();
        db.Context.EnergyTypes.Single(type => type.Id == 1).HasEnergyReturn = true;
        db.AddCategory(2, 1, 3);
        db.AddRate(1, 0.30m);
        db.AddRate(2, 0.10m);
        db.AddReading(10m, 4m);
        db.Context.Nettings.Add(new Netting { EnergyTypeId = 1, StartDate = day, EndDate = day, Rate = nettingPercent });
        db.Context.SaveChanges();
        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        var lines = await new RepoCostCategories(db.Context).MapCostCategories(periodic);
        Assert.Equal(decimal.Parse(expected, System.Globalization.CultureInfo.InvariantCulture),
            lines.Single(line => line.CostCategory.Id == 2).Value);
    }

    [Fact]
    public async Task MultipleReturnComponentsCreditNettedDeliveryOnlyOnce()
    {
        using var db = new TestDatabase();
        db.Context.EnergyTypes.Single(type => type.Id == 1).HasEnergyReturn = true;
        db.AddCategory(2, 1, 3);
        db.AddCategory(3, 1, 3);
        db.AddRate(1, 0.30m);
        db.AddRate(2, 0.10m);
        db.AddRate(3, 0.05m);
        db.AddReading(10m, 4m);
        db.Context.Nettings.Add(new Netting { EnergyTypeId = 1, StartDate = day, EndDate = day, Rate = 50m });
        db.Context.SaveChanges();
        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        var lines = await new RepoCostCategories(db.Context).MapCostCategories(periodic);
        Assert.Equal(-0.60m, lines.Single(line => line.CostCategory.Id == 2).Value);
        Assert.Equal(-0.30m, lines.Single(line => line.CostCategory.Id == 3).Value);
        Assert.Equal(2.10m, lines.Sum(line => line.Value));
        Assert.Equal(2.10m, Math.Round(periodic.Sum(item => item.ValueMonetaryY), 2, MidpointRounding.AwayFromZero));
    }

    [Fact]
    public async Task HistoricalEstimateIsVisibleOnSettlementLine()
    {
        using var db = new TestDatabase();
        db.Category.CalculationTypeId = 3;
        db.Context.SaveChanges();
        db.AddRate(1, 1.005m, day.AddDays(-2), day.AddDays(-1));
        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        var line = Assert.Single(await new RepoCostCategories(db.Context).MapCostCategories(periodic));
        Assert.True(line.LastAvailableRateUsed);
        Assert.Equal(1.01m, line.Value);
    }

    [Fact]
    public async Task OverlapOnDayWithoutReadingsStopsSettlement()
    {
        using var db = new TestDatabase();
        db.AddRate(1, 0.25m);
        db.AddRate(1, 0.30m);
        using var calculation = new LibPeriodicDate(db.Path);
        var error = await Assert.ThrowsAsync<RateValidationException>(() => calculation.GetRangeAsync(db.Parameters(day, day)));
        Assert.Contains("Overlapping", error.Message);
    }

    [Fact]
    public async Task ValidZeroRateProducesAZeroSettlementLine()
    {
        using var db = new TestDatabase();
        db.Category.CalculationTypeId = 3;
        db.Context.SaveChanges();
        db.AddRate(1, 0m);
        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        var line = Assert.Single(await new RepoCostCategories(db.Context).MapCostCategories(periodic));
        Assert.Equal(0m, line.Value);
        Assert.False(line.LastAvailableRateUsed);
    }

    [Fact]
    public async Task AmountAndVatAreRoundedAfterAccumulatingTheEntireLine()
    {
        using var db = new TestDatabase();
        db.Category.CalculateVat = true;
        db.Context.SaveChanges();
        var days = Enumerable.Range(0, 3).Select(index => new PeriodicData
        {
            ValueXDate = day.AddDays(index),
            OtherCosts = [new OtherCost { CostCategoryId = 1, RateId = 1, Rate = 0.335m, Quantity = 1m, VatTarif = 21m }]
        }).ToList();
        var line = Assert.Single(await new RepoCostCategories(db.Context).MapCostCategories(days));
        Assert.Equal(1.01m, line.Value);
        Assert.Equal(0.21m, line.VatAmount);
        Assert.Equal(1.22m, line.Value + line.VatAmount);
    }

    [Theory]
    [InlineData("1.005", "1.01")]
    [InlineData("-1.005", "-1.01")]
    [InlineData("1.004", "1.00")]
    [InlineData("-1.004", "-1.00")]
    public async Task CompletedSettlementLineRoundsHalfCentsAwayFromZero(string rate, string expected)
    {
        using var db = new TestDatabase();
        var culture = System.Globalization.CultureInfo.InvariantCulture;
        var line = Assert.Single(await new RepoCostCategories(db.Context).MapCostCategories([
            new PeriodicData { ValueXDate = day, OtherCosts = [
                new OtherCost { CostCategoryId = 1, Rate = decimal.Parse(rate, culture), Quantity = 1m }] }]));
        Assert.Equal(decimal.Parse(expected, culture), line.Value);
    }

    [Fact]
    public async Task CompactReportKeepsTheSameRoundedTotalAsDetailedLines()
    {
        using var db = new TestDatabase();
        db.Category.CalculateVat = true;
        db.Context.SaveChanges();
        var lines = await new RepoCostCategories(db.Context).MapCostCategories([
            new PeriodicData { ValueXDate = day, OtherCosts = [
                new OtherCost { CostCategoryId = 1, RateId = 1, Rate = 0.335m, Quantity = 1m, VatTarif = 21m }] },
            new PeriodicData { ValueXDate = day.AddDays(1), OtherCosts = [
                new OtherCost { CostCategoryId = 1, RateId = 2, Rate = 0.335m, Quantity = 1m, VatTarif = 21m }] }]);
        var report = new EnergyUse.Core.Reports.SettlementBase(db.Path);
        var merged = Assert.Single(report.mergeSettlementData(lines));
        Assert.Equal(0.68m, merged.Value);
        Assert.Equal(0.14m, merged.VatAmount);
        Assert.Equal(lines.Sum(line => line.Value + line.VatAmount), merged.Value + merged.VatAmount);
    }

    [Fact]
    public async Task PercentageAndNegativeCreditUseTheSameMoneyRule()
    {
        using var db = new TestDatabase();
        db.Category.CalculationTypeId = 2;
        db.Context.SaveChanges();
        var line = Assert.Single(await new RepoCostCategories(db.Context).MapCostCategories([
            new PeriodicData { ValueXDate = day, OtherCosts = [
                new OtherCost { CostCategoryId = 1, Rate = -10m, Quantity = 10.05m }] }]));
        Assert.Equal(-1.01m, line.Value);
        Assert.Equal(0m, line.VatAmount);
    }

    [Theory]
    [InlineData(2023, 28)]
    [InlineData(2024, 29)]
    public async Task FullFebruaryWithoutReadingsChargesExactlyOneMonth(int year, int days)
    {
        using var db = new TestDatabase();
        db.Category.CalculationTypeId = 4;
        db.Category.EnergySubTypeId = 5;
        db.Context.SaveChanges();
        var from = new DateTime(year, 2, 1);
        db.AddRate(1, 30m, from, from.AddDays(days - 1));
        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(from, from.AddDays(days - 1)));
        var line = Assert.Single(await new RepoCostCategories(db.Context).MapCostCategories(periodic));
        Assert.Equal(30m, line.Value);
    }

    [Theory]
    [InlineData(ShowType.Rate)]
    [InlineData(ShowType.AvgRate)]
    [InlineData(ShowType.Efficiency)]
    [InlineData(ShowType.Unit)]
    public async Task NonMonetaryGraphDoesNotRequirePriceRates(ShowType showType)
    {
        using var db = new TestDatabase();
        db.AddReading(10m, 0m);
        var parameters = db.Parameters(day, day);
        parameters.PeriodType = Period.Day;
        parameters.ShowType = showType;
        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(parameters);
        Assert.Equal(10m, periodic.Sum(item => item.ValueYNormal));
        Assert.All(periodic, item => Assert.Empty(item.OtherCosts));
    }

    [Theory]
    [InlineData(ShowType.Value)]
    [InlineData(ShowType.AvgValue)]
    public async Task MonetaryGraphStillRejectsMissingPriceRates(ShowType showType)
    {
        using var db = new TestDatabase();
        db.AddReading(10m, 0m);
        var parameters = db.Parameters(day, day);
        parameters.PeriodType = Period.Day;
        parameters.ShowType = showType;
        using var calculation = new LibPeriodicDate(db.Path);
        await Assert.ThrowsAsync<RateValidationException>(() => calculation.GetRangeAsync(parameters));
    }

    [Fact]
    public async Task MissingRateOnDayWithoutReadingsStopsSettlement()
    {
        using var db = new TestDatabase();
        using var calculation = new LibPeriodicDate(db.Path);
        await Assert.ThrowsAsync<RateValidationException>(() => calculation.GetRangeAsync(db.Parameters(day, day)));
    }

    [Fact]
    public async Task RateChangeCreatesSeparateRoundedLines()
    {
        using var db = new TestDatabase();
        var lines = await new RepoCostCategories(db.Context).MapCostCategories([
            new PeriodicData { ValueXDate = day, OtherCosts = [
                new OtherCost { CostCategoryId = 1, RateId = 1, Rate = 0.335m, Quantity = 1m }] },
            new PeriodicData { ValueXDate = day.AddDays(1), OtherCosts = [
                new OtherCost { CostCategoryId = 1, RateId = 2, Rate = 0.335m, Quantity = 1m }] }]);
        Assert.Equal(2, lines.Count);
        Assert.Equal(0.68m, lines.Sum(line => line.Value));
    }

    private static PriceRate select(IEnumerable<Rate> rates) => RateSelection.Select(rates, 1, 1, "Delivery", day, 1);

    private static Rate rate(long id, decimal value, DateTime start, DateTime end) => new()
    {
        Id = id, EnergyTypeId = 1, CostCategoryId = 1, TariffGroupId = 1,
        RateValue = value, StartRate = start, EndRate = end
    };

    private sealed class TestDatabase : IDisposable
    {
        public string Path { get; } = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"EnergyUse-tests-{Guid.NewGuid():N}.db");
        public EnergyUseContext Context { get; }
        public CostCategory Category { get; }

        public TestDatabase()
        {
            Context = new EnergyUseContext(Path);
            Context.EnergyTypes.Single(type => type.Id == 1).HasNormalAndLow = false;
            Context.EnergyTypes.Single(type => type.Id == 1).HasEnergyReturn = false;
            Context.TariffGroups.Single(group => group.Id == 1).TypeId = 2;
            Context.Addresses.Add(new Address { Id = 1, Description = "Test address", DefaultTariffGroupId = 1, GeneralTariffGroupId = 1 });
            Context.SaveChanges();
            Category = AddCategory(1, 1, 1);
        }

        public CostCategory AddCategory(long id, long calculationTypeId, long subtypeId)
        {
            var category = new CostCategory { Id = id, Name = $"Category {id}", EnergyTypeId = 1,
                CalculationTypeId = calculationTypeId, EnergySubTypeId = subtypeId, TariffGroupTypeId = 2 };
            Context.CostCategories.Add(category);
            Context.SaveChanges();
            return category;
        }

        public void AddRate(long categoryId, decimal value, DateTime? start = null, DateTime? end = null)
        {
            Context.Rates.Add(new Rate { EnergyTypeId = 1, CostCategoryId = categoryId, TariffGroupId = 1,
                RateValue = value, StartRate = start ?? day, EndRate = end ?? day });
            Context.SaveChanges();
        }

        public void AddReading(decimal consumed, decimal returned)
        {
            Context.Meters.Add(new Meter { Id = 1, Description = "Test meter", AddressId = 1,
                EnergyTypeId = 1, Active = true, ActiveFrom = day.AddYears(-1) });
            Context.MeterReadings.Add(new MeterReading { MeterId = 1, EnergyTypeId = 1, RegistrationDate = day,
                DeltaNormal = consumed, ReturnDeliveryDeltaNormal = returned });
            Context.SaveChanges();
        }

        public ParameterPeriod Parameters(DateTime from, DateTime to) => new()
        {
            EnergyType = Context.EnergyTypes.Single(type => type.Id == 1), AddressId = 1, TarifGroupId = 1,
            StartRange = from, EndRange = to, PeriodType = Period.SettlementDay, ShowType = ShowType.Value, QuantityReduction = 1m
        };

        public void Dispose()
        {
            Context.Dispose();
            SqliteConnection.ClearAllPools();
            System.IO.File.Delete(Path);
        }
    }
}
