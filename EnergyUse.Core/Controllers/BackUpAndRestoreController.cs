using EnergyUse.Core.Interfaces;
using System.Diagnostics;

namespace EnergyUse.Core.Controllers;

public class BackUpAndRestoreController : IController
{
    #region ControlerProperties

    private string _dbFileName { get; set; } = string.Empty;

    private EnergyUse.Core.Manager.LibSettings? _libSettings { get; set; } = null;

    public bool InitSettings { get; set; } = false;

    #endregion

    #region InitControler

    public BackUpAndRestoreController(string dbFileName)
    {
        _dbFileName = dbFileName;
    }

    public void Initialize()
    {
        setSettingsManager();
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

    #region Methods

    public string getSettingBackUpDir(string settingKey)
    {
        string settingValue = string.Empty;

        if (_libSettings != null)
        {
            Models.Setting setting = _libSettings.GetSetting(settingKey);
            if ((setting == null || (setting != null && string.IsNullOrWhiteSpace(setting.KeyValue))) || !Directory.Exists(setting.KeyValue))
                settingValue = getDefaultBackUpDir();
            else
                settingValue = setting.KeyValue;
        }

        return settingValue;
    }

    public string GetSourceDbFile()
    {
        string dbFile;

        dbFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "EnergyUse.db");

        return dbFile;
    }

    private string getDefaultBackUpDir()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackUp");
    }

    public void CreateBackUpFile(string targetPath, string sourceFile)
    {
        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);

        var fileName = $"EnergyUse_{DateTime.Now.ToString("yyyyMMddHHmmss")}.db";
        var destFile = Path.Combine(targetPath, fileName);
        File.Copy(sourceFile, destFile, true);
    }

    #endregion
}