using EnergyUse.Core.Manager;
using System.Globalization;

namespace WpfUI.Managers;

public static class SettingsWpf
{
    private static LibSettings Lib => new(Config.GetDbFileName());

    public static string? Get(string key)
    {
        var setting = Lib.GetSetting(key);
        return setting?.KeyValue;
    }

    public static void Save(string key, string value)
    {
        Lib.SaveSetting(key, value);
    }

    public static void SaveDate(string key, DateTime value)
    {
        Lib.SaveSetting(key, value.ToString("yyyyMMdd"));
    }

    public static void SaveSettingDecimal(string key, decimal value)
    {
        Lib.SaveSetting(key, value.ToString(CultureInfo.InvariantCulture));
    }

    public static DateTime GetDate(string key, DateTime defaultValue)
    {
        var setting = Lib.GetSetting(key);
        if (setting == null || string.IsNullOrWhiteSpace(setting.KeyValue))
            return defaultValue;

        if (DateTime.TryParseExact(setting.KeyValue, "yyyyMMdd", null,
            System.Globalization.DateTimeStyles.None, out var parsed))
            return parsed;

        return defaultValue;
    }

    public static decimal GetSettingDecimal(string key, decimal defaultValue = 0)
    {
        var setting = Lib.GetSetting(key);

        if (setting == null || string.IsNullOrWhiteSpace(setting.KeyValue))
            return defaultValue;

        if (decimal.TryParse(setting.KeyValue,
                             NumberStyles.Any,
                             CultureInfo.InvariantCulture,
                             out decimal result))
            return result;

        return defaultValue;
    }


}