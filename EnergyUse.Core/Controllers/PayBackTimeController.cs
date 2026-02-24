using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class PayBackTimeController : BaseController, IController
{
    #region ControlerProperties

    public UnitOfWork.PayBackTime? UnitOfWork { get; set; } = null;

    private LibPeriodicDate? _libPeriodicDate = null;

    #endregion

    #region InitControler

    public PayBackTimeController(string dbFileName) : base(dbFileName)
    {
        _libPeriodicDate = new LibPeriodicDate(_dbFileName);
        
    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.PayBackTime(_dbFileName);
    }

    #endregion

    public async Task<PayBackTime> CalculatePayBackPeriodAsync(ParameterCalcPeriod parameterCalcPeriod)
    {
        decimal quantityReduction = 0;
        decimal totalCapacity = parameterCalcPeriod.TotalCapacitySolarPanels;

        Models.Common.PayBackTime payBackTime = new();
        payBackTime.PeriodId = parameterCalcPeriod.PeriodId;
        payBackTime.StartPeriod = parameterCalcPeriod.PeriodStart;
        payBackTime.EndPeriod = parameterCalcPeriod.PeriodStart.AddYears(1);

        var settlementDataList = new List<SettlementData>();
        List<PeriodicData> periodicData = new();

        if (parameterCalcPeriod.Address.TariffGroup != null)
        {
            long defaultTarifGroupId = parameterCalcPeriod.Address.DefaultTariffGroupId.HasValue ? parameterCalcPeriod.Address.DefaultTariffGroupId.Value : 0;

            if (payBackTime.StartPeriod >= DateTime.Now)
                quantityReduction = Common.Libs.LibGeneral.GetQuantityReduction(parameterCalcPeriod.QualityReductionSolarPanels, parameterCalcPeriod.PeriodId);

            if (quantityReduction != 0)
                totalCapacity = totalCapacity * (quantityReduction / 100);

            ParameterPeriod parameterPeriod = new()
            {
                EnergyType = parameterCalcPeriod.EnergyType,
                AddressId = parameterCalcPeriod.Address.Id,
                StartRange = payBackTime.StartPeriod,
                EndRange = payBackTime.EndPeriod,
                ShowType = EnergyUse.Common.Enums.ShowType.Value,
                PeriodType = EnergyUse.Common.Enums.Period.SettlementDay,
                PredictMissingData = true,
                TarifGroupId = defaultTarifGroupId,
                QuantityReduction = quantityReduction / 100
            };

            if (_libPeriodicDate != null)
            {
                periodicData = await _libPeriodicDate.GetRangeAsync(parameterPeriod);
            }

            // Get all cost category with cost and values
            var costCategories = UnitOfWork?.CostCategoryRepo.MapCostCategories(periodicData);
            if (costCategories != null)
            {
                // Kolom: verbruikte energie in kw
                payBackTime.ValueConsumed = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 1 || w.CostCategory.EnergySubTypeId == 2).Sum(s => s.ValueBaseConsumed), 2);
                // Kolom: Opgewekte energie in kw
                payBackTime.ValueProduced = Math.Abs(Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 3 || w.CostCategory.EnergySubTypeId == 4).Sum(s => s.ValueBaseProduced), 2));

                // Kolom: Geschatte direct verbruik in kw
                payBackTime.EstimateDirectUsed = Math.Round((totalCapacity * (parameterCalcPeriod.AverageReturn / 100)) - payBackTime.ValueProduced, 2);
                payBackTime.ValueProducedEstimateDirectUsed = Math.Round(payBackTime.EstimateDirectUsed * await GetPricePerUnitPerYear(parameterCalcPeriod.PeriodStart.Year + parameterCalcPeriod.PeriodId, defaultTarifGroupId, parameterCalcPeriod.EnergyType.Id), 2);

                // Kolom: Verbruikte energie in Euro                
                payBackTime.MonetaryValueConsumed = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 1 || w.CostCategory.EnergySubTypeId == 2).Sum(s => s.Value), 2);
                // Kolom: Overige (vast) kosten in euro
                payBackTime.OtherCostConsumed = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 5).Sum(s => s.Value), 2);

                // Kolom: Opgewekte energie in Euro (inc kosten)
                payBackTime.MonetaryValueProduced = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 3 || w.CostCategory.EnergySubTypeId == 4).Sum(s => s.Value), 2);
                payBackTime.OtherCostProduced = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 6 || w.CostCategory.EnergySubTypeId == 7).Sum(s => s.Value), 2);
                payBackTime.NettoProduced = payBackTime.MonetaryValueProduced + payBackTime.OtherCostProduced;

                // Kolom: Total kosten in euro
                payBackTime.TotalCost = payBackTime.MonetaryValueConsumed + payBackTime.MonetaryValueProduced + payBackTime.OtherCostConsumed + payBackTime.OtherCostProduced;

                // Kolom: ROI
                payBackTime.ReturnOnInvestment = Math.Abs(payBackTime.MonetaryValueProduced) + Math.Abs(payBackTime.OtherCostProduced) + Math.Abs(payBackTime.ValueProducedEstimateDirectUsed);

                if (parameterCalcPeriod.InitialInvestment != 0)
                    payBackTime.Return = Math.Round((payBackTime.ReturnOnInvestment / parameterCalcPeriod.InitialInvestment) * 100, 2);
            }
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