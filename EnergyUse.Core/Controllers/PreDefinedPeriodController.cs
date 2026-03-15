using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class PreDefinedPeriodController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.PreDefinedPeriod UnitOfWork { get; set; }
    public EnergyUse.Core.UnitOfWork.PredefinedPeriodDate UnitOfWorkPd { get; set; }

    #endregion

    #region InitControler

    public PreDefinedPeriodController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.PreDefinedPeriod(_dbFileName);
        UnitOfWorkPd = new EnergyUse.Core.UnitOfWork.PredefinedPeriodDate(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}