namespace EnergyUse.Core.Interfaces
{
    public interface ISettingsService
    {
        string? Get(string key);
        void Save(string key, string value);
        void SaveDate(string key, DateTime value);
        DateTime GetDate(string key, DateTime defaultValue);
        decimal GetSettingDecimal(string key, decimal defaultValue = 0);
    }
}