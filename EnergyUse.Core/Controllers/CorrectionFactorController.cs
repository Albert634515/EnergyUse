using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class CorrectionFactorController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.CorrectionFactor? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public CorrectionFactorController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.CorrectionFactor(_dbFileName);
    }

    #endregion
}