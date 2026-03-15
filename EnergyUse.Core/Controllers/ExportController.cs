using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class ExportController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Export UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public ExportController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Export(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}