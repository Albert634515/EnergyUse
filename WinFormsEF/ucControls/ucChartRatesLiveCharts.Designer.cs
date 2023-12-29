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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucChartRatesLiveCharts));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pnlType = new System.Windows.Forms.Panel();
            this.rbUnit = new System.Windows.Forms.RadioButton();
            this.rbRate = new System.Windows.Forms.RadioButton();
            this.dtpTill = new System.Windows.Forms.DateTimePicker();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblRange = new System.Windows.Forms.Label();
            this.lblCostCategory = new System.Windows.Forms.Label();
            this.chkListCostCategory = new System.Windows.Forms.CheckedListBox();
            this.cmdExport = new System.Windows.Forms.Button();
            this.cmdReset = new System.Windows.Forms.Button();
            this.lblType = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlType.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // pnlType
            // 
            this.pnlType.Controls.Add(this.rbUnit);
            this.pnlType.Controls.Add(this.rbRate);
            resources.ApplyResources(this.pnlType, "pnlType");
            this.pnlType.Name = "pnlType";
            // 
            // rbUnit
            // 
            resources.ApplyResources(this.rbUnit, "rbUnit");
            this.rbUnit.Name = "rbUnit";
            this.rbUnit.UseVisualStyleBackColor = true;
            this.rbUnit.CheckedChanged += new System.EventHandler(this.rbType_CheckedChanged);
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
            // dtpTill
            // 
            this.dtpTill.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpTill, "dtpTill");
            this.dtpTill.Name = "dtpTill";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpFrom, "dtpFrom");
            this.dtpFrom.Name = "dtpFrom";
            // 
            // lblRange
            // 
            resources.ApplyResources(this.lblRange, "lblRange");
            this.lblRange.Name = "lblRange";
            // 
            // lblCostCategory
            // 
            resources.ApplyResources(this.lblCostCategory, "lblCostCategory");
            this.lblCostCategory.Name = "lblCostCategory";
            // 
            // chkListCostCategory
            // 
            this.chkListCostCategory.CheckOnClick = true;
            this.chkListCostCategory.FormattingEnabled = true;
            resources.ApplyResources(this.chkListCostCategory, "chkListCostCategory");
            this.chkListCostCategory.Name = "chkListCostCategory";
            this.chkListCostCategory.SelectedIndexChanged += new System.EventHandler(this.cboPeriodType_SelectedIndexChanged);
            // 
            // cmdExport
            // 
            this.cmdExport.Image = global::WinFormsUI.Properties.Resources.upload_16x16;
            resources.ApplyResources(this.cmdExport, "cmdExport");
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
            // lblType
            // 
            resources.ApplyResources(this.lblType, "lblType");
            this.lblType.Name = "lblType";
            // 
            // ucChartRatesLiveCharts
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlType);
            this.Controls.Add(this.dtpTill);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.lblCostCategory);
            this.Controls.Add(this.chkListCostCategory);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.cmdReset);
            this.Controls.Add(this.lblType);
            this.Name = "ucChartRatesLiveCharts";
            this.pnlType.ResumeLayout(false);
            this.pnlType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
