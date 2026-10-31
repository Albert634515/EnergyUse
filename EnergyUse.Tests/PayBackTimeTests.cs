using EnergyUse.Core.Context;
using EnergyUse.Core.Controllers;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using Microsoft.Data.Sqlite;
using Xunit;

namespace EnergyUse.Tests;

public class PayBackTimeTests
{
    private static readonly DateTime start = new(2024, 1, 1);
    private static readonly DateTime end = new(2024, 12, 31);
    private static readonly TariffGroup[] groups =
        [new() { Id = 1, Description = "Eneco" }, new() { Id = 2, Description = "Greenchoice" }];

    [Fact]
    public void ContractSwitchUsesInclusiveDatesAndIgnoresOtherEnergyTypes()
    {
        var periods = select([
            date(start, new(2024, 6, 30), 1),
            date(new(2024, 7, 1), end, 2),
            date(start, end, 2, 2)]);
        Assert.Equal(2, periods.Count);
        Assert.Equal(new TariffPeriod(start, new(2024, 6, 30), 1, "Eneco"), periods[0]);
        Assert.Equal(new TariffPeriod(new(2024, 7, 1), end, 2, "Greenchoice"), periods[1]);
    }

    [Fact]
    public void UnlinkedDatesUseAddressDefaultAndSameGroupOverlapsAreMerged()
    {
        var periods = select([date(new(2024, 2, 1), new(2024, 2, 15), 1),
            date(new(2024, 2, 10), new(2024, 2, 29), 1)]);
        Assert.Equal(new TariffPeriod(start, end, 1, "Eneco"), Assert.Single(periods));
        Assert.Equal(1, Assert.Single(select([])).TariffGroupId);
    }

    [Fact]
    public void ConflictingContractsAreRejectedOnSharedBoundaryDay()
    {
        var error = Assert.Throws<RateValidationException>(() => select([
            date(start, new(2024, 7, 1), 1), date(new(2024, 7, 1), end, 2)]));
        Assert.Contains("01-07-2024", error.Message);
    }

    [Fact]
    public void FuturePeriodsKeepLastContractAndHistoricalGapsKeepDefault()
    {
        var dates = new[] { date(new(2024, 2, 1), new(2024, 2, 29), 2),
            date(new(2024, 3, 1), new(2024, 12, 31), 1, 2) };
        var periods = select(dates);
        Assert.Equal(new TariffPeriod(start, new(2024, 1, 31), 1, "Eneco"), periods[0]);
        Assert.Equal(new TariffPeriod(new(2024, 2, 1), end, 2, "Greenchoice"), periods[1]);
        Assert.Equal(new TariffPeriod(new(2025, 1, 1), new(2025, 12, 31), 2, "Greenchoice"),
            Assert.Single(PayBackTariffSelection.Select(new(2025, 1, 1), new(2025, 12, 31), 1, 1, dates, groups)));
    }

    [Fact]
    public void FutureProjectionRejectsAmbiguousLastContract()
    {
        Assert.Throws<RateValidationException>(() => PayBackTariffSelection.Select(
            new(2025, 1, 1), new(2025, 12, 31), 1, 1,
            [date(start, end, 1), date(start, end, 2)], groups));
    }

    [Fact]
    public async Task ControllerLoadsExpiredContractForFutureSelection()
    {
        using var db = new TestDatabase();
        db.Context.PreDefinedPeriods.Add(new PreDefinedPeriod { Description = "Last contract",
            PreDefinedPeriodDates = [date(start, end, 2)] });
        db.Context.SaveChanges();
        using var controller = new PayBackTimeController(db.Path);
        Assert.Equal(2, Assert.Single(await controller.GetTariffPeriodsAsync(
            new(2025, 1, 1), new(2025, 12, 31), db.Address, 1)).TariffGroupId);
    }

    [Theory]
    [InlineData(100, 0.363, 0.121, 0.0605)]
    [InlineData(0, 0.121, 0.0, 0.0605)]
    public async Task RoiIncludesVatNettingAndReturnCostsWithoutLosingFlooredCredits(
        int netting, decimal deliveryCredit, decimal taxCredit, decimal returnCost)
    {
        using var db = new TestDatabase();
        db.Context.EnergySubTypes.Add(new EnergySubType { Id = 6, Description = "Return costs normal" });
        db.Context.Meters.Add(new Meter { Id = 1, Description = "Meter", AddressId = 1,
            EnergyTypeId = 1, Active = true, ActiveFrom = start.AddYears(-1) });
        for (var day = start; day <= end; day = day.AddDays(1))
            db.Context.MeterReadings.Add(new MeterReading { MeterId = 1, EnergyTypeId = 1,
                RegistrationDate = day, DeltaNormal = day == start ? 2 : 0,
                ReturnDeliveryDeltaNormal = day == start ? 1 : 0 });
        db.Context.Nettings.Add(new Netting { EnergyTypeId = 1, StartDate = start, EndDate = end, Rate = netting });
        foreach (var (id, subtype, rate) in new (long, long, decimal)[] {
            (1, 1, .3m), (2, 3, -.1m), (3, 5, .1m), (4, 6, -.05m) })
        {
            db.Context.CostCategories.Add(new CostCategory { Id = id, Name = $"Category {id}",
                EnergyTypeId = 1, EnergySubTypeId = subtype, CalculationTypeId = 1, TariffGroupTypeId = 2,
                CalculateVat = true, CanNotBeNegative = subtype == 3 });
            db.Context.TariffGroupCostCategories.Add(new TariffGroupCostCategory { TariffGroupId = 1, CostCategoryId = id });
            db.Context.Rates.Add(new Rate { EnergyTypeId = 1, CostCategoryId = id, TariffGroupId = 1,
                StartRate = start, EndRate = end, RateValue = rate });
            db.Context.VatTarifs.Add(new VatTarif { CostCategoryId = id, StartDate = start, EndDate = end, Tarif = 21m });
        }
        db.Context.SaveChanges();
        using var controller = new PayBackTimeController(db.Path);
        var result = await controller.CalculatePayBackPeriodAsync(new ParameterCalcPeriod {
            PeriodId = 1, PeriodStart = start, Address = db.Address,
            EnergyType = db.Context.EnergyTypes.Single(type => type.Id == 1),
            TotalCapacitySolarPanels = 1000, AverageReturn = 100, InitialInvestment = 2000 });
        var credit = Math.Round(deliveryCredit, 2, MidpointRounding.AwayFromZero)
            + Math.Round(taxCredit, 2, MidpointRounding.AwayFromZero);
        Assert.Equal(1m, result.ValueProduced);
        Assert.Equal(2m, result.ValueConsumed);
        Assert.Equal(credit, result.MonetaryValueProduced);
        Assert.Equal(-Math.Round(returnCost, 2, MidpointRounding.AwayFromZero), result.OtherCostProduced);
        Assert.Equal(result.MonetaryValueConsumed + credit + result.OtherCostProduced, result.ReturnOnInvestment);
        Assert.Equal(result.MonetaryValueProduced + result.OtherCostProduced, result.NettoProduced);
    }

    [Fact]
    public async Task RoiUsesLinkedGroupsForFeedInAndYearlyUnitPrice()
    {
        using var db = new TestDatabase();
        var switchDate = new DateTime(2024, 7, 1);
        db.Context.PreDefinedPeriods.Add(new PreDefinedPeriod
        {
            Description = "Contracts", PreDefinedPeriodDates =
                [date(start, switchDate.AddDays(-1), 1), date(switchDate, end, 2)]
        });
        db.Context.Meters.Add(new Meter { Id = 1, Description = "Meter", AddressId = 1,
            EnergyTypeId = 1, Active = true, ActiveFrom = start.AddYears(-1) });
        for (var day = start; day <= end; day = day.AddDays(1))
            db.Context.MeterReadings.Add(new MeterReading { MeterId = 1, EnergyTypeId = 1,
                RegistrationDate = day, DeltaNormal = 2, ReturnDeliveryDeltaNormal = 1 });
        foreach (long subtype in new long[] { 1, 3 })
        {
            db.Context.CostCategories.Add(new CostCategory { Id = subtype, Name = $"Category {subtype}",
                EnergyTypeId = 1, EnergySubTypeId = subtype, CalculationTypeId = 1, TariffGroupTypeId = 2,
                CalculateVat = true });
            db.Context.VatTarifs.Add(new VatTarif { CostCategoryId = subtype,
                StartDate = start, EndDate = end, Tarif = 0m });
            foreach (long group in new long[] { 1, 2 })
            {
                db.Context.TariffGroupCostCategories.Add(new TariffGroupCostCategory
                    { TariffGroupId = group, CostCategoryId = subtype });
                // Both providers have rates for the entire year.
                db.Context.Rates.Add(new Rate { EnergyTypeId = 1, TariffGroupId = group,
                    CostCategoryId = subtype, StartRate = start, EndRate = end,
                    RateValue = subtype == 3 ? group * -0.1m : group * 0.3m });
            }
        }
        db.Context.SaveChanges();
        var controller = new PayBackTimeController(db.Path);
        try
        {
            var result = await controller.CalculatePayBackPeriodAsync(new ParameterCalcPeriod
            {
                PeriodId = 1, PeriodStart = start, Address = db.Address,
                EnergyType = db.Context.EnergyTypes.Single(type => type.Id == 1),
                TotalCapacitySolarPanels = 1000, AverageReturn = 100, InitialInvestment = 2000
            });
            Assert.Equal(366m, result.ValueProduced);
            Assert.Equal(732m, result.ValueConsumed);
            Assert.Equal(55m, result.MonetaryValueProduced); // 182 * .10 + 184 * .20
            var expectedPrice = .3m;
            Assert.Equal(Math.Round(634m * expectedPrice, 2), result.MonetaryValueConsumed);
            Assert.Equal(result.MonetaryValueConsumed + 55m, result.ReturnOnInvestment);
            Assert.Contains("Eneco (01-01-2024 - 30-06-2024)", result.TariffGroups);
            Assert.Contains("Greenchoice (01-07-2024 - 31-12-2024)", result.TariffGroups);
        }
        finally { controller.Dispose(); }
    }

    [Fact]
    public async Task PricesFollowCalendarYearsIndependentlyOfTariffGroup()
    {
        using var db = new TestDatabase();
        db.Context.Set<CalculatedUnitPrice>().Add(new CalculatedUnitPrice
            { Year = 2023, EnergyTypeId = 1, TariffGroupId = 1, Price = .9m });
        db.Context.SaveChanges();
        var controller = new PayBackTimeController(db.Path);
        try
        {
            var price = await controller.GetPriceForTariffPeriodsAsync([
                new(new(2023, 12, 31), new(2024, 1, 1), 1, "Eneco")], 1);
            Assert.Equal(.6m, price);
            Assert.Equal(.3m, await controller.GetPricePerUnitPerYear(2024, 999, 1));
        }
        finally { controller.Dispose(); }
    }

    private static List<TariffPeriod> select(IEnumerable<PreDefinedPeriodDate> dates) =>
        PayBackTariffSelection.Select(start, end, 1, 1, dates, groups);

    private static PreDefinedPeriodDate date(DateTime from, DateTime to, long group, long energy = 1) =>
        new() { StartDate = from, EndDate = to, TariffGroupId = group, EnergyTypeId = energy };

    private sealed class TestDatabase : IDisposable
    {
        public string Path { get; } = System.IO.Path.Combine(System.IO.Path.GetTempPath(), $"EnergyUse-roi-{Guid.NewGuid():N}.db");
        public EnergyUseContext Context { get; }
        public Address Address { get; } = new() { Id = 1, Description = "Address", DefaultTariffGroupId = 1, GeneralTariffGroupId = 1 };
        public TestDatabase()
        {
            Context = new EnergyUseContext(Path);
            Context.Addresses.Add(Address);
            Context.EnergyTypes.Single(type => type.Id == 1).HasNormalAndLow = false;
            Context.EnergyTypes.Single(type => type.Id == 1).HasEnergyReturn = true;
            Context.TariffGroups.Single(group => group.Id == 1).Description = "Eneco";
            Context.TariffGroups.Single(group => group.Id == 1).TypeId = 2;
            Context.TariffGroups.Single(group => group.Id == 2).Description = "Greenchoice";
            Context.TariffGroups.Single(group => group.Id == 2).TypeId = 2;
            Context.Set<CalculatedUnitPrice>().Add(
                new CalculatedUnitPrice { Year = 2024, EnergyTypeId = 1, Price = .3m });
            Context.SaveChanges();
        }
        public void Dispose()
        {
            Context.Dispose();
            SqliteConnection.ClearAllPools();
            System.IO.File.Delete(Path);
        }
    }
}
