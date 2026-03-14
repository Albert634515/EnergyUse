using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class PreDefinedPeriodController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.PreDefinedPeriod? UnitOfWork { get; set; } = null;
    public EnergyUse.Core.UnitOfWork.PredefinedPeriodDate? UnitOfWorkPd { get; set; } = null;

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
        UnitOfWorkPd = new EnergyUse.Core.UnitOfWork.PredefinedPeriodDate(_dbFileName);
    }

    #endregion
}