using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class SettingsController : BaseController, IController
{
    #region ControlerProperties

    private EnergyUse.Core.UnitOfWork.Setting? _unitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public SettingsController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        _unitOfWork = new EnergyUse.Core.UnitOfWork.Setting(_dbFileName);
    }

    #endregion

    #region Methods

    public void resetColorsAndLayout()
    {
        DeleteSetting("BackgroundColorForms");
        DeleteSetting("SliderColor");
    }

    public void resetChartSettings()
    {
        DeleteSetting("BackgroundColorChart");
        DeleteSetting("ForeColorChart");
        DeleteSetting("LineColorChart");
        DeleteSetting("LabelsYColorChart");
        DeleteSetting("GraphType");
        DeleteSetting("UseAllDataForAvg");
    }

    public void resetDataPredictionSettings()
    {
        DeleteSetting("AvgCorrectionPercentage");
        DeleteSetting("AvgCorrectionPercentageReturn");

        DeleteSetting("UseAllDataForAvg");
        DeleteSetting("CalculateAvgDateFrom");
        DeleteSetting("AvgDateFromDate");
    }

    #endregion
}