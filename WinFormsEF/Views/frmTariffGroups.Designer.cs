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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTariffGroups));
            ToolStrip1 = new ToolStrip();
            TsbAdd = new ToolStripButton();
            TsbSave = new ToolStripButton();
            TbsCancel = new ToolStripButton();
            TsbDelete = new ToolStripButton();
            TsbRefresh = new ToolStripButton();
            TsbClose = new ToolStripButton();
            StatusStrip1 = new StatusStrip();
            ToolTip1 = new ToolTip(components);
            DgTarifGroups = new DataGridView();
            IdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            DescriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            BsTarifGroups = new BindingSource(components);
            ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgTarifGroups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BsTarifGroups).BeginInit();
            SuspendLayout();
            // 
            // ToolStrip1
            // 
            ToolStrip1.ImageScalingSize = new Size(20, 20);
            ToolStrip1.Items.AddRange(new ToolStripItem[] { TsbAdd, TsbSave, TbsCancel, TsbDelete, TsbRefresh, TsbClose });
            resources.ApplyResources(ToolStrip1, "ToolStrip1");
            ToolStrip1.Name = "ToolStrip1";
            // 
            // TsbAdd
            // 
            TsbAdd.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(TsbAdd, "TsbAdd");
            TsbAdd.Name = "TsbAdd";
            TsbAdd.Click += TsbAdd_Click;
            // 
            // TsbSave
            // 
            TsbSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(TsbSave, "TsbSave");
            TsbSave.Name = "TsbSave";
            TsbSave.Click += TsbSave_Click;
            // 
            // TbsCancel
            // 
            TbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(TbsCancel, "TbsCancel");
            TbsCancel.Name = "TbsCancel";
            TbsCancel.Click += TbsCancel_Click;
            // 
            // TsbDelete
            // 
            TsbDelete.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(TsbDelete, "TsbDelete");
            TsbDelete.Name = "TsbDelete";
            TsbDelete.Click += TsbDelete_Click;
            // 
            // TsbRefresh
            // 
            TsbRefresh.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(TsbRefresh, "TsbRefresh");
            TsbRefresh.Name = "TsbRefresh";
            TsbRefresh.Click += TsbRefresh_Click;
            // 
            // TsbClose
            // 
            TsbClose.Alignment = ToolStripItemAlignment.Right;
            TsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(TsbClose, "TsbClose");
            TsbClose.Name = "TsbClose";
            TsbClose.Click += TsbClose_Click;
            // 
            // StatusStrip1
            // 
            StatusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(StatusStrip1, "StatusStrip1");
            StatusStrip1.Name = "StatusStrip1";
            // 
            // DgTarifGroups
            // 
            DgTarifGroups.AllowUserToAddRows = false;
            DgTarifGroups.AllowUserToDeleteRows = false;
            resources.ApplyResources(DgTarifGroups, "DgTarifGroups");
            DgTarifGroups.AutoGenerateColumns = false;
            DgTarifGroups.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgTarifGroups.Columns.AddRange(new DataGridViewColumn[] { IdDataGridViewTextBoxColumn, DescriptionDataGridViewTextBoxColumn });
            DgTarifGroups.DataSource = BsTarifGroups;
            DgTarifGroups.Name = "DgTarifGroups";
            // 
            // IdDataGridViewTextBoxColumn
            // 
            IdDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(IdDataGridViewTextBoxColumn, "IdDataGridViewTextBoxColumn");
            IdDataGridViewTextBoxColumn.Name = "IdDataGridViewTextBoxColumn";
            // 
            // DescriptionDataGridViewTextBoxColumn
            // 
            DescriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(DescriptionDataGridViewTextBoxColumn, "DescriptionDataGridViewTextBoxColumn");
            DescriptionDataGridViewTextBoxColumn.Name = "DescriptionDataGridViewTextBoxColumn";
            // 
            // BsTarifGroups
            // 
            BsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // frmTariffGroups
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(DgTarifGroups);
            Controls.Add(StatusStrip1);
            Controls.Add(ToolStrip1);
            Name = "frmTariffGroups";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += FrmTarifGroups_FormClosing;
            ToolStrip1.ResumeLayout(false);
            ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DgTarifGroups).EndInit();
            ((System.ComponentModel.ISupportInitialize)BsTarifGroups).EndInit();
            ResumeLayout(false);
            PerformLayout();
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