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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucChartCompareLiveCharts));
            this.chkShowStacked = new System.Windows.Forms.CheckBox();
            this.lblNetto = new System.Windows.Forms.Label();
            this.lblProduction = new System.Windows.Forms.Label();
            this.lblConsumption = new System.Windows.Forms.Label();
            this.rbTotals = new System.Windows.Forms.RadioButton();
            this.rbSubCategory = new System.Windows.Forms.RadioButton();
            this.rbCategory = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbEfficiency = new System.Windows.Forms.RadioButton();
            this.rbValue = new System.Windows.Forms.RadioButton();
            this.rbRate = new System.Windows.Forms.RadioButton();
            this.pnChartContainer = new System.Windows.Forms.Panel();
            this.pnlType = new System.Windows.Forms.Panel();
            this.lblShowBy = new System.Windows.Forms.Label();
            this.lblType = new System.Windows.Forms.Label();
            this.chkPredictMissingData = new System.Windows.Forms.CheckBox();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cmdReset = new System.Windows.Forms.Button();
            this.cboNumbers2 = new System.Windows.Forms.ComboBox();
            this.lblNumber2 = new System.Windows.Forms.Label();
            this.cboEndYear = new System.Windows.Forms.ComboBox();
            this.lblUntil = new System.Windows.Forms.Label();
            this.cboNumbers = new System.Windows.Forms.ComboBox();
            this.LblNumber = new System.Windows.Forms.Label();
            this.cboStartYear = new System.Windows.Forms.ComboBox();
            this.LblRange = new System.Windows.Forms.Label();
            this.CboPeriodType = new System.Windows.Forms.ComboBox();
            this.LblPeriod = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bsPeriodType = new System.Windows.Forms.BindingSource(this.components);
            this.panel1.SuspendLayout();
            this.pnlType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPeriodType)).BeginInit();
            this.SuspendLayout();
            // 
            // chkShowStacked
            // 
            resources.ApplyResources(this.chkShowStacked, "chkShowStacked");
            this.chkShowStacked.Checked = true;
            this.chkShowStacked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowStacked.Name = "chkShowStacked";
            this.chkShowStacked.UseVisualStyleBackColor = true;
            // 
            // lblNetto
            // 
            resources.ApplyResources(this.lblNetto, "lblNetto");
            this.lblNetto.Name = "lblNetto";
            // 
            // lblProduction
            // 
            resources.ApplyResources(this.lblProduction, "lblProduction");
            this.lblProduction.Name = "lblProduction";
            // 
            // lblConsumption
            // 
            resources.ApplyResources(this.lblConsumption, "lblConsumption");
            this.lblConsumption.Name = "lblConsumption";
            // 
            // rbTotals
            // 
            resources.ApplyResources(this.rbTotals, "rbTotals");
            this.rbTotals.Name = "rbTotals";
            this.rbTotals.UseVisualStyleBackColor = true;
            this.rbTotals.CheckedChanged += new System.EventHandler(this.rbShowType_CheckedChanged);
            // 
            // rbSubCategory
            // 
            resources.ApplyResources(this.rbSubCategory, "rbSubCategory");
            this.rbSubCategory.Name = "rbSubCategory";
            this.rbSubCategory.UseVisualStyleBackColor = true;
            this.rbSubCategory.CheckedChanged += new System.EventHandler(this.rbShowType_CheckedChanged);
            // 
            // rbCategory
            // 
            resources.ApplyResources(this.rbCategory, "rbCategory");
            this.rbCategory.Checked = true;
            this.rbCategory.Name = "rbCategory";
            this.rbCategory.TabStop = true;
            this.rbCategory.UseVisualStyleBackColor = true;
            this.rbCategory.CheckedChanged += new System.EventHandler(this.rbShowType_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbTotals);
            this.panel1.Controls.Add(this.rbSubCategory);
            this.panel1.Controls.Add(this.rbCategory);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // rbEfficiency
            // 
            resources.ApplyResources(this.rbEfficiency, "rbEfficiency");
            this.rbEfficiency.Name = "rbEfficiency";
            this.rbEfficiency.UseVisualStyleBackColor = true;
            this.rbEfficiency.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbValue
            // 
            resources.ApplyResources(this.rbValue, "rbValue");
            this.rbValue.Name = "rbValue";
            this.rbValue.UseVisualStyleBackColor = true;
            this.rbValue.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // rbRate
            // 
            resources.ApplyResources(this.rbRate, "rbRate");
            this.rbRate.Checked = true;
            this.rbRate.Name = "rbRate";
            this.rbRate.TabStop = true;
            this.rbRate.UseVisualStyleBackColor = true;
            this.rbRate.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
            // 
            // pnChartContainer
            // 
            resources.ApplyResources(this.pnChartContainer, "pnChartContainer");
            this.pnChartContainer.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnChartContainer.Name = "pnChartContainer";
            // 
            // pnlType
            // 
            this.pnlType.Controls.Add(this.rbEfficiency);
            this.pnlType.Controls.Add(this.rbValue);
            this.pnlType.Controls.Add(this.rbRate);
            resources.ApplyResources(this.pnlType, "pnlType");
            this.pnlType.Name = "pnlType";
            // 
            // lblShowBy
            // 
            resources.ApplyResources(this.lblShowBy, "lblShowBy");
            this.lblShowBy.Name = "lblShowBy";
            // 
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // chkPredictMissingData
            // 
            resources.ApplyResources(this.chkPredictMissingData, "chkPredictMissingData");
            this.chkPredictMissingData.Checked = true;
            this.chkPredictMissingData.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPredictMissingData.Name = "chkPredictMissingData";
            this.chkPredictMissingData.UseVisualStyleBackColor = true;
            // 
            // cmdExport
            // 
            resources.ApplyResources(this.cmdExport, "cmdExport");
            this.cmdExport.Image = global::WinFormsUI.Properties.Resources.upload_16x16;
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // cmdReset
            // 
            resources.ApplyResources(this.cmdReset, "cmdReset");
            this.cmdReset.Name = "cmdReset";
            this.cmdReset.UseVisualStyleBackColor = true;
            this.cmdReset.Click += new System.EventHandler(this.cmdReset_Click);
            // 
            // cboNumbers2
            // 
            this.cboNumbers2.FormattingEnabled = true;
            this.cboNumbers2.Items.AddRange(new object[] {
            resources.GetString("cboNumbers2.Items"),
            resources.GetString("cboNumbers2.Items1"),
            resources.GetString("cboNumbers2.Items2"),
            resources.GetString("cboNumbers2.Items3")});
            resources.ApplyResources(this.cboNumbers2, "cboNumbers2");
            this.cboNumbers2.Name = "cboNumbers2";
            this.cboNumbers2.SelectedIndexChanged += new System.EventHandler(this.cboNumbers2_SelectedIndexChanged);
            // 
            // lblNumber2
            // 
            resources.ApplyResources(this.lblNumber2, "lblNumber2");
            this.lblNumber2.Name = "lblNumber2";
            // 
            // cboEndYear
            // 
            this.cboEndYear.FormattingEnabled = true;
            this.cboEndYear.Items.AddRange(new object[] {
            resources.GetString("cboEndYear.Items"),
            resources.GetString("cboEndYear.Items1"),
            resources.GetString("cboEndYear.Items2"),
            resources.GetString("cboEndYear.Items3")});
            resources.ApplyResources(this.cboEndYear, "cboEndYear");
            this.cboEndYear.Name = "cboEndYear";
            this.cboEndYear.SelectedIndexChanged += new System.EventHandler(this.cboEndYear_SelectedIndexChanged);
            // 
            // lblUntil
            // 
            resources.ApplyResources(this.lblUntil, "lblUntil");
            this.lblUntil.Name = "lblUntil";
            // 
            // cboNumbers
            // 
            this.cboNumbers.FormattingEnabled = true;
            this.cboNumbers.Items.AddRange(new object[] {
            resources.GetString("cboNumbers.Items"),
            resources.GetString("cboNumbers.Items1"),
            resources.GetString("cboNumbers.Items2"),
            resources.GetString("cboNumbers.Items3")});
            resources.ApplyResources(this.cboNumbers, "cboNumbers");
            this.cboNumbers.Name = "cboNumbers";
            this.cboNumbers.SelectedIndexChanged += new System.EventHandler(this.cboNumbers_SelectedValueChanged);
            // 
            // LblNumber
            // 
            resources.ApplyResources(this.LblNumber, "LblNumber");
            this.LblNumber.Name = "LblNumber";
            // 
            // cboStartYear
            // 
            this.cboStartYear.FormattingEnabled = true;
            this.cboStartYear.Items.AddRange(new object[] {
            resources.GetString("cboStartYear.Items"),
            resources.GetString("cboStartYear.Items1"),
            resources.GetString("cboStartYear.Items2"),
            resources.GetString("cboStartYear.Items3")});
            resources.ApplyResources(this.cboStartYear, "cboStartYear");
            this.cboStartYear.Name = "cboStartYear";
            this.cboStartYear.SelectedIndexChanged += new System.EventHandler(this.cboStartYear_SelectedIndexChanged);
            // 
            // LblRange
            // 
            resources.ApplyResources(this.LblRange, "LblRange");
            this.LblRange.Name = "LblRange";
            // 
            // CboPeriodType
            // 
            this.CboPeriodType.DataSource = this.bsPeriodType;
            this.CboPeriodType.DisplayMember = "Description";
            this.CboPeriodType.FormattingEnabled = true;
            resources.ApplyResources(this.CboPeriodType, "CboPeriodType");
            this.CboPeriodType.Name = "CboPeriodType";
            this.CboPeriodType.ValueMember = "Key";
            this.CboPeriodType.SelectedIndexChanged += new System.EventHandler(this.cboPeriodType_SelectedIndexChanged);
            // 
            // LblPeriod
            // 
            resources.ApplyResources(this.LblPeriod, "LblPeriod");
            this.LblPeriod.Name = "LblPeriod";
            // 
            // bsPeriodType
            // 
            this.bsPeriodType.DataSource = typeof(EnergyUse.Models.Common.SelectionItem);
            // 
            // ucChartCompareLiveCharts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chkShowStacked);
            this.Controls.Add(this.lblNetto);
            this.Controls.Add(this.lblProduction);
            this.Controls.Add(this.lblConsumption);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnChartContainer);
            this.Controls.Add(this.pnlType);
            this.Controls.Add(this.lblShowBy);
            this.Controls.Add(this.lblType);
            this.Controls.Add(this.chkPredictMissingData);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.cboNumbers2);
            this.Controls.Add(this.lblNumber2);
            this.Controls.Add(this.cboEndYear);
            this.Controls.Add(this.lblUntil);
            this.Controls.Add(this.cboNumbers);
            this.Controls.Add(this.LblNumber);
            this.Controls.Add(this.cboStartYear);
            this.Controls.Add(this.LblRange);
            this.Controls.Add(this.CboPeriodType);
            this.Controls.Add(this.LblPeriod);
            this.Name = "ucChartCompareLiveCharts";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlType.ResumeLayout(false);
            this.pnlType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsPeriodType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private CheckBox chkShowStacked;
        private Label lblNetto;
        private Label lblProduction;
        private Label lblConsumption;
        private RadioButton rbTotals;
        private RadioButton rbSubCategory;
        private RadioButton rbCategory;
        private Panel panel1;
        private RadioButton rbEfficiency;
        private RadioButton rbValue;
        private RadioButton rbRate;
        private Panel pnChartContainer;
        private Panel pnlType;
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
