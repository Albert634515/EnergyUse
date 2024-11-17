using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class PayBackTimeController : IController
{
    #region ControlerProperties

    public bool InitSettings { get; set; } = false;
    public EnergyUse.Core.UnitOfWork.PayBackTime? UnitOfWork { get; set; } = null;

    private string _dbFileName { get; set; } = string.Empty;
    private LibSettings? _libSettings { get; set; } = null;
    private LibPeriodicDate? _libPeriodicDate = null;

    #endregion

    #region InitControler

    public PayBackTimeController(string dbFileName)
    {
        _dbFileName = dbFileName;
        _libPeriodicDate = new LibPeriodicDate(_dbFileName);
        UnitOfWork = new EnergyUse.Core.UnitOfWork.PayBackTime(_dbFileName);
    }

    public void Initialize()
    {
        setSettingsManager();
    }

    private void setSettingsManager()
    {
        _libSettings = new EnergyUse.Core.Manager.LibSettings(_dbFileName);
    }

    public string getDbFileName()
    {
        return _dbFileName;
    }

    #endregion

    public PayBackTime CalculatePayBackPeriod(int periodId, DateTime lastPeriodStart, DateTime startYear, Address address, EnergyType energyType, decimal initialInvestment, decimal quantityReductionPerc, decimal totalCapacity)
    {
        decimal quantityReduction = 0;

        PayBackTime payBackTime = new();
        payBackTime.PeriodId = periodId;
        payBackTime.StartPeriod = lastPeriodStart;
        payBackTime.EndPeriod = lastPeriodStart.AddYears(1);

        var settlementDataList = new List<SettlementData>();
        List<PeriodicData> periodicData = new();

        if (address.TariffGroup != null)
        {
            long tarifGroupId = address.TariffGroup.Id;

            if (payBackTime.StartPeriod >= DateTime.Now)
                quantityReduction = Common.Libs.LibGeneral.GetQuantityReduction(quantityReductionPerc, periodId);

            if (quantityReduction != 0)
                totalCapacity = totalCapacity * (quantityReduction / 100);

            ParameterPeriod parameterPeriod = new()
            {
                EnergyType = energyType,
                AddressId = address.Id,
                StartRange = payBackTime.StartPeriod,
                EndRange = payBackTime.EndPeriod,
                ShowType = EnergyUse.Common.Enums.ShowType.Value,
                PeriodType = EnergyUse.Common.Enums.Period.SettlementDay,
                PredictMissingData = true,
                TarifGroupId = tarifGroupId,
                QuantityReduction = quantityReduction / 100
            };

            periodicData = _libPeriodicDate.GetRange(parameterPeriod);
            // Get all cost category with cost and values
            var costCategories = UnitOfWork?.CostCategoryRepo.MapCostCategories(periodicData);
            if (costCategories != null)
            {
                // Kolom1: verbruikte energie in kw
                payBackTime.ValueConsumed = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 1 || w.CostCategory.EnergySubTypeId == 2).Sum(s => s.ValueBaseConsumed), 2);
                // Kolom2: Opgewekte energie in kw
                payBackTime.ValueProduced = Math.Abs(Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 3 || w.CostCategory.EnergySubTypeId == 4).Sum(s => s.ValueBaseProduced), 2));

                // Kolom3: Geschatte direct verbruik in kw
                payBackTime.EstimateDirectUsed = Math.Round(totalCapacity - payBackTime.ValueProduced, 2);

                // Kolom4: Verbruikte energie in Euro                
                payBackTime.MonetaryValueConsumed = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 1 || w.CostCategory.EnergySubTypeId == 2).Sum(s => s.Value), 2);
                // Kolom5: Overige (vast) kosten in euro
                payBackTime.OtherCostConsumed = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 5).Sum(s => s.Value), 2);

                // Kolom6: Opgewekte energie in Euro (inc kosten)
                payBackTime.MonetaryValueProduced = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 3 || w.CostCategory.EnergySubTypeId == 4).Sum(s => s.Value), 2);
                payBackTime.OtherCostProduced = Math.Round(costCategories.Where(w => w.CostCategory.EnergySubTypeId == 6 || w.CostCategory.EnergySubTypeId == 7).Sum(s => s.Value), 2);

                // Kolom7: Total kosten in euro
                payBackTime.MonetaryValueProducedAndConsumed = payBackTime.MonetaryValueConsumed + payBackTime.MonetaryValueProduced + payBackTime.OtherCostConsumed + +payBackTime.OtherCostProduced;

                // Kolom8: ROI
                payBackTime.ReturnOnInvestment = Math.Abs(payBackTime.MonetaryValueProduced) + Math.Abs(payBackTime.OtherCostProduced) + Math.Abs(payBackTime.MonetaryValueProducedAndConsumed);

                payBackTime.Return = Math.Round((payBackTime.ReturnOnInvestment / initialInvestment) * 100, 2);
            }
        }

        return payBackTime;
    }

    public decimal GetPricePerUnitPerYear(int year, long tarifGroupId, EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
    {
        decimal price = 0;
        if (year <= DateTime.Now.Year)
        {
            var calculatedUnitPrice = UnitOfWork.CalculatedUnitPriceRepo.GetByYear(year, energyType.Id, (long)address.TariffGroupId);
            if (calculatedUnitPrice != null)
                price = calculatedUnitPrice.Price;
        }

        if (price == 0)
        {
            price = UnitOfWork.CalculatedUnitPriceRepo.GetByAverage(energyType.Id, (long)address.TariffGroupId);
        }

        return price;
    }
}