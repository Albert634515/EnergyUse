using EnergyUse.Core.Interfaces;
using iText.Kernel.Colors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnergyUse.Core.Controllers
{
    public class SettingsController : IController
    {
        #region ControlerProperties

        private string _dbFileName { get; set; } = string.Empty;
        private EnergyUse.Core.UnitOfWork.Setting? _unitOfWork { get; set; } = null;
        private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

        public bool InitSettings { get; set; } = false;

        public SettingsController(string dbFileName)
        {
            _dbFileName = dbFileName;
        }

        #endregion

        public void Initialize()
        {
            setUnitOfWork();
            setSettingsManager();
        }

        private void setUnitOfWork()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Setting(_dbFileName);
        }

        private void setSettingsManager()
        {
            _libSettings = new EnergyUse.Core.Manager.LibSettings(_dbFileName);
        }

        public string getDbFileName()
        {
            return _dbFileName;
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
                setting =_libSettings.GetSetting(key.Trim());

            return setting;
        }

        public void SetColorSetting(string key, System.Drawing.Color color)
        {
            if (_libSettings != null)
                _libSettings.SaveColorSetting(key, color);
        }

        public void DeleteSetting(string key)
        {
            if (_libSettings != null)
                _libSettings.DeleteSetting(key);
        }

        #endregion

        #region Methods

        public void resetColorsAndLayout()
        {
            DeleteSetting("BackgroundColorForms");
            DeleteSetting("SliderColor");
        }

        public void resetChartSettings()
        {
            DeleteSetting("BackgroundColorChart");
            DeleteSetting("ForeColorChart");
            DeleteSetting("LineColorChart");
            DeleteSetting("LabelsYColorChart");
            DeleteSetting("GraphType");
            DeleteSetting("UseAllDataForAvg");
        }

        public void resetDataPredictionSettings()
        {
            DeleteSetting("AvgCorrectionPercentage");
            DeleteSetting("AvgCorrectionPercentageReturn");

            DeleteSetting("UseAllDataForAvg");
            DeleteSetting("CalculateAvgDateFrom");
            DeleteSetting("AvgDateFromDate");
        }

        #endregion
    }
}
