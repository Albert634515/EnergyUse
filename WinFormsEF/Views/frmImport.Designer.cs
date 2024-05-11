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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImport));
            toolStrip1 = new ToolStrip();
            tsbCloseImport = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            cboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            lblEnergyType = new Label();
            cmbAddress = new ComboBox();
            bsAddresses = new BindingSource(components);
            lblAddress = new Label();
            panel1 = new Panel();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbCloseImport });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbCloseImport
            // 
            tsbCloseImport.Alignment = ToolStripItemAlignment.Right;
            tsbCloseImport.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbCloseImport, "tsbCloseImport");
            tsbCloseImport.Name = "tsbCloseImport";
            tsbCloseImport.Click += tsbCloseImport_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // cboEnergyType
            // 
            cboEnergyType.DataSource = bsEnergyTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // cmbAddress
            // 
            cmbAddress.DataSource = bsAddresses;
            cmbAddress.DisplayMember = "Description";
            cmbAddress.FormattingEnabled = true;
            resources.ApplyResources(cmbAddress, "cmbAddress");
            cmbAddress.Name = "cmbAddress";
            cmbAddress.ValueMember = "Id";
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // lblAddress
            // 
            resources.ApplyResources(lblAddress, "lblAddress");
            lblAddress.Name = "lblAddress";
            // 
            // panel1
            // 
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // frmImport
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(panel1);
            Controls.Add(cboEnergyType);
            Controls.Add(lblEnergyType);
            Controls.Add(cmbAddress);
            Controls.Add(lblAddress);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmImport";
            ShowIcon = false;
            ShowInTaskbar = false;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ResumeLayout(false);
            PerformLayout();
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