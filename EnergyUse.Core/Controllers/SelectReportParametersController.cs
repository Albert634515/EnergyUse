using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class SelectReportParametersController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.SelectParameter? UnitOfWork { get; set; } = null;


    #endregion

    #region InitControler

    public SelectReportParametersController(string dbFileName) : base(dbFileName)
    { 

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.SelectParameter(_dbFileName);
    }

    #endregion
}