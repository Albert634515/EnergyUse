using System.Drawing;
using EnergyUse.Common.Enums;
using EnergyUse.Core.Context;

namespace EnergyUse.Core.Manager;

public class LibSettings
{
    private readonly EnergyUseContext _context;

    public LibSettings(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);
    }

    public void SetLastUsedImportFile(string lastImportFile, string fileKey)
    {
        if (!string.IsNullOrWhiteSpace(lastImportFile))
        {
            SaveSetting("ImportDirectory", Path.GetDirectoryName(lastImportFile));

            if (!string.IsNullOrWhiteSpace(fileKey))
                SaveSetting(fileKey, lastImportFile);
        }
    }

    public string GetLastUsedImportFile(string fileKey)
    {
        var lastUsedImportFile = string.Empty;
        if (!string.IsNullOrWhiteSpace(fileKey))
            lastUsedImportFile = GetSettingValue(fileKey);

        return lastUsedImportFile;
    }

    public string GetLastImportDirectory()
    {
        return GetSettingValue("ImportDirectory");
    }

    public int GetNumberOfEnergyTypesOnReport(long addressId)
    {
        int numberOfEnergyTypesOnReport = 0;

        var setting = GetSetting($"NumberOfEnergyTypesOnReport_A{addressId}");
        if (setting != null && !string.IsNullOrWhiteSpace(setting.KeyValue))
        {
            _ = int.TryParse(setting.KeyValue, out numberOfEnergyTypesOnReport);
        }

        return numberOfEnergyTypesOnReport;
    }

    public int GetMainSpitterDistance(string splitterName)
    {
        int splitterDistance = 360;

        if (!string.IsNullOrWhiteSpace(splitterName))
        {
            var setting = GetSetting(splitterName);
            if (setting != null && !string.IsNullOrWhiteSpace(setting.KeyValue))
            {
                _ = int.TryParse(setting.KeyValue, out splitterDistance);
            }
        }

        return splitterDistance;
    }

    public Color GetChartColor(string colorKey)
    {
        Color color = GetColorSetting(colorKey);

        return color;
    }

    public Color GetChartColor(Models.EnergyType energyType, SubEnergyType subType)
    {
        string colorKey;

        if (energyType.HasNormalAndLow)
            colorKey = $"Color{subType}{energyType.Id}";
        else
            colorKey = $"Color{energyType.Id}";

        Color color = GetColorSetting(colorKey);

        return color;
    }

    public Color GetColorSetting(string settingKey, Color? fallback = null)
    {
        var setting = GetSetting(settingKey);
        if (setting?.KeyValue == null)
            return fallback ?? Color.Empty;

        string value = setting.KeyValue.Trim();

        // 1. HTML color (#RRGGBB of name)
        if (value.StartsWith("#") || value.All(char.IsLetter))
            return ColorTranslator.FromHtml(value);

        // 2. Decimaal ARGB or RGB
        if (int.TryParse(value, out int dec))
        {
            // if 24-bit RGB value is → force Alpha = 255
            if (dec <= 0xFFFFFF)
                dec |= unchecked((int)0xFF000000);

            return Color.FromArgb(dec);
        }

        return fallback ?? Color.Empty;
    }

    public string GetCurrentLanguage()
    {
        string language = "en-US";
        var setting = GetSetting("Language");
        if (setting != null && setting.KeyValue != null)
        {
            language = setting.KeyValue switch
            {
                "English" => "en-US",
                "Nederlands" => "nl-NL",
                "Polski" => "pl-PL",
                _ => "en-US",
            };
        }

        return language;
    }


    public string GetSettingValue(string key)
    {
        Models.Setting? setting = GetSetting(key);
        if (setting != null)
            return setting.KeyValue;
        else
            return string.Empty;
    }

    public Models.Setting? GetSetting(string key)
    {
        var repo = new Repositories.RepoSettings(_context);
        return repo.GetByKey(key);
    }

    public void SaveSetting(string settingTag, string newSettingValue)
    {
        var repo = new Repositories.RepoSettings(_context);
        var setting = repo.GetByKey(settingTag);

        if (setting == null || setting.Id == 0)
        {
            setting = new Models.Setting();
            setting.Key = settingTag;
            setting.KeyValue = newSettingValue;
            repo.Add(setting);
        }
        else
        {
            setting.KeyValue = newSettingValue;
        }

        _context.SaveChanges();
    }

    public void SaveDateSetting(string settingTag, DateTime dateSettingValue)
    {
        string newSettingValue = dateSettingValue.ToString("yyyyMMdd");
        SaveSetting(settingTag, newSettingValue);
    }

    public void SaveColorSetting(string settingTag, Color newColor)
    {
        var repo = new Repositories.RepoSettings(_context);
        var setting = repo.GetByKey(settingTag);

        string html = ColorTranslator.ToHtml(newColor);

        if (setting == null || setting.Id == 0)
        {
            setting = new Models.Setting
            {
                Key = settingTag,
                KeyValue = html
            };
            repo.Add(setting);
        }
        else
        {
            setting.KeyValue = html;
        }

        _context.SaveChanges();
    }

    public void DeleteSetting(string settingTag)
    {
        var repo = new Repositories.RepoSettings(_context);
        var setting = repo.GetByKey(settingTag);

        if (setting == null)
            return;

        repo.Remove(setting);
        _context.SaveChanges();
    }
}