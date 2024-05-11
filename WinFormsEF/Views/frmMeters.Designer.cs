namespace WinFormsEF.Views
{
    partial class frmMeters
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMeters));
            toolStrip1 = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbSave = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            tsbClose = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            dgMeters = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            numberDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            activeDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            bsMeters = new BindingSource(components);
            gbMeter = new GroupBox();
            dtpFrom = new DateTimePicker();
            lblRange = new Label();
            label1 = new Label();
            cboAddress = new ComboBox();
            bsAddresses = new BindingSource(components);
            lblActive = new Label();
            chkActive = new CheckBox();
            lblEnergyType = new Label();
            cboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            txtNumber = new TextBox();
            lblNumber = new Label();
            txtDescription = new TextBox();
            lblDescription = new Label();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgMeters).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsMeters).BeginInit();
            gbMeter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAdd, tsbSave, tbsCancel, tsbDelete, tsbRefresh, tsbClose });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbAdd
            // 
            tsbAdd.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(tsbAdd, "tsbAdd");
            tsbAdd.Name = "tsbAdd";
            tsbAdd.Click += tsbAdd_Click;
            // 
            // tsbSave
            // 
            tsbSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(tsbSave, "tsbSave");
            tsbSave.Name = "tsbSave";
            tsbSave.Click += tsbSave_Click;
            // 
            // tbsCancel
            // 
            tbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(tbsCancel, "tbsCancel");
            tbsCancel.Name = "tbsCancel";
            tbsCancel.Click += tbsCancel_Click;
            // 
            // tsbDelete
            // 
            tsbDelete.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(tsbDelete, "tsbDelete");
            tsbDelete.Name = "tsbDelete";
            tsbDelete.Click += tsbDelete_Click;
            // 
            // tsbRefresh
            // 
            tsbRefresh.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(tsbRefresh, "tsbRefresh");
            tsbRefresh.Name = "tsbRefresh";
            tsbRefresh.Click += tsbRefresh_Click;
            // 
            // tsbClose
            // 
            tsbClose.Alignment = ToolStripItemAlignment.Right;
            tsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbClose, "tsbClose");
            tsbClose.Name = "tsbClose";
            tsbClose.Click += tsbClose_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // dgMeters
            // 
            dgMeters.AllowUserToAddRows = false;
            dgMeters.AllowUserToDeleteRows = false;
            resources.ApplyResources(dgMeters, "dgMeters");
            dgMeters.AutoGenerateColumns = false;
            dgMeters.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgMeters.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, numberDataGridViewTextBoxColumn, activeDataGridViewCheckBoxColumn });
            dgMeters.DataSource = bsMeters;
            dgMeters.Name = "dgMeters";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // numberDataGridViewTextBoxColumn
            // 
            numberDataGridViewTextBoxColumn.DataPropertyName = "Number";
            resources.ApplyResources(numberDataGridViewTextBoxColumn, "numberDataGridViewTextBoxColumn");
            numberDataGridViewTextBoxColumn.Name = "numberDataGridViewTextBoxColumn";
            // 
            // activeDataGridViewCheckBoxColumn
            // 
            activeDataGridViewCheckBoxColumn.DataPropertyName = "Active";
            resources.ApplyResources(activeDataGridViewCheckBoxColumn, "activeDataGridViewCheckBoxColumn");
            activeDataGridViewCheckBoxColumn.Name = "activeDataGridViewCheckBoxColumn";
            // 
            // bsMeters
            // 
            bsMeters.DataSource = typeof(EnergyUse.Models.Meter);
            // 
            // gbMeter
            // 
            resources.ApplyResources(gbMeter, "gbMeter");
            gbMeter.Controls.Add(dtpFrom);
            gbMeter.Controls.Add(lblRange);
            gbMeter.Controls.Add(label1);
            gbMeter.Controls.Add(cboAddress);
            gbMeter.Controls.Add(lblActive);
            gbMeter.Controls.Add(chkActive);
            gbMeter.Controls.Add(lblEnergyType);
            gbMeter.Controls.Add(cboEnergyType);
            gbMeter.Controls.Add(txtNumber);
            gbMeter.Controls.Add(lblNumber);
            gbMeter.Controls.Add(txtDescription);
            gbMeter.Controls.Add(lblDescription);
            gbMeter.Name = "gbMeter";
            gbMeter.TabStop = false;
            // 
            // dtpFrom
            // 
            dtpFrom.DataBindings.Add(new Binding("Value", bsMeters, "ActiveFrom", true));
            dtpFrom.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpFrom, "dtpFrom");
            dtpFrom.Name = "dtpFrom";
            // 
            // lblRange
            // 
            resources.ApplyResources(lblRange, "lblRange");
            lblRange.Name = "lblRange";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // cboAddress
            // 
            resources.ApplyResources(cboAddress, "cboAddress");
            cboAddress.DataBindings.Add(new Binding("SelectedValue", bsMeters, "AddressId", true));
            cboAddress.DataSource = bsAddresses;
            cboAddress.DisplayMember = "Description";
            cboAddress.FormattingEnabled = true;
            cboAddress.Name = "cboAddress";
            cboAddress.ValueMember = "Id";
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // lblActive
            // 
            resources.ApplyResources(lblActive, "lblActive");
            lblActive.Name = "lblActive";
            // 
            // chkActive
            // 
            resources.ApplyResources(chkActive, "chkActive");
            chkActive.DataBindings.Add(new Binding("Checked", bsMeters, "Active", true));
            chkActive.Name = "chkActive";
            chkActive.UseVisualStyleBackColor = true;
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // cboEnergyType
            // 
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.DataBindings.Add(new Binding("SelectedValue", bsMeters, "EnergyTypeId", true));
            cboEnergyType.DataSource = bsEnergyTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // txtNumber
            // 
            resources.ApplyResources(txtNumber, "txtNumber");
            txtNumber.DataBindings.Add(new Binding("Text", bsMeters, "Number", true));
            txtNumber.Name = "txtNumber";
            // 
            // lblNumber
            // 
            resources.ApplyResources(lblNumber, "lblNumber");
            lblNumber.Name = "lblNumber";
            // 
            // txtDescription
            // 
            resources.ApplyResources(txtDescription, "txtDescription");
            txtDescription.DataBindings.Add(new Binding("Text", bsMeters, "Description", true));
            txtDescription.Name = "txtDescription";
            // 
            // lblDescription
            // 
            resources.ApplyResources(lblDescription, "lblDescription");
            lblDescription.Name = "lblDescription";
            // 
            // frmMeters
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbMeter);
            Controls.Add(dgMeters);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmMeters";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmMeters_FormClosing;
            Load += frmMeters_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgMeters).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsMeters).EndInit();
            gbMeter.ResumeLayout(false);
            gbMeter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbSave;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbRefresh;
        private ToolStripButton tsbClose;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private BindingSource bsEnergyTypes;
        private BindingSource bsMeters;
        private DataGridView dgMeters;
        private GroupBox gbMeter;
        private Label label1;
        private ComboBox cboAddress;
        private Label lblActive;
        private CheckBox chkActive;
        private Label lblEnergyType;
        private ComboBox cboEnergyType;
        private TextBox txtNumber;
        private Label lblNumber;
        private TextBox txtDescription;
        private Label lblDescription;
        private BindingSource bsAddresses;
        private ToolStripButton tbsCancel;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn numberDataGridViewTextBoxColumn;
        private DataGridViewCheckBoxColumn activeDataGridViewCheckBoxColumn;
        private DateTimePicker dtpFrom;
        private Label lblRange;
    }
}