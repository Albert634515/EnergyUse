using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class CostcategoriesController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.CostCategory UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public CostcategoriesController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.CostCategory(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}