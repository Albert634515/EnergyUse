namespace WinFormsEF.Views
{
    partial class frmCorrectionFactor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorrectionFactor));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            toolStrip1 = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbSave = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            tsbClose = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            cboEnergyType = new ComboBox();
            lblEnergyType = new Label();
            toolTip1 = new ToolTip(components);
            dgCorrectionFactor = new DataGridView();
            bsCorrectionFactors = new BindingSource(components);
            gbCorrectionFactor = new GroupBox();
            txtFactor = new TextBox();
            lblFactor = new Label();
            lblEndDate = new Label();
            dtEndDate = new DateTimePicker();
            lblStartDate = new Label();
            dtStartDate = new DateTimePicker();
            startFactorDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            endFactorDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            factorDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            energyTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgCorrectionFactor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCorrectionFactors).BeginInit();
            gbCorrectionFactor.SuspendLayout();
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
            // cboEnergyType
            // 
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            cboEnergyType.SelectedIndexChanged += cboEnergyType_SelectedIndexChanged;
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // dgCorrectionFactor
            // 
            dgCorrectionFactor.AllowUserToAddRows = false;
            dgCorrectionFactor.AllowUserToDeleteRows = false;
            resources.ApplyResources(dgCorrectionFactor, "dgCorrectionFactor");
            dgCorrectionFactor.AutoGenerateColumns = false;
            dgCorrectionFactor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgCorrectionFactor.Columns.AddRange(new DataGridViewColumn[] { startFactorDataGridViewTextBoxColumn, endFactorDataGridViewTextBoxColumn, factorDataGridViewTextBoxColumn, energyTypeDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn });
            dgCorrectionFactor.DataSource = bsCorrectionFactors;
            dgCorrectionFactor.Name = "dgCorrectionFactor";
            dgCorrectionFactor.ReadOnly = true;
            // 
            // bsCorrectionFactors
            // 
            bsCorrectionFactors.DataSource = typeof(EnergyUse.Models.CorrectionFactor);
            // 
            // gbCorrectionFactor
            // 
            resources.ApplyResources(gbCorrectionFactor, "gbCorrectionFactor");
            gbCorrectionFactor.Controls.Add(txtFactor);
            gbCorrectionFactor.Controls.Add(lblFactor);
            gbCorrectionFactor.Controls.Add(lblEndDate);
            gbCorrectionFactor.Controls.Add(dtEndDate);
            gbCorrectionFactor.Controls.Add(lblStartDate);
            gbCorrectionFactor.Controls.Add(dtStartDate);
            gbCorrectionFactor.Name = "gbCorrectionFactor";
            gbCorrectionFactor.TabStop = false;
            // 
            // txtFactor
            // 
            txtFactor.DataBindings.Add(new Binding("Text", bsCorrectionFactors, "Factor", true));
            resources.ApplyResources(txtFactor, "txtFactor");
            txtFactor.Name = "txtFactor";
            // 
            // lblFactor
            // 
            resources.ApplyResources(lblFactor, "lblFactor");
            lblFactor.Name = "lblFactor";
            // 
            // lblEndDate
            // 
            resources.ApplyResources(lblEndDate, "lblEndDate");
            lblEndDate.Name = "lblEndDate";
            // 
            // dtEndDate
            // 
            dtEndDate.DataBindings.Add(new Binding("Value", bsCorrectionFactors, "EndFactor", true));
            dtEndDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtEndDate, "dtEndDate");
            dtEndDate.Name = "dtEndDate";
            dtEndDate.Value = new DateTime(2022, 10, 1, 0, 0, 0, 0);
            // 
            // lblStartDate
            // 
            resources.ApplyResources(lblStartDate, "lblStartDate");
            lblStartDate.Name = "lblStartDate";
            // 
            // dtStartDate
            // 
            dtStartDate.DataBindings.Add(new Binding("Value", bsCorrectionFactors, "StartFactor", true));
            dtStartDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtStartDate, "dtStartDate");
            dtStartDate.Name = "dtStartDate";
            dtStartDate.Value = new DateTime(2022, 10, 1, 0, 0, 0, 0);
            // 
            // startFactorDataGridViewTextBoxColumn
            // 
            startFactorDataGridViewTextBoxColumn.DataPropertyName = "StartFactor";
            resources.ApplyResources(startFactorDataGridViewTextBoxColumn, "startFactorDataGridViewTextBoxColumn");
            startFactorDataGridViewTextBoxColumn.Name = "startFactorDataGridViewTextBoxColumn";
            startFactorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // endFactorDataGridViewTextBoxColumn
            // 
            endFactorDataGridViewTextBoxColumn.DataPropertyName = "EndFactor";
            resources.ApplyResources(endFactorDataGridViewTextBoxColumn, "endFactorDataGridViewTextBoxColumn");
            endFactorDataGridViewTextBoxColumn.Name = "endFactorDataGridViewTextBoxColumn";
            endFactorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // factorDataGridViewTextBoxColumn
            // 
            factorDataGridViewTextBoxColumn.DataPropertyName = "Factor";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N5";
            factorDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(factorDataGridViewTextBoxColumn, "factorDataGridViewTextBoxColumn");
            factorDataGridViewTextBoxColumn.Name = "factorDataGridViewTextBoxColumn";
            factorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // energyTypeDataGridViewTextBoxColumn
            // 
            energyTypeDataGridViewTextBoxColumn.DataPropertyName = "EnergyType";
            resources.ApplyResources(energyTypeDataGridViewTextBoxColumn, "energyTypeDataGridViewTextBoxColumn");
            energyTypeDataGridViewTextBoxColumn.Name = "energyTypeDataGridViewTextBoxColumn";
            energyTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // frmCorrectionFactor
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbCorrectionFactor);
            Controls.Add(dgCorrectionFactor);
            Controls.Add(cboEnergyType);
            Controls.Add(lblEnergyType);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmCorrectionFactor";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmCorrectionFactor_FormClosing;
            Load += frmCorrectionFactor_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgCorrectionFactor).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCorrectionFactors).EndInit();
            gbCorrectionFactor.ResumeLayout(false);
            gbCorrectionFactor.PerformLayout();
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
        private ComboBox cboEnergyType;
        private Label lblEnergyType;
        private ToolTip toolTip1;
        private BindingSource bsCorrectionFactors;
        private DataGridView dgCorrectionFactor;
        private ToolStripButton tbsCancel;
        private GroupBox gbCorrectionFactor;
        private DateTimePicker dtStartDate;
        private Label lblStartDate;
        private Label lblEndDate;
        private DateTimePicker dtEndDate;
        private Label lblFactor;
        private TextBox txtFactor;
        private DataGridViewTextBoxColumn startFactorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endFactorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn factorDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn energyTypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    }
}