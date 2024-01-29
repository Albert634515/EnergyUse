using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers
{
    public class AddressController : IController
    {
        #region ControlerProperties

        private string _dbFileName { get; set; } = string.Empty;
        public EnergyUse.Core.UnitOfWork.Address? UnitOfWork { get; set; } = null;
        private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

        public bool InitSettings { get; set; } = false;

        #endregion

        #region InitControler

        public AddressController(string dbFileName)
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
            UnitOfWork = new EnergyUse.Core.UnitOfWork.Address(_dbFileName);
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

        public Models.Address AddDefaultEntity(string defaultDescription)
        {
            return UnitOfWork.AddDefaultEntity(defaultDescription);
        }

        #region Settings

        public void SaveSetting(string key, string settingValue)
        {
            if (_libSettings != null)
                _libSettings.SaveSetting(key.Trim(), settingValue);
        }

        public void SaveSetting(Models.Setting setting)
        {
            if (_libSettings != null && setting != null)
                _libSettings.SaveSetting(setting.Key.Trim(), setting.KeyValue);
        }

        public Models.Setting? GetSetting(string key)
        {
            Models.Setting? setting = null;
            if (_libSettings != null)
                setting = _libSettings.GetSetting(key.Trim());

            return setting;
        }

        #endregion
    }
}
