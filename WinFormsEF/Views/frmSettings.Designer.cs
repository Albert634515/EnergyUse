namespace WinFormsEF.Views
{
    partial class frmSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            toolStrip1 = new ToolStrip();
            tsbClose = new ToolStripButton();
            tsbResetAllSettings = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            gbGeneral = new GroupBox();
            lblLanguage = new Label();
            txtCurrency = new TextBox();
            cboLanguage = new ComboBox();
            lblCurrency = new Label();
            cmdSelectImportDir = new Button();
            txtImportDirectory = new TextBox();
            lblImportDirectory = new Label();
            cmdSelectExportDir = new Button();
            txtExportDirectory = new TextBox();
            lblExportDirectory = new Label();
            gbColorsAndLayout = new GroupBox();
            cmdResetColorsAndLayout = new Button();
            lblSliderColor = new Label();
            txtSliderColor = new TextBox();
            lblFormBackColor = new Label();
            txtFormBackGroundColor = new TextBox();
            gbChart = new GroupBox();
            lblGraphLabelColor = new Label();
            txtLabelsYColorChart = new TextBox();
            lblGraphForeColor = new Label();
            txtLineColorChart = new TextBox();
            lblGraphLines = new Label();
            cmdResetChart = new Button();
            txtForeColorChart = new TextBox();
            txtBackgroundColorChart = new TextBox();
            lblGraphBackGround = new Label();
            colorDialog1 = new ColorDialog();
            toolTip1 = new ToolTip(components);
            groupBox1 = new GroupBox();
            AvgCorrectionPercentageReturnTextBox = new TextBox();
            AvgCorrectionPercentageTextBox = new TextBox();
            label2 = new Label();
            ResetDataPredictionButton = new Button();
            label4 = new Label();
            toolStrip1.SuspendLayout();
            gbGeneral.SuspendLayout();
            gbColorsAndLayout.SuspendLayout();
            gbChart.SuspendLayout();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbClose, tsbResetAllSettings });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbClose
            // 
            tsbClose.Alignment = ToolStripItemAlignment.Right;
            tsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbClose, "tsbClose");
            tsbClose.Name = "tsbClose";
            tsbClose.Click += tsbClose_Click;
            // 
            // tsbResetAllSettings
            // 
            tsbResetAllSettings.Image = WinFormsUI.Properties.Resources.switch_24x24;
            resources.ApplyResources(tsbResetAllSettings, "tsbResetAllSettings");
            tsbResetAllSettings.Name = "tsbResetAllSettings";
            tsbResetAllSettings.Click += tsbResetAllSettings_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // gbGeneral
            // 
            resources.ApplyResources(gbGeneral, "gbGeneral");
            gbGeneral.Controls.Add(lblLanguage);
            gbGeneral.Controls.Add(txtCurrency);
            gbGeneral.Controls.Add(cboLanguage);
            gbGeneral.Controls.Add(lblCurrency);
            gbGeneral.Controls.Add(cmdSelectImportDir);
            gbGeneral.Controls.Add(txtImportDirectory);
            gbGeneral.Controls.Add(lblImportDirectory);
            gbGeneral.Controls.Add(cmdSelectExportDir);
            gbGeneral.Controls.Add(txtExportDirectory);
            gbGeneral.Controls.Add(lblExportDirectory);
            gbGeneral.Name = "gbGeneral";
            gbGeneral.TabStop = false;
            // 
            // lblLanguage
            // 
            resources.ApplyResources(lblLanguage, "lblLanguage");
            lblLanguage.Name = "lblLanguage";
            // 
            // txtCurrency
            // 
            resources.ApplyResources(txtCurrency, "txtCurrency");
            txtCurrency.Name = "txtCurrency";
            txtCurrency.Tag = "Currency";
            txtCurrency.TextChanged += txtCurrency_TextChanged;
            // 
            // cboLanguage
            // 
            cboLanguage.FormattingEnabled = true;
            cboLanguage.Items.AddRange(new object[] { resources.GetString("cboLanguage.Items"), resources.GetString("cboLanguage.Items1"), resources.GetString("cboLanguage.Items2") });
            resources.ApplyResources(cboLanguage, "cboLanguage");
            cboLanguage.Name = "cboLanguage";
            cboLanguage.Tag = "Language";
            cboLanguage.SelectedIndexChanged += cboLanguage_SelectedIndexChanged;
            // 
            // lblCurrency
            // 
            resources.ApplyResources(lblCurrency, "lblCurrency");
            lblCurrency.Name = "lblCurrency";
            // 
            // cmdSelectImportDir
            // 
            cmdSelectImportDir.Image = WinFormsUI.Properties.Resources.open_file_folder_icon;
            resources.ApplyResources(cmdSelectImportDir, "cmdSelectImportDir");
            cmdSelectImportDir.Name = "cmdSelectImportDir";
            cmdSelectImportDir.UseVisualStyleBackColor = true;
            cmdSelectImportDir.Click += cmdSelectImportDir_Click;
            // 
            // txtImportDirectory
            // 
            resources.ApplyResources(txtImportDirectory, "txtImportDirectory");
            txtImportDirectory.Name = "txtImportDirectory";
            txtImportDirectory.ReadOnly = true;
            txtImportDirectory.Tag = "ImportDirectory";
            // 
            // lblImportDirectory
            // 
            resources.ApplyResources(lblImportDirectory, "lblImportDirectory");
            lblImportDirectory.Name = "lblImportDirectory";
            // 
            // cmdSelectExportDir
            // 
            cmdSelectExportDir.Image = WinFormsUI.Properties.Resources.open_file_folder_icon;
            resources.ApplyResources(cmdSelectExportDir, "cmdSelectExportDir");
            cmdSelectExportDir.Name = "cmdSelectExportDir";
            cmdSelectExportDir.UseVisualStyleBackColor = true;
            cmdSelectExportDir.Click += cmdSelectExportDir_Click;
            // 
            // txtExportDirectory
            // 
            resources.ApplyResources(txtExportDirectory, "txtExportDirectory");
            txtExportDirectory.Name = "txtExportDirectory";
            txtExportDirectory.ReadOnly = true;
            txtExportDirectory.Tag = "ExportDirectory";
            // 
            // lblExportDirectory
            // 
            resources.ApplyResources(lblExportDirectory, "lblExportDirectory");
            lblExportDirectory.Name = "lblExportDirectory";
            // 
            // gbColorsAndLayout
            // 
            resources.ApplyResources(gbColorsAndLayout, "gbColorsAndLayout");
            gbColorsAndLayout.Controls.Add(cmdResetColorsAndLayout);
            gbColorsAndLayout.Controls.Add(lblSliderColor);
            gbColorsAndLayout.Controls.Add(txtSliderColor);
            gbColorsAndLayout.Controls.Add(lblFormBackColor);
            gbColorsAndLayout.Controls.Add(txtFormBackGroundColor);
            gbColorsAndLayout.Name = "gbColorsAndLayout";
            gbColorsAndLayout.TabStop = false;
            // 
            // cmdResetColorsAndLayout
            // 
            resources.ApplyResources(cmdResetColorsAndLayout, "cmdResetColorsAndLayout");
            cmdResetColorsAndLayout.Image = WinFormsUI.Properties.Resources.switch_24x24;
            cmdResetColorsAndLayout.Name = "cmdResetColorsAndLayout";
            cmdResetColorsAndLayout.UseVisualStyleBackColor = true;
            cmdResetColorsAndLayout.Click += cmdResetColorsAndLayout_Click;
            // 
            // lblSliderColor
            // 
            resources.ApplyResources(lblSliderColor, "lblSliderColor");
            lblSliderColor.Name = "lblSliderColor";
            // 
            // txtSliderColor
            // 
            resources.ApplyResources(txtSliderColor, "txtSliderColor");
            txtSliderColor.Name = "txtSliderColor";
            txtSliderColor.ReadOnly = true;
            txtSliderColor.Tag = "SliderColor";
            txtSliderColor.Click += txtSliderColor_Click;
            // 
            // lblFormBackColor
            // 
            resources.ApplyResources(lblFormBackColor, "lblFormBackColor");
            lblFormBackColor.Name = "lblFormBackColor";
            // 
            // txtFormBackGroundColor
            // 
            resources.ApplyResources(txtFormBackGroundColor, "txtFormBackGroundColor");
            txtFormBackGroundColor.Name = "txtFormBackGroundColor";
            txtFormBackGroundColor.ReadOnly = true;
            txtFormBackGroundColor.Tag = "BackgroundColorForms";
            txtFormBackGroundColor.Click += txtFormBackGroundColor_Click;
            // 
            // gbChart
            // 
            resources.ApplyResources(gbChart, "gbChart");
            gbChart.Controls.Add(lblGraphLabelColor);
            gbChart.Controls.Add(txtLabelsYColorChart);
            gbChart.Controls.Add(lblGraphForeColor);
            gbChart.Controls.Add(txtLineColorChart);
            gbChart.Controls.Add(lblGraphLines);
            gbChart.Controls.Add(cmdResetChart);
            gbChart.Controls.Add(txtForeColorChart);
            gbChart.Controls.Add(txtBackgroundColorChart);
            gbChart.Controls.Add(lblGraphBackGround);
            gbChart.Name = "gbChart";
            gbChart.TabStop = false;
            // 
            // lblGraphLabelColor
            // 
            resources.ApplyResources(lblGraphLabelColor, "lblGraphLabelColor");
            lblGraphLabelColor.Name = "lblGraphLabelColor";
            // 
            // txtLabelsYColorChart
            // 
            resources.ApplyResources(txtLabelsYColorChart, "txtLabelsYColorChart");
            txtLabelsYColorChart.Name = "txtLabelsYColorChart";
            txtLabelsYColorChart.ReadOnly = true;
            txtLabelsYColorChart.Tag = "LabelsYColorChart";
            txtLabelsYColorChart.Click += txtLabelsYColorChart_Click;
            // 
            // lblGraphForeColor
            // 
            resources.ApplyResources(lblGraphForeColor, "lblGraphForeColor");
            lblGraphForeColor.Name = "lblGraphForeColor";
            // 
            // txtLineColorChart
            // 
            resources.ApplyResources(txtLineColorChart, "txtLineColorChart");
            txtLineColorChart.Name = "txtLineColorChart";
            txtLineColorChart.ReadOnly = true;
            txtLineColorChart.Tag = "LineColorChart";
            txtLineColorChart.Click += txtLineColorChart_Click;
            // 
            // lblGraphLines
            // 
            resources.ApplyResources(lblGraphLines, "lblGraphLines");
            lblGraphLines.Name = "lblGraphLines";
            // 
            // cmdResetChart
            // 
            resources.ApplyResources(cmdResetChart, "cmdResetChart");
            cmdResetChart.Image = WinFormsUI.Properties.Resources.switch_24x24;
            cmdResetChart.Name = "cmdResetChart";
            cmdResetChart.UseVisualStyleBackColor = true;
            cmdResetChart.Click += cmdResetChart_Click;
            // 
            // txtForeColorChart
            // 
            resources.ApplyResources(txtForeColorChart, "txtForeColorChart");
            txtForeColorChart.Name = "txtForeColorChart";
            txtForeColorChart.ReadOnly = true;
            txtForeColorChart.Tag = "ForeColorChart";
            txtForeColorChart.Click += txtForeColorChart_Click;
            // 
            // txtBackgroundColorChart
            // 
            resources.ApplyResources(txtBackgroundColorChart, "txtBackgroundColorChart");
            txtBackgroundColorChart.Name = "txtBackgroundColorChart";
            txtBackgroundColorChart.ReadOnly = true;
            txtBackgroundColorChart.Tag = "BackgroundColorChart";
            txtBackgroundColorChart.Click += txtBackgroundColorChart_Click;
            // 
            // lblGraphBackGround
            // 
            resources.ApplyResources(lblGraphBackGround, "lblGraphBackGround");
            lblGraphBackGround.Name = "lblGraphBackGround";
            // 
            // groupBox1
            // 
            resources.ApplyResources(groupBox1, "groupBox1");
            groupBox1.Controls.Add(AvgCorrectionPercentageReturnTextBox);
            groupBox1.Controls.Add(AvgCorrectionPercentageTextBox);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(ResetDataPredictionButton);
            groupBox1.Controls.Add(label4);
            groupBox1.Name = "groupBox1";
            groupBox1.TabStop = false;
            // 
            // AvgCorrectionPercentageReturnTextBox
            // 
            resources.ApplyResources(AvgCorrectionPercentageReturnTextBox, "AvgCorrectionPercentageReturnTextBox");
            AvgCorrectionPercentageReturnTextBox.Name = "AvgCorrectionPercentageReturnTextBox";
            AvgCorrectionPercentageReturnTextBox.Tag = "AvgCorrectionPercentageReturn";
            AvgCorrectionPercentageReturnTextBox.TextChanged += AvgCorrectionPercentageReturnTextBox_TextChanged;
            // 
            // AvgCorrectionPercentageTextBox
            // 
            resources.ApplyResources(AvgCorrectionPercentageTextBox, "AvgCorrectionPercentageTextBox");
            AvgCorrectionPercentageTextBox.Name = "AvgCorrectionPercentageTextBox";
            AvgCorrectionPercentageTextBox.Tag = "AvgCorrectionPercentage";
            AvgCorrectionPercentageTextBox.TextChanged += AvgCorrectionPercentageTextBox_TextChanged;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // ResetDataPredictionButton
            // 
            resources.ApplyResources(ResetDataPredictionButton, "ResetDataPredictionButton");
            ResetDataPredictionButton.Image = WinFormsUI.Properties.Resources.switch_24x24;
            ResetDataPredictionButton.Name = "ResetDataPredictionButton";
            ResetDataPredictionButton.UseVisualStyleBackColor = true;
            ResetDataPredictionButton.Click += ResetDataPredictionButton_Click;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // frmSettings
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(groupBox1);
            Controls.Add(gbChart);
            Controls.Add(gbColorsAndLayout);
            Controls.Add(gbGeneral);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmSettings";
            ShowIcon = false;
            ShowInTaskbar = false;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            gbGeneral.ResumeLayout(false);
            gbGeneral.PerformLayout();
            gbColorsAndLayout.ResumeLayout(false);
            gbColorsAndLayout.PerformLayout();
            gbChart.ResumeLayout(false);
            gbChart.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbClose;
        private ToolStripButton tsbResetAllSettings;
        private StatusStrip statusStrip1;
        private GroupBox gbGeneral;
        private Label lblLanguage;
        private TextBox txtCurrency;
        private ComboBox cboLanguage;
        private Label lblCurrency;
        private Button cmdSelectImportDir;
        private TextBox txtImportDirectory;
        private Label lblImportDirectory;
        private Button cmdSelectExportDir;
        private TextBox txtExportDirectory;
        private Label lblExportDirectory;
        private GroupBox gbColorsAndLayout;
        private Button cmdResetColorsAndLayout;
        private Label lblSliderColor;
        private TextBox txtSliderColor;
        private Label lblFormBackColor;
        private TextBox txtFormBackGroundColor;
        private GroupBox gbChart;
        private Label lblGraphLabelColor;
        private TextBox txtLabelsYColorChart;
        private Label lblGraphForeColor;
        private TextBox txtLineColorChart;
        private Label lblGraphLines;
        private Button cmdResetChart;
        private TextBox txtForeColorChart;
        private TextBox txtBackgroundColorChart;
        private Label lblGraphBackGround;
        private ColorDialog colorDialog1;
        private ToolTip toolTip1;
        private GroupBox groupBox1;
        private TextBox AvgCorrectionPercentageReturnTextBox;
        private TextBox AvgCorrectionPercentageTextBox;
        private Label label2;
        private Button ResetDataPredictionButton;
        private Label label4;
    }
}