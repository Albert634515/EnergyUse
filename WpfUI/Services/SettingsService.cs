using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Manager;
using System.Globalization;
using WpfUI.Managers;

public class SettingsService : ISettingsService
{
    private LibSettings Lib => new(Config.GetDbFileName());

    public string? Get(string key) => Lib.GetSetting(key)?.KeyValue;
    public void Save(string key, string value) => Lib.SaveSetting(key, value);
    public void SaveDate(string key, DateTime value) => Lib.SaveSetting(key, value.ToString("yyyyMMdd"));
    public DateTime GetDate(string key, DateTime defaultValue)
    {
        var setting = Lib.GetSetting(key);
        if (setting == null) return defaultValue;

        return DateTime.TryParseExact(setting.KeyValue, "yyyyMMdd", null,
            System.Globalization.DateTimeStyles.None, out var parsed)
            ? parsed
            : defaultValue;
    }
    public decimal GetSettingDecimal(string key, decimal defaultValue = 0)
    {
        var setting = Lib.GetSetting(key);
        if (setting == null) return defaultValue;

        return decimal.TryParse(setting.KeyValue,
            NumberStyles.Any,
            CultureInfo.InvariantCulture,
            out var result)
            ? result
            : defaultValue;
    }
}