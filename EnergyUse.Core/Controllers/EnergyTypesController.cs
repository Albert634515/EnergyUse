using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class EnergyTypesController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.EnergyType? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public EnergyTypesController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.EnergyType(_dbFileName);
    }

    #endregion
}