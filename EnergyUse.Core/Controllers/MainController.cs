using EnergyUse.Core.Interfaces;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class MainController : IController
{
    #region ControlerProperties

    private string _dbFileName { get; set; }  = string.Empty;
    private EnergyUse.Core.UnitOfWork.MainForm? _unitOfWork { get; set; } = null;
    private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

    #endregion

    public MainController(string dbFileName)
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
        _unitOfWork = new EnergyUse.Core.UnitOfWork.MainForm(_dbFileName);
    }

    private void setSettingsManager()
    {
        _libSettings = new EnergyUse.Core.Manager.LibSettings(_dbFileName);
    }

    public string getDbFileName()
    {
        return _dbFileName;
    }

    #region Address

    public IEnumerable<Models.Address> GetAllAddresses()
    {
        return _unitOfWork.AddressRepo.GetAll().ToList();
    }

    #endregion

    #region EnergyType

    public IEnumerable<Models.EnergyType> getEnergyTypesByAddressId(long addressId)
    {
        return _unitOfWork.EnergyTypeRepo.SelectByAddressId(addressId).ToList();
    }

    #endregion

    #region MeterReadings

    public void RecalculateReadingsDiffPreviousDay(DateTime startRange, DateTime endRange, long energyTypeId, long addressId)
    {
        var libMeterReading = new EnergyUse.Core.Manager.LibMeterReading(_dbFileName);
        libMeterReading.RecalculateReadingsDiffPreviousDay(DateTime.MinValue, DateTime.MinValue, energyTypeId, addressId);
    }

    #endregion

    #region Settings

    public int GetMainSpitterDistance(string splitterName)
    {
        return _libSettings.GetMainSpitterDistance(splitterName);
    }

    public void SaveSetting(string settingTag, string newSettingValue)
    {
        _libSettings.SaveSetting(settingTag, newSettingValue);
    }

    public Models.Setting GetKey(string key)
    {
        return _libSettings.GetSetting(key);
    }

    #endregion

    #region Report


    public string GetSettlementPdf(ParameterSelection parameterSelection)
    {
        if (parameterSelection == null) return string.Empty;

        if (parameterSelection.ReportType == EnergyUse.Common.Enums.ReportType.SettlementCompact)
        {
            var LibSettlement = new EnergyUse.Core.Reports.SettlementCompact(_dbFileName);
            return LibSettlement.GetSettlementPdf(parameterSelection);
        } else if (parameterSelection.ReportType == EnergyUse.Common.Enums.ReportType.SettlementSplitByType)
        {
            var LibSettlement = new EnergyUse.Core.Reports.SettlementSplitByType(_dbFileName);
            return LibSettlement.GetSettlementPdf(parameterSelection);
        }
        else
        {
            var LibSettlement = new EnergyUse.Core.Reports.Settlement(_dbFileName);
            return LibSettlement.GetSettlementPdf(parameterSelection);
        }
    }

    public string GetRatingReportPdf(Models.Address address, ParameterSelection parameterSelection)
    {
        var LibSettlement = new EnergyUse.Core.Reports.RatingReport(_dbFileName);
        var fileName = LibSettlement.GetRatingReportPdf(address, parameterSelection);

        return fileName;
    }

    #endregion
}