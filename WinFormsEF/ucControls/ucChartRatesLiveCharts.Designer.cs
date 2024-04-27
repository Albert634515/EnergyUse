namespace WinFormsEF.ucControls
{
    partial class ucChartRatesLiveCharts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucChartRatesLiveCharts));
            panel1 = new Panel();
            pnlType = new Panel();
            rbUnit = new RadioButton();
            rbRate = new RadioButton();
            dtpTill = new DateTimePicker();
            dtpFrom = new DateTimePicker();
            lblRange = new Label();
            lblCostCategory = new Label();
            chkListCostCategory = new CheckedListBox();
            cmdExport = new Button();
            cmdReset = new Button();
            lblType = new Label();
            toolTip1 = new ToolTip(components);
            pnlType.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // pnlType
            // 
            pnlType.Controls.Add(rbUnit);
            pnlType.Controls.Add(rbRate);
            resources.ApplyResources(pnlType, "pnlType");
            pnlType.Name = "pnlType";
            pnlType.Tag = "ChartRatesPeriodType";
            // 
            // rbUnit
            // 
            resources.ApplyResources(rbUnit, "rbUnit");
            rbUnit.Name = "rbUnit";
            rbUnit.UseVisualStyleBackColor = true;
            rbUnit.CheckedChanged += rbType_CheckedChanged;
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
            // dtpTill
            // 
            dtpTill.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpTill, "dtpTill");
            dtpTill.Name = "dtpTill";
            dtpTill.Tag = "ChartRatesPeriodEnd";
            dtpTill.ValueChanged += dtpTill_ValueChanged;
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpFrom, "dtpFrom");
            dtpFrom.Name = "dtpFrom";
            dtpFrom.Tag = "ChartRatesPeriodStart";
            dtpFrom.ValueChanged += dtpFrom_ValueChanged;
            // 
            // lblRange
            // 
            resources.ApplyResources(lblRange, "lblRange");
            lblRange.Name = "lblRange";
            // 
            // lblCostCategory
            // 
            resources.ApplyResources(lblCostCategory, "lblCostCategory");
            lblCostCategory.Name = "lblCostCategory";
            // 
            // chkListCostCategory
            // 
            chkListCostCategory.CheckOnClick = true;
            chkListCostCategory.FormattingEnabled = true;
            resources.ApplyResources(chkListCostCategory, "chkListCostCategory");
            chkListCostCategory.Name = "chkListCostCategory";
            chkListCostCategory.Tag = "ChartRatesPeriodCategories";
            chkListCostCategory.SelectedIndexChanged += chkListCostCategory_SelectedIndexChanged;
            // 
            // cmdExport
            // 
            cmdExport.Image = WinFormsUI.Properties.Resources.upload_16x16;
            resources.ApplyResources(cmdExport, "cmdExport");
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
            // lblType
            // 
            resources.ApplyResources(lblType, "lblType");
            lblType.Name = "lblType";
            // 
            // ucChartRatesLiveCharts
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(pnlType);
            Controls.Add(dtpTill);
            Controls.Add(dtpFrom);
            Controls.Add(lblRange);
            Controls.Add(lblCostCategory);
            Controls.Add(chkListCostCategory);
            Controls.Add(cmdExport);
            Controls.Add(cmdReset);
            Controls.Add(lblType);
            Name = "ucChartRatesLiveCharts";
            pnlType.ResumeLayout(false);
            pnlType.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private Panel pnlType;
        private RadioButton rbUnit;
        private RadioButton rbRate;
        private DateTimePicker dtpTill;
        private DateTimePicker dtpFrom;
        private Label lblRange;
        private Label lblCostCategory;
        private CheckedListBox chkListCostCategory;
        private Button cmdExport;
        private Button cmdReset;
        private Label lblType;
        private ToolTip toolTip1;
    }
}
