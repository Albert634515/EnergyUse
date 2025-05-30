using EnergyUse.Core.Interfaces;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class MainController : BaseController, IController
{
    #region ControlerProperties

    private EnergyUse.Core.UnitOfWork.MainForm? _unitOfWork { get; set; } = null;

    #endregion

    public MainController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        _unitOfWork = new EnergyUse.Core.UnitOfWork.MainForm(_dbFileName);
    }


    #region Address

    public IEnumerable<Models.Address> GetAllAddresses()
    {
        if (_unitOfWork?.AddressRepo == null)
            throw new InvalidOperationException("UnitOfWork or AddressRepo is not initialized.");

        return _unitOfWork.AddressRepo.GetAll().ToList();
    }

    #endregion

    #region EnergyType

    public IEnumerable<Models.EnergyType> getEnergyTypesByAddressId(long addressId)
    {
        if (_unitOfWork?.AddressRepo == null)
            throw new InvalidOperationException("UnitOfWork or AddressRepo is not initialized.");

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
        if (_libSettings != null)
            return _libSettings.GetMainSpitterDistance(splitterName);
        else
            return 360;
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
            throw new Exception("Unknown report type");
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