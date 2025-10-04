namespace EnergyUse.Core.Controllers;

public class BaseController
{
    #region Properties

    protected string _dbFileName { get; set; } = string.Empty;
    protected EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;
    public bool InitSettings { get; set; } = false;

    #endregion

    public BaseController(string dbFileName)
    {
        _dbFileName = dbFileName.Trim();
    }

    public BaseController()
    {
       
    }

    protected void setSettingsManager()
    {
        _libSettings = new EnergyUse.Core.Manager.LibSettings(_dbFileName);
    }

    public string getDbFileName() => _dbFileName;

    #region Settings

    public Models.Setting? GetSetting(string key)
    {
        Models.Setting? setting = null;
        if (_libSettings != null)
            setting = _libSettings.GetSetting(key.Trim());

        return setting;
    }

    public string GetSettingValue(string key)
    {
        var settingValue = string.Empty;
        if (_libSettings != null)
            settingValue = _libSettings.GetSettingValue(key);

        return settingValue;
    }

    public void SaveSetting(string key, string settingValue)
    {
        if (_libSettings != null)
            _libSettings.SaveSetting(key.Trim(), settingValue);
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
}
