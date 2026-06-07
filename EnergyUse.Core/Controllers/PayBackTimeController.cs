using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class PayBackTimeController : BaseController, IController
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
            EndPeriod = p.PeriodStart.AddYears(1)
        };

        long defaultTariffGroupId = p.Address.DefaultTariffGroupId ?? 0;

        // ---------------------------------------------------------
        // 1. Apply degradation (only for future periods)
        // ---------------------------------------------------------
        if (payBackTime.StartPeriod >= DateTime.Now)
            quantityReduction = Common.Libs.LibGeneral.GetQuantityReduction(p.QualityReductionSolarPanels, p.PeriodId);

        if (quantityReduction != 0)
            totalCapacity *= (quantityReduction / 100);

        // ---------------------------------------------------------
        // 2. Load periodic data (measured or predicted)
        // ---------------------------------------------------------
        List<PeriodicData> periodicData = new();
        if (_libPeriodicDate != null)
        {
            var parameterPeriod = new ParameterPeriod
            {
                EnergyType = p.EnergyType,
                AddressId = p.Address.Id,
                StartRange = payBackTime.StartPeriod,
                EndRange = payBackTime.EndPeriod,
                ShowType = EnergyUse.Common.Enums.ShowType.Value,
                PeriodType = EnergyUse.Common.Enums.Period.SettlementDay,
                PredictMissingData = true,
                TarifGroupId = defaultTariffGroupId,
                QuantityReduction = quantityReduction / 100m
            };

            periodicData = await _libPeriodicDate.GetRangeAsync(parameterPeriod);
        }

        var costCategories = UnitOfWork?.CostCategoryRepo.MapCostCategories(periodicData);
        if (costCategories == null)
            return payBackTime;

        // ---------------------------------------------------------
        // 3. Consumption & production from periodic data
        // ---------------------------------------------------------
        payBackTime.ValueConsumed = Math.Round(
            costCategories.Where(w => w.CostCategory.EnergySubTypeId is 1 or 2)
                          .Sum(s => s.ValueBaseConsumed), 2);

        payBackTime.ValueProduced = Math.Abs(Math.Round(
            costCategories.Where(w => w.CostCategory.EnergySubTypeId is 3 or 4)
                          .Sum(s => s.ValueBaseProduced), 2));

        // ---------------------------------------------------------
        // 4. Theoretical yearly production (kWh)
        // totalCapacity is in Wp → convert to kWp
        // AverageReturn = kWh per kWp per year
        // ---------------------------------------------------------
        decimal yearlyEstimateProduction = totalCapacity - payBackTime.ValueProduced;
        payBackTime.EstimateDirectUsed = yearlyEstimateProduction;

        // ---------------------------------------------------------
        // 6. Feed-in = remaining production
        // ---------------------------------------------------------
        decimal feedInKwh = Math.Max(0, payBackTime.ValueProduced - payBackTime.EstimateDirectUsed);

        // ---------------------------------------------------------
        // 7. Determine tariffs
        // ---------------------------------------------------------
        decimal pricePerKwh = await GetPricePerUnitPerYear(payBackTime.StartPeriod.Year, defaultTariffGroupId, p.EnergyType.Id);

        // ---------------------------------------------------------
        // 8. Determine feed-in tariff from periodic data
        // Works for measured AND predicted data (IsPredicted = true)
        // ---------------------------------------------------------
        decimal producedKwhDb = Math.Abs(
                                        costCategories.Where(x => x.CostCategory.EnergySubTypeId is 3 or 4)
                                                      .Sum(x => x.ValueBaseProduced));

        decimal producedEuroDb = Math.Abs(
                                        costCategories.Where(x => x.CostCategory.EnergySubTypeId is 3 or 4)
                                                      .Sum(x => x.Value));

        // €/kWh = total € / total kWh
        decimal feedInTariff = producedKwhDb > 0 ? producedEuroDb / producedKwhDb : 0m;

        // ---------------------------------------------------------
        // 9. Monetary values
        // ---------------------------------------------------------

        // Savings from direct consumption (self-use)
        payBackTime.MonetaryValueConsumed = Math.Round(payBackTime.EstimateDirectUsed * pricePerKwh, 2);

        // Income from feed-in (use predicted/measured € directly)
        payBackTime.MonetaryValueProduced = Math.Abs(
            costCategories.Where(x => x.CostCategory.EnergySubTypeId is 3 or 4)
                          .Sum(x => x.Value));

        // NEW: Monetary value of direct-used solar energy
        payBackTime.ValueProducedEstimateDirectUsed = Math.Round(payBackTime.EstimateDirectUsed * pricePerKwh, 2);

        // NEW: Percentage of solar energy used directly
        payBackTime.EstimateDirectUsedPercentage = payBackTime.EstimateDirectUsed > 0 ? Math.Round((payBackTime.EstimateDirectUsed / totalCapacity) * 100m, 2) : 0;

        payBackTime.OtherCostConsumed = Math.Round(
            costCategories.Where(w => w.CostCategory.EnergySubTypeId == 5)
                          .Sum(s => s.Value), 2);

        payBackTime.OtherCostProduced = Math.Round(
            costCategories.Where(w => w.CostCategory.EnergySubTypeId is 6 or 7)
                          .Sum(s => s.Value), 2);

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
            payBackTime.MonetaryValueProduced;    // income from feed-in

        if (p.InitialInvestment != 0)
        {
            payBackTime.Return = Math.Round(
                (payBackTime.ReturnOnInvestment / p.InitialInvestment) * 100m, 2);
        }

        return payBackTime;
    }

    public async Task<decimal> GetPricePerUnitPerYear(int year, long defaultTarifGroupId, long energyTypeId)
    {
        decimal price = 0;
        if (UnitOfWork != null)
        {
            if (year <= DateTime.Now.Year)
            {
                var calculatedUnitPrice = await UnitOfWork.CalculatedUnitPriceRepo.GetByYear(year, energyTypeId, defaultTarifGroupId);
                if (calculatedUnitPrice != null)
                    price = calculatedUnitPrice.Price;
            }

            if (price == 0)
            {
                price = await UnitOfWork.CalculatedUnitPriceRepo.GetByAverage(energyTypeId, defaultTarifGroupId);
            }
        }

        return price;
    }
}