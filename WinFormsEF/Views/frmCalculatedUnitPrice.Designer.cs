namespace WinFormsEF.Views
{
    partial class frmCalculatedUnitPrice
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
            toolStrip1 = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbSave = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            tsbClose = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            bsCalculatedUnitPrice = new BindingSource(components);
            dgCalculatedUnitPrice = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            energyTypeIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            tariffGroupIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            yearDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            cboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            lblEnergyType = new Label();
            gbCalculatedUnitPrice = new GroupBox();
            label1 = new Label();
            nudYear = new NumericUpDown();
            txtPrice = new TextBox();
            lblPrice = new Label();
            cboTarifGroup = new ComboBox();
            bsTarifGroups = new BindingSource(components);
            lblTarifGroup = new Label();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsCalculatedUnitPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgCalculatedUnitPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            gbCalculatedUnitPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudYear).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAdd, tsbSave, tbsCancel, tsbDelete, tsbRefresh, tsbClose });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(792, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            tsbAdd.Image = WinFormsUI.Properties.Resources.add_24x24;
            tsbAdd.ImageTransparentColor = Color.Magenta;
            tsbAdd.Name = "tsbAdd";
            tsbAdd.Size = new Size(61, 24);
            tsbAdd.Text = "&Add";
            tsbAdd.Click += tsbAdd_Click;
            // 
            // tsbSave
            // 
            tsbSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            tsbSave.ImageTransparentColor = Color.Magenta;
            tsbSave.Name = "tsbSave";
            tsbSave.Size = new Size(64, 24);
            tsbSave.Text = "&Save";
            tsbSave.Click += tsbSave_Click;
            // 
            // tbsCancel
            // 
            tbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            tbsCancel.ImageTransparentColor = Color.Magenta;
            tbsCancel.Name = "tbsCancel";
            tbsCancel.Size = new Size(77, 24);
            tbsCancel.Text = "&Cancel";
            tbsCancel.Click += tbsCancel_Click;
            // 
            // tsbDelete
            // 
            tsbDelete.Image = WinFormsUI.Properties.Resources.delete_24x24;
            tsbDelete.ImageTransparentColor = Color.Magenta;
            tsbDelete.Name = "tsbDelete";
            tsbDelete.Size = new Size(77, 24);
            tsbDelete.Text = "&Delete";
            tsbDelete.Click += tsbDelete_Click;
            // 
            // tsbRefresh
            // 
            tsbRefresh.Image = WinFormsUI.Properties.Resources.clock_24x24;
            tsbRefresh.ImageTransparentColor = Color.Magenta;
            tsbRefresh.Name = "tsbRefresh";
            tsbRefresh.Size = new Size(82, 24);
            tsbRefresh.Text = "&Refresh";
            tsbRefresh.Click += tsbRefresh_Click;
            // 
            // tsbClose
            // 
            tsbClose.Alignment = ToolStripItemAlignment.Right;
            tsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            tsbClose.ImageTransparentColor = Color.Magenta;
            tsbClose.Name = "tsbClose";
            tsbClose.Size = new Size(69, 24);
            tsbClose.Text = "&Close";
            tsbClose.TextAlign = ContentAlignment.MiddleLeft;
            tsbClose.TextImageRelation = TextImageRelation.TextBeforeImage;
            tsbClose.Click += tsbClose_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(792, 22);
            statusStrip1.TabIndex = 3;
            statusStrip1.Text = "statusStrip1";
            // 
            // bsCalculatedUnitPrice
            // 
            bsCalculatedUnitPrice.DataSource = typeof(EnergyUse.Models.CalculatedUnitPrice);
            // 
            // dgCalculatedUnitPrice
            // 
            dgCalculatedUnitPrice.AllowUserToAddRows = false;
            dgCalculatedUnitPrice.AllowUserToDeleteRows = false;
            dgCalculatedUnitPrice.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgCalculatedUnitPrice.AutoGenerateColumns = false;
            dgCalculatedUnitPrice.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgCalculatedUnitPrice.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, energyTypeIdDataGridViewTextBoxColumn, tariffGroupIdDataGridViewTextBoxColumn, priceDataGridViewTextBoxColumn, yearDataGridViewTextBoxColumn });
            dgCalculatedUnitPrice.DataSource = bsCalculatedUnitPrice;
            dgCalculatedUnitPrice.Location = new Point(15, 127);
            dgCalculatedUnitPrice.Name = "dgCalculatedUnitPrice";
            dgCalculatedUnitPrice.RowHeadersWidth = 51;
            dgCalculatedUnitPrice.Size = new Size(504, 286);
            dgCalculatedUnitPrice.TabIndex = 5;
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
            // energyTypeIdDataGridViewTextBoxColumn
            // 
            energyTypeIdDataGridViewTextBoxColumn.DataPropertyName = "EnergyTypeId";
            energyTypeIdDataGridViewTextBoxColumn.HeaderText = "EnergyTypeId";
            energyTypeIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            energyTypeIdDataGridViewTextBoxColumn.Name = "energyTypeIdDataGridViewTextBoxColumn";
            energyTypeIdDataGridViewTextBoxColumn.Visible = false;
            energyTypeIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // tariffGroupIdDataGridViewTextBoxColumn
            // 
            tariffGroupIdDataGridViewTextBoxColumn.DataPropertyName = "TariffGroupId";
            tariffGroupIdDataGridViewTextBoxColumn.HeaderText = "TariffGroupId";
            tariffGroupIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            tariffGroupIdDataGridViewTextBoxColumn.Name = "tariffGroupIdDataGridViewTextBoxColumn";
            tariffGroupIdDataGridViewTextBoxColumn.Visible = false;
            tariffGroupIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            priceDataGridViewTextBoxColumn.HeaderText = "Price";
            priceDataGridViewTextBoxColumn.MinimumWidth = 6;
            priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            priceDataGridViewTextBoxColumn.Width = 125;
            // 
            // yearDataGridViewTextBoxColumn
            // 
            yearDataGridViewTextBoxColumn.DataPropertyName = "Year";
            yearDataGridViewTextBoxColumn.HeaderText = "Year";
            yearDataGridViewTextBoxColumn.MinimumWidth = 6;
            yearDataGridViewTextBoxColumn.Name = "yearDataGridViewTextBoxColumn";
            yearDataGridViewTextBoxColumn.Width = 125;
            // 
            // cboEnergyType
            // 
            cboEnergyType.DataSource = bsEnergyTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            cboEnergyType.Location = new Point(130, 45);
            cboEnergyType.Margin = new Padding(3, 2, 3, 2);
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.Size = new Size(210, 28);
            cboEnergyType.TabIndex = 2;
            cboEnergyType.ValueMember = "Id";
            cboEnergyType.SelectedIndexChanged += cboEnergyType_SelectedIndexChanged;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            lblEnergyType.AutoSize = true;
            lblEnergyType.ImeMode = ImeMode.NoControl;
            lblEnergyType.Location = new Point(17, 48);
            lblEnergyType.Name = "lblEnergyType";
            lblEnergyType.Size = new Size(90, 20);
            lblEnergyType.TabIndex = 1;
            lblEnergyType.Text = "Energy type:";
            // 
            // gbCalculatedUnitPrice
            // 
            gbCalculatedUnitPrice.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gbCalculatedUnitPrice.Controls.Add(label1);
            gbCalculatedUnitPrice.Controls.Add(nudYear);
            gbCalculatedUnitPrice.Controls.Add(txtPrice);
            gbCalculatedUnitPrice.Controls.Add(lblPrice);
            gbCalculatedUnitPrice.Location = new Point(525, 157);
            gbCalculatedUnitPrice.Name = "gbCalculatedUnitPrice";
            gbCalculatedUnitPrice.Size = new Size(255, 256);
            gbCalculatedUnitPrice.TabIndex = 6;
            gbCalculatedUnitPrice.TabStop = false;
            gbCalculatedUnitPrice.Text = "Calculated Unit Price";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ImeMode = ImeMode.NoControl;
            label1.Location = new Point(27, 42);
            label1.Name = "label1";
            label1.Size = new Size(40, 20);
            label1.TabIndex = 0;
            label1.Text = "Year:";
            // 
            // nudYear
            // 
            nudYear.DataBindings.Add(new Binding("Value", bsCalculatedUnitPrice, "Year", true));
            nudYear.Location = new Point(89, 40);
            nudYear.Maximum = new decimal(new int[] { 9999, 0, 0, 0 });
            nudYear.Minimum = new decimal(new int[] { 1900, 0, 0, 0 });
            nudYear.Name = "nudYear";
            nudYear.Size = new Size(130, 27);
            nudYear.TabIndex = 1;
            nudYear.Value = new decimal(new int[] { 1900, 0, 0, 0 });
            // 
            // txtPrice
            // 
            txtPrice.DataBindings.Add(new Binding("Text", bsCalculatedUnitPrice, "Price", true));
            txtPrice.Location = new Point(89, 73);
            txtPrice.Name = "txtPrice";
            txtPrice.Size = new Size(130, 27);
            txtPrice.TabIndex = 3;
            // 
            // lblPrice
            // 
            lblPrice.AutoSize = true;
            lblPrice.ImeMode = ImeMode.NoControl;
            lblPrice.Location = new Point(27, 76);
            lblPrice.Name = "lblPrice";
            lblPrice.Size = new Size(44, 20);
            lblPrice.TabIndex = 2;
            lblPrice.Text = "Price:";
            // 
            // cboTarifGroup
            // 
            cboTarifGroup.DataSource = bsTarifGroups;
            cboTarifGroup.DisplayMember = "Description";
            cboTarifGroup.FormattingEnabled = true;
            cboTarifGroup.Location = new Point(130, 79);
            cboTarifGroup.Margin = new Padding(3, 4, 3, 4);
            cboTarifGroup.Name = "cboTarifGroup";
            cboTarifGroup.Size = new Size(210, 28);
            cboTarifGroup.TabIndex = 4;
            cboTarifGroup.ValueMember = "Id";
            cboTarifGroup.SelectedIndexChanged += cboTarifGroup_SelectedIndexChanged;
            // 
            // bsTarifGroups
            // 
            bsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // lblTarifGroup
            // 
            lblTarifGroup.AutoSize = true;
            lblTarifGroup.ImeMode = ImeMode.NoControl;
            lblTarifGroup.Location = new Point(17, 82);
            lblTarifGroup.Name = "lblTarifGroup";
            lblTarifGroup.Size = new Size(84, 20);
            lblTarifGroup.TabIndex = 3;
            lblTarifGroup.Text = "Tarif group:";
            // 
            // frmCalculatedUnitPrice
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(792, 450);
            Controls.Add(cboTarifGroup);
            Controls.Add(lblTarifGroup);
            Controls.Add(gbCalculatedUnitPrice);
            Controls.Add(cboEnergyType);
            Controls.Add(lblEnergyType);
            Controls.Add(dgCalculatedUnitPrice);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmCalculatedUnitPrice";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "Calculated Unit Price";
            FormClosing += frmCalculatedUnitPrice_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsCalculatedUnitPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgCalculatedUnitPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            gbCalculatedUnitPrice.ResumeLayout(false);
            gbCalculatedUnitPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudYear).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbSave;
        private ToolStripButton tbsCancel;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbRefresh;
        private ToolStripButton tsbClose;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private BindingSource bsCalculatedUnitPrice;
        private DataGridView dgCalculatedUnitPrice;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn energyTypeIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn tariffGroupIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn yearDataGridViewTextBoxColumn;
        private ComboBox cboEnergyType;
        private Label lblEnergyType;
        private GroupBox gbCalculatedUnitPrice;
        private TextBox txtPrice;
        private Label lblPrice;
        private Label label1;
        private NumericUpDown nudYear;
        private ComboBox cboTarifGroup;
        private Label lblTarifGroup;
        private BindingSource bsTarifGroups;
        private BindingSource bsEnergyTypes;
    }
}