namespace WinFormsEF.Views
{
    partial class frmTariffGroups
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTariffGroups));
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbAdd = new System.Windows.Forms.ToolStripButton();
            this.TsbSave = new System.Windows.Forms.ToolStripButton();
            this.TbsCancel = new System.Windows.Forms.ToolStripButton();
            this.TsbDelete = new System.Windows.Forms.ToolStripButton();
            this.TsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.TsbClose = new System.Windows.Forms.ToolStripButton();
            this.StatusStrip1 = new System.Windows.Forms.StatusStrip();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.BsTarifGroups = new System.Windows.Forms.BindingSource(this.components);
            this.DgTarifGroups = new System.Windows.Forms.DataGridView();
            this.IdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DescriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BsTarifGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgTarifGroups)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStrip1
            // 
            resources.ApplyResources(this.ToolStrip1, "ToolStrip1");
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbAdd,
            this.TsbSave,
            this.TbsCancel,
            this.TsbDelete,
            this.TsbRefresh,
            this.TsbClose});
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolTip1.SetToolTip(this.ToolStrip1, resources.GetString("ToolStrip1.ToolTip"));
            // 
            // TsbAdd
            // 
            resources.ApplyResources(this.TsbAdd, "TsbAdd");
            this.TsbAdd.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            this.TsbAdd.Name = "TsbAdd";
            this.TsbAdd.Click += new System.EventHandler(this.TsbAdd_Click);
            // 
            // TsbSave
            // 
            resources.ApplyResources(this.TsbSave, "TsbSave");
            this.TsbSave.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            this.TsbSave.Name = "TsbSave";
            this.TsbSave.Click += new System.EventHandler(this.TsbSave_Click);
            // 
            // TbsCancel
            // 
            resources.ApplyResources(this.TbsCancel, "TbsCancel");
            this.TbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            this.TbsCancel.Name = "TbsCancel";
            this.TbsCancel.Click += new System.EventHandler(this.TbsCancel_Click);
            // 
            // TsbDelete
            // 
            resources.ApplyResources(this.TsbDelete, "TsbDelete");
            this.TsbDelete.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            this.TsbDelete.Name = "TsbDelete";
            this.TsbDelete.Click += new System.EventHandler(this.TsbDelete_Click);
            // 
            // TsbRefresh
            // 
            resources.ApplyResources(this.TsbRefresh, "TsbRefresh");
            this.TsbRefresh.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            this.TsbRefresh.Name = "TsbRefresh";
            this.TsbRefresh.Click += new System.EventHandler(this.TsbRefresh_Click);
            // 
            // TsbClose
            // 
            resources.ApplyResources(this.TsbClose, "TsbClose");
            this.TsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.TsbClose.Name = "TsbClose";
            this.TsbClose.Click += new System.EventHandler(this.TsbClose_Click);
            // 
            // StatusStrip1
            // 
            resources.ApplyResources(this.StatusStrip1, "StatusStrip1");
            this.StatusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.StatusStrip1.Name = "StatusStrip1";
            this.ToolTip1.SetToolTip(this.StatusStrip1, resources.GetString("StatusStrip1.ToolTip"));
            // 
            // BsTarifGroups
            // 
            this.BsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // DgTarifGroups
            // 
            resources.ApplyResources(this.DgTarifGroups, "DgTarifGroups");
            this.DgTarifGroups.AllowUserToAddRows = false;
            this.DgTarifGroups.AllowUserToDeleteRows = false;
            this.DgTarifGroups.AutoGenerateColumns = false;
            this.DgTarifGroups.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgTarifGroups.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdDataGridViewTextBoxColumn,
            this.DescriptionDataGridViewTextBoxColumn});
            this.DgTarifGroups.DataSource = this.BsTarifGroups;
            this.DgTarifGroups.Name = "DgTarifGroups";
            this.DgTarifGroups.RowTemplate.Height = 29;
            this.ToolTip1.SetToolTip(this.DgTarifGroups, resources.GetString("DgTarifGroups.ToolTip"));
            // 
            // IdDataGridViewTextBoxColumn
            // 
            this.IdDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.IdDataGridViewTextBoxColumn, "IdDataGridViewTextBoxColumn");
            this.IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn";
            // 
            // DescriptionDataGridViewTextBoxColumn
            // 
            this.DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(this.DescriptionDataGridViewTextBoxColumn, "DescriptionDataGridViewTextBoxColumn");
            this.DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn";
            // 
            // frmTariffGroups
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DgTarifGroups);
            this.Controls.Add(this.StatusStrip1);
            this.Controls.Add(this.ToolStrip1);
            this.Name = "frmTariffGroups";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ToolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmTarifGroups_FormClosing);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.BsTarifGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgTarifGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip ToolStrip1;
        private ToolStripButton TsbAdd;
        private ToolStripButton TsbSave;
        private ToolStripButton TsbDelete;
        private ToolStripButton TsbRefresh;
        private ToolStripButton TsbClose;
        private StatusStrip StatusStrip1;
        private ToolTip ToolTip1;
        private BindingSource BsTarifGroups;
        private DataGridView DgTarifGroups;
        private ToolStripButton TbsCancel;
        private DataGridViewTextBoxColumn IdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn DescriptionDataGridViewTextBoxColumn;
    }
}