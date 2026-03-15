using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class DemoDataController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.DemoData UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public DemoDataController(string dbFileName): base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.DemoData(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}