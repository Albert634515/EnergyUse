namespace WinFormsEF.Views
{
    partial class frmNetting
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmNetting));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddNetting = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteNetting = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshNetting = new System.Windows.Forms.ToolStripButton();
            this.tsbCloseNetting = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgNetting = new System.Windows.Forms.DataGridView();
            this.bsNetting = new System.Windows.Forms.BindingSource(this.components);
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.energyTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNetting)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsNetting)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddNetting,
            this.tsbSave,
            this.tbsCancel,
            this.tsbDeleteNetting,
            this.tsbRefreshNetting,
            this.tsbCloseNetting});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbAddNetting
            // 
            this.tsbAddNetting.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(this.tsbAddNetting, "tsbAddNetting");
            this.tsbAddNetting.Name = "tsbAddNetting";
            this.tsbAddNetting.Click += new System.EventHandler(this.tsbAddNetting_Click);
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
            // tsbDeleteNetting
            // 
            this.tsbDeleteNetting.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(this.tsbDeleteNetting, "tsbDeleteNetting");
            this.tsbDeleteNetting.Name = "tsbDeleteNetting";
            this.tsbDeleteNetting.Click += new System.EventHandler(this.tsbDeleteNetting_Click);
            // 
            // tsbRefreshNetting
            // 
            this.tsbRefreshNetting.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(this.tsbRefreshNetting, "tsbRefreshNetting");
            this.tsbRefreshNetting.Name = "tsbRefreshNetting";
            this.tsbRefreshNetting.Click += new System.EventHandler(this.tsbRefreshNetting_Click);
            // 
            // tsbCloseNetting
            // 
            this.tsbCloseNetting.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCloseNetting.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbCloseNetting, "tsbCloseNetting");
            this.tsbCloseNetting.Name = "tsbCloseNetting";
            this.tsbCloseNetting.Click += new System.EventHandler(this.tsbCloseNetting_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // dgNetting
            // 
            this.dgNetting.AllowUserToAddRows = false;
            this.dgNetting.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgNetting, "dgNetting");
            this.dgNetting.AutoGenerateColumns = false;
            this.dgNetting.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgNetting.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.startDateDataGridViewTextBoxColumn,
            this.endDateDataGridViewTextBoxColumn,
            this.rateDataGridViewTextBoxColumn,
            this.energyTypeDataGridViewTextBoxColumn});
            this.dgNetting.DataSource = this.bsNetting;
            this.dgNetting.Name = "dgNetting";
            this.dgNetting.RowTemplate.Height = 29;
            // 
            // bsNetting
            // 
            this.bsNetting.DataSource = typeof(EnergyUse.Models.Netting);
            // 
            // cboEnergyType
            // 
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.Name = "cboEnergyType";
            this.cboEnergyType.ValueMember = "Id";
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // startDateDataGridViewTextBoxColumn
            // 
            this.startDateDataGridViewTextBoxColumn.DataPropertyName = "StartDate";
            resources.ApplyResources(this.startDateDataGridViewTextBoxColumn, "startDateDataGridViewTextBoxColumn");
            this.startDateDataGridViewTextBoxColumn.Name = "startDateDataGridViewTextBoxColumn";
            // 
            // endDateDataGridViewTextBoxColumn
            // 
            this.endDateDataGridViewTextBoxColumn.DataPropertyName = "EndDate";
            resources.ApplyResources(this.endDateDataGridViewTextBoxColumn, "endDateDataGridViewTextBoxColumn");
            this.endDateDataGridViewTextBoxColumn.Name = "endDateDataGridViewTextBoxColumn";
            // 
            // rateDataGridViewTextBoxColumn
            // 
            this.rateDataGridViewTextBoxColumn.DataPropertyName = "Rate";
            resources.ApplyResources(this.rateDataGridViewTextBoxColumn, "rateDataGridViewTextBoxColumn");
            this.rateDataGridViewTextBoxColumn.Name = "rateDataGridViewTextBoxColumn";
            // 
            // energyTypeDataGridViewTextBoxColumn
            // 
            this.energyTypeDataGridViewTextBoxColumn.DataPropertyName = "EnergyType";
            resources.ApplyResources(this.energyTypeDataGridViewTextBoxColumn, "energyTypeDataGridViewTextBoxColumn");
            this.energyTypeDataGridViewTextBoxColumn.Name = "energyTypeDataGridViewTextBoxColumn";
            // 
            // frmNetting
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.dgNetting);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmNetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmNetting_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgNetting)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsNetting)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbAddNetting;
        private ToolStripButton tsbDeleteNetting;
        private ToolStripButton tsbRefreshNetting;
        private ToolStripButton tsbCloseNetting;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private BindingSource bsNetting;
        private ToolStripButton tsbSave;
        private DataGridView dgNetting;
        private ComboBox cboEnergyType;
        private Label lblEnergyType;
        private ToolStripButton tbsCancel;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn energyTypeDataGridViewTextBoxColumn;
    }
}