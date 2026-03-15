using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class TariffGroupController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.TariffGroup UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public TariffGroupController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.TariffGroup(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}