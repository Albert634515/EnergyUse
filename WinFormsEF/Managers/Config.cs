using System.Configuration;

namespace WinFormsEF.Managers;

public class Config
{
    public static string GetDbFileName()
    {
        return GetSetting("FileName");
    }

    public static void SetDbFileName(string dbFileName)
    {
        SetSetting("FileName", dbFileName);
    }

    private static string GetSetting(string key)
    {
        return ConfigurationManager.AppSettings[key];
    }

    private static void SetSetting(string key, string value)
    {
        Configuration configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        configuration.AppSettings.Settings[key].Value = value;
        configuration.Save(ConfigurationSaveMode.Full, true);
        ConfigurationManager.RefreshSection("appSettings");
    }
}