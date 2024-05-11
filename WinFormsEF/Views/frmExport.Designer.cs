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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExport));
            toolStrip1 = new ToolStrip();
            tsbCloseExport = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            lblAddress = new Label();
            cmbAddress = new ComboBox();
            panel1 = new Panel();
            rbExportSelectedRange = new RadioButton();
            rbExportAll = new RadioButton();
            dtpTill = new DateTimePicker();
            label2 = new Label();
            dtpFrom = new DateTimePicker();
            lblRange = new Label();
            cmdExport = new Button();
            lblEnergyType = new Label();
            cboEnergyType = new ComboBox();
            cmdSelectExportFile = new Button();
            txtExportFile = new TextBox();
            label1 = new Label();
            bsAddresses = new BindingSource(components);
            bsEnergyTypes = new BindingSource(components);
            toolStrip1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbCloseExport });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbCloseExport
            // 
            tsbCloseExport.Alignment = ToolStripItemAlignment.Right;
            tsbCloseExport.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbCloseExport, "tsbCloseExport");
            tsbCloseExport.Name = "tsbCloseExport";
            tsbCloseExport.Click += tsbCloseExport_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // lblAddress
            // 
            resources.ApplyResources(lblAddress, "lblAddress");
            lblAddress.Name = "lblAddress";
            // 
            // cmbAddress
            // 
            cmbAddress.DisplayMember = "Description";
            cmbAddress.FormattingEnabled = true;
            resources.ApplyResources(cmbAddress, "cmbAddress");
            cmbAddress.Name = "cmbAddress";
            cmbAddress.ValueMember = "Id";
            // 
            // panel1
            // 
            panel1.Controls.Add(rbExportSelectedRange);
            panel1.Controls.Add(rbExportAll);
            resources.ApplyResources(panel1, "panel1");
            panel1.Name = "panel1";
            // 
            // rbExportSelectedRange
            // 
            resources.ApplyResources(rbExportSelectedRange, "rbExportSelectedRange");
            rbExportSelectedRange.Name = "rbExportSelectedRange";
            rbExportSelectedRange.UseVisualStyleBackColor = true;
            // 
            // rbExportAll
            // 
            resources.ApplyResources(rbExportAll, "rbExportAll");
            rbExportAll.Checked = true;
            rbExportAll.Name = "rbExportAll";
            rbExportAll.TabStop = true;
            rbExportAll.UseVisualStyleBackColor = true;
            // 
            // dtpTill
            // 
            dtpTill.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpTill, "dtpTill");
            dtpTill.Name = "dtpTill";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // dtpFrom
            // 
            dtpFrom.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpFrom, "dtpFrom");
            dtpFrom.Name = "dtpFrom";
            // 
            // lblRange
            // 
            resources.ApplyResources(lblRange, "lblRange");
            lblRange.Name = "lblRange";
            // 
            // cmdExport
            // 
            resources.ApplyResources(cmdExport, "cmdExport");
            cmdExport.Image = WinFormsUI.Properties.Resources.upload_24x24;
            cmdExport.Name = "cmdExport";
            cmdExport.UseVisualStyleBackColor = true;
            cmdExport.Click += cmdExport_Click;
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // cboEnergyType
            // 
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            // 
            // cmdSelectExportFile
            // 
            cmdSelectExportFile.Image = WinFormsUI.Properties.Resources.open_file_icon;
            resources.ApplyResources(cmdSelectExportFile, "cmdSelectExportFile");
            cmdSelectExportFile.Name = "cmdSelectExportFile";
            cmdSelectExportFile.UseVisualStyleBackColor = true;
            cmdSelectExportFile.Click += cmdSelectExportFile_Click;
            // 
            // txtExportFile
            // 
            resources.ApplyResources(txtExportFile, "txtExportFile");
            txtExportFile.Name = "txtExportFile";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // frmExport
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(lblAddress);
            Controls.Add(cmbAddress);
            Controls.Add(panel1);
            Controls.Add(dtpTill);
            Controls.Add(label2);
            Controls.Add(dtpFrom);
            Controls.Add(lblRange);
            Controls.Add(cmdExport);
            Controls.Add(lblEnergyType);
            Controls.Add(cboEnergyType);
            Controls.Add(cmdSelectExportFile);
            Controls.Add(txtExportFile);
            Controls.Add(label1);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmExport";
            ShowIcon = false;
            ShowInTaskbar = false;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ResumeLayout(false);
            PerformLayout();
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