using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class PreDefinedPeriodController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.PreDefinedPeriod? UnitOfWork { get; set; } = null;


    #endregion

    #region InitControler

    public PreDefinedPeriodController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.PreDefinedPeriod(_dbFileName);
    }

    #endregion
}