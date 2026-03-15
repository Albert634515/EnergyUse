using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class VatTariffController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.VatTarif UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public VatTariffController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.VatTarif(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}