using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class MeterReadingController : BaseController, IController
{
    #region ControlerProperties

    public EnergyUse.Core.UnitOfWork.MeterReading? UnitOfWork { get; set; } = null;

    #endregion

    #region InitControler

    public MeterReadingController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        setUnitOfWork();
        base.setSettingsManager();
    }

    private void setUnitOfWork()
    {
        UnitOfWork = new EnergyUse.Core.UnitOfWork.MeterReading(_dbFileName);
    }

    #endregion
}