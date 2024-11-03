using System.Globalization;

namespace WinFormsEF.Managers;

internal class Settings
{
    public static void SetBaseFormSettings(Form frmBaseForm, string settingsKey = "BackgroundColorForms")
    {
        var formBackgroundColor = GetColorSetting(settingsKey);
        if (formBackgroundColor != Color.Empty)
            frmBaseForm.BackColor = formBackgroundColor;
    }

    public static void SetBaseUserControlSettings(UserControl userControl, string settingsKey = "BackgroundColorForms")
    {
        var formBackgroundColor = GetColorSetting(settingsKey);
        if (formBackgroundColor != Color.Empty)
            userControl.BackColor = formBackgroundColor;
    }

    public static Color GetColorSetting(string key)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Config.GetDbFileName());
        return libSettings.GetChartColor(key);
    }

    public static void SetLanguage()
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Config.GetDbFileName());
        var language = libSettings.GetCurrentLanguage();
        Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(language);
    }

    /// <summary>
    /// Returns setting object from a TextBox
    /// </summary>
    /// <param name="sender"></param>
    /// <returns>Settings model</returns>
    public static Tuple<string, string> GetSetting(TextBox sender)
    {
        return new(sender.Tag.ToString(), sender.Text);
    }

    /// <summary>
    /// Returns setting object from a ComboBoxs
    /// </summary>
    /// <param name="sender"></param>
    /// <returns>Settings model</returns>
    public static Tuple<string, string> GetSetting(ComboBox sender)
    {
        return new(sender.Tag.ToString(), sender.Text);
    }

    /// <summary>
    /// Returns setting object from a RadioButton
    /// </summary>
    /// <param name="sender"></param>
    /// <returns>Settings model</returns>
    public static Tuple<string, string> GetSetting(RadioButton sender)
    {
        string settingValue = "No";

        if (sender.Checked)
            settingValue = "Yes";

        return new(sender.Tag.ToString(), settingValue);
    }

    public static Tuple<string, string> GetSetting(DateTimePicker sender)
    {
        return new(sender.Tag.ToString(), sender.Value.ToString("yyyyMMdd"));
    }

    #region LoadSettings

    public static void GetColorSetting(TextBox textBox)
    {
        textBox.BackColor = Color.Empty;

        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        var setting = libSettings.GetSetting(textBox.Tag.ToString());
        if (setting != null && setting.Id > 0)
        {
            if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                textBox.BackColor = System.Drawing.ColorTranslator.FromWin32(int.Parse(setting.KeyValue));
        }
    }

    public static void GetSettingDateBox(DateTimePicker dateTimePicker, DateTime defaultValue)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        var setting = libSettings.GetSetting(dateTimePicker.Tag.ToString());
        SetSettingDateTimePicker(dateTimePicker, setting, defaultValue);
    }

    public static void GetSettingTextBox(TextBox textBox)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        EnergyUse.Models.Setting setting = libSettings.GetSetting(textBox.Tag.ToString());
        SetTextBox(textBox, setting);
    }

    public static void SetTextBox(TextBox textBox, EnergyUse.Models.Setting setting)
    {
        textBox.Text = string.Empty;
        if (setting != null && setting.Id > 0)
        {
            if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                textBox.Text = setting.KeyValue;
        }
    }

    public static void SetTextBoxColor(TextBox textBox, EnergyUse.Models.Setting setting)
    {
        textBox.BackColor = Color.Empty;
        if (setting != null && setting.Id > 0)
        {
            if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                textBox.BackColor = System.Drawing.ColorTranslator.FromWin32(int.Parse(setting.KeyValue));
        }
    }

    public static void SetRadioButtonSetting(RadioButton sender, EnergyUse.Models.Setting setting)
    {
        if (setting != null && setting.Id > 0)
        {
            if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                sender.Checked = (setting.KeyValue == "Yes");
        }
    }

    public static void SetSettingCombo(ComboBox comboBox, EnergyUse.Models.Setting setting, string defaultValue)
    {
        if (setting != null && setting.Id > 0)
        {
            if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                comboBox.SelectedIndex = comboBox.FindString(setting.KeyValue);
        }

        if (comboBox.SelectedIndex == -1 && !string.IsNullOrWhiteSpace(defaultValue))
            comboBox.SelectedIndex = comboBox.FindString(defaultValue);
    }

    public static void SetSettingDateTimePicker(DateTimePicker dateTimePicker, EnergyUse.Models.Setting setting, DateTime defaultValue)
    {
        // If no value found, use default value
        var settingVaue = defaultValue.ToString("yyyyMMdd");

        if (setting != null && setting.Id > 0)
            settingVaue = setting.KeyValue;

        if (!string.IsNullOrWhiteSpace(settingVaue))
            dateTimePicker.Value = DateTime.ParseExact(settingVaue, "yyyyMMdd", CultureInfo.InvariantCulture);
    }

    public static void SetSettingCheckBox(CheckBox checkbox)
    {
        EnergyUse.Models.Setting setting = GetSetting(checkbox.Tag.ToString());
        if (setting != null && setting.Id > 0)
        {
            if (!string.IsNullOrWhiteSpace(setting.KeyValue))
                checkbox.Checked = (setting.KeyValue == "Yes");
        }
    }

    public static EnergyUse.Models.Setting GetSetting(string key)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        return libSettings.GetSetting(key);
    }

    #endregion

    #region SaveSettings

    public static void SaveSettingTextBox(TextBox sender)
    {
        SaveSetting(sender.Tag.ToString().Trim(), sender.Text.Trim());
    }

    public static void SaveSettingCheckBox(CheckBox sender)
    {
        string settingValue;

        if (sender.Checked)
            settingValue = "Yes";
        else
            settingValue = "No";

        SaveSetting(sender.Tag.ToString().Trim(), settingValue);
    }

    public static void SaveColorSetting(TextBox sender, Color color)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        libSettings.SaveColorSetting(sender.Tag.ToString(), color);
    }

    public static void SaveSetting(string key, string settingValue)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        libSettings.SaveSetting(key.Trim(), settingValue);
    }

    #endregion

    #region DeleteSettings

    public static void DeleteSettingTextBox(TextBox textBox)
    {
        textBox.Text = string.Empty;
        DeleteSetting(textBox.Tag.ToString());
    }

    public static void DeleteSetting(string key)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        EnergyUse.Models.Setting setting = libSettings.GetSetting(key);
        if (setting != null && setting.Id > 0)
            libSettings.DeleteSetting(key);
    }

    #endregion
}