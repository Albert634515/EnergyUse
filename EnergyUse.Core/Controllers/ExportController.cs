using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers
{
    public class ExportController : IController
    {
        #region ControlerProperties

        private string _dbFileName { get; set; } = string.Empty;
        public EnergyUse.Core.UnitOfWork.Export? UnitOfWork { get; set; } = null;
        private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

        public bool InitSettings { get; set; } = false;

        #endregion

        #region InitControler

        public ExportController(string dbFileName)
        {
            _dbFileName = dbFileName;
        }

        public void Initialize()
        {
            setUnitOfWork();
            setSettingsManager();
        }

        private void setUnitOfWork()
        {
            UnitOfWork = new EnergyUse.Core.UnitOfWork.Export(_dbFileName);
        }

        private void setSettingsManager()
        {
            _libSettings = new EnergyUse.Core.Manager.LibSettings(_dbFileName);
        }

        public string getDbFileName()
        {
            return _dbFileName;
        }

        #endregion
    }
}
