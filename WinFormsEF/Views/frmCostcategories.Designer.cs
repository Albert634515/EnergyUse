namespace WinFormsEF.Views
{
    partial class frmCostcategories
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCostcategories));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAddCostCategory = new System.Windows.Forms.ToolStripButton();
            this.tsbSaveCostCategory = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDeleteCostCategory = new System.Windows.Forms.ToolStripButton();
            this.tsbRefreshCostCategory = new System.Windows.Forms.ToolStripButton();
            this.tsbCloseCostCategory = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgCostCategories = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sortOrderDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.UnitName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsCostCategories = new System.Windows.Forms.BindingSource(this.components);
            this.gbCostCategory = new System.Windows.Forms.GroupBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.dtEndDate = new System.Windows.Forms.DateTimePicker();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtStartDate = new System.Windows.Forms.DateTimePicker();
            this.chkNotCalculateOverReturnValue = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkCanBeNegative = new System.Windows.Forms.CheckBox();
            this.chkCalculateVat = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cboTariffGroup = new System.Windows.Forms.ComboBox();
            this.bsTariffGroups = new System.Windows.Forms.BindingSource(this.components);
            this.cboUnit = new System.Windows.Forms.ComboBox();
            this.bsUnits = new System.Windows.Forms.BindingSource(this.components);
            this.lblUnit = new System.Windows.Forms.Label();
            this.lblSubType = new System.Windows.Forms.Label();
            this.cboEnergySubType = new System.Windows.Forms.ComboBox();
            this.bsEnergySubType = new System.Windows.Forms.BindingSource(this.components);
            this.lblCalculationType = new System.Windows.Forms.Label();
            this.cboCalculationType = new System.Windows.Forms.ComboBox();
            this.bsCalculationType = new System.Windows.Forms.BindingSource(this.components);
            this.txtColor = new System.Windows.Forms.TextBox();
            this.lblColor = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtSortOrder = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCostCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCostCategories)).BeginInit();
            this.gbCostCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTariffGroups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUnits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergySubType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCalculationType)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAddCostCategory,
            this.tsbSaveCostCategory,
            this.tbsCancel,
            this.tsbDeleteCostCategory,
            this.tsbRefreshCostCategory,
            this.tsbCloseCostCategory});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbAddCostCategory
            // 
            this.tsbAddCostCategory.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(this.tsbAddCostCategory, "tsbAddCostCategory");
            this.tsbAddCostCategory.Name = "tsbAddCostCategory";
            this.tsbAddCostCategory.Click += new System.EventHandler(this.tsbAddCostCategory_Click);
            // 
            // tsbSaveCostCategory
            // 
            this.tsbSaveCostCategory.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(this.tsbSaveCostCategory, "tsbSaveCostCategory");
            this.tsbSaveCostCategory.Name = "tsbSaveCostCategory";
            this.tsbSaveCostCategory.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tbsCancel
            // 
            this.tbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(this.tbsCancel, "tbsCancel");
            this.tbsCancel.Name = "tbsCancel";
            this.tbsCancel.Click += new System.EventHandler(this.tbsCancel_Click);
            // 
            // tsbDeleteCostCategory
            // 
            this.tsbDeleteCostCategory.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(this.tsbDeleteCostCategory, "tsbDeleteCostCategory");
            this.tsbDeleteCostCategory.Name = "tsbDeleteCostCategory";
            this.tsbDeleteCostCategory.Click += new System.EventHandler(this.tsbDeleteCostCategory_Click);
            // 
            // tsbRefreshCostCategory
            // 
            this.tsbRefreshCostCategory.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(this.tsbRefreshCostCategory, "tsbRefreshCostCategory");
            this.tsbRefreshCostCategory.Name = "tsbRefreshCostCategory";
            this.tsbRefreshCostCategory.Click += new System.EventHandler(this.tsbRefreshCostCategory_Click);
            // 
            // tsbCloseCostCategory
            // 
            this.tsbCloseCostCategory.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbCloseCostCategory.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbCloseCostCategory, "tsbCloseCostCategory");
            this.tsbCloseCostCategory.Name = "tsbCloseCostCategory";
            this.tsbCloseCostCategory.Click += new System.EventHandler(this.tsbCloseCostCategory_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // cboEnergyType
            // 
            this.cboEnergyType.DataSource = this.bsEnergyTypes;
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.Name = "cboEnergyType";
            this.cboEnergyType.ValueMember = "Id";
            this.cboEnergyType.SelectedIndexChanged += new System.EventHandler(this.cboEnergyType_SelectedIndexChanged);
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            // 
            // dgCostCategories
            // 
            this.dgCostCategories.AllowUserToAddRows = false;
            this.dgCostCategories.AllowUserToDeleteRows = false;
            this.dgCostCategories.AutoGenerateColumns = false;
            this.dgCostCategories.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgCostCategories.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.sortOrderDataGridViewTextBoxColumn,
            this.UnitName});
            this.dgCostCategories.DataSource = this.bsCostCategories;
            resources.ApplyResources(this.dgCostCategories, "dgCostCategories");
            this.dgCostCategories.Name = "dgCostCategories";
            this.dgCostCategories.RowTemplate.Height = 29;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(this.nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // sortOrderDataGridViewTextBoxColumn
            // 
            this.sortOrderDataGridViewTextBoxColumn.DataPropertyName = "SortOrder";
            resources.ApplyResources(this.sortOrderDataGridViewTextBoxColumn, "sortOrderDataGridViewTextBoxColumn");
            this.sortOrderDataGridViewTextBoxColumn.Name = "sortOrderDataGridViewTextBoxColumn";
            // 
            // UnitName
            // 
            this.UnitName.DataPropertyName = "UnitName";
            resources.ApplyResources(this.UnitName, "UnitName");
            this.UnitName.Name = "UnitName";
            this.UnitName.ReadOnly = true;
            // 
            // bsCostCategories
            // 
            this.bsCostCategories.DataSource = typeof(EnergyUse.Models.CostCategory);
            this.bsCostCategories.CurrentChanged += new System.EventHandler(this.bsCostCategories_CurrentChanged);
            // 
            // gbCostCategory
            // 
            resources.ApplyResources(this.gbCostCategory, "gbCostCategory");
            this.gbCostCategory.Controls.Add(this.lblEndDate);
            this.gbCostCategory.Controls.Add(this.dtEndDate);
            this.gbCostCategory.Controls.Add(this.lblStartDate);
            this.gbCostCategory.Controls.Add(this.dtStartDate);
            this.gbCostCategory.Controls.Add(this.chkNotCalculateOverReturnValue);
            this.gbCostCategory.Controls.Add(this.label2);
            this.gbCostCategory.Controls.Add(this.chkCanBeNegative);
            this.gbCostCategory.Controls.Add(this.chkCalculateVat);
            this.gbCostCategory.Controls.Add(this.label4);
            this.gbCostCategory.Controls.Add(this.label3);
            this.gbCostCategory.Controls.Add(this.cboTariffGroup);
            this.gbCostCategory.Controls.Add(this.cboUnit);
            this.gbCostCategory.Controls.Add(this.lblUnit);
            this.gbCostCategory.Controls.Add(this.lblSubType);
            this.gbCostCategory.Controls.Add(this.cboEnergySubType);
            this.gbCostCategory.Controls.Add(this.lblCalculationType);
            this.gbCostCategory.Controls.Add(this.cboCalculationType);
            this.gbCostCategory.Controls.Add(this.txtColor);
            this.gbCostCategory.Controls.Add(this.lblColor);
            this.gbCostCategory.Controls.Add(this.label1);
            this.gbCostCategory.Controls.Add(this.txtSortOrder);
            this.gbCostCategory.Controls.Add(this.txtDescription);
            this.gbCostCategory.Controls.Add(this.lblDescription);
            this.gbCostCategory.Name = "gbCostCategory";
            this.gbCostCategory.TabStop = false;
            // 
            // lblEndDate
            // 
            resources.ApplyResources(this.lblEndDate, "lblEndDate");
            this.lblEndDate.Name = "lblEndDate";
            // 
            // dtEndDate
            // 
            resources.ApplyResources(this.dtEndDate, "dtEndDate");
            this.dtEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsCostCategories, "End", true));
            this.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
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
            resources.ApplyResources(this.dtStartDate, "dtStartDate");
            this.dtStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsCostCategories, "Start", true));
            this.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtStartDate.Name = "dtStartDate";
            this.dtStartDate.Value = new System.DateTime(2022, 10, 1, 0, 0, 0, 0);
            // 
            // chkNotCalculateOverReturnValue
            // 
            resources.ApplyResources(this.chkNotCalculateOverReturnValue, "chkNotCalculateOverReturnValue");
            this.chkNotCalculateOverReturnValue.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsCostCategories, "NotCalculateOverReturn", true));
            this.chkNotCalculateOverReturnValue.Name = "chkNotCalculateOverReturnValue";
            this.chkNotCalculateOverReturnValue.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // chkCanBeNegative
            // 
            resources.ApplyResources(this.chkCanBeNegative, "chkCanBeNegative");
            this.chkCanBeNegative.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsCostCategories, "CanNotBeNegative", true));
            this.chkCanBeNegative.Name = "chkCanBeNegative";
            this.chkCanBeNegative.UseVisualStyleBackColor = true;
            // 
            // chkCalculateVat
            // 
            resources.ApplyResources(this.chkCalculateVat, "chkCalculateVat");
            this.chkCalculateVat.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsCostCategories, "CalculateVat", true));
            this.chkCalculateVat.Name = "chkCalculateVat";
            this.chkCalculateVat.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // cboTariffGroup
            // 
            this.cboTariffGroup.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsCostCategories, "TariffGroupId", true));
            this.cboTariffGroup.DataSource = this.bsTariffGroups;
            this.cboTariffGroup.DisplayMember = "Description";
            this.cboTariffGroup.FormattingEnabled = true;
            resources.ApplyResources(this.cboTariffGroup, "cboTariffGroup");
            this.cboTariffGroup.Name = "cboTariffGroup";
            this.cboTariffGroup.ValueMember = "Id";
            // 
            // bsTariffGroups
            // 
            this.bsTariffGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // cboUnit
            // 
            this.cboUnit.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsCostCategories, "UnitId", true));
            this.cboUnit.DataSource = this.bsUnits;
            this.cboUnit.DisplayMember = "Description";
            this.cboUnit.FormattingEnabled = true;
            resources.ApplyResources(this.cboUnit, "cboUnit");
            this.cboUnit.Name = "cboUnit";
            this.cboUnit.ValueMember = "Id";
            // 
            // bsUnits
            // 
            this.bsUnits.DataSource = typeof(EnergyUse.Models.Unit);
            // 
            // lblUnit
            // 
            resources.ApplyResources(this.lblUnit, "lblUnit");
            this.lblUnit.Name = "lblUnit";
            // 
            // lblSubType
            // 
            resources.ApplyResources(this.lblSubType, "lblSubType");
            this.lblSubType.Name = "lblSubType";
            // 
            // cboEnergySubType
            // 
            this.cboEnergySubType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsCostCategories, "EnergySubTypeId", true));
            this.cboEnergySubType.DataSource = this.bsEnergySubType;
            this.cboEnergySubType.DisplayMember = "Description";
            this.cboEnergySubType.FormattingEnabled = true;
            resources.ApplyResources(this.cboEnergySubType, "cboEnergySubType");
            this.cboEnergySubType.Name = "cboEnergySubType";
            this.cboEnergySubType.ValueMember = "Id";
            // 
            // bsEnergySubType
            // 
            this.bsEnergySubType.DataSource = typeof(EnergyUse.Models.EnergySubType);
            // 
            // lblCalculationType
            // 
            resources.ApplyResources(this.lblCalculationType, "lblCalculationType");
            this.lblCalculationType.Name = "lblCalculationType";
            // 
            // cboCalculationType
            // 
            this.cboCalculationType.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsCostCategories, "CalculationTypeId", true));
            this.cboCalculationType.DataSource = this.bsCalculationType;
            this.cboCalculationType.DisplayMember = "Description";
            this.cboCalculationType.FormattingEnabled = true;
            resources.ApplyResources(this.cboCalculationType, "cboCalculationType");
            this.cboCalculationType.Name = "cboCalculationType";
            this.cboCalculationType.ValueMember = "Id";
            // 
            // bsCalculationType
            // 
            this.bsCalculationType.DataSource = typeof(EnergyUse.Models.CalculationType);
            // 
            // txtColor
            // 
            resources.ApplyResources(this.txtColor, "txtColor");
            this.txtColor.Name = "txtColor";
            this.txtColor.ReadOnly = true;
            this.txtColor.Tag = "ColorLow";
            // 
            // lblColor
            // 
            resources.ApplyResources(this.lblColor, "lblColor");
            this.lblColor.Name = "lblColor";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtSortOrder
            // 
            this.txtSortOrder.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCostCategories, "SortOrder", true));
            resources.ApplyResources(this.txtSortOrder, "txtSortOrder");
            this.txtSortOrder.Name = "txtSortOrder";
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsCostCategories, "Name", true));
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // lblDescription
            // 
            resources.ApplyResources(this.lblDescription, "lblDescription");
            this.lblDescription.Name = "lblDescription";
            // 
            // frmCostcategories
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbCostCategory);
            this.Controls.Add(this.dgCostCategories);
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmCostcategories";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmCostcategories_FormClosing);
            this.Load += new System.EventHandler(this.frmCostcategories_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgCostCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCostCategories)).EndInit();
            this.gbCostCategory.ResumeLayout(false);
            this.gbCostCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsTariffGroups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsUnits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergySubType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsCalculationType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ToolStrip toolStrip1;
        private ToolStripButton tsbAddCostCategory;
        private ToolStripButton tsbDeleteCostCategory;
        private ToolStripButton tsbRefreshCostCategory;
        private ToolStripButton tsbCloseCostCategory;
        private StatusStrip statusStrip1;
        private ComboBox cboEnergyType;
        private Label lblEnergyType;
        private ToolTip toolTip1;
        private BindingSource bsCostCategories;
        private DataGridView dgCostCategories;
        private GroupBox gbCostCategory;
        private ToolStripButton tsbSaveCostCategory;
        private TextBox txtDescription;
        private Label lblDescription;
        private CheckBox chkNotCalculateOverReturnValue;
        private Label label2;
        private CheckBox chkCanBeNegative;
        private CheckBox chkCalculateVat;
        private Label label4;
        private Label label3;
        private ComboBox cboTariffGroup;
        private ComboBox cboUnit;
        private Label lblUnit;
        private Label lblSubType;
        private ComboBox cboEnergySubType;
        private Label lblCalculationType;
        private ComboBox cboCalculationType;
        private TextBox txtColor;
        private Label lblColor;
        private Label label1;
        private TextBox txtSortOrder;
        private BindingSource bsTariffGroups;
        private BindingSource bsEnergySubType;
        private BindingSource bsCalculationType;
        private ColorDialog colorDialog1;
        private BindingSource bsUnits;
        private BindingSource bsEnergyTypes;
        private ToolStripButton tbsCancel;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn sortOrderDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn UnitName;
        private Label lblEndDate;
        private DateTimePicker dtEndDate;
        private Label lblStartDate;
        private DateTimePicker dtStartDate;
    }
}