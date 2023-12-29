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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCorrectionFactor));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgCorrectionFactor = new System.Windows.Forms.DataGridView();
            this.startFactorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endFactorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.factorDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.energyTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsCorrectionFactors = new System.Windows.Forms.BindingSource(this.components);
            this.gbCorrectionFactor = new System.Windows.Forms.GroupBox();
            this.txtFactor = new System.Windows.Forms.TextBox();
            this.lblFactor = new System.Windows.Forms.Label();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCorrectionFactor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCorrectionFactors)).BeginInit();
            this.gbCorrectionFactor.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbSave,
            this.tbsCancel,
            this.tsbDelete,
            this.tsbRefresh,
            this.tsbClose});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(this.tsbAdd, "tsbAdd");
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(this.tsbSave, "tsbSave");
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tbsCancel
            // 
            this.tbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(this.tbsCancel, "tbsCancel");
            this.tbsCancel.Name = "tbsCancel";
            this.tbsCancel.Click += new System.EventHandler(this.tbsCancel_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(this.tsbDelete, "tsbDelete");
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(this.tsbRefresh, "tsbRefresh");
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbClose, "tsbClose");
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // cboEnergyType
            // 
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.Name = "cboEnergyType";
            this.cboEnergyType.ValueMember = "Id";
            this.cboEnergyType.SelectedIndexChanged += new System.EventHandler(this.cboEnergyType_SelectedIndexChanged);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            // 
            // dgCorrectionFactor
            // 
            this.dgCorrectionFactor.AllowUserToAddRows = false;
            this.dgCorrectionFactor.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgCorrectionFactor, "dgCorrectionFactor");
            this.dgCorrectionFactor.AutoGenerateColumns = false;
            this.dgCorrectionFactor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCorrectionFactor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.startFactorDataGridViewTextBoxColumn,
            this.endFactorDataGridViewTextBoxColumn,
            this.factorDataGridViewTextBoxColumn,
            this.energyTypeDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn});
            this.dgCorrectionFactor.DataSource = this.bsCorrectionFactors;
            this.dgCorrectionFactor.Name = "dgCorrectionFactor";
            this.dgCorrectionFactor.ReadOnly = true;
            this.dgCorrectionFactor.RowTemplate.Height = 29;
            // 
            // startFactorDataGridViewTextBoxColumn
            // 
            this.startFactorDataGridViewTextBoxColumn.DataPropertyName = "StartFactor";
            resources.ApplyResources(this.startFactorDataGridViewTextBoxColumn, "startFactorDataGridViewTextBoxColumn");
            this.startFactorDataGridViewTextBoxColumn.Name = "startFactorDataGridViewTextBoxColumn";
            this.startFactorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // endFactorDataGridViewTextBoxColumn
            // 
            this.endFactorDataGridViewTextBoxColumn.DataPropertyName = "EndFactor";
            resources.ApplyResources(this.endFactorDataGridViewTextBoxColumn, "endFactorDataGridViewTextBoxColumn");
            this.endFactorDataGridViewTextBoxColumn.Name = "endFactorDataGridViewTextBoxColumn";
            this.endFactorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // factorDataGridViewTextBoxColumn
            // 
            this.factorDataGridViewTextBoxColumn.DataPropertyName = "Factor";
            resources.ApplyResources(this.factorDataGridViewTextBoxColumn, "factorDataGridViewTextBoxColumn");
            this.factorDataGridViewTextBoxColumn.Name = "factorDataGridViewTextBoxColumn";
            this.factorDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // energyTypeDataGridViewTextBoxColumn
            // 
            this.energyTypeDataGridViewTextBoxColumn.DataPropertyName = "EnergyType";
            resources.ApplyResources(this.energyTypeDataGridViewTextBoxColumn, "energyTypeDataGridViewTextBoxColumn");
            this.energyTypeDataGridViewTextBoxColumn.Name = "energyTypeDataGridViewTextBoxColumn";
            this.energyTypeDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsCorrectionFactors
            // 
            this.bsCorrectionFactors.DataSource = typeof(EnergyUse.Models.CorrectionFactor);
            // 
            // gbCorrectionFactor
            // 
            resources.ApplyResources(this.gbCorrectionFactor, "gbCorrectionFactor");
            this.gbCorrectionFactor.Controls.Add(this.txtFactor);
            this.gbCorrectionFactor.Controls.Add(this.lblFactor);
            this.gbCorrectionFactor.Controls.Add(this.lblEndDate);
            this.gbCorrectionFactor.Controls.Add(this.dtEndDate);
            this.gbCorrectionFactor.Controls.Add(this.lblStartDate);
            this.gbCorrectionFactor.Controls.Add(this.dtStartDate);
            this.gbCorrectionFactor.Name = "gbCorrectionFactor";
            this.gbCorrectionFactor.TabStop = false;
            // 
            // txtFactor
            // 
            this.txtFactor.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCorrectionFactors, "Factor", true));
            resources.ApplyResources(this.txtFactor, "txtFactor");
            this.txtFactor.Name = "txtFactor";
            // 
            // lblFactor
            // 
            resources.ApplyResources(this.lblFactor, "lblFactor");
            this.lblFactor.Name = "lblFactor";
            // 
            // lblEndDate
            // 
            resources.ApplyResources(this.lblEndDate, "lblEndDate");
            this.lblEndDate.Name = "lblEndDate";
            // 
            // dtEndDate
            // 
            this.dtEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsCorrectionFactors, "EndFactor", true));
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtEndDate, "dtEndDate");
            this.dtEndDate.Name = "dtEndDate";
            this.dtEndDate.Value = new System.DateTime(2022, 10, 1, 0, 0, 0, 0);
            // 
            // lblStartDate
            // 
            resources.ApplyResources(this.lblStartDate, "lblStartDate");
            this.lblStartDate.Name = "lblStartDate";
            // 
            // dtStartDate
            // 
            this.dtStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsCorrectionFactors, "StartFactor", true));
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtStartDate, "dtStartDate");
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Value = new System.DateTime(2022, 10, 1, 0, 0, 0, 0);
            // 
            // frmCorrectionFactor
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbCorrectionFactor);
            this.Controls.Add(this.dgCorrectionFactor);
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmCorrectionFactor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCorrectionFactor_FormClosing);
            this.Load += new System.EventHandler(this.frmCorrectionFactor_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgCorrectionFactor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCorrectionFactors)).EndInit();
            this.gbCorrectionFactor.ResumeLayout(false);
            this.gbCorrectionFactor.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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