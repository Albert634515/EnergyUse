using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class TariffGroupController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.TariffGroup? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public TariffGroupController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.TariffGroup(_dbFileName);
    }

    #endregion
}