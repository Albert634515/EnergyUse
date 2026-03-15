using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class SelectReportParametersController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.SelectParameter UnitOfWork { get; set; }


    #endregion

    #region InitControler

    public SelectReportParametersController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.SelectParameter(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}