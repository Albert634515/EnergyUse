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
    [InlineData(Period.Day, 6)]
    [InlineData(Period.SettlementDay, 6)]
    [InlineData(Period.Day, 1)]
    [InlineData(Period.SettlementDay, 1)]
    public async Task CalculationSkipsCategoriesAssignedOnlyToAnotherTariffGroup(Period period, long calculationType)
    {
        using var db = new TestDatabase();
        db.AddRate(1, 0.25m);
        var other = db.AddCategory(2, calculationType, calculationType == 1 ? 1 : 5);
        other.Start = day;
        db.Context.TariffGroupCostCategories.Remove(
            db.Context.TariffGroupCostCategories.Single(link => link.CostCategoryId == 2));
        db.Context.TariffGroupCostCategories.Add(new TariffGroupCostCategory
        {
            TariffGroupId = 2, CostCategoryId = 2
        });
        db.Context.Rates.Add(new Rate { EnergyTypeId = 1, CostCategoryId = 2, TariffGroupId = 2,
            StartRate = day, EndRate = day.AddYears(1), RateValue = 160m });
        db.Context.SaveChanges();
        db.AddReading(10m, 0m);
        var parameters = db.Parameters(day, day);
        parameters.PeriodType = period;

        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(parameters);
        Assert.Equal(2.50m, periodic.Sum(item => item.ValueMonetaryY));
        Assert.All(periodic, item => Assert.All(item.OtherCosts, cost => Assert.Equal(1, cost.CostCategoryId)));
        using var prices = new LibPriceRate(db.Path);
        Assert.Equal(0.25m, (await prices.GetCalculatedRate(1, day, SubEnergyType.Normal, 1)).Rate);
    }

    [Fact]
    public async Task UncheckingCategoryExcludesItEvenWhenItsRateStillExists()
    {
        using var db = new TestDatabase();
        db.AddRate(1, 0.25m);
        db.AddCategory(2, 1, 1);
        db.AddRate(2, 9m);
        db.Context.TariffGroupCostCategories.Remove(
            db.Context.TariffGroupCostCategories.Single(link => link.CostCategoryId == 2));
        db.Context.SaveChanges();
        db.AddReading(10m, 0m);

        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        Assert.Equal(2.50m, periodic.Sum(item => item.ValueMonetaryY));
        Assert.Equal(1, Assert.Single(Assert.Single(periodic).OtherCosts).CostCategoryId);
        using var prices = new LibPriceRate(db.Path);
        Assert.Equal(0.25m, (await prices.GetCalculatedRate(1, day, SubEnergyType.Normal, 1)).Rate);
    }

    [Fact]
    public async Task GeneralCostsUseTheAddressesGeneralTariffGroupForCategorySelection()
    {
        using var db = new TestDatabase();
        db.AddRate(1, 0.25m);
        var general = db.AddCategory(2, 3, 5);
        general.TariffGroupTypeId = (int)TariffGroupType.GeneralCosts;
        db.Context.Addresses.Single().GeneralTariffGroupId = 2;
        db.Context.TariffGroupCostCategories.Remove(
            db.Context.TariffGroupCostCategories.Single(link => link.CostCategoryId == 2));
        db.Context.TariffGroupCostCategories.Add(new TariffGroupCostCategory
        {
            TariffGroupId = 2, CostCategoryId = 2
        });
        db.Context.Rates.Add(new Rate { EnergyTypeId = 1, CostCategoryId = 2, TariffGroupId = 2,
            StartRate = day, EndRate = day, RateValue = 1m });
        db.Context.SaveChanges();
        db.AddReading(10m, 0m);

        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        var lines = await new RepoCostCategories(db.Context).MapCostCategories(periodic);
        Assert.Equal(2.50m, lines.Single(line => line.CostCategory.Id == 1).Value);
        Assert.Equal(1m, lines.Single(line => line.CostCategory.Id == 2).Value);
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

    [Theory]
    [InlineData(10, 4, 100, 4, "1.20")]
    [InlineData(10, 10, 100, 10, "3.00")]
    [InlineData(10, 20, 100, 10, "4.00")]
    [InlineData(0, 20, 100, 0, "2.00")]
    [InlineData(10, 40, 50, 10, "6.00")]
    [InlineData(10, 20, 0, 0, "2.00")]
    public async Task SettlementCapsNettedReturnsAndTaxCreditAtPeriodConsumption(
        int consumed, int returned, int percentage, int expectedNetted, string expectedCredit)
    {
        using var db = new TestDatabase();
        db.Context.EnergyTypes.Single(type => type.Id == 1).HasEnergyReturn = true;
        db.AddCategory(2, 1, 3);
        db.AddCategory(3, 1, 5);
        db.AddRate(1, 0.30m);
        db.AddRate(2, 0.10m);
        db.AddRate(3, 0.10m);
        db.AddReading(consumed, returned);
        db.Context.Nettings.Add(new Netting { EnergyTypeId = 1, StartDate = day, EndDate = day, Rate = percentage });
        db.Context.SaveChanges();

        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day));
        var lines = await new RepoCostCategories(db.Context).MapCostCategories(periodic);
        Assert.Equal(expectedNetted, periodic.Sum(item => item.NettingValueYReturnNormal));
        Assert.Equal(-decimal.Parse(expectedCredit, System.Globalization.CultureInfo.InvariantCulture),
            lines.Single(line => line.CostCategory.Id == 2).Value);
        Assert.Equal((consumed - expectedNetted) * 0.10m, lines.Single(line => line.CostCategory.Id == 3).Value);
    }

    [Fact]
    public async Task SettlementSharesOneNettingLimitAcrossNormalAndLowRegisters()
    {
        using var db = new TestDatabase();
        var energy = db.Context.EnergyTypes.Single(type => type.Id == 1);
        energy.HasNormalAndLow = true;
        energy.HasEnergyReturn = true;
        db.AddCategory(2, 1, 2);
        db.AddCategory(3, 1, 3);
        db.AddCategory(4, 1, 4);
        db.Context.EnergySubTypes.Add(new EnergySubType { Id = 6, Description = "Return delivery cost normal" });
        db.Context.SaveChanges();
        db.AddCategory(5, 1, 6);
        db.AddRate(1, 0.30m);
        db.AddRate(2, 0.40m);
        db.AddRate(3, 0.10m);
        db.AddRate(4, 0.20m);
        db.AddRate(5, -0.05m);
        db.AddReading(10m, 8m);
        db.Context.MeterReadings.Single().ReturnDeliveryDeltaLow = 12m;
        db.Context.Nettings.Add(new Netting { EnergyTypeId = 1, StartDate = day, EndDate = day, Rate = 100m });
        db.Context.SaveChanges();

        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = Assert.Single(await calculation.GetRangeAsync(db.Parameters(day, day)));
        var lines = await new RepoCostCategories(db.Context).MapCostCategories([periodic]);
        Assert.Equal(4m, periodic.NettingValueYReturnNormal);
        Assert.Equal(6m, periodic.NettingValueYReturnLow);
        Assert.Equal(-1.60m, lines.Single(line => line.CostCategory.Id == 3).Value);
        Assert.Equal(-3.60m, lines.Single(line => line.CostCategory.Id == 4).Value);
        Assert.Equal(0.40m, lines.Single(line => line.CostCategory.Id == 5).Value);
    }

    [Fact]
    public async Task SettlementUsesConsumptionFromTheWholePeriodAndResetsAllowanceOnReuse()
    {
        using var db = new TestDatabase();
        db.Context.EnergyTypes.Single(type => type.Id == 1).HasEnergyReturn = true;
        db.AddCategory(2, 1, 3);
        db.AddRate(1, 0.30m, day, day.AddDays(1));
        db.AddRate(2, 0.10m, day, day.AddDays(1));
        db.AddReading(0m, 20m);
        db.Context.MeterReadings.Add(new MeterReading
        {
            MeterId = 1, EnergyTypeId = 1, RegistrationDate = day.AddDays(1), DeltaNormal = 10m
        });
        db.Context.Nettings.Add(new Netting { EnergyTypeId = 1, StartDate = day, EndDate = day.AddDays(1), Rate = 100m });
        db.Context.SaveChanges();

        using var calculation = new LibPeriodicDate(db.Path);
        var periodic = await calculation.GetRangeAsync(db.Parameters(day, day.AddDays(1)));
        Assert.Equal(10m, periodic.Sum(item => item.NettingValueYReturnNormal));
        var lines = await new RepoCostCategories(db.Context).MapCostCategories(periodic);
        Assert.Equal(-4m, lines.Where(line => line.CostCategory.Id == 2).Sum(line => line.Value));

        var returnDayOnly = await calculation.GetRangeAsync(db.Parameters(day, day));
        Assert.Equal(0m, returnDayOnly.Sum(item => item.NettingValueYReturnNormal));
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

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async Task SettlementReportsExcludeTheClosingReadingDeltaAndChargeOnlyOneDay(bool splitByType)
    {
        using var db = new TestDatabase();
        db.AddCategory(2, 3, 5);
        db.AddRate(1, 0.25m, day, day.AddDays(1));
        db.AddRate(2, 1m, day, day.AddDays(1));
        db.AddReading(10m, 0m);
        var start = db.Context.MeterReadings.Single();
        start.RateNormal = 100m;
        db.Context.MeterReadings.Add(new MeterReading
        {
            MeterId = 1, EnergyTypeId = 1, RegistrationDate = day.AddDays(1),
            RateNormal = 110m, DeltaNormal = 99m
        });
        db.Context.SaveChanges();
        var selection = new SelectedEnergyType
        {
            EnergyType = db.Context.EnergyTypes.Single(type => type.Id == 1),
            TarifGroup = 1, StartRange = day, EndRange = day.AddDays(1)
        };
        var parameters = new ParameterSelection
        {
            AddressId = 1, StartRange = selection.StartRange, EndRange = selection.EndRange,
            ShowRates = true, SelectedEnergyTypeList = [selection]
        };

        var path = splitByType
            ? await new EnergyUse.Core.Reports.SettlementSplitByType(db.Path).GetSettlementPdfAsync(parameters)
            : await new EnergyUse.Core.Reports.SettlementCompact(db.Path).GetSettlementPdfAsync(parameters);
        try
        {
            using var pdf = new iText.Kernel.Pdf.PdfDocument(new iText.Kernel.Pdf.PdfReader(path));
            var text = string.Join("\n", Enumerable.Range(1, pdf.GetNumberOfPages())
                .Select(number => iText.Kernel.Pdf.Canvas.Parser.PdfTextExtractor.GetTextFromPage(pdf.GetPage(number))));
            Assert.Contains(3.50m.ToString("##0.00"), text);
            Assert.DoesNotContain(99m.ToString("##0.00"), text);
            Assert.Contains("100", text);
            Assert.Contains("110", text);
        }
        finally
        {
            System.IO.File.Delete(path);
        }
    }

    [Fact]
    public void AnnualSettlementHas365CostDaysAndKeepsTheClosingReadingOutsideCalculation()
    {
        var period = EnergyUse.Core.Reports.SettlementBase.getCalculationPeriod(new SelectedEnergyType
        {
            EnergyType = new EnergyType { Id = 1 },
            StartRange = new DateTime(2025, 10, 1), EndRange = new DateTime(2026, 10, 1)
        }, 4, false);
        Assert.Equal(new DateTime(2026, 9, 30), period.EndRange);
        Assert.Equal(365, (period.EndRange - period.StartRange).Days + 1);
    }

    [Theory]
    [InlineData(100)]
    [InlineData(0)]
    public void UnitPricesIncludeAllChargesAndCreditsPerSuppliedUnitWithActualVat(int suppliedUnits)
    {
        using var db = new TestDatabase();
        var report = new EnergyUse.Core.Reports.SettlementBase(db.Path);
        var table = report.getPricePerUnit(new SelectedEnergyType
        {
            EnergyType = new EnergyType { UnitId = "kWh" }
        }, [new PeriodicData
        {
            ValueYNormal = suppliedUnits * 0.6m, ValueYLow = suppliedUnits * 0.4m,
            ValueYReturnNormal = 40m
        }], [
            cost(1, 1, 200m, 42m),
            cost(2, 1, 50m, 0m),
            cost(3, 1, -40m, -8.4m),
            cost(6, 1, 10m, 2.1m),
            cost(5, 1, 30m, 6.3m),
            cost(5, 3, 100m, 21m),
            cost(5, 3, -20m, -4.2m)
        ]);

        decimal[] expectedExVat = [3.3m, 3.6m, 2.8m];
        decimal[] expectedIncVat = [3.888m, 4.251m, 3.283m];
        for (var row = 0; row < 3; row++)
        {
            Assert.Equal(suppliedUnits == 0 ? "N/A" : expectedExVat[row].ToString("##0.00000"), textAt(row, 6));
            Assert.Equal(suppliedUnits == 0 ? "N/A" : expectedIncVat[row].ToString("##0.00000"), textAt(row, 7));
        }

        string textAt(int row, int column) => string.Concat(table.GetCell(row, column).GetChildren()
            .OfType<iText.Layout.Element.Paragraph>().SelectMany(paragraph => paragraph.GetChildren())
            .OfType<iText.Layout.Element.Text>().Select(text => text.GetText()));

        static SettlementData cost(long subtype, long calculation, decimal net, decimal vat) => new()
        {
            Value = net, VatAmount = vat,
            CostCategory = new CostCategory
            {
                EnergySubTypeId = subtype, CalculationTypeId = calculation, UnitId = "kWh"
            }
        };
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
            Context.TariffGroupCostCategories.Add(new TariffGroupCostCategory
            {
                TariffGroupId = 1, CostCategoryId = id
            });
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
