namespace WinFormsEF.Views
{
    partial class frmEnergyTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnergyTypes));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgEnergyTypes = new System.Windows.Forms.DataGridView();
            this.EnergyTypeName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.gbEnergyType = new System.Windows.Forms.GroupBox();
            this.lblUnit = new System.Windows.Forms.Label();
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.bsUnits = new System.Windows.Forms.BindingSource(this.components);
            this.chkHasEnergyReturn = new System.Windows.Forms.CheckBox();
            this.chkHasNormalAndLow = new System.Windows.Forms.CheckBox();
            this.chkDefaultType = new System.Windows.Forms.CheckBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblName = new System.Windows.Forms.Label();
            this.gbColors = new System.Windows.Forms.GroupBox();
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.txtReturnDeliveryHigh = new System.Windows.Forms.TextBox();
            this.txtReturnDeliveryLow = new System.Windows.Forms.TextBox();
            this.txtColorHigh = new System.Windows.Forms.TextBox();
            this.txtColorLow = new System.Windows.Forms.TextBox();
            this.lblReturnDeliveryHigh = new System.Windows.Forms.Label();
            this.lblReturnDeliveryLow = new System.Windows.Forms.Label();
            this.lblColorHigh = new System.Windows.Forms.Label();
            this.lblColorLow = new System.Windows.Forms.Label();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEnergyTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            this.gbEnergyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsUnits)).BeginInit();
            this.gbColors.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Name = "statusStrip1";
            this.toolTip1.SetToolTip(this.statusStrip1, resources.GetString("statusStrip1.ToolTip"));
            // 
            // toolStrip1
            // 
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbSave,
            this.tbsCancel,
            this.tsbDelete,
            this.tsbRefresh,
            this.tsbClose});
            this.toolStrip1.Name = "toolStrip1";
            this.toolTip1.SetToolTip(this.toolStrip1, resources.GetString("toolStrip1.ToolTip"));
            // 
            // tsbAdd
            // 
            resources.ApplyResources(this.tsbAdd, "tsbAdd");
            this.tsbAdd.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbSave
            // 
            resources.ApplyResources(this.tsbSave, "tsbSave");
            this.tsbSave.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tbsCancel
            // 
            resources.ApplyResources(this.tbsCancel, "tbsCancel");
            this.tbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            this.tbsCancel.Name = "tbsCancel";
            this.tbsCancel.Click += new System.EventHandler(this.tbsCancel_Click);
            // 
            // tsbDelete
            // 
            resources.ApplyResources(this.tsbDelete, "tsbDelete");
            this.tsbDelete.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbRefresh
            // 
            resources.ApplyResources(this.tsbRefresh, "tsbRefresh");
            this.tsbRefresh.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbClose
            // 
            resources.ApplyResources(this.tsbClose, "tsbClose");
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Click += new System.EventHandler(this.TsbClose_Click);
            // 
            // dgEnergyTypes
            // 
            resources.ApplyResources(this.dgEnergyTypes, "dgEnergyTypes");
            this.dgEnergyTypes.AllowUserToAddRows = false;
            this.dgEnergyTypes.AllowUserToDeleteRows = false;
            this.dgEnergyTypes.AutoGenerateColumns = false;
            this.dgEnergyTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgEnergyTypes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.EnergyTypeName,
            this.UnitName});
            this.dgEnergyTypes.DataSource = this.bsEnergyTypes;
            this.dgEnergyTypes.Name = "dgEnergyTypes";
            this.dgEnergyTypes.RowTemplate.Height = 29;
            this.toolTip1.SetToolTip(this.dgEnergyTypes, resources.GetString("dgEnergyTypes.ToolTip"));
            // 
            // EnergyTypeName
            // 
            this.EnergyTypeName.DataPropertyName = "Name";
            resources.ApplyResources(this.EnergyTypeName, "EnergyTypeName");
            this.EnergyTypeName.Name = "EnergyTypeName";
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            resources.ApplyResources(this.UnitName, "UnitName");
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            this.bsEnergyTypes.CurrentChanged += new System.EventHandler(this.bsEnergyTypes_CurrentChanged);
            // 
            // gbEnergyType
            // 
            resources.ApplyResources(this.gbEnergyType, "gbEnergyType");
            this.gbEnergyType.Controls.Add(this.lblUnit);
            this.gbEnergyType.Controls.Add(this.cboUnit);
            this.gbEnergyType.Controls.Add(this.chkHasEnergyReturn);
            this.gbEnergyType.Controls.Add(this.chkHasNormalAndLow);
            this.gbEnergyType.Controls.Add(this.chkDefaultType);
            this.gbEnergyType.Controls.Add(this.txtName);
            this.gbEnergyType.Controls.Add(this.lblName);
            this.gbEnergyType.Name = "gbEnergyType";
            this.gbEnergyType.TabStop = false;
            this.toolTip1.SetToolTip(this.gbEnergyType, resources.GetString("gbEnergyType.ToolTip"));
            // 
            // lblUnit
            // 
            resources.ApplyResources(this.lblUnit, "lblUnit");
            this.lblUnit.Name = "lblUnit";
            this.toolTip1.SetToolTip(this.lblUnit, resources.GetString("lblUnit.ToolTip"));
            // 
            // cboUnit
            // 
            resources.ApplyResources(this.cboUnit, "cboUnit");
            this.cboUnit.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsEnergyTypes, "UnitId", true));
            this.cboUnit.DataSource = this.bsUnits;
            this.cboUnit.DisplayMember = "Description";
            this.cboUnit.FormattingEnabled = true;
            this.cboUnit.Name = "cboUnit";
            this.toolTip1.SetToolTip(this.cboUnit, resources.GetString("cboUnit.ToolTip"));
            this.cboUnit.ValueMember = "Id";
            // 
            // bsUnits
            // 
            this.bsUnits.DataSource = typeof(EnergyUse.Models.Unit);
            // 
            // chkHasEnergyReturn
            // 
            resources.ApplyResources(this.chkHasEnergyReturn, "chkHasEnergyReturn");
            this.chkHasEnergyReturn.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsEnergyTypes, "HasEnergyReturn", true));
            this.chkHasEnergyReturn.Name = "chkHasEnergyReturn";
            this.toolTip1.SetToolTip(this.chkHasEnergyReturn, resources.GetString("chkHasEnergyReturn.ToolTip"));
            this.chkHasEnergyReturn.UseVisualStyleBackColor = true;
            this.chkHasEnergyReturn.CheckedChanged += new System.EventHandler(this.chkHasEnergyReturn_CheckedChanged);
            // 
            // chkHasNormalAndLow
            // 
            resources.ApplyResources(this.chkHasNormalAndLow, "chkHasNormalAndLow");
            this.chkHasNormalAndLow.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsEnergyTypes, "HasNormalAndLow", true));
            this.chkHasNormalAndLow.Name = "chkHasNormalAndLow";
            this.toolTip1.SetToolTip(this.chkHasNormalAndLow, resources.GetString("chkHasNormalAndLow.ToolTip"));
            this.chkHasNormalAndLow.UseVisualStyleBackColor = true;
            this.chkHasNormalAndLow.CheckedChanged += new System.EventHandler(this.chkHasNormalAndLow_CheckedChanged);
            // 
            // chkDefaultType
            // 
            resources.ApplyResources(this.chkDefaultType, "chkDefaultType");
            this.chkDefaultType.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsEnergyTypes, "DefaultType", true));
            this.chkDefaultType.Name = "chkDefaultType";
            this.toolTip1.SetToolTip(this.chkDefaultType, resources.GetString("chkDefaultType.ToolTip"));
            this.chkDefaultType.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsEnergyTypes, "Name", true));
            this.txtName.Name = "txtName";
            this.toolTip1.SetToolTip(this.txtName, resources.GetString("txtName.ToolTip"));
            // 
            // lblName
            // 
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Name = "lblName";
            this.toolTip1.SetToolTip(this.lblName, resources.GetString("lblName.ToolTip"));
            // 
            // gbColors
            // 
            resources.ApplyResources(this.gbColors, "gbColors");
            this.gbColors.Controls.Add(this.txtColor);
            this.gbColors.Controls.Add(this.lblColor);
            this.gbColors.Controls.Add(this.txtReturnDeliveryHigh);
            this.gbColors.Controls.Add(this.txtReturnDeliveryLow);
            this.gbColors.Controls.Add(this.txtColorHigh);
            this.gbColors.Controls.Add(this.txtColorLow);
            this.gbColors.Controls.Add(this.lblReturnDeliveryHigh);
            this.gbColors.Controls.Add(this.lblReturnDeliveryLow);
            this.gbColors.Controls.Add(this.lblColorHigh);
            this.gbColors.Controls.Add(this.lblColorLow);
            this.gbColors.Name = "gbColors";
            this.gbColors.TabStop = false;
            this.toolTip1.SetToolTip(this.gbColors, resources.GetString("gbColors.ToolTip"));
            // 
            // txtColor
            // 
            resources.ApplyResources(this.txtColor, "txtColor");
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Tag = "Color";
            this.toolTip1.SetToolTip(this.txtColor, resources.GetString("txtColor.ToolTip"));
            this.txtColor.Click += new System.EventHandler(this.txtColor_Click);
            // 
            // lblColor
            // 
            resources.ApplyResources(this.lblColor, "lblColor");
            this.lblColor.Name = "lblColor";
            this.toolTip1.SetToolTip(this.lblColor, resources.GetString("lblColor.ToolTip"));
            // 
            // txtReturnDeliveryHigh
            // 
            resources.ApplyResources(this.txtReturnDeliveryHigh, "txtReturnDeliveryHigh");
            this.txtReturnDeliveryHigh.Name = "txtReturnDeliveryHigh";
            this.txtReturnDeliveryHigh.ReadOnly = true;
            this.txtReturnDeliveryHigh.Tag = "ReturnNormal";
            this.toolTip1.SetToolTip(this.txtReturnDeliveryHigh, resources.GetString("txtReturnDeliveryHigh.ToolTip"));
            this.txtReturnDeliveryHigh.Click += new System.EventHandler(this.txtReturnDeliveryHigh_Click);
            // 
            // txtReturnDeliveryLow
            // 
            resources.ApplyResources(this.txtReturnDeliveryLow, "txtReturnDeliveryLow");
            this.txtReturnDeliveryLow.Name = "txtReturnDeliveryLow";
            this.txtReturnDeliveryLow.ReadOnly = true;
            this.txtReturnDeliveryLow.Tag = "ReturnLow";
            this.toolTip1.SetToolTip(this.txtReturnDeliveryLow, resources.GetString("txtReturnDeliveryLow.ToolTip"));
            this.txtReturnDeliveryLow.Click += new System.EventHandler(this.txtReturnDeliveryLow_Click);
            // 
            // txtColorHigh
            // 
            resources.ApplyResources(this.txtColorHigh, "txtColorHigh");
            this.txtColorHigh.Name = "txtColorHigh";
            this.txtColorHigh.ReadOnly = true;
            this.txtColorHigh.Tag = "ColorNormal";
            this.toolTip1.SetToolTip(this.txtColorHigh, resources.GetString("txtColorHigh.ToolTip"));
            this.txtColorHigh.Click += new System.EventHandler(this.txtColorHigh_Click);
            // 
            // txtColorLow
            // 
            resources.ApplyResources(this.txtColorLow, "txtColorLow");
            this.txtColorLow.Name = "txtColorLow";
            this.txtColorLow.ReadOnly = true;
            this.txtColorLow.Tag = "ColorLow";
            this.toolTip1.SetToolTip(this.txtColorLow, resources.GetString("txtColorLow.ToolTip"));
            this.txtColorLow.Click += new System.EventHandler(this.txtColorLow_Click);
            // 
            // lblReturnDeliveryHigh
            // 
            resources.ApplyResources(this.lblReturnDeliveryHigh, "lblReturnDeliveryHigh");
            this.lblReturnDeliveryHigh.Name = "lblReturnDeliveryHigh";
            this.toolTip1.SetToolTip(this.lblReturnDeliveryHigh, resources.GetString("lblReturnDeliveryHigh.ToolTip"));
            // 
            // lblReturnDeliveryLow
            // 
            resources.ApplyResources(this.lblReturnDeliveryLow, "lblReturnDeliveryLow");
            this.lblReturnDeliveryLow.Name = "lblReturnDeliveryLow";
            this.toolTip1.SetToolTip(this.lblReturnDeliveryLow, resources.GetString("lblReturnDeliveryLow.ToolTip"));
            // 
            // lblColorHigh
            // 
            resources.ApplyResources(this.lblColorHigh, "lblColorHigh");
            this.lblColorHigh.Name = "lblColorHigh";
            this.toolTip1.SetToolTip(this.lblColorHigh, resources.GetString("lblColorHigh.ToolTip"));
            // 
            // lblColorLow
            // 
            resources.ApplyResources(this.lblColorLow, "lblColorLow");
            this.lblColorLow.Name = "lblColorLow";
            this.toolTip1.SetToolTip(this.lblColorLow, resources.GetString("lblColorLow.ToolTip"));
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(this.nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // frmEnergyTypes
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbColors);
            this.Controls.Add(this.gbEnergyType);
            this.Controls.Add(this.dgEnergyTypes);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmEnergyTypes";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.toolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEnergyTypes_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgEnergyTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            this.gbEnergyType.ResumeLayout(false);
            this.gbEnergyType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsUnits)).EndInit();
            this.gbColors.ResumeLayout(false);
            this.gbColors.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private StatusStrip statusStrip1;
        private ToolStrip toolStrip1;
        private ToolTip toolTip1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbSave;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbRefresh;
        private ToolStripButton tsbClose;
        private BindingSource bsEnergyTypes;
        private DataGridView dgEnergyTypes;
        private GroupBox gbEnergyType;
        private GroupBox gbColors;
        private Label lblUnit;
        private ComboBox cboUnit;
        private CheckBox chkHasEnergyReturn;
        private CheckBox chkHasNormalAndLow;
        private CheckBox chkDefaultType;
        private TextBox txtName;
        private Label lblName;
        private TextBox txtColor;
        private Label lblColor;
        private TextBox txtReturnDeliveryHigh;
        private TextBox txtReturnDeliveryLow;
        private TextBox txtColorHigh;
        private TextBox txtColorLow;
        private Label lblReturnDeliveryHigh;
        private Label lblReturnDeliveryLow;
        private Label lblColorHigh;
        private Label lblColorLow;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private BindingSource bsUnits;
        private ToolStripButton tbsCancel;
        private ColorDialog colorDialog1;
        private DataGridViewTextBoxColumn EnergyTypeName;
        private DataGridViewTextBoxColumn UnitName;
    }
}