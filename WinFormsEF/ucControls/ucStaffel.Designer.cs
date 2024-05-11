namespace WinFormsEF.ucControls
{
    partial class ucStaffel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ToolStrip1 = new ToolStrip();
            TsbAdd = new ToolStripButton();
            TsbSave = new ToolStripButton();
            TbsCancel = new ToolStripButton();
            TsbDelete = new ToolStripButton();
            TsbRefresh = new ToolStripButton();
            ToolTip1 = new ToolTip(components);
            DgStaffel = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            rateIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            valueFromDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            valueTillDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            staffelValueDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            BsStaffel = new BindingSource(components);
            ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgStaffel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BsStaffel).BeginInit();
            SuspendLayout();
            // 
            // ToolStrip1
            // 
            ToolStrip1.ImageScalingSize = new Size(20, 20);
            ToolStrip1.Items.AddRange(new ToolStripItem[] { TsbAdd, TsbSave, TbsCancel, TsbDelete, TsbRefresh });
            ToolStrip1.Location = new Point(0, 0);
            ToolStrip1.Name = "ToolStrip1";
            ToolStrip1.Size = new Size(487, 27);
            ToolStrip1.TabIndex = 0;
            ToolStrip1.Text = "toolStrip1";
            // 
            // TsbAdd
            // 
            TsbAdd.Image = WinFormsUI.Properties.Resources.add_24x24;
            TsbAdd.ImageTransparentColor = Color.Magenta;
            TsbAdd.Name = "TsbAdd";
            TsbAdd.Size = new Size(61, 24);
            TsbAdd.Text = "&Add";
            TsbAdd.Click += tsbAdd_Click;
            // 
            // TsbSave
            // 
            TsbSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            TsbSave.ImageTransparentColor = Color.Magenta;
            TsbSave.Name = "TsbSave";
            TsbSave.Size = new Size(64, 24);
            TsbSave.Text = "&Save";
            TsbSave.Click += tsbSave_Click;
            // 
            // TbsCancel
            // 
            TbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            TbsCancel.ImageTransparentColor = Color.Magenta;
            TbsCancel.Name = "TbsCancel";
            TbsCancel.Size = new Size(77, 24);
            TbsCancel.Text = "&Cancel";
            TbsCancel.Click += tbsCancel_Click;
            // 
            // TsbDelete
            // 
            TsbDelete.Image = WinFormsUI.Properties.Resources.delete_24x24;
            TsbDelete.ImageTransparentColor = Color.Magenta;
            TsbDelete.Name = "TsbDelete";
            TsbDelete.Size = new Size(77, 24);
            TsbDelete.Text = "&Delete";
            TsbDelete.Click += tsbDelete_Click;
            // 
            // TsbRefresh
            // 
            TsbRefresh.Image = WinFormsUI.Properties.Resources.clock_24x24;
            TsbRefresh.ImageTransparentColor = Color.Magenta;
            TsbRefresh.Name = "TsbRefresh";
            TsbRefresh.Size = new Size(82, 24);
            TsbRefresh.Text = "&Refresh";
            TsbRefresh.Click += tsbRefresh_Click;
            // 
            // DgStaffel
            // 
            DgStaffel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DgStaffel.AutoGenerateColumns = false;
            DgStaffel.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgStaffel.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, rateIdDataGridViewTextBoxColumn, valueFromDataGridViewTextBoxColumn, valueTillDataGridViewTextBoxColumn, staffelValueDataGridViewTextBoxColumn });
            DgStaffel.DataSource = BsStaffel;
            DgStaffel.Location = new Point(19, 43);
            DgStaffel.Name = "DgStaffel";
            DgStaffel.RowHeadersWidth = 51;
            DgStaffel.Size = new Size(462, 326);
            DgStaffel.TabIndex = 1;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            idDataGridViewTextBoxColumn.HeaderText = "Id";
            idDataGridViewTextBoxColumn.MinimumWidth = 6;
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            idDataGridViewTextBoxColumn.Visible = false;
            idDataGridViewTextBoxColumn.Width = 125;
            // 
            // rateIdDataGridViewTextBoxColumn
            // 
            rateIdDataGridViewTextBoxColumn.DataPropertyName = "RateId";
            rateIdDataGridViewTextBoxColumn.HeaderText = "RateId";
            rateIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            rateIdDataGridViewTextBoxColumn.Name = "rateIdDataGridViewTextBoxColumn";
            rateIdDataGridViewTextBoxColumn.Visible = false;
            rateIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // valueFromDataGridViewTextBoxColumn
            // 
            valueFromDataGridViewTextBoxColumn.DataPropertyName = "ValueFrom";
            valueFromDataGridViewTextBoxColumn.HeaderText = "Value from";
            valueFromDataGridViewTextBoxColumn.MinimumWidth = 6;
            valueFromDataGridViewTextBoxColumn.Name = "valueFromDataGridViewTextBoxColumn";
            valueFromDataGridViewTextBoxColumn.Width = 125;
            // 
            // valueTillDataGridViewTextBoxColumn
            // 
            valueTillDataGridViewTextBoxColumn.DataPropertyName = "ValueTill";
            valueTillDataGridViewTextBoxColumn.HeaderText = "Value till";
            valueTillDataGridViewTextBoxColumn.MinimumWidth = 6;
            valueTillDataGridViewTextBoxColumn.Name = "valueTillDataGridViewTextBoxColumn";
            valueTillDataGridViewTextBoxColumn.Width = 125;
            // 
            // staffelValueDataGridViewTextBoxColumn
            // 
            staffelValueDataGridViewTextBoxColumn.DataPropertyName = "StaffelValue";
            staffelValueDataGridViewTextBoxColumn.HeaderText = "Rate";
            staffelValueDataGridViewTextBoxColumn.MinimumWidth = 6;
            staffelValueDataGridViewTextBoxColumn.Name = "staffelValueDataGridViewTextBoxColumn";
            staffelValueDataGridViewTextBoxColumn.Width = 125;
            // 
            // BsStaffel
            // 
            BsStaffel.DataSource = typeof(EnergyUse.Models.Staffel);
            // 
            // ucStaffel
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(DgStaffel);
            Controls.Add(ToolStrip1);
            Name = "ucStaffel";
            Size = new Size(487, 372);
            ToolStrip1.ResumeLayout(false);
            ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DgStaffel).EndInit();
            ((System.ComponentModel.ISupportInitialize)BsStaffel).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolStrip1;
        private ToolStripButton TsbAdd;
        private ToolStripButton TsbSave;
        private ToolStripButton TbsCancel;
        private ToolStripButton TsbDelete;
        private ToolStripButton TsbRefresh;
        private ToolTip ToolTip1;
        private DataGridView DgStaffel;
        private BindingSource BsStaffel;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rateIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn valueFromDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn valueTillDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn staffelValueDataGridViewTextBoxColumn;
    }
}
