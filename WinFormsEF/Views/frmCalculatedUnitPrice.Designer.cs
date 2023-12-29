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
            this.components = new System.ComponentModel.Container();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.bsCalculatedUnitPrice = new System.Windows.Forms.BindingSource(this.components);
            this.dgCalculatedUnitPrice = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.energyTypeIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tariffGroupIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.yearDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.gbCalculatedUnitPrice = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nudYear = new System.Windows.Forms.NumericUpDown();
            this.txtPrice = new System.Windows.Forms.TextBox();
            this.lblPrice = new System.Windows.Forms.Label();
            this.cboTarifGroup = new System.Windows.Forms.ComboBox();
            this.bsTarifGroups = new System.Windows.Forms.BindingSource(this.components);
            this.lblTarifGroup = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCalculatedUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCalculatedUnitPrice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            this.gbCalculatedUnitPrice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTarifGroups)).BeginInit();
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
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 27);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(61, 24);
            this.tsbAdd.Text = "&Add";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            this.tsbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Size = new System.Drawing.Size(64, 24);
            this.tsbSave.Text = "&Save";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tbsCancel
            // 
            this.tbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            this.tbsCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbsCancel.Name = "tbsCancel";
            this.tbsCancel.Size = new System.Drawing.Size(77, 24);
            this.tbsCancel.Text = "&Cancel";
            this.tbsCancel.Click += new System.EventHandler(this.tbsCancel_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(77, 24);
            this.tsbDelete.Text = "&Delete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(82, 24);
            this.tsbRefresh.Text = "&Refresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.tsbClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Size = new System.Drawing.Size(69, 24);
            this.tsbClose.Text = "&Close";
            this.tsbClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tsbClose.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(792, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // bsCalculatedUnitPrice
            // 
            this.bsCalculatedUnitPrice.DataSource = typeof(EnergyUse.Models.CalculatedUnitPrice);
            // 
            // dgCalculatedUnitPrice
            // 
            this.dgCalculatedUnitPrice.AllowUserToAddRows = false;
            this.dgCalculatedUnitPrice.AllowUserToDeleteRows = false;
            this.dgCalculatedUnitPrice.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgCalculatedUnitPrice.AutoGenerateColumns = false;
            this.dgCalculatedUnitPrice.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCalculatedUnitPrice.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.energyTypeIdDataGridViewTextBoxColumn,
            this.tariffGroupIdDataGridViewTextBoxColumn,
            this.priceDataGridViewTextBoxColumn,
            this.yearDataGridViewTextBoxColumn});
            this.dgCalculatedUnitPrice.DataSource = this.bsCalculatedUnitPrice;
            this.dgCalculatedUnitPrice.Location = new System.Drawing.Point(15, 127);
            this.dgCalculatedUnitPrice.Name = "dgCalculatedUnitPrice";
            this.dgCalculatedUnitPrice.RowHeadersWidth = 51;
            this.dgCalculatedUnitPrice.RowTemplate.Height = 29;
            this.dgCalculatedUnitPrice.Size = new System.Drawing.Size(504, 286);
            this.dgCalculatedUnitPrice.TabIndex = 4;
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
            // energyTypeIdDataGridViewTextBoxColumn
            // 
            this.energyTypeIdDataGridViewTextBoxColumn.DataPropertyName = "EnergyTypeId";
            this.energyTypeIdDataGridViewTextBoxColumn.HeaderText = "EnergyTypeId";
            this.energyTypeIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.energyTypeIdDataGridViewTextBoxColumn.Name = "energyTypeIdDataGridViewTextBoxColumn";
            this.energyTypeIdDataGridViewTextBoxColumn.Visible = false;
            this.energyTypeIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // tariffGroupIdDataGridViewTextBoxColumn
            // 
            this.tariffGroupIdDataGridViewTextBoxColumn.DataPropertyName = "TariffGroupId";
            this.tariffGroupIdDataGridViewTextBoxColumn.HeaderText = "TariffGroupId";
            this.tariffGroupIdDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.tariffGroupIdDataGridViewTextBoxColumn.Name = "tariffGroupIdDataGridViewTextBoxColumn";
            this.tariffGroupIdDataGridViewTextBoxColumn.Visible = false;
            this.tariffGroupIdDataGridViewTextBoxColumn.Width = 125;
            // 
            // priceDataGridViewTextBoxColumn
            // 
            this.priceDataGridViewTextBoxColumn.DataPropertyName = "Price";
            this.priceDataGridViewTextBoxColumn.HeaderText = "Price";
            this.priceDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.priceDataGridViewTextBoxColumn.Name = "priceDataGridViewTextBoxColumn";
            this.priceDataGridViewTextBoxColumn.Width = 125;
            // 
            // yearDataGridViewTextBoxColumn
            // 
            this.yearDataGridViewTextBoxColumn.DataPropertyName = "Year";
            this.yearDataGridViewTextBoxColumn.HeaderText = "Year";
            this.yearDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.yearDataGridViewTextBoxColumn.Name = "yearDataGridViewTextBoxColumn";
            this.yearDataGridViewTextBoxColumn.Width = 125;
            // 
            // cboEnergyType
            // 
            this.cboEnergyType.DataSource = this.bsEnergyTypes;
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            this.cboEnergyType.Location = new System.Drawing.Point(130, 45);
            this.cboEnergyType.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cboEnergyType.Name = "cboEnergyType";
            this.cboEnergyType.Size = new System.Drawing.Size(210, 28);
            this.cboEnergyType.TabIndex = 27;
            this.cboEnergyType.ValueMember = "Id";
            this.cboEnergyType.SelectedIndexChanged += new System.EventHandler(this.cboEnergyType_SelectedIndexChanged);
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            this.lblEnergyType.AutoSize = true;
            this.lblEnergyType.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblEnergyType.Location = new System.Drawing.Point(17, 48);
            this.lblEnergyType.Name = "lblEnergyType";
            this.lblEnergyType.Size = new System.Drawing.Size(90, 20);
            this.lblEnergyType.TabIndex = 26;
            this.lblEnergyType.Text = "Energy type:";
            // 
            // gbCalculatedUnitPrice
            // 
            this.gbCalculatedUnitPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbCalculatedUnitPrice.Controls.Add(this.label1);
            this.gbCalculatedUnitPrice.Controls.Add(this.nudYear);
            this.gbCalculatedUnitPrice.Controls.Add(this.txtPrice);
            this.gbCalculatedUnitPrice.Controls.Add(this.lblPrice);
            this.gbCalculatedUnitPrice.Location = new System.Drawing.Point(525, 157);
            this.gbCalculatedUnitPrice.Name = "gbCalculatedUnitPrice";
            this.gbCalculatedUnitPrice.Size = new System.Drawing.Size(255, 256);
            this.gbCalculatedUnitPrice.TabIndex = 28;
            this.gbCalculatedUnitPrice.TabStop = false;
            this.gbCalculatedUnitPrice.Text = "Calculated Unit Price";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.label1.Location = new System.Drawing.Point(27, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 20);
            this.label1.TabIndex = 34;
            this.label1.Text = "Year:";
            // 
            // nudYear
            // 
            this.nudYear.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsCalculatedUnitPrice, "Year", true));
            this.nudYear.Location = new System.Drawing.Point(89, 40);
            this.nudYear.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.nudYear.Minimum = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            this.nudYear.Name = "nudYear";
            this.nudYear.Size = new System.Drawing.Size(130, 27);
            this.nudYear.TabIndex = 33;
            this.nudYear.Value = new decimal(new int[] {
            1900,
            0,
            0,
            0});
            // 
            // txtPrice
            // 
            this.txtPrice.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCalculatedUnitPrice, "Price", true));
            this.txtPrice.Location = new System.Drawing.Point(89, 73);
            this.txtPrice.Name = "txtPrice";
            this.txtPrice.Size = new System.Drawing.Size(130, 27);
            this.txtPrice.TabIndex = 32;
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblPrice.Location = new System.Drawing.Point(27, 76);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(44, 20);
            this.lblPrice.TabIndex = 31;
            this.lblPrice.Text = "Price:";
            // 
            // cboTarifGroup
            // 
            this.cboTarifGroup.DataSource = this.bsTarifGroups;
            this.cboTarifGroup.DisplayMember = "Description";
            this.cboTarifGroup.FormattingEnabled = true;
            this.cboTarifGroup.Location = new System.Drawing.Point(130, 79);
            this.cboTarifGroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboTarifGroup.Name = "cboTarifGroup";
            this.cboTarifGroup.Size = new System.Drawing.Size(210, 28);
            this.cboTarifGroup.TabIndex = 31;
            this.cboTarifGroup.ValueMember = "Id";
            this.cboTarifGroup.SelectedIndexChanged += new System.EventHandler(this.cboTarifGroup_SelectedIndexChanged);
            // 
            // bsTarifGroups
            // 
            this.bsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // lblTarifGroup
            // 
            this.lblTarifGroup.AutoSize = true;
            this.lblTarifGroup.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.lblTarifGroup.Location = new System.Drawing.Point(17, 82);
            this.lblTarifGroup.Name = "lblTarifGroup";
            this.lblTarifGroup.Size = new System.Drawing.Size(84, 20);
            this.lblTarifGroup.TabIndex = 30;
            this.lblTarifGroup.Text = "Tarif group:";
            // 
            // frmCalculatedUnitPrice
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 450);
            this.Controls.Add(this.cboTarifGroup);
            this.Controls.Add(this.lblTarifGroup);
            this.Controls.Add(this.gbCalculatedUnitPrice);
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.dgCalculatedUnitPrice);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmCalculatedUnitPrice";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Calculated Unit Price";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCalculatedUnitPrice_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCalculatedUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCalculatedUnitPrice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            this.gbCalculatedUnitPrice.ResumeLayout(false);
            this.gbCalculatedUnitPrice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudYear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsTarifGroups)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

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