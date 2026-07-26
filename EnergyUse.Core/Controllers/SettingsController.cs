using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class SettingsController : BaseController, IController
{
    #region ControlerProperties

    private EnergyUse.Core.UnitOfWork.Setting _unitOfWork { get; set; }

    #endregion

    #region InitControler

    public SettingsController(string dbFileName) : base(dbFileName)
    {
        _unitOfWork = new EnergyUse.Core.UnitOfWork.Setting(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion

    #region Methods

    public void ResetColorsAndLayout()
    {
        DeleteSetting("BackgroundColorForms");
        DeleteSetting("SliderColor");
    }

    public void ResetChartSettings()
    {
        DeleteSetting("BackgroundColorChart");
        DeleteSetting("ForeColorChart");
        DeleteSetting("LineColorChart");
        DeleteSetting("LabelsYColorChart");
        DeleteSetting("GraphType");
        DeleteSetting("UseAllDataForAvg");
    }

    public void ResetDataPredictionSettings()
    {
        DeleteSetting("AvgCorrectionPercentage");
        DeleteSetting("AvgCorrectionPercentageReturn");

        DeleteSetting("UseAllDataForAvg");
        DeleteSetting("CalculateAvgDateFrom");
        DeleteSetting("AvgDateFromDate");
    }

    #endregion
}