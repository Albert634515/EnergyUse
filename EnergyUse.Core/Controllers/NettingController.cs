using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class NettingController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Netting? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public NettingController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Netting(_dbFileName);
    }

    #endregion
}