using System.Globalization;

namespace WinFormsEF.Managers
{
    internal class Settings
    {
        public static void SetBaseFormSettings(Form frmBaseForm, string settingsKey = "BackgroundColorForms")
        {
            Color formBackgroundColor;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Config.GetDbFileName());
            formBackgroundColor = libSettings.GetChartColor(settingsKey);
            if (formBackgroundColor != Color.Empty)
                frmBaseForm.BackColor = formBackgroundColor;
        }

        public static void SetBaseUserControlSettings(UserControl userControl, string settingsKey = "BackgroundColorForms")
        {
            Color formBackgroundColor;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Config.GetDbFileName());
            formBackgroundColor = libSettings.GetChartColor(settingsKey);
            if (formBackgroundColor != Color.Empty)
                userControl.BackColor = formBackgroundColor;
        }

        public static void SetLanguage()
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Config.GetDbFileName());
            var language = libSettings.GetCurrentLanguage();
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
        }

        public static void SaveSettingComboBox(ComboBox txtSettingsComboBox)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            libSettings.SaveSetting(txtSettingsComboBox.Tag.ToString().Trim(), txtSettingsComboBox.Text.Trim());
        }

        public static void SaveSettingTextBox(TextBox txtSettingsTextBox)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            libSettings.SaveSetting(txtSettingsTextBox.Tag.ToString().Trim(), txtSettingsTextBox.Text.Trim());
        }

        public static void SaveSettingCheckBox(CheckBox checkBox)
        {
            string settingValue;

            if (checkBox.Checked)
                settingValue = "Yes";
            else
                settingValue = "No";

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            libSettings.SaveSetting(checkBox.Tag.ToString().Trim(), settingValue);
        }

        public static void LoadSettingTextBox(TextBox textBox)
        {
            textBox.Text = string.Empty;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            EnergyUse.Models.Setting setting = libSettings.GetKey(textBox.Tag.ToString());
            if (setting != null && setting.Id > 0)
            {
                if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                    textBox.Text = setting.KeyValue;                   
            }
        }

        public static void DeleteSettingTextBox(TextBox textBox)
        {
            textBox.Text = string.Empty;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            EnergyUse.Models.Setting setting = libSettings.GetKey(textBox.Tag.ToString());
            if (setting != null && setting.Id > 0)
                libSettings.DeleteSetting(textBox.Tag.ToString());
        }

        public static void LoadSettingCheckBox(CheckBox checkbox)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            EnergyUse.Models.Setting setting = libSettings.GetKey(checkbox.Tag.ToString());
            if (setting != null && setting.Id > 0)
            {
                if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                    checkbox.Checked = (setting.KeyValue == "Yes");
            }
        }

        public static void LoadColorSetting(TextBox textBox)
        {
            textBox.BackColor = Color.Empty;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetKey(textBox.Tag.ToString());
            if (setting != null && setting.Id > 0)
            {
                if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                    textBox.BackColor = System.Drawing.ColorTranslator.FromWin32(int.Parse(setting.KeyValue));
            }
        }

        public static void SaveColorSetting(TextBox textBox, Color color)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            libSettings.SaveColorSetting(textBox.Tag.ToString(), color);
        }

        public static void LoadSettingCombo(ComboBox comboBox, string defaultValue)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetKey(comboBox.Tag.ToString());
            if (setting != null && setting.Id > 0)
            {
                if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                    comboBox.SelectedIndex = comboBox.FindString(setting.KeyValue);
            }

            if (comboBox.SelectedIndex == -1 && !string.IsNullOrWhiteSpace(defaultValue))
                comboBox.SelectedIndex = comboBox.FindString(defaultValue);
        }

        public static void LoadSettingDateBox(DateTimePicker dateTimePicker)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetKey(dateTimePicker.Tag.ToString());
            if (setting != null && setting.Id > 0)
            {
                if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                    dateTimePicker.Value = DateTime.ParseExact(setting.KeyValue, "yyyyMMdd", CultureInfo.InvariantCulture);
            }
        }
    }
}
