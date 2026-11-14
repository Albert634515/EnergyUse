using EnergyUse.Core.Interfaces;

namespace EnergyUse.Core.Controllers;

public class BackUpAndRestoreController : BaseController, IController
{
    #region ControlerProperties

    #endregion

    #region InitControler

    public BackUpAndRestoreController(string dbFileName) : base(dbFileName)
    {

    }

    public void Initialize()
    {
        base.setSettingsManager();
    }

    #endregion

    #region Methods

    public string GetSettingBackUpDir(string settingKey)
    {
        if (string.IsNullOrWhiteSpace(settingKey))
            throw new ArgumentException("SettingKey cannot be null or empty.", nameof(settingKey));

        if (_libSettings == null)
            return getDefaultBackUpDir();

        var settingValue = GetSettingValue(settingKey);
        if (string.IsNullOrWhiteSpace(settingValue) || !Directory.Exists(settingValue))
            settingValue = getDefaultBackUpDir();

        return settingValue;
    }

    public string GetSourceDbFile()
    {
        return _dbFileName;
    }

    private string getDefaultBackUpDir()
    {
        return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "EnergyUse", "BackUp");
    }

    public string CreateBackUpFile(string targetPath, string sourceFile)
    {
        return Manager.DatabaseBackup.Create(sourceFile, targetPath);
    }

    public string? RestoreBackUpFile(string sourceFile, bool backupBeforeRestore, string backupDirectory)
    {
        if (backupBeforeRestore)
            ArgumentException.ThrowIfNullOrWhiteSpace(backupDirectory);
        return Manager.DatabaseBackup.Restore(sourceFile, _dbFileName, backupBeforeRestore ? backupDirectory : null);
    }

    #endregion
}
