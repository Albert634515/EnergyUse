using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmSettings : Form
{
    #region FormProperties

    private SettingsController _controller;

    #endregion

    #region InitForm

    public frmSettings()
    {
        _controller = new SettingsController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
        loadSettings();
    }

    #endregion

    #region Events

    private void txtFormBackGroundColor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            setColorSetting((TextBox)sender);
            setBaseFormSettings();
        }
    }
    private void txtSliderColor_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
            setColorSetting((TextBox)sender);
    }

    private void txtBackgroundColorChart_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            setColorSetting((TextBox)sender);
        }
    }

    private void txtForeColorChart_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            setColorSetting((TextBox)sender);
        }
    }

    private void txtLineColorChart_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            setColorSetting((TextBox)sender);
        }
    }

    private void txtLabelsYColorChart_Click(object sender, EventArgs e)
    {
        if (colorDialog1.ShowDialog() == DialogResult.OK)
        {
            setColorSetting((TextBox)sender);
        }
    }

    private void txtCurrency_TextChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            var settingValue = Managers.Settings.GetSetting((TextBox)sender);
            _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
        }
    }

    private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            var settingValue = Managers.Settings.GetSetting((ComboBox)sender);
            _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
        }
    }

    private void useAllDataForAvgRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            var settingValue = Managers.Settings.GetSetting((RadioButton)sender);
            _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
        }
    }

    private void useDataFromForAvgRadioButton_CheckedChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            var settingValue = Managers.Settings.GetSetting((RadioButton)sender);
            _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
        }
    }

    private void avgDateFromDateTimePicker_ValueChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            var settingValues = Managers.Settings.GetSetting((DateTimePicker)sender);
            _controller.SaveSetting(settingValues.Item1, settingValues.Item2);
        }
    }

    #endregion

    #region ButtonEvents

    private void cmdSelectExportDir_Click(object sender, EventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                txtExportDirectory.Text = fbd.SelectedPath;
                _controller.SaveSetting(txtExportDirectory.Tag.ToString(), fbd.SelectedPath);
            }
        }
    }

    private void cmdSelectImportDir_Click(object sender, EventArgs e)
    {
        using (var fbd = new FolderBrowserDialog())
        {
            DialogResult result = fbd.ShowDialog();

            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
            {
                txtImportDirectory.Text = fbd.SelectedPath;
                _controller.SaveSetting(txtImportDirectory.Tag.ToString(), fbd.SelectedPath);
            }
        }
    }

    private void cmdResetColorsAndLayout_Click(object sender, EventArgs e)
    {
        _controller.resetColorsAndLayout();
    }

    private void cmdResetChart_Click(object sender, EventArgs e)
    {
        _controller.resetChartSettings();
    }

    private void resetDataPredictionButton_Click(object sender, EventArgs e)
    {
        _controller.resetDataPredictionSettings();
    }

    private void avgCorrectionPercentageTextBox_TextChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            var settingValue = Managers.Settings.GetSetting((TextBox)sender);
            _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
        }
    }

    private void AvgCorrectionPercentageReturnTextBox_TextChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            var settingValue = Managers.Settings.GetSetting((TextBox)sender);
            _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
        }
    }

    #endregion

    #region Toolbar

    private void tsbResetAllSettings_Click(object sender, EventArgs e)
    {
        _controller.DeleteSetting("ExportDirectory");
        _controller.DeleteSetting("ImportDirectory");
        _controller.DeleteSetting("VatPerc");
        _controller.DeleteSetting("Currency");

        _controller.resetColorsAndLayout();
        _controller.resetChartSettings();
        _controller.resetDataPredictionSettings();

        loadSettings();
    }

    private void tsbClose_Click(object sender, EventArgs e)
    {
        CloseSettings();
    }

    #endregion

    #region Methods

    private void loadSettings()
    {
        _controller.InitSettings = true;

        setTextBoxSetting(txtExportDirectory);
        setTextBoxSetting(txtImportDirectory);
        setTextBoxSetting(txtCurrency);
        setComboSetting(cboLanguage, Language.English.ToString());

        setTextBoxColorSetting(txtBackgroundColorChart);
        setTextBoxColorSetting(txtForeColorChart);
        setTextBoxColorSetting(txtLineColorChart);
        setTextBoxColorSetting(txtLabelsYColorChart);
        setTextBoxColorSetting(txtFormBackGroundColor);
        setTextBoxColorSetting(txtSliderColor);

        setTextBoxSetting(AvgCorrectionPercentageTextBox);
        setTextBoxSetting(AvgCorrectionPercentageReturnTextBox);

        setAvgRadioButtons();

        setDateTimeSetting(avgDateFromDateTimePicker, DateTime.Now.AddYears(-2));

        _controller.InitSettings = false;
    }

    private void setAvgRadioButtons()
    {
        setRadioButtonSetting(useAllDataForAvgRadioButton);
        setRadioButtonSetting(useDataFromForAvgRadioButton);

        if (useAllDataForAvgRadioButton.Checked == false && useDataFromForAvgRadioButton.Checked == false)
            useAllDataForAvgRadioButton.Checked = true;
    }

    private void setColorSetting(TextBox sender)
    {
        sender.BackColor = colorDialog1.Color;
        _controller.SetColorSetting(sender.Tag.ToString(), colorDialog1.Color);
    }

    private void setTextBoxSetting(TextBox sender)
    {
        sender.Text = string.Empty;

        var setting = _controller.GetSetting(sender.Tag.ToString());
        Managers.Settings.SetTextBox(sender, setting);
    }

    private void setRadioButtonSetting(RadioButton sender)
    {
        EnergyUse.Models.Setting setting = _controller.GetSetting(sender.Tag.ToString());
        Managers.Settings.SetRadioButtonSetting(sender, setting);
    }

    private void setTextBoxColorSetting(TextBox sender)
    {
        sender.BackColor = Color.Empty;            
        var setting = _controller.GetSetting(sender.Tag.ToString());
        Managers.Settings.SetTextBoxColor(sender, setting);
    }

    private void setComboSetting(ComboBox sender, string defaultValue)
    {
        var setting = _controller.GetSetting(sender.Tag.ToString());
        Managers.Settings.SetSettingCombo(sender, setting, defaultValue);
    }

    private void setDateTimeSetting(DateTimePicker sender, DateTime defaultValue)
    {
        var setting = _controller.GetSetting(sender.Tag.ToString());
        Managers.Settings.SetSettingDateTimePicker(sender, setting, defaultValue);
    }

    private void CloseSettings()
    {
        Close();
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
    }

    #endregion
}