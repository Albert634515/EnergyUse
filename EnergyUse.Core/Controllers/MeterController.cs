using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class MeterController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Meter UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public MeterController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Meter(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}