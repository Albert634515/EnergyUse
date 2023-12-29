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
            this.components = new System.ComponentModel.Container();
            this.ToolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbAdd = new System.Windows.Forms.ToolStripButton();
            this.TsbSave = new System.Windows.Forms.ToolStripButton();
            this.TbsCancel = new System.Windows.Forms.ToolStripButton();
            this.TsbDelete = new System.Windows.Forms.ToolStripButton();
            this.TsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.ToolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DgStaffel = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rateIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueFromDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueTillDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.staffelValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BsStaffel = new System.Windows.Forms.BindingSource(this.components);
            this.ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgStaffel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BsStaffel)).BeginInit();
            this.SuspendLayout();
            // 
            // ToolStrip1
            // 
            this.ToolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ToolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbAdd,
            this.TsbSave,
            this.TbsCancel,
            this.TsbDelete,
            this.TsbRefresh});
            this.ToolStrip1.Location = new System.Drawing.Point(0, 0);
            this.ToolStrip1.Name = "ToolStrip1";
            this.ToolStrip1.Size = new System.Drawing.Size(487, 27);
            this.ToolStrip1.TabIndex = 1;
            this.ToolStrip1.Text = "toolStrip1";
            // 
            // TsbAdd
            // 
            this.TsbAdd.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            this.TsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbAdd.Name = "TsbAdd";
            this.TsbAdd.Size = new System.Drawing.Size(61, 24);
            this.TsbAdd.Text = "&Add";
            this.TsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // TsbSave
            // 
            this.TsbSave.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            this.TsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbSave.Name = "TsbSave";
            this.TsbSave.Size = new System.Drawing.Size(64, 24);
            this.TsbSave.Text = "&Save";
            this.TsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // TbsCancel
            // 
            this.TbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            this.TbsCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TbsCancel.Name = "TbsCancel";
            this.TbsCancel.Size = new System.Drawing.Size(77, 24);
            this.TbsCancel.Text = "&Cancel";
            this.TbsCancel.Click += new System.EventHandler(this.tbsCancel_Click);
            // 
            // TsbDelete
            // 
            this.TsbDelete.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            this.TsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbDelete.Name = "TsbDelete";
            this.TsbDelete.Size = new System.Drawing.Size(77, 24);
            this.TsbDelete.Text = "&Delete";
            this.TsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // TsbRefresh
            // 
            this.TsbRefresh.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            this.TsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TsbRefresh.Name = "TsbRefresh";
            this.TsbRefresh.Size = new System.Drawing.Size(82, 24);
            this.TsbRefresh.Text = "&Refresh";
            this.TsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // DgStaffel
            // 
            this.DgStaffel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DgStaffel.AutoGenerateColumns = false;
            this.DgStaffel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgStaffel.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.rateIdDataGridViewTextBoxColumn,
            this.valueFromDataGridViewTextBoxColumn,
            this.valueTillDataGridViewTextBoxColumn,
            this.staffelValueDataGridViewTextBoxColumn});
            this.DgStaffel.DataSource = this.BsStaffel;
            this.DgStaffel.Location = new System.Drawing.Point(19, 43);
            this.DgStaffel.Name = "DgStaffel";
            this.DgStaffel.RowHeadersWidth = 51;
            this.DgStaffel.RowTemplate.Height = 29;
            this.DgStaffel.Size = new System.Drawing.Size(462, 326);
            this.DgStaffel.TabIndex = 2;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            this.idDataGridViewTextBoxColumn.HeaderText = "Id";
            this.idDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.Visible = false;
            this.idDataGridViewTextBoxColumn.Width = 125;
            // 
            // rateIdDataGridViewTextBoxColumn
            // 
            this.rateIdDataGridViewTextBoxColumn.DataPropertyName = "RateId";
            this.rateIdDataGridViewTextBoxColumn.HeaderText = "RateId";
            this.rateIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.rateIdDataGridViewTextBoxColumn.Name = "rateIdDataGridViewTextBoxColumn";
            this.rateIdDataGridViewTextBoxColumn.Visible = false;
            this.rateIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // valueFromDataGridViewTextBoxColumn
            // 
            this.valueFromDataGridViewTextBoxColumn.DataPropertyName = "ValueFrom";
            this.valueFromDataGridViewTextBoxColumn.HeaderText = "Value from";
            this.valueFromDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.valueFromDataGridViewTextBoxColumn.Name = "valueFromDataGridViewTextBoxColumn";
            this.valueFromDataGridViewTextBoxColumn.Width = 125;
            // 
            // valueTillDataGridViewTextBoxColumn
            // 
            this.valueTillDataGridViewTextBoxColumn.DataPropertyName = "ValueTill";
            this.valueTillDataGridViewTextBoxColumn.HeaderText = "Value till";
            this.valueTillDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.valueTillDataGridViewTextBoxColumn.Name = "valueTillDataGridViewTextBoxColumn";
            this.valueTillDataGridViewTextBoxColumn.Width = 125;
            // 
            // staffelValueDataGridViewTextBoxColumn
            // 
            this.staffelValueDataGridViewTextBoxColumn.DataPropertyName = "StaffelValue";
            this.staffelValueDataGridViewTextBoxColumn.HeaderText = "Rate";
            this.staffelValueDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.staffelValueDataGridViewTextBoxColumn.Name = "staffelValueDataGridViewTextBoxColumn";
            this.staffelValueDataGridViewTextBoxColumn.Width = 125;
            // 
            // BsStaffel
            // 
            this.BsStaffel.DataSource = typeof(EnergyUse.Models.Staffel);
            // 
            // ucStaffel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DgStaffel);
            this.Controls.Add(this.ToolStrip1);
            this.Name = "ucStaffel";
            this.Size = new System.Drawing.Size(487, 372);
            this.ToolStrip1.ResumeLayout(false);
            this.ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgStaffel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BsStaffel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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
