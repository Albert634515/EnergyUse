namespace EnergyUse.Core.Interfaces
{
    public interface ISettingsService
    {
        string? Get(string key);
        void Save(string key, string value);

        string? GetLastUsedImportFile(string key);
        void SaveLastUsedImportFile(string key, string file);

        DateTime GetDate(string key, DateTime defaultValue);
        void SaveDate(string key, DateTime value);

        decimal GetDecimal(string key, decimal defaultValue = 0);
        void SaveDecimal(string key, decimal value);
    }
}