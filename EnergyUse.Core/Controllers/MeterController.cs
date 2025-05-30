using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class MeterController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Meter? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public MeterController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Meter(_dbFileName);
    }

    #endregion
}