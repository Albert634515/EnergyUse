using System.Drawing;
using EnergyUse.Common.Enums;
using EnergyUse.Core.Context;

namespace EnergyUse.Core.Manager
{
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
            {
                var setting = GetKey(fileKey);
                if (setting != null && setting.Id > 0)
                    lastUsedImportFile = setting.KeyValue;
            }

            return lastUsedImportFile;
        }

        public string GetLastImportDirectory()
        {
            string lastImportDirectory;
            Models.Setting setting = GetKey("ImportDirectory");
            if (setting != null && setting.Id > 0)
                lastImportDirectory = setting.KeyValue;
            else
                lastImportDirectory = string.Empty;

            return lastImportDirectory;
        }

        public int GetNumberOfEnergyTypesOnReport(long addressId)
        {
            int numberOfEnergyTypesOnReport = 0;

            var setting = GetKey($"NumberOfEnergyTypesOnReport_A{addressId}");
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
                var setting = GetKey(splitterName);
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

        public Color GetColorSetting(string settingKey, Color? color = null)
        {
            if (color == null)
                color = Color.Empty;

            var setting = GetKey(settingKey);
            if (setting != null && setting.KeyValue != null)
            {
                color = ColorTranslator.FromWin32(int.Parse(setting.KeyValue));
            }

            return color.Value;
        }

        public string GetCurrentLanguage()
        {
            string language = "en-US";
            var setting = GetKey("Language");
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

        public Models.Setting GetKey(string key)
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

        public void SaveColorSetting(string settingTag, Color newColor)
        {
            var repo = new Repositories.RepoSettings(_context);
            var setting = repo.GetByKey(settingTag);

            if (setting == null || setting.Id == 0)
            {
                setting = new Models.Setting();
                setting.Key = settingTag;
                setting.KeyValue =ColorTranslator.ToWin32(newColor).ToString();
                repo.Add(setting);
            }
            else
            {
                setting.KeyValue = ColorTranslator.ToWin32(newColor).ToString();
            }

            _context.SaveChanges();
        }

        public void DeleteSetting(string settingTag)
        {
            var repo = new Repositories.RepoSettings(_context);
            var setting = repo.GetByKey(settingTag);

            repo.Remove(setting);
            _context.SaveChanges();
        }
    }
}