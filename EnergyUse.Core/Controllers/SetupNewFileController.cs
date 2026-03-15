using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class SetupNewFileController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.SetupNewFile UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public SetupNewFileController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.SetupNewFile(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}