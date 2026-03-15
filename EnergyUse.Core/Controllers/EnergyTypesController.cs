using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class EnergyTypesController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.EnergyType UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public EnergyTypesController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.EnergyType(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}