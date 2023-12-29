namespace WinFormsEF.Views
{
    partial class frmImport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCloseImport = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.bsAddresses = new System.Windows.Forms.BindingSource(this.components);
            this.lblAddress = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCloseImport});
            this.toolStrip1.Name = "toolStrip1";
            this.toolTip1.SetToolTip(this.toolStrip1, resources.GetString("toolStrip1.ToolTip"));
            // 
            // tsbCloseImport
            // 
            resources.ApplyResources(this.tsbCloseImport, "tsbCloseImport");
            this.tsbCloseImport.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCloseImport.Image = 
                
                global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.tsbCloseImport.Name = "tsbCloseImport";
            this.tsbCloseImport.Click += new System.EventHandler(this.tsbCloseImport_Click);
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Name = "statusStrip1";
            this.toolTip1.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // cboEnergyType
            // 
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.DataSource = this.bsEnergyTypes;
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            this.cboEnergyType.Name = "cboEnergyType";
            this.toolTip1.SetToolTip(this.cboEnergyType, resources.GetString("cboEnergyType.ToolTip"));
            this.cboEnergyType.ValueMember = "Id";
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            this.toolTip1.SetToolTip(this.lblEnergyType, resources.GetString("lblEnergyType.ToolTip"));
            // 
            // cmbAddress
            // 
            resources.ApplyResources(this.cmbAddress, "cmbAddress");
            this.cmbAddress.DataSource = this.bsAddresses;
            this.cmbAddress.DisplayMember = "Description";
            this.cmbAddress.FormattingEnabled = true;
            this.cmbAddress.Name = "cmbAddress";
            this.toolTip1.SetToolTip(this.cmbAddress, resources.GetString("cmbAddress.ToolTip"));
            this.cmbAddress.ValueMember = "Id";
            // 
            // bsAddresses
            // 
            this.bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            this.toolTip1.SetToolTip(this.lblAddress, resources.GetString("lblAddress.ToolTip"));
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Name = "panel1";
            this.toolTip1.SetToolTip(this.panel1, resources.GetString("panel1.ToolTip"));
            // 
            // frmImport
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.cmbAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmImport";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbCloseImport;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private BindingSource bsAddresses;
        private BindingSource bsEnergyTypes;
        private ComboBox cboEnergyType;
        private Label lblEnergyType;
        private ComboBox cmbAddress;
        private Label lblAddress;
        private Panel panel1;
    }
}