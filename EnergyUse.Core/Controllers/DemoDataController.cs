using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class DemoDataController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.DemoData? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public DemoDataController(string dbFileName): base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.DemoData(_dbFileName);
    }

    #endregion
}