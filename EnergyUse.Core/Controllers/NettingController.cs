using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class NettingController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Netting UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public NettingController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Netting(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}