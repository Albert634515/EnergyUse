using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class PaymentsController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.Payment UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public PaymentsController(string dbFileName) : base(dbFileName)
    {
        _dbFileName = dbFileName;
        UnitOfWork = new EnergyUse.Core.UnitOfWork.Payment(dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}