﻿using EnergyUse.Core.Interfaces;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class RateController : IController
{
    #region ControlerProperties

    private string _dbFileName { get; set; } = string.Empty;
    public EnergyUse.Core.UnitOfWork.Rate? UnitOfWork { get; set; } = null;
    private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

    public bool InitSettings { get; set; } = false;

    #endregion

    #region InitControler

    public RateController(string dbFileName)
    {
        _dbFileName = dbFileName;
    }

    public void Initialize()
    {
        setUnitOfWork();
        setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Rate(_dbFileName);
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

    #region methods

    public RateTaxInfo GetRateIncExTax(Models.CostCategory costCategory, Models.Rate rate)
    {
        var rateTaxInfo = new EnergyUse.Models.Common.RateTaxInfo();

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