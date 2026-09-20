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
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BackUp");
    }

    public void CreateBackUpFile(string targetPath, string sourceFile)
    {
        if (!Directory.Exists(targetPath))
            Directory.CreateDirectory(targetPath);

        var sourceFileName = Path.GetFileNameWithoutExtension(sourceFile);
        var sourceFileExtension = Path.GetExtension(sourceFile);
        var fileName = $"{sourceFileName}_{DateTime.Now:yyyyMMddHHmmss}{sourceFileExtension}";
        var destFile = Path.Combine(targetPath, fileName);
        File.Copy(sourceFile, destFile, true);
    }

    #endregion
}
