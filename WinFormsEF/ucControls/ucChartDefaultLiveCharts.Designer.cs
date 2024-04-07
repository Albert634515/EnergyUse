namespace WinFormsEF.ucControls
{
    partial class ucChartDefaultLiveCharts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucChartDefaultLiveCharts));
            chkShowStacked = new CheckBox();
            lblNetto = new Label();
            lblProduction = new Label();
            lblConsumption = new Label();
            cmdExport = new Button();
            cmdReset = new Button();
            chkShowAvg = new CheckBox();
            chkPredictMissingData = new CheckBox();
            lblShowBy = new Label();
            lblType = new Label();
            rbEfficiency = new RadioButton();
            rbValue = new RadioButton();
            rbRate = new RadioButton();
            rbTotals = new RadioButton();
            rbSubCategory = new RadioButton();
            rbCategory = new RadioButton();
            pnChartContainer = new Panel();
            plShowBy = new Panel();
            pnlType = new Panel();
            DtpTill = new DateTimePicker();
            DtpFrom = new DateTimePicker();
            PeriodStartLabel = new Label();
            CboPeriodType = new ComboBox();
            bsPeriodType = new BindingSource(components);
            LblPeriod = new Label();
            toolTip1 = new ToolTip(components);
            CboCompareWith = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            EnergyTypeLabel = new Label();
            PeriodEndLabel = new Label();
            plShowBy.SuspendLayout();
            pnlType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsPeriodType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // chkShowStacked
            // 
            resources.ApplyResources(chkShowStacked, "chkShowStacked");
            chkShowStacked.Checked = true;
            chkShowStacked.CheckState = CheckState.Checked;
            chkShowStacked.Name = "chkShowStacked";
            chkShowStacked.Tag = "DefaultChartPeriodShowStacked";
            chkShowStacked.UseVisualStyleBackColor = true;
            chkShowStacked.CheckedChanged += chkShowStacked_CheckedChanged;
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
            // cmdExport
            // 
            resources.ApplyResources(cmdExport, "cmdExport");
            cmdExport.Image = WinFormsUI.Properties.Resources.upload_16x16;
            cmdExport.Name = "cmdExport";
            cmdExport.UseVisualStyleBackColor = true;
            cmdExport.Click += CmdExport_Click;
            // 
            // cmdReset
            // 
            resources.ApplyResources(cmdReset, "cmdReset");
            cmdReset.Name = "cmdReset";
            cmdReset.UseVisualStyleBackColor = true;
            cmdReset.Click += CmdReset_Click;
            // 
            // chkShowAvg
            // 
            resources.ApplyResources(chkShowAvg, "chkShowAvg");
            chkShowAvg.Checked = true;
            chkShowAvg.CheckState = CheckState.Checked;
            chkShowAvg.Name = "chkShowAvg";
            chkShowAvg.Tag = "DefaultChartPeriodShowAverage";
            chkShowAvg.UseVisualStyleBackColor = true;
            chkShowAvg.CheckedChanged += chkShowAvg_CheckedChanged;
            // 
            // chkPredictMissingData
            // 
            resources.ApplyResources(chkPredictMissingData, "chkPredictMissingData");
            chkPredictMissingData.Checked = true;
            chkPredictMissingData.CheckState = CheckState.Checked;
            chkPredictMissingData.Name = "chkPredictMissingData";
            chkPredictMissingData.Tag = "DefaultChartPeriodPredictMissing";
            chkPredictMissingData.UseVisualStyleBackColor = true;
            chkPredictMissingData.CheckedChanged += chkPredictMissingData_CheckedChanged;
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
            // rbEfficiency
            // 
            resources.ApplyResources(rbEfficiency, "rbEfficiency");
            rbEfficiency.Name = "rbEfficiency";
            rbEfficiency.UseVisualStyleBackColor = true;
            rbEfficiency.CheckedChanged += rbType_CheckedChanged;
            // 
            // rbValue
            // 
            resources.ApplyResources(rbValue, "rbValue");
            rbValue.Name = "rbValue";
            rbValue.UseVisualStyleBackColor = true;
            rbValue.CheckedChanged += rbType_CheckedChanged;
            // 
            // rbRate
            // 
            resources.ApplyResources(rbRate, "rbRate");
            rbRate.Checked = true;
            rbRate.Name = "rbRate";
            rbRate.TabStop = true;
            rbRate.UseVisualStyleBackColor = true;
            rbRate.CheckedChanged += rbType_CheckedChanged;
            // 
            // rbTotals
            // 
            resources.ApplyResources(rbTotals, "rbTotals");
            rbTotals.Name = "rbTotals";
            rbTotals.UseVisualStyleBackColor = true;
            rbTotals.CheckedChanged += rbShowBy_CheckedChanged;
            // 
            // rbSubCategory
            // 
            resources.ApplyResources(rbSubCategory, "rbSubCategory");
            rbSubCategory.Name = "rbSubCategory";
            rbSubCategory.UseVisualStyleBackColor = true;
            rbSubCategory.CheckedChanged += rbShowBy_CheckedChanged;
            // 
            // rbCategory
            // 
            resources.ApplyResources(rbCategory, "rbCategory");
            rbCategory.Checked = true;
            rbCategory.Name = "rbCategory";
            rbCategory.TabStop = true;
            rbCategory.UseVisualStyleBackColor = true;
            rbCategory.CheckedChanged += rbShowBy_CheckedChanged;
            // 
            // pnChartContainer
            // 
            resources.ApplyResources(pnChartContainer, "pnChartContainer");
            pnChartContainer.BorderStyle = BorderStyle.Fixed3D;
            pnChartContainer.Name = "pnChartContainer";
            // 
            // plShowBy
            // 
            plShowBy.Controls.Add(rbTotals);
            plShowBy.Controls.Add(rbSubCategory);
            plShowBy.Controls.Add(rbCategory);
            resources.ApplyResources(plShowBy, "plShowBy");
            plShowBy.Name = "plShowBy";
            plShowBy.Tag = "DefaultChartPeriodShowBy";
            // 
            // pnlType
            // 
            pnlType.Controls.Add(rbEfficiency);
            pnlType.Controls.Add(rbValue);
            pnlType.Controls.Add(rbRate);
            resources.ApplyResources(pnlType, "pnlType");
            pnlType.Name = "pnlType";
            pnlType.Tag = "DefaultChartPeriodType";
            // 
            // DtpTill
            // 
            DtpTill.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DtpTill, "DtpTill");
            DtpTill.Name = "DtpTill";
            DtpTill.Tag = "DefaultChartPeriodPeriodEnd";
            DtpTill.ValueChanged += DtpTill_ValueChanged;

            // 
            // DtpFrom
            // 
            DtpFrom.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DtpFrom, "DtpFrom");
            DtpFrom.Name = "DtpFrom";
            DtpFrom.Tag = "DefaultChartPeriodPeriodStart";
            DtpFrom.ValueChanged += DtpFrom_ValueChanged;

            // 
            // PeriodStartLabel
            // 
            resources.ApplyResources(PeriodStartLabel, "PeriodStartLabel");
            PeriodStartLabel.Name = "PeriodStartLabel";
            // 
            // CboPeriodType
            // 
            CboPeriodType.DataSource = bsPeriodType;
            CboPeriodType.DisplayMember = "Description";
            CboPeriodType.FormattingEnabled = true;
            resources.ApplyResources(CboPeriodType, "CboPeriodType");
            CboPeriodType.Name = "CboPeriodType";
            CboPeriodType.Tag = "DefaultChartPeriodType";
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
            // CboCompareWith
            // 
            CboCompareWith.DataSource = bsEnergyTypes;
            CboCompareWith.DisplayMember = "Name";
            CboCompareWith.FormattingEnabled = true;
            resources.ApplyResources(CboCompareWith, "CboCompareWith");
            CboCompareWith.Name = "CboCompareWith";
            CboCompareWith.Tag = "DefaultChartPeriodCompareWith";
            CboCompareWith.ValueMember = "Id";
            CboCompareWith.SelectedIndexChanged += CboCompareWith_SelectedIndexChanged;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // EnergyTypeLabel
            // 
            resources.ApplyResources(EnergyTypeLabel, "EnergyTypeLabel");
            EnergyTypeLabel.Name = "EnergyTypeLabel";
            // 
            // PeriodEndLabel
            // 
            resources.ApplyResources(PeriodEndLabel, "PeriodEndLabel");
            PeriodEndLabel.Name = "PeriodEndLabel";
            // 
            // ucChartDefaultLiveCharts
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(PeriodEndLabel);
            Controls.Add(CboCompareWith);
            Controls.Add(EnergyTypeLabel);
            Controls.Add(chkShowStacked);
            Controls.Add(lblNetto);
            Controls.Add(lblProduction);
            Controls.Add(lblConsumption);
            Controls.Add(cmdExport);
            Controls.Add(cmdReset);
            Controls.Add(chkShowAvg);
            Controls.Add(chkPredictMissingData);
            Controls.Add(lblShowBy);
            Controls.Add(lblType);
            Controls.Add(pnChartContainer);
            Controls.Add(plShowBy);
            Controls.Add(pnlType);
            Controls.Add(DtpTill);
            Controls.Add(DtpFrom);
            Controls.Add(PeriodStartLabel);
            Controls.Add(CboPeriodType);
            Controls.Add(LblPeriod);
            Name = "ucChartDefaultLiveCharts";
            plShowBy.ResumeLayout(false);
            plShowBy.PerformLayout();
            pnlType.ResumeLayout(false);
            pnlType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsPeriodType).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private CheckBox chkShowStacked;
        private Label lblNetto;
        private Label lblProduction;
        private Label lblConsumption;
        private Button cmdExport;
        private Button cmdReset;
        private CheckBox chkShowAvg;
        private CheckBox chkPredictMissingData;
        private Label lblShowBy;
        private Label lblType;
        private RadioButton rbEfficiency;
        private RadioButton rbValue;
        private RadioButton rbRate;
        private RadioButton rbTotals;
        private RadioButton rbSubCategory;
        private RadioButton rbCategory;
        private Panel pnChartContainer;
        private Panel plShowBy;
        private Panel pnlType;
        private DateTimePicker DtpTill;
        private DateTimePicker DtpFrom;
        private Label PeriodStartLabel;
        private ComboBox CboPeriodType;
        private Label LblPeriod;
        private BindingSource bsPeriodType;
        private ToolTip toolTip1;
        private ComboBox CboCompareWith;
        private Label EnergyTypeLabel;
        private Label PeriodEndLabel;
        private BindingSource bsEnergyTypes;
    }
}
