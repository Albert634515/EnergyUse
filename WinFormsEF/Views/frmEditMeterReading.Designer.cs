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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEditMeterReading));
            toolTip1 = new ToolTip(components);
            lblReturnDeliveryDeltaLow = new Label();
            txtReturnDeliveryDeltaLow = new TextBox();
            bsMeterReading = new BindingSource(components);
            lblReturnDeliveryDeltaNormal = new Label();
            txtReturnDeliveryDeltaNormal = new TextBox();
            lblDeltaLow = new Label();
            txtDeltaLow = new TextBox();
            lblDeltaNormal = new Label();
            txtDeltaNormal = new TextBox();
            statusStrip1 = new StatusStrip();
            label2 = new Label();
            cboMeters = new ComboBox();
            bsMeters = new BindingSource(components);
            cmdSave = new Button();
            cmdClose = new Button();
            lblReturnDeliveryLow = new Label();
            txtReturnDeliveryLow = new TextBox();
            lblReturnDeliveryNormal = new Label();
            txtReturnDeliveryNormal = new TextBox();
            lblRateLow = new Label();
            txtRateLow = new TextBox();
            lblRateNormal = new Label();
            txtRateNormal = new TextBox();
            dtpFrom = new DateTimePicker();
            lblRange = new Label();
            ((System.ComponentModel.ISupportInitialize)bsMeterReading).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsMeters).BeginInit();
            SuspendLayout();
            // 
            // lblReturnDeliveryDeltaLow
            // 
            resources.ApplyResources(lblReturnDeliveryDeltaLow, "lblReturnDeliveryDeltaLow");
            lblReturnDeliveryDeltaLow.Name = "lblReturnDeliveryDeltaLow";
            toolTip1.SetToolTip(lblReturnDeliveryDeltaLow, resources.GetString("lblReturnDeliveryDeltaLow.ToolTip"));
            // 
            // txtReturnDeliveryDeltaLow
            // 
            txtReturnDeliveryDeltaLow.DataBindings.Add(new Binding("Text", bsMeterReading, "ReturnDeliveryDeltaLow", true));
            resources.ApplyResources(txtReturnDeliveryDeltaLow, "txtReturnDeliveryDeltaLow");
            txtReturnDeliveryDeltaLow.Name = "txtReturnDeliveryDeltaLow";
            txtReturnDeliveryDeltaLow.ReadOnly = true;
            toolTip1.SetToolTip(txtReturnDeliveryDeltaLow, resources.GetString("txtReturnDeliveryDeltaLow.ToolTip"));
            // 
            // bsMeterReading
            // 
            bsMeterReading.DataSource = typeof(EnergyUse.Models.MeterReading);
            // 
            // lblReturnDeliveryDeltaNormal
            // 
            resources.ApplyResources(lblReturnDeliveryDeltaNormal, "lblReturnDeliveryDeltaNormal");
            lblReturnDeliveryDeltaNormal.Name = "lblReturnDeliveryDeltaNormal";
            toolTip1.SetToolTip(lblReturnDeliveryDeltaNormal, resources.GetString("lblReturnDeliveryDeltaNormal.ToolTip"));
            // 
            // txtReturnDeliveryDeltaNormal
            // 
            txtReturnDeliveryDeltaNormal.DataBindings.Add(new Binding("Text", bsMeterReading, "ReturnDeliveryDeltaNormal", true));
            resources.ApplyResources(txtReturnDeliveryDeltaNormal, "txtReturnDeliveryDeltaNormal");
            txtReturnDeliveryDeltaNormal.Name = "txtReturnDeliveryDeltaNormal";
            txtReturnDeliveryDeltaNormal.ReadOnly = true;
            toolTip1.SetToolTip(txtReturnDeliveryDeltaNormal, resources.GetString("txtReturnDeliveryDeltaNormal.ToolTip"));
            // 
            // lblDeltaLow
            // 
            resources.ApplyResources(lblDeltaLow, "lblDeltaLow");
            lblDeltaLow.Name = "lblDeltaLow";
            toolTip1.SetToolTip(lblDeltaLow, resources.GetString("lblDeltaLow.ToolTip"));
            // 
            // txtDeltaLow
            // 
            txtDeltaLow.DataBindings.Add(new Binding("Text", bsMeterReading, "DeltaLow", true));
            resources.ApplyResources(txtDeltaLow, "txtDeltaLow");
            txtDeltaLow.Name = "txtDeltaLow";
            txtDeltaLow.ReadOnly = true;
            toolTip1.SetToolTip(txtDeltaLow, resources.GetString("txtDeltaLow.ToolTip"));
            // 
            // lblDeltaNormal
            // 
            resources.ApplyResources(lblDeltaNormal, "lblDeltaNormal");
            lblDeltaNormal.Name = "lblDeltaNormal";
            toolTip1.SetToolTip(lblDeltaNormal, resources.GetString("lblDeltaNormal.ToolTip"));
            // 
            // txtDeltaNormal
            // 
            txtDeltaNormal.DataBindings.Add(new Binding("Text", bsMeterReading, "DeltaNormal", true));
            resources.ApplyResources(txtDeltaNormal, "txtDeltaNormal");
            txtDeltaNormal.Name = "txtDeltaNormal";
            txtDeltaNormal.ReadOnly = true;
            toolTip1.SetToolTip(txtDeltaNormal, resources.GetString("txtDeltaNormal.ToolTip"));
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // cboMeters
            // 
            cboMeters.DataBindings.Add(new Binding("SelectedValue", bsMeterReading, "MeterId", true));
            cboMeters.DataSource = bsMeters;
            cboMeters.DisplayMember = "Description";
            cboMeters.FormattingEnabled = true;
            resources.ApplyResources(cboMeters, "cboMeters");
            cboMeters.Name = "cboMeters";
            cboMeters.ValueMember = "Id";
            // 
            // bsMeters
            // 
            bsMeters.DataSource = typeof(EnergyUse.Models.Meter);
            // 
            // cmdSave
            // 
            resources.ApplyResources(cmdSave, "cmdSave");
            cmdSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            cmdSave.Name = "cmdSave";
            cmdSave.UseVisualStyleBackColor = true;
            cmdSave.Click += cmdSave_Click;
            // 
            // cmdClose
            // 
            resources.ApplyResources(cmdClose, "cmdClose");
            cmdClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            cmdClose.Name = "cmdClose";
            cmdClose.UseVisualStyleBackColor = true;
            cmdClose.Click += cmdClose_Click;
            // 
            // lblReturnDeliveryLow
            // 
            resources.ApplyResources(lblReturnDeliveryLow, "lblReturnDeliveryLow");
            lblReturnDeliveryLow.Name = "lblReturnDeliveryLow";
            // 
            // txtReturnDeliveryLow
            // 
            txtReturnDeliveryLow.DataBindings.Add(new Binding("Text", bsMeterReading, "ReturnDeliveryLow", true));
            resources.ApplyResources(txtReturnDeliveryLow, "txtReturnDeliveryLow");
            txtReturnDeliveryLow.Name = "txtReturnDeliveryLow";
            // 
            // lblReturnDeliveryNormal
            // 
            resources.ApplyResources(lblReturnDeliveryNormal, "lblReturnDeliveryNormal");
            lblReturnDeliveryNormal.Name = "lblReturnDeliveryNormal";
            // 
            // txtReturnDeliveryNormal
            // 
            txtReturnDeliveryNormal.DataBindings.Add(new Binding("Text", bsMeterReading, "ReturnDeliveryNormal", true));
            resources.ApplyResources(txtReturnDeliveryNormal, "txtReturnDeliveryNormal");
            txtReturnDeliveryNormal.Name = "txtReturnDeliveryNormal";
            // 
            // lblRateLow
            // 
            resources.ApplyResources(lblRateLow, "lblRateLow");
            lblRateLow.Name = "lblRateLow";
            // 
            // txtRateLow
            // 
            txtRateLow.DataBindings.Add(new Binding("Text", bsMeterReading, "RateLow", true));
            resources.ApplyResources(txtRateLow, "txtRateLow");
            txtRateLow.Name = "txtRateLow";
            // 
            // lblRateNormal
            // 
            resources.ApplyResources(lblRateNormal, "lblRateNormal");
            lblRateNormal.Name = "lblRateNormal";
            // 
            // txtRateNormal
            // 
            txtRateNormal.DataBindings.Add(new Binding("Text", bsMeterReading, "RateNormal", true));
            resources.ApplyResources(txtRateNormal, "txtRateNormal");
            txtRateNormal.Name = "txtRateNormal";
            // 
            // dtpFrom
            // 
            dtpFrom.DataBindings.Add(new Binding("Value", bsMeterReading, "RegistrationDate", true));
            dtpFrom.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpFrom, "dtpFrom");
            dtpFrom.Name = "dtpFrom";
            // 
            // lblRange
            // 
            resources.ApplyResources(lblRange, "lblRange");
            lblRange.Name = "lblRange";
            // 
            // frmEditMeterReading
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblReturnDeliveryDeltaLow);
            Controls.Add(txtReturnDeliveryDeltaLow);
            Controls.Add(lblReturnDeliveryDeltaNormal);
            Controls.Add(txtReturnDeliveryDeltaNormal);
            Controls.Add(lblDeltaLow);
            Controls.Add(txtDeltaLow);
            Controls.Add(lblDeltaNormal);
            Controls.Add(txtDeltaNormal);
            Controls.Add(label2);
            Controls.Add(cboMeters);
            Controls.Add(cmdSave);
            Controls.Add(cmdClose);
            Controls.Add(lblReturnDeliveryLow);
            Controls.Add(txtReturnDeliveryLow);
            Controls.Add(lblReturnDeliveryNormal);
            Controls.Add(txtReturnDeliveryNormal);
            Controls.Add(lblRateLow);
            Controls.Add(txtRateLow);
            Controls.Add(lblRateNormal);
            Controls.Add(txtRateNormal);
            Controls.Add(dtpFrom);
            Controls.Add(lblRange);
            Controls.Add(statusStrip1);
            Name = "frmEditMeterReading";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmEditMeterReading_FormClosing;
            ((System.ComponentModel.ISupportInitialize)bsMeterReading).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsMeters).EndInit();
            ResumeLayout(false);
            PerformLayout();
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