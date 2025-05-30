using EnergyUse.Core.Interfaces;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class RateController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Rate? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public RateController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Rate(_dbFileName);
    }

    #endregion

    #region Methods

    public decimal GetPriceChange(EnergyUse.Models.Rate rate)
    {
        decimal priceChange = 0;

        if (UnitOfWork?.RateRepo == null)
        {
            throw new InvalidOperationException("UnitOfWork or RateRepo is not initialized.");
        }

        var previousRate = UnitOfWork.RateRepo.SelectLastRateByDate(rate.EnergyType.Id, rate.CostCategory.Id, rate.StartRate.AddDays(-1), rate.TariffGroup.Id);
        if (previousRate != null && previousRate.Id > 0)
            priceChange = Math.Round(((rate.RateValue - previousRate.RateValue) / previousRate.RateValue) * 100, 2);

        return priceChange;
    }

    public RateTaxInfo GetRateIncExTax(Models.CostCategory costCategory, Models.Rate rate)
    {
        var rateTaxInfo = new EnergyUse.Models.Common.RateTaxInfo();

        if (UnitOfWork?.RateRepo == null)
        {
            throw new InvalidOperationException("UnitOfWork or RateRepo is not initialized.");
        }

        if (rate == null || costCategory == null)
            return rateTaxInfo;

        var vatTarif = UnitOfWork.RepoVatTarif.GetByCostCategoryIdAndDate(costCategory.Id, rate.StartRate);
        if (vatTarif == null)
            return rateTaxInfo;            

        if (costCategory.CalculateVat == true)
        {
            //Rate price is ex taxes
            rateTaxInfo.Tax = rate.RateValue * vatTarif.Tarif / 100;                
        }
        else
        {
            //Rate price is inc. taxes
            rateTaxInfo.Tax = rate.RateValue / (1 + vatTarif.Tarif / 100);
        }

        rateTaxInfo.Tax = Math.Round(rateTaxInfo.Tax, 4);
        rateTaxInfo.IsTaxIncluded = costCategory.CalculateVat;
        rateTaxInfo.RateIncTax = rate.RateValue + rateTaxInfo.Tax;

        return rateTaxInfo;
    }

    #endregion
}