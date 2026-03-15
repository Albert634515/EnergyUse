using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class MeterReadingController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.MeterReading UnitOfWork { get; set; }

    #endregion

    #region InitControler

    public MeterReadingController(string dbFileName) : base(dbFileName)
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.MeterReading(_dbFileName);
    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion
}