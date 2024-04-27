namespace WinFormsEF.ucControls
{
    partial class ucChartCompareLiveCharts
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucChartCompareLiveCharts));
            chkShowStacked = new CheckBox();
            lblNetto = new Label();
            lblProduction = new Label();
            lblConsumption = new Label();
            TotalsRadioButton = new RadioButton();
            SubCategoryRadioButton = new RadioButton();
            CategoryRadioButton = new RadioButton();
            ShowByPanel = new Panel();
            EfficiencyRadioButton = new RadioButton();
            ValueRadioButton = new RadioButton();
            RateRadioButton = new RadioButton();
            pnChartContainer = new Panel();
            TypePanel = new Panel();
            lblShowBy = new Label();
            lblType = new Label();
            chkPredictMissingData = new CheckBox();
            cmdExport = new Button();
            cmdReset = new Button();
            cboNumbers2 = new ComboBox();
            lblNumber2 = new Label();
            cboEndYear = new ComboBox();
            lblUntil = new Label();
            cboNumbers = new ComboBox();
            LblNumber = new Label();
            cboStartYear = new ComboBox();
            LblRange = new Label();
            CboPeriodType = new ComboBox();
            bsPeriodType = new BindingSource(components);
            LblPeriod = new Label();
            toolTip1 = new ToolTip(components);
            ShowByPanel.SuspendLayout();
            TypePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsPeriodType).BeginInit();
            SuspendLayout();
            // 
            // chkShowStacked
            // 
            resources.ApplyResources(chkShowStacked, "chkShowStacked");
            chkShowStacked.Checked = true;
            chkShowStacked.CheckState = CheckState.Checked;
            chkShowStacked.Name = "chkShowStacked";
            chkShowStacked.Tag = "ChartCompareShowStacked";
            chkShowStacked.UseVisualStyleBackColor = true;
            // 
            // lblNetto
            // 
            resources.ApplyResources(lblNetto, "lblNetto");
            lblNetto.Name = "lblNetto";
            // 
            // lblProduction
            // 
            resources.ApplyResources(lblProduction, "lblProduction");
            lblProduction.Name = "lblProduction";
            // 
            // lblConsumption
            // 
            resources.ApplyResources(lblConsumption, "lblConsumption");
            lblConsumption.Name = "lblConsumption";
            // 
            // TotalsRadioButton
            // 
            resources.ApplyResources(TotalsRadioButton, "TotalsRadioButton");
            TotalsRadioButton.Name = "TotalsRadioButton";
            TotalsRadioButton.Tag = "ChartCompareShowBy";
            TotalsRadioButton.UseVisualStyleBackColor = true;
            TotalsRadioButton.CheckedChanged += TotalsRadioButton_CheckedChanged;
            // 
            // SubCategoryRadioButton
            // 
            resources.ApplyResources(SubCategoryRadioButton, "SubCategoryRadioButton");
            SubCategoryRadioButton.Name = "SubCategoryRadioButton";
            SubCategoryRadioButton.Tag = "ChartCompareSubCategoryShowBy";
            SubCategoryRadioButton.UseVisualStyleBackColor = true;
            SubCategoryRadioButton.CheckedChanged += SubCategoryRadioButton_CheckedChanged;
            // 
            // CategoryRadioButton
            // 
            resources.ApplyResources(CategoryRadioButton, "CategoryRadioButton");
            CategoryRadioButton.Checked = true;
            CategoryRadioButton.Name = "CategoryRadioButton";
            CategoryRadioButton.TabStop = true;
            CategoryRadioButton.Tag = "ChartCompareCategoryShowBy";
            CategoryRadioButton.UseVisualStyleBackColor = true;
            CategoryRadioButton.CheckedChanged += categoryRadioButton_CheckedChanged;
            // 
            // ShowByPanel
            // 
            ShowByPanel.Controls.Add(TotalsRadioButton);
            ShowByPanel.Controls.Add(SubCategoryRadioButton);
            ShowByPanel.Controls.Add(CategoryRadioButton);
            resources.ApplyResources(ShowByPanel, "ShowByPanel");
            ShowByPanel.Name = "ShowByPanel";
            ShowByPanel.Tag = "ChartCompareShowBy";
            // 
            // EfficiencyRadioButton
            // 
            resources.ApplyResources(EfficiencyRadioButton, "EfficiencyRadioButton");
            EfficiencyRadioButton.Name = "EfficiencyRadioButton";
            EfficiencyRadioButton.Tag = "ChartCompareEfficiencyType";
            EfficiencyRadioButton.UseVisualStyleBackColor = true;
            EfficiencyRadioButton.CheckedChanged += efficiencyRadioButton_CheckedChanged;
            // 
            // ValueRadioButton
            // 
            resources.ApplyResources(ValueRadioButton, "ValueRadioButton");
            ValueRadioButton.Name = "ValueRadioButton";
            ValueRadioButton.Tag = "ChartCompareValueType";
            ValueRadioButton.UseVisualStyleBackColor = true;
            ValueRadioButton.CheckedChanged += valueRadioButton_CheckedChanged;
            // 
            // RateRadioButton
            // 
            resources.ApplyResources(RateRadioButton, "RateRadioButton");
            RateRadioButton.Checked = true;
            RateRadioButton.Name = "RateRadioButton";
            RateRadioButton.TabStop = true;
            RateRadioButton.Tag = "ChartCompareRateType";
            RateRadioButton.UseVisualStyleBackColor = true;
            RateRadioButton.CheckedChanged += rateRadioButton_CheckedChanged;
            // 
            // pnChartContainer
            // 
            resources.ApplyResources(pnChartContainer, "pnChartContainer");
            pnChartContainer.BorderStyle = BorderStyle.Fixed3D;
            pnChartContainer.Name = "pnChartContainer";
            // 
            // TypePanel
            // 
            TypePanel.Controls.Add(EfficiencyRadioButton);
            TypePanel.Controls.Add(ValueRadioButton);
            TypePanel.Controls.Add(RateRadioButton);
            resources.ApplyResources(TypePanel, "TypePanel");
            TypePanel.Name = "TypePanel";
            TypePanel.Tag = "ChartCompareType";
            // 
            // lblShowBy
            // 
            resources.ApplyResources(lblShowBy, "lblShowBy");
            lblShowBy.Name = "lblShowBy";
            // 
            // lblType
            // 
            resources.ApplyResources(lblType, "lblType");
            lblType.Name = "lblType";
            // 
            // chkPredictMissingData
            // 
            resources.ApplyResources(chkPredictMissingData, "chkPredictMissingData");
            chkPredictMissingData.Checked = true;
            chkPredictMissingData.CheckState = CheckState.Checked;
            chkPredictMissingData.Name = "chkPredictMissingData";
            chkPredictMissingData.Tag = "ChartComparePredictMissing";
            chkPredictMissingData.UseVisualStyleBackColor = true;
            chkPredictMissingData.CheckedChanged += chkPredictMissingData_CheckedChanged;
            // 
            // cmdExport
            // 
            resources.ApplyResources(cmdExport, "cmdExport");
            cmdExport.Image = WinFormsUI.Properties.Resources.upload_16x16;
            cmdExport.Name = "cmdExport";
            cmdExport.UseVisualStyleBackColor = true;
            cmdExport.Click += cmdExport_Click;
            // 
            // cmdReset
            // 
            resources.ApplyResources(cmdReset, "cmdReset");
            cmdReset.Name = "cmdReset";
            cmdReset.UseVisualStyleBackColor = true;
            cmdReset.Click += cmdReset_Click;
            // 
            // cboNumbers2
            // 
            cboNumbers2.FormattingEnabled = true;
            cboNumbers2.Items.AddRange(new object[] { resources.GetString("cboNumbers2.Items"), resources.GetString("cboNumbers2.Items1"), resources.GetString("cboNumbers2.Items2"), resources.GetString("cboNumbers2.Items3") });
            resources.ApplyResources(cboNumbers2, "cboNumbers2");
            cboNumbers2.Name = "cboNumbers2";
            cboNumbers2.Tag = "ChartCompareNumbers2";
            cboNumbers2.SelectedIndexChanged += cboNumbers2_SelectedIndexChanged;
            // 
            // lblNumber2
            // 
            resources.ApplyResources(lblNumber2, "lblNumber2");
            lblNumber2.Name = "lblNumber2";
            // 
            // cboEndYear
            // 
            cboEndYear.FormattingEnabled = true;
            cboEndYear.Items.AddRange(new object[] { resources.GetString("cboEndYear.Items"), resources.GetString("cboEndYear.Items1"), resources.GetString("cboEndYear.Items2"), resources.GetString("cboEndYear.Items3") });
            resources.ApplyResources(cboEndYear, "cboEndYear");
            cboEndYear.Name = "cboEndYear";
            cboEndYear.Tag = "ChartCompareEndYear";
            cboEndYear.SelectedIndexChanged += cboEndYear_SelectedIndexChanged;
            // 
            // lblUntil
            // 
            resources.ApplyResources(lblUntil, "lblUntil");
            lblUntil.Name = "lblUntil";
            lblUntil.Tag = "";
            // 
            // cboNumbers
            // 
            cboNumbers.FormattingEnabled = true;
            cboNumbers.Items.AddRange(new object[] { resources.GetString("cboNumbers.Items"), resources.GetString("cboNumbers.Items1"), resources.GetString("cboNumbers.Items2"), resources.GetString("cboNumbers.Items3") });
            resources.ApplyResources(cboNumbers, "cboNumbers");
            cboNumbers.Name = "cboNumbers";
            cboNumbers.Tag = "ChartCompareNumbers";
            cboNumbers.SelectedIndexChanged += cboNumbers_SelectedValueChanged;
            // 
            // LblNumber
            // 
            resources.ApplyResources(LblNumber, "LblNumber");
            LblNumber.Name = "LblNumber";
            // 
            // cboStartYear
            // 
            cboStartYear.FormattingEnabled = true;
            cboStartYear.Items.AddRange(new object[] { resources.GetString("cboStartYear.Items"), resources.GetString("cboStartYear.Items1"), resources.GetString("cboStartYear.Items2"), resources.GetString("cboStartYear.Items3") });
            resources.ApplyResources(cboStartYear, "cboStartYear");
            cboStartYear.Name = "cboStartYear";
            cboStartYear.Tag = "ChartCompareStartYear";
            cboStartYear.SelectedIndexChanged += cboStartYear_SelectedIndexChanged;
            // 
            // LblRange
            // 
            resources.ApplyResources(LblRange, "LblRange");
            LblRange.Name = "LblRange";
            // 
            // CboPeriodType
            // 
            CboPeriodType.DataSource = bsPeriodType;
            CboPeriodType.DisplayMember = "Description";
            CboPeriodType.FormattingEnabled = true;
            resources.ApplyResources(CboPeriodType, "CboPeriodType");
            CboPeriodType.Name = "CboPeriodType";
            CboPeriodType.Tag = "ChartComparePeriodType";
            CboPeriodType.ValueMember = "Key";
            CboPeriodType.SelectedIndexChanged += cboPeriodType_SelectedIndexChanged;
            // 
            // bsPeriodType
            // 
            bsPeriodType.DataSource = typeof(EnergyUse.Models.Common.SelectionItem);
            // 
            // LblPeriod
            // 
            resources.ApplyResources(LblPeriod, "LblPeriod");
            LblPeriod.Name = "LblPeriod";
            // 
            // ucChartCompareLiveCharts
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(chkShowStacked);
            Controls.Add(lblNetto);
            Controls.Add(lblProduction);
            Controls.Add(lblConsumption);
            Controls.Add(ShowByPanel);
            Controls.Add(pnChartContainer);
            Controls.Add(TypePanel);
            Controls.Add(lblShowBy);
            Controls.Add(lblType);
            Controls.Add(chkPredictMissingData);
            Controls.Add(cmdExport);
            Controls.Add(cmdReset);
            Controls.Add(cboNumbers2);
            Controls.Add(lblNumber2);
            Controls.Add(cboEndYear);
            Controls.Add(lblUntil);
            Controls.Add(cboNumbers);
            Controls.Add(LblNumber);
            Controls.Add(cboStartYear);
            Controls.Add(LblRange);
            Controls.Add(CboPeriodType);
            Controls.Add(LblPeriod);
            Name = "ucChartCompareLiveCharts";
            ShowByPanel.ResumeLayout(false);
            ShowByPanel.PerformLayout();
            TypePanel.ResumeLayout(false);
            TypePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsPeriodType).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkShowStacked;
        private Label lblNetto;
        private Label lblProduction;
        private Label lblConsumption;
        private RadioButton TotalsRadioButton;
        private RadioButton SubCategoryRadioButton;
        private RadioButton CategoryRadioButton;
        private Panel ShowByPanel;
        private RadioButton EfficiencyRadioButton;
        private RadioButton ValueRadioButton;
        private RadioButton RateRadioButton;
        private Panel pnChartContainer;
        private Panel TypePanel;
        private Label lblShowBy;
        private Label lblType;
        private CheckBox chkPredictMissingData;
        private Button cmdExport;
        private Button cmdReset;
        private ComboBox cboNumbers2;
        private Label lblNumber2;
        private ComboBox cboEndYear;
        private Label lblUntil;
        private ComboBox cboNumbers;
        private Label LblNumber;
        private ComboBox cboStartYear;
        private Label LblRange;
        private ComboBox CboPeriodType;
        private Label LblPeriod;
        private ToolTip toolTip1;
        private BindingSource bsPeriodType;
    }
}
