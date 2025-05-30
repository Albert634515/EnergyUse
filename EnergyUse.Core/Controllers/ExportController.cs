using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class ExportController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Export? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public ExportController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Export(_dbFileName);
    }

    #endregion
}