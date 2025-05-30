using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class PaymentsController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Payment? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public PaymentsController(string dbFileName) : base(dbFileName)
    {
        _dbFileName = dbFileName;
    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Payment(_dbFileName);
    }

    #endregion
}