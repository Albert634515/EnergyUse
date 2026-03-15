using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class CalculatedUnitPriceController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.CalculatedUnitPrice? UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public CalculatedUnitPriceController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.CalculatedUnitPrice(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}