using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class CorrectionFactorController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.CorrectionFactor? UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public CorrectionFactorController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.CorrectionFactor(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}