using System.Configuration;

namespace WpfUI.Managers;

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

    public static string GetSetting(string key)
    {
        var value = ConfigurationManager.AppSettings[key];
        return value ?? string.Empty;
    }

    public static void SetSetting(string key, string value)
    {
        var configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

        // If key doesn't exist, add it; otherwise, update the value
        if (configuration.AppSettings.Settings[key] == null)
            configuration.AppSettings.Settings.Add(key, value);
        else
            configuration.AppSettings.Settings[key].Value = value;

        configuration.Save(ConfigurationSaveMode.Full, true);
        ConfigurationManager.RefreshSection("appSettings");

    }

    public static bool GetBool(string key)
    {
        var value = GetSetting(key);
        return value.Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    public static void SetBool(string key, bool value)
    {
        SetSetting(key, value ? "true" : "false");
    }
}