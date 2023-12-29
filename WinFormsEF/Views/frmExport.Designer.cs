namespace WinFormsEF.Views
{
    partial class frmExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExport));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCloseExport = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblAddress = new System.Windows.Forms.Label();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.rbExportSelectedRange = new System.Windows.Forms.RadioButton();
            this.rbExportAll = new System.Windows.Forms.RadioButton();
            this.dtpTill = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.lblRange = new System.Windows.Forms.Label();
            this.cmdExport = new System.Windows.Forms.Button();
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.cmdSelectExportFile = new System.Windows.Forms.Button();
            this.txtExportFile = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.bsAddresses = new System.Windows.Forms.BindingSource(this.components);
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseExport});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbCloseExport
            // 
            this.tsbCloseExport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCloseExport.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbCloseExport, "tsbCloseExport");
            this.tsbCloseExport.Name = "tsbCloseExport";
            this.tsbCloseExport.Click += new System.EventHandler(this.tsbCloseExport_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // cmbAddress
            // 
            this.cmbAddress.DisplayMember = "Description";
            this.cmbAddress.FormattingEnabled = true;
            resources.ApplyResources(this.cmbAddress, "cmbAddress");
            this.cmbAddress.Name = "cmbAddress";
            this.cmbAddress.ValueMember = "Id";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.rbExportSelectedRange);
            this.panel1.Controls.Add(this.rbExportAll);
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            // 
            // rbExportSelectedRange
            // 
            resources.ApplyResources(this.rbExportSelectedRange, "rbExportSelectedRange");
            this.rbExportSelectedRange.Name = "rbExportSelectedRange";
            this.rbExportSelectedRange.UseVisualStyleBackColor = true;
            // 
            // rbExportAll
            // 
            resources.ApplyResources(this.rbExportAll, "rbExportAll");
            this.rbExportAll.Checked = true;
            this.rbExportAll.Name = "rbExportAll";
            this.rbExportAll.TabStop = true;
            this.rbExportAll.UseVisualStyleBackColor = true;
            // 
            // dtpTill
            // 
            this.dtpTill.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpTill, "dtpTill");
            this.dtpTill.Name = "dtpTill";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
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
            // cmdExport
            // 
            resources.ApplyResources(this.cmdExport, "cmdExport");
            this.cmdExport.Image = global::WinFormsUI.Properties.Resources.upload_24x24;
            this.cmdExport.Name = "cmdExport";
            this.cmdExport.UseVisualStyleBackColor = true;
            this.cmdExport.Click += new System.EventHandler(this.cmdExport_Click);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            // 
            // cboEnergyType
            // 
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.Name = "cboEnergyType";
            this.cboEnergyType.ValueMember = "Id";
            // 
            // cmdSelectExportFile
            // 
            this.cmdSelectExportFile.Image = global::WinFormsUI.Properties.Resources.open_file_icon;
            resources.ApplyResources(this.cmdSelectExportFile, "cmdSelectExportFile");
            this.cmdSelectExportFile.Name = "cmdSelectExportFile";
            this.cmdSelectExportFile.UseVisualStyleBackColor = true;
            this.cmdSelectExportFile.Click += new System.EventHandler(this.cmdSelectExportFile_Click);
            // 
            // txtExportFile
            // 
            resources.ApplyResources(this.txtExportFile, "txtExportFile");
            this.txtExportFile.Name = "txtExportFile";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // bsAddresses
            // 
            this.bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // frmExport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.cmbAddress);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dtpTill);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.lblRange);
            this.Controls.Add(this.cmdExport);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.cmdSelectExportFile);
            this.Controls.Add(this.txtExportFile);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmExport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbCloseExport;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private BindingSource bsAddresses;
        private BindingSource bsEnergyTypes;
        private Label lblAddress;
        private ComboBox cmbAddress;
        private Panel panel1;
        private RadioButton rbExportSelectedRange;
        private RadioButton rbExportAll;
        private DateTimePicker dtpTill;
        private Label label2;
        private DateTimePicker dtpFrom;
        private Label lblRange;
        private Button cmdExport;
        private Label lblEnergyType;
        private ComboBox cboEnergyType;
        private Button cmdSelectExportFile;
        private TextBox txtExportFile;
        private Label label1;
    }
}