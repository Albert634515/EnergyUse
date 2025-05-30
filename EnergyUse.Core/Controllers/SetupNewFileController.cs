using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class SetupNewFileController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.SetupNewFile? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public SetupNewFileController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.SetupNewFile(_dbFileName);
    }

    #endregion
}