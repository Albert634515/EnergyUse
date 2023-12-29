namespace WinFormsEF.Views
{
    partial class frmEditMeterReading
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditMeterReading));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblReturnDeliveryDeltaLow = new System.Windows.Forms.Label();
            this.txtReturnDeliveryDeltaLow = new System.Windows.Forms.TextBox();
            this.bsMeterReading = new System.Windows.Forms.BindingSource(this.components);
            this.lblReturnDeliveryDeltaNormal = new System.Windows.Forms.Label();
            this.txtReturnDeliveryDeltaNormal = new System.Windows.Forms.TextBox();
            this.lblDeltaLow = new System.Windows.Forms.Label();
            this.txtDeltaLow = new System.Windows.Forms.TextBox();
            this.lblDeltaNormal = new System.Windows.Forms.Label();
            this.txtDeltaNormal = new System.Windows.Forms.TextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.label2 = new System.Windows.Forms.Label();
            this.cboMeters = new System.Windows.Forms.ComboBox();
            this.bsMeters = new System.Windows.Forms.BindingSource(this.components);
            this.cmdSave = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.lblReturnDeliveryLow = new System.Windows.Forms.Label();
            this.txtReturnDeliveryLow = new System.Windows.Forms.TextBox();
            this.lblReturnDeliveryNormal = new System.Windows.Forms.Label();
            this.txtReturnDeliveryNormal = new System.Windows.Forms.TextBox();
            this.lblRateLow = new System.Windows.Forms.Label();
            this.txtRateLow = new System.Windows.Forms.TextBox();
            this.lblRateNormal = new System.Windows.Forms.Label();
            this.txtRateNormal = new System.Windows.Forms.TextBox();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblRange = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.bsMeterReading)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMeters)).BeginInit();
            this.SuspendLayout();
            // 
            // lblReturnDeliveryDeltaLow
            // 
            resources.ApplyResources(this.lblReturnDeliveryDeltaLow, "lblReturnDeliveryDeltaLow");
            this.lblReturnDeliveryDeltaLow.Name = "lblReturnDeliveryDeltaLow";
            this.toolTip1.SetToolTip(this.lblReturnDeliveryDeltaLow, resources.GetString("lblReturnDeliveryDeltaLow.ToolTip"));
            // 
            // txtReturnDeliveryDeltaLow
            // 
            resources.ApplyResources(this.txtReturnDeliveryDeltaLow, "txtReturnDeliveryDeltaLow");
            this.txtReturnDeliveryDeltaLow.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "ReturnDeliveryDeltaLow", true));
            this.txtReturnDeliveryDeltaLow.Name = "txtReturnDeliveryDeltaLow";
            this.txtReturnDeliveryDeltaLow.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtReturnDeliveryDeltaLow, resources.GetString("txtReturnDeliveryDeltaLow.ToolTip"));
            // 
            // bsMeterReading
            // 
            this.bsMeterReading.DataSource = typeof(EnergyUse.Models.MeterReading);
            // 
            // lblReturnDeliveryDeltaNormal
            // 
            resources.ApplyResources(this.lblReturnDeliveryDeltaNormal, "lblReturnDeliveryDeltaNormal");
            this.lblReturnDeliveryDeltaNormal.Name = "lblReturnDeliveryDeltaNormal";
            this.toolTip1.SetToolTip(this.lblReturnDeliveryDeltaNormal, resources.GetString("lblReturnDeliveryDeltaNormal.ToolTip"));
            // 
            // txtReturnDeliveryDeltaNormal
            // 
            resources.ApplyResources(this.txtReturnDeliveryDeltaNormal, "txtReturnDeliveryDeltaNormal");
            this.txtReturnDeliveryDeltaNormal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "ReturnDeliveryDeltaNormal", true));
            this.txtReturnDeliveryDeltaNormal.Name = "txtReturnDeliveryDeltaNormal";
            this.txtReturnDeliveryDeltaNormal.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtReturnDeliveryDeltaNormal, resources.GetString("txtReturnDeliveryDeltaNormal.ToolTip"));
            // 
            // lblDeltaLow
            // 
            resources.ApplyResources(this.lblDeltaLow, "lblDeltaLow");
            this.lblDeltaLow.Name = "lblDeltaLow";
            this.toolTip1.SetToolTip(this.lblDeltaLow, resources.GetString("lblDeltaLow.ToolTip"));
            // 
            // txtDeltaLow
            // 
            resources.ApplyResources(this.txtDeltaLow, "txtDeltaLow");
            this.txtDeltaLow.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "DeltaLow", true));
            this.txtDeltaLow.Name = "txtDeltaLow";
            this.txtDeltaLow.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtDeltaLow, resources.GetString("txtDeltaLow.ToolTip"));
            // 
            // lblDeltaNormal
            // 
            resources.ApplyResources(this.lblDeltaNormal, "lblDeltaNormal");
            this.lblDeltaNormal.Name = "lblDeltaNormal";
            this.toolTip1.SetToolTip(this.lblDeltaNormal, resources.GetString("lblDeltaNormal.ToolTip"));
            // 
            // txtDeltaNormal
            // 
            resources.ApplyResources(this.txtDeltaNormal, "txtDeltaNormal");
            this.txtDeltaNormal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "DeltaNormal", true));
            this.txtDeltaNormal.Name = "txtDeltaNormal";
            this.txtDeltaNormal.ReadOnly = true;
            this.toolTip1.SetToolTip(this.txtDeltaNormal, resources.GetString("txtDeltaNormal.ToolTip"));
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Name = "statusStrip1";
            this.toolTip1.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            this.toolTip1.SetToolTip(this.label2, resources.GetString("label2.ToolTip"));
            // 
            // cboMeters
            // 
            resources.ApplyResources(this.cboMeters, "cboMeters");
            this.cboMeters.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsMeterReading, "MeterId", true));
            this.cboMeters.DataSource = this.bsMeters;
            this.cboMeters.DisplayMember = "Description";
            this.cboMeters.FormattingEnabled = true;
            this.cboMeters.Name = "cboMeters";
            this.toolTip1.SetToolTip(this.cboMeters, resources.GetString("cboMeters.ToolTip"));
            this.cboMeters.ValueMember = "Id";
            // 
            // bsMeters
            // 
            this.bsMeters.DataSource = typeof(EnergyUse.Models.Meter);
            // 
            // cmdSave
            // 
            resources.ApplyResources(this.cmdSave, "cmdSave");
            this.cmdSave.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            this.cmdSave.Name = "cmdSave";
            this.toolTip1.SetToolTip(this.cmdSave, resources.GetString("cmdSave.ToolTip"));
            this.cmdSave.UseVisualStyleBackColor = true;
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            // 
            // cmdClose
            // 
            resources.ApplyResources(this.cmdClose, "cmdClose");
            this.cmdClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.cmdClose.Name = "cmdClose";
            this.toolTip1.SetToolTip(this.cmdClose, resources.GetString("cmdClose.ToolTip"));
            this.cmdClose.UseVisualStyleBackColor = true;
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // lblReturnDeliveryLow
            // 
            resources.ApplyResources(this.lblReturnDeliveryLow, "lblReturnDeliveryLow");
            this.lblReturnDeliveryLow.Name = "lblReturnDeliveryLow";
            this.toolTip1.SetToolTip(this.lblReturnDeliveryLow, resources.GetString("lblReturnDeliveryLow.ToolTip"));
            // 
            // txtReturnDeliveryLow
            // 
            resources.ApplyResources(this.txtReturnDeliveryLow, "txtReturnDeliveryLow");
            this.txtReturnDeliveryLow.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "ReturnDeliveryLow", true));
            this.txtReturnDeliveryLow.Name = "txtReturnDeliveryLow";
            this.toolTip1.SetToolTip(this.txtReturnDeliveryLow, resources.GetString("txtReturnDeliveryLow.ToolTip"));
            // 
            // lblReturnDeliveryNormal
            // 
            resources.ApplyResources(this.lblReturnDeliveryNormal, "lblReturnDeliveryNormal");
            this.lblReturnDeliveryNormal.Name = "lblReturnDeliveryNormal";
            this.toolTip1.SetToolTip(this.lblReturnDeliveryNormal, resources.GetString("lblReturnDeliveryNormal.ToolTip"));
            // 
            // txtReturnDeliveryNormal
            // 
            resources.ApplyResources(this.txtReturnDeliveryNormal, "txtReturnDeliveryNormal");
            this.txtReturnDeliveryNormal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "ReturnDeliveryNormal", true));
            this.txtReturnDeliveryNormal.Name = "txtReturnDeliveryNormal";
            this.toolTip1.SetToolTip(this.txtReturnDeliveryNormal, resources.GetString("txtReturnDeliveryNormal.ToolTip"));
            // 
            // lblRateLow
            // 
            resources.ApplyResources(this.lblRateLow, "lblRateLow");
            this.lblRateLow.Name = "lblRateLow";
            this.toolTip1.SetToolTip(this.lblRateLow, resources.GetString("lblRateLow.ToolTip"));
            // 
            // txtRateLow
            // 
            resources.ApplyResources(this.txtRateLow, "txtRateLow");
            this.txtRateLow.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "RateLow", true));
            this.txtRateLow.Name = "txtRateLow";
            this.toolTip1.SetToolTip(this.txtRateLow, resources.GetString("txtRateLow.ToolTip"));
            // 
            // lblRateNormal
            // 
            resources.ApplyResources(this.lblRateNormal, "lblRateNormal");
            this.lblRateNormal.Name = "lblRateNormal";
            this.toolTip1.SetToolTip(this.lblRateNormal, resources.GetString("lblRateNormal.ToolTip"));
            // 
            // txtRateNormal
            // 
            resources.ApplyResources(this.txtRateNormal, "txtRateNormal");
            this.txtRateNormal.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsMeterReading, "RateNormal", true));
            this.txtRateNormal.Name = "txtRateNormal";
            this.toolTip1.SetToolTip(this.txtRateNormal, resources.GetString("txtRateNormal.ToolTip"));
            // 
            // dtpFrom
            // 
            resources.ApplyResources(this.dtpFrom, "dtpFrom");
            this.dtpFrom.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsMeterReading, "RegistrationDate", true));
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpFrom.Name = "dtpFrom";
            this.toolTip1.SetToolTip(this.dtpFrom, resources.GetString("dtpFrom.ToolTip"));
            // 
            // lblRange
            // 
            resources.ApplyResources(this.lblRange, "lblRange");
            this.lblRange.Name = "lblRange";
            this.toolTip1.SetToolTip(this.lblRange, resources.GetString("lblRange.ToolTip"));
            // 
            // frmEditMeterReading
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblReturnDeliveryDeltaLow);
            this.Controls.Add(this.txtReturnDeliveryDeltaLow);
            this.Controls.Add(this.lblReturnDeliveryDeltaNormal);
            this.Controls.Add(this.txtReturnDeliveryDeltaNormal);
            this.Controls.Add(this.lblDeltaLow);
            this.Controls.Add(this.txtDeltaLow);
            this.Controls.Add(this.lblDeltaNormal);
            this.Controls.Add(this.txtDeltaNormal);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboMeters);
            this.Controls.Add(this.cmdSave);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.lblReturnDeliveryLow);
            this.Controls.Add(this.txtReturnDeliveryLow);
            this.Controls.Add(this.lblReturnDeliveryNormal);
            this.Controls.Add(this.txtReturnDeliveryNormal);
            this.Controls.Add(this.lblRateLow);
            this.Controls.Add(this.txtRateLow);
            this.Controls.Add(this.lblRateNormal);
            this.Controls.Add(this.txtRateNormal);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmEditMeterReading";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEditMeterReading_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bsMeterReading)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsMeters)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolTip toolTip1;
        private StatusStrip statusStrip1;
        private BindingSource bsMeterReading;
        public BindingSource bsMeters;
        private Label lblReturnDeliveryDeltaLow;
        private TextBox txtReturnDeliveryDeltaLow;
        private Label lblReturnDeliveryDeltaNormal;
        private TextBox txtReturnDeliveryDeltaNormal;
        private Label lblDeltaLow;
        private TextBox txtDeltaLow;
        private Label lblDeltaNormal;
        private TextBox txtDeltaNormal;
        private Label label2;
        private ComboBox cboMeters;
        private Button cmdSave;
        private Button cmdClose;
        private Label lblReturnDeliveryLow;
        private TextBox txtReturnDeliveryLow;
        private Label lblReturnDeliveryNormal;
        private TextBox txtReturnDeliveryNormal;
        private Label lblRateLow;
        private TextBox txtRateLow;
        private Label lblRateNormal;
        private TextBox txtRateNormal;
        private DateTimePicker dtpFrom;
        private Label lblRange;
    }
}