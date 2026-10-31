using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class PayBackTimeController : BaseController, IController, IDisposable
{
    #region ControlerProperties

    public UnitOfWork.PayBackTime UnitOfWork { get; set; }

    private LibPeriodicDate? _libPeriodicDate = null;

    #endregion

    #region InitControler

    public PayBackTimeController(string dbFileName) : base(dbFileName)
    {
        _libPeriodicDate = new LibPeriodicDate(_dbFileName);
        UnitOfWork = new EnergyUse.Core.UnitOfWork.PayBackTime(_dbFileName);

    }

    public void Dispose()
    {
        _libPeriodicDate?.Dispose();
        UnitOfWork.Dispose();
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion

    public async Task<PayBackTime> CalculatePayBackPeriodAsync(ParameterCalcPeriod p)
    {
        decimal quantityReduction = 0;
        decimal totalCapacity = p.TotalCapacitySolarPanels;

        var payBackTime = new PayBackTime
        {
            PeriodId = p.PeriodId,
            StartPeriod = p.PeriodStart,
            EndPeriod = p.PeriodStart.AddYears(1).AddDays(-1)
        };

        var tariffPeriods = await GetTariffPeriodsAsync(payBackTime.StartPeriod, payBackTime.EndPeriod,
            p.Address, p.EnergyType.Id);
        payBackTime.TariffGroups = string.Join("; ", tariffPeriods.Select(period =>
            $"{period.TariffGroupName} ({period.StartDate:dd-MM-yyyy} - {period.EndDate:dd-MM-yyyy})"));

        // ---------------------------------------------------------
        // 1. Apply degradation by installation age
        // ---------------------------------------------------------
        if (p.QualityReductionSolarPanels != 0)
            quantityReduction = Common.Libs.LibGeneral.GetQuantityReduction(p.QualityReductionSolarPanels, p.PeriodId);

        if (quantityReduction != 0)
            totalCapacity *= (quantityReduction / 100);

        // ---------------------------------------------------------
        // 2. Load periodic data (measured or predicted)
        // ---------------------------------------------------------
        List<PeriodicData> periodicData = new();
        foreach (var tariffPeriod in tariffPeriods)
        {
            var parameterPeriod = new ParameterPeriod
            {
                EnergyType = p.EnergyType,
                AddressId = p.Address.Id,
                StartRange = tariffPeriod.StartDate,
                EndRange = tariffPeriod.EndDate,
                ShowType = EnergyUse.Common.Enums.ShowType.Value,
                PeriodType = EnergyUse.Common.Enums.Period.SettlementDay,
                PredictMissingData = true,
                TarifGroupId = tariffPeriod.TariffGroupId,
                QuantityReduction = quantityReduction / 100m
            };

            if (_libPeriodicDate != null)
                periodicData.AddRange(await _libPeriodicDate.GetRangeAsync(parameterPeriod));
        }

        var costCategories = UnitOfWork == null
            ? null
            : await UnitOfWork.CostCategoryRepo.MapCostCategories(periodicData);
        if (costCategories == null)
            return payBackTime;

        // ---------------------------------------------------------
        // 3. Consumption & production from periodic data
        // ---------------------------------------------------------
        payBackTime.ValueConsumed = Math.Round(
            periodicData.Sum(day => day.ValueYNormal + day.ValueYLow), 2);

        payBackTime.ValueProduced = Math.Round(
            periodicData.Sum(day => day.ValueYReturnNormal + day.ValueYReturnLow), 2);

        // ---------------------------------------------------------
        // 4. Theoretical yearly production (kWh)
        // TotalCapacity is Wp; AverageReturn is the expected yearly yield percentage.
        // ---------------------------------------------------------
        decimal yearlyEstimateProduction = totalCapacity * (p.AverageReturn / 100m);
        payBackTime.EstimateDirectUsed = Math.Max(0, yearlyEstimateProduction - payBackTime.ValueProduced);

        // ---------------------------------------------------------
        // 7. Determine tariffs
        // ---------------------------------------------------------
        decimal pricePerKwh = await GetPriceForTariffPeriodsAsync(tariffPeriods, p.EnergyType.Id);
        if (pricePerKwh < 0)
            throw new RateValidationException("No unit price is configured for the selected tariff periods.");

        // ---------------------------------------------------------
        // 9. Monetary values
        // ---------------------------------------------------------

        // Savings from direct consumption (self-use)
        payBackTime.MonetaryValueConsumed = Math.Round(payBackTime.EstimateDirectUsed * pricePerKwh, 2);

        // Value the returned kWh directly: a settlement's non-negative floor must not
        // discard a solar credit. Include VAT and energy tax saved through netting.
        payBackTime.MonetaryValueProduced = costCategories
            .Where(line => line.CostCategory.CalculationTypeId == 1
                && line.CostCategory.EnergySubTypeId is 3 or 4 or 5)
            .Sum(getProducedCredit);

        // NEW: Monetary value of direct-used solar energy
        payBackTime.ValueProducedEstimateDirectUsed = Math.Round(payBackTime.EstimateDirectUsed * pricePerKwh, 2);

        // NEW: Percentage of solar energy used directly
        payBackTime.EstimateDirectUsedPercentage = yearlyEstimateProduction > 0 ? Math.Round((payBackTime.EstimateDirectUsed / yearlyEstimateProduction) * 100m, 2) : 0;

        payBackTime.OtherCostConsumed = Math.Round(
            costCategories.Where(w => w.CostCategory.EnergySubTypeId == 5)
                          .Sum(s => s.Value + s.VatAmount), 2);

        payBackTime.OtherCostProduced = -Math.Round(
            costCategories.Where(w => w.CostCategory.EnergySubTypeId is 6 or 7)
                          .Sum(s => s.Value + s.VatAmount), 2);

        // ---------------------------------------------------------
        // 10. Net produced value and total cost
        // ---------------------------------------------------------
        payBackTime.NettoProduced =
            payBackTime.MonetaryValueProduced + payBackTime.OtherCostProduced;

        payBackTime.TotalCost =
            payBackTime.MonetaryValueConsumed +
            payBackTime.MonetaryValueProduced +
            payBackTime.OtherCostConsumed +
            payBackTime.OtherCostProduced;

        // ---------------------------------------------------------
        // 11. ROI calculation
        // ---------------------------------------------------------
        payBackTime.ReturnOnInvestment =
            payBackTime.MonetaryValueConsumed +   // savings from self-consumption
            payBackTime.NettoProduced;            // feed-in and netting, less return costs

        if (p.InitialInvestment != 0)
        {
            payBackTime.Return = Math.Round(
                (payBackTime.ReturnOnInvestment / p.InitialInvestment) * 100m, 2);
        }

        return payBackTime;
    }

    public async Task<List<TariffPeriod>> GetTariffPeriodsAsync(DateTime start, DateTime end,
        Models.Address address, long energyTypeId)
    {
        var dates = await UnitOfWork.PreDefinedPeriodDateRepo.Find(date => date.EnergyTypeId == energyTypeId);
        var groups = await UnitOfWork.TariffGroupRepo.GetAll();
        return PayBackTariffSelection.Select(start, end, energyTypeId,
            address.DefaultTariffGroupId ?? 0, dates, groups);
    }

    private static decimal getProducedCredit(SettlementData line)
    {
        var net = Math.Round(-line.ValueBaseProduced * line.Rate, 2, MidpointRounding.AwayFromZero);
        var vat = line.CostCategory.CalculateVat
            ? Math.Round(net * line.VatTarif / 100m, 2, MidpointRounding.AwayFromZero)
            : 0m;
        return net + vat;
    }

    public async Task<decimal> GetPriceForTariffPeriodsAsync(IEnumerable<TariffPeriod> periods, long energyTypeId)
    {
        decimal total = 0;
        int days = 0;
        foreach (var period in periods)
        {
            for (int year = period.StartDate.Year; year <= period.EndDate.Year; year++)
            {
                var price = await GetPricePerUnitPerYear(year, period.TariffGroupId, energyTypeId);
                if (price < 0)
                    return -1;
                var start = period.StartDate > new DateTime(year, 1, 1) ? period.StartDate : new DateTime(year, 1, 1);
                var end = period.EndDate < new DateTime(year, 12, 31) ? period.EndDate : new DateTime(year, 12, 31);
                int periodDays = (end - start).Days + 1;
                total += price * periodDays;
                days += periodDays;
            }
        }
        return days > 0 ? total / days : -1;
    }

    public async Task<decimal> GetPricePerUnitPerYear(int year, long defaultTarifGroupId, long energyTypeId)
    {
        decimal price = 0;
        if (UnitOfWork != null)
        {
            if (year <= DateTime.Now.Year)
            {
                var calculatedUnitPrice = await UnitOfWork.CalculatedUnitPriceRepo.GetByYear(year, energyTypeId);
                if (calculatedUnitPrice != null)
                    return calculatedUnitPrice.Price;
            }

            if (price == 0)
            {
                price = await UnitOfWork.CalculatedUnitPriceRepo.GetByAverage(energyTypeId);
            }
        }

        return price;
    }
}
