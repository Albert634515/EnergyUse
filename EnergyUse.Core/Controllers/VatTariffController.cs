using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class VatTariffController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.VatTarif? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public VatTariffController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.VatTarif(_dbFileName);
    }

    #endregion
}