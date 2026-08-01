using EnergyUse.Core.Interfaces;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Controllers;

public class MainController : BaseController, IController
{
    #region ControlerProperties

    private EnergyUse.Core.UnitOfWork.MainForm _unitOfWork { get; set; }

    #endregion

    public MainController(string dbFileName) : base(dbFileName)
    {
        _unitOfWork = new EnergyUse.Core.UnitOfWork.MainForm(_dbFileName);
    }

    public void Initialize()
    {
        InitSettings = true;

        base.setSettingsManager();
    }

    #region Address

    public async Task<IEnumerable<Models.Address>> GetAllAddresses()
    {
        if (_unitOfWork.AddressRepo == null)
            throw new InvalidOperationException("UnitOfWork or AddressRepo is not initialized.");

        var addresses = await _unitOfWork.AddressRepo.GetAll();
        return addresses.ToList();
    }

    #endregion

    #region EnergyType

    public async Task<IEnumerable<Models.EnergyType>> GetEnergyTypesByAddressId(long addressId)
    {
        if (_unitOfWork?.AddressRepo == null)
            throw new InvalidOperationException("UnitOfWork or AddressRepo is not initialized.");

        return (await _unitOfWork.EnergyTypeRepo.SelectByAddressId(addressId)).ToList();
    }

    #endregion

    #region MeterReadings

    public async Task RecalculateReadingsDiffPreviousDay(DateTime startRange, DateTime endRange, long energyTypeId, long addressId)
    {
        var libMeterReading = new EnergyUse.Core.Manager.LibMeterReading(_dbFileName);
        await libMeterReading.RecalculateReadingsDiffPreviousDay(DateTime.MinValue, DateTime.MinValue, energyTypeId, addressId);
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

    public async Task<string> GetReportPdfAsync(ParameterSelection parameterSelection)
    {
        if (parameterSelection == null) return string.Empty;

        switch (parameterSelection.ReportType)
        {
            case EnergyUse.Common.Enums.ReportType.SettlementCompact:
                var compactReport = new EnergyUse.Core.Reports.SettlementCompact(_dbFileName);
                return await compactReport.GetSettlementPdfAsync(parameterSelection);

            case EnergyUse.Common.Enums.ReportType.SettlementSplitByType:
                var splitReport = new EnergyUse.Core.Reports.SettlementSplitByType(_dbFileName);
                return await splitReport.GetSettlementPdfAsync(parameterSelection);

            case EnergyUse.Common.Enums.ReportType.Rates:
                var address = await _unitOfWork.AddressRepo.Get(parameterSelection.AddressId);
                if (address is null)
                    throw new InvalidOperationException($"Address with ID {parameterSelection.AddressId} was not found.");

                var rateReport = new EnergyUse.Core.Reports.RatingReport(_dbFileName);
                return await rateReport.GetRatingReportPdf(address, parameterSelection);

            default:
                throw new InvalidOperationException($"Unknown report type: {parameterSelection.ReportType}.");
        }
    }

    #endregion
}
