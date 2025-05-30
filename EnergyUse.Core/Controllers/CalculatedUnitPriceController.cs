using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class CalculatedUnitPriceController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.CalculatedUnitPrice? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public CalculatedUnitPriceController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.CalculatedUnitPrice(_dbFileName);
    }

    #endregion
}