using EnergyUse.Common.Enums;

namespace WinFormsEF.Views
{
    public partial class frmSettings : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.Setting _unitOfWork;
        private bool initSettings;

        #endregion

        #region InitForm

        public frmSettings()
        {
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
                setColorSetting((TextBox)sender);
        }

        private void txtForeColorChart_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                setColorSetting((TextBox)sender);
        }

        private void txtLineColorChart_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                setColorSetting((TextBox)sender);
        }

        private void txtLabelsYColorChart_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
                setColorSetting((TextBox)sender);
        }

        private void txtCurrency_TextChanged(object sender, EventArgs e)
        {
            if (initSettings == false)
                Managers.Settings.SaveSettingTextBox((TextBox)sender);
        }

        private void cboLanguage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (initSettings == false)
                Managers.Settings.SaveSettingComboBox((ComboBox)sender);
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

                    var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
                    libSettings.SaveSetting(txtExportDirectory.Tag.ToString(), fbd.SelectedPath);
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

                    var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
                    libSettings.SaveSetting(txtImportDirectory.Tag.ToString(), fbd.SelectedPath);
                }
            }
        }

        private void cmdResetColorsAndLayout_Click(object sender, EventArgs e)
        {
            resetColorsAndLayout();
        }

        private void cmdResetChart_Click(object sender, EventArgs e)
        {
            resetChartSettings();
        }

        private void ResetDataPredictionButton_Click(object sender, EventArgs e)
        {
            resetDataPredictionSettings();
        }

        private void AvgCorrectionPercentageTextBox_TextChanged(object sender, EventArgs e)
        {
            if (initSettings == false)
                Managers.Settings.SaveSettingTextBox((TextBox)sender);
        }

        private void AvgCorrectionPercentageReturnTextBox_TextChanged(object sender, EventArgs e)
        {
            if (initSettings == false)
                Managers.Settings.SaveSettingTextBox((TextBox)sender);
        }

        #endregion

        #region Toolbar

        private void tsbResetAllSettings_Click(object sender, EventArgs e)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

            libSettings.DeleteSetting("ExportDirectory");
            libSettings.DeleteSetting("ImportDirectory");
            libSettings.DeleteSetting("VatPerc");
            libSettings.DeleteSetting("Currency");

            resetColorsAndLayout();
            resetChartSettings();
            resetDataPredictionSettings();

            loadSettings();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            CloseSettings();
        }

        #endregion

        #region Methods

        private void resetColorsAndLayout()
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

            libSettings.DeleteSetting("BackgroundColorForms");
            libSettings.DeleteSetting("SliderColor");
        }

        private void resetChartSettings()
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

            libSettings.DeleteSetting("BackgroundColorChart");
            libSettings.DeleteSetting("ForeColorChart");
            libSettings.DeleteSetting("LineColorChart");
            libSettings.DeleteSetting("LabelsYColorChart");
            libSettings.DeleteSetting("GraphType");
        }

        private void resetDataPredictionSettings()
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

            libSettings.DeleteSetting("AvgCorrectionPercentage");
            libSettings.DeleteSetting("AvgCorrectionPercentageReturn");
        }

        private void loadSettings()
        {
            initSettings = true;

            Managers.Settings.LoadSettingTextBox(txtExportDirectory);
            Managers.Settings.LoadSettingTextBox(txtImportDirectory);
            Managers.Settings.LoadSettingTextBox(txtCurrency);
            Managers.Settings.LoadSettingCombo(cboLanguage, Language.English.ToString());

            Managers.Settings.LoadColorSetting(txtBackgroundColorChart);
            Managers.Settings.LoadColorSetting(txtForeColorChart);
            Managers.Settings.LoadColorSetting(txtLineColorChart);
            Managers.Settings.LoadColorSetting(txtLabelsYColorChart);
            Managers.Settings.LoadColorSetting(txtFormBackGroundColor);
            Managers.Settings.LoadColorSetting(txtSliderColor);

            Managers.Settings.LoadSettingTextBox(AvgCorrectionPercentageTextBox);
            Managers.Settings.LoadSettingTextBox(AvgCorrectionPercentageReturnTextBox);

            initSettings = false;
        }

        private void setColorSetting(TextBox textBox)
        {
            textBox.BackColor = colorDialog1.Color;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            libSettings.SaveColorSetting(textBox.Tag.ToString(), colorDialog1.Color);
        }

        private void CloseSettings()
        {
            Close();
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Setting(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
        }

        #endregion
    }
}