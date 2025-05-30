using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class CostcategoriesController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.CostCategory? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public CostcategoriesController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.CostCategory(_dbFileName);
    }

    #endregion
}