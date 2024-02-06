using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers
{
    public class NettingController : IController
    {
        #region ControlerProperties

        private string _dbFileName { get; set; } = string.Empty;
        public EnergyUse.Core.UnitOfWork.Netting? UnitOfWork { get; set; } = null;
        private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

        public bool InitSettings { get; set; } = false;

        #endregion

        #region InitControler

        public NettingController(string dbFileName)
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
            UnitOfWork = new EnergyUse.Core.UnitOfWork.Netting(_dbFileName);
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

        #region Settings

        public Models.Setting? GetSetting(string key)
        {
            Models.Setting? setting = null;
            if (_libSettings != null)
                setting = _libSettings.GetSetting(key.Trim());

            return setting;
        }

        public void SaveSetting(string key, string settingValue)
        {
            if (_libSettings != null)
                _libSettings.SaveSetting(key.Trim(), settingValue);
        }

        #endregion
    }
}
