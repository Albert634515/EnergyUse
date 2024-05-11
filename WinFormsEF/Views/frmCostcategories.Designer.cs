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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCostcategories));
            toolStrip1 = new ToolStrip();
            tsbAddCostCategory = new ToolStripButton();
            tsbSaveCostCategory = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDeleteCostCategory = new ToolStripButton();
            tsbRefreshCostCategory = new ToolStripButton();
            tsbCloseCostCategory = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            cboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            lblEnergyType = new Label();
            toolTip1 = new ToolTip(components);
            dgCostCategories = new DataGridView();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            sortOrderDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            UnitName = new DataGridViewTextBoxColumn();
            bsCostCategories = new BindingSource(components);
            gbCostCategory = new GroupBox();
            lblEndDate = new Label();
            dtEndDate = new DateTimePicker();
            lblStartDate = new Label();
            dtStartDate = new DateTimePicker();
            chkNotCalculateOverReturnValue = new CheckBox();
            label2 = new Label();
            chkCanBeNegative = new CheckBox();
            chkCalculateVat = new CheckBox();
            label4 = new Label();
            label3 = new Label();
            cboTariffGroup = new ComboBox();
            bsTariffGroups = new BindingSource(components);
            cboUnit = new ComboBox();
            bsUnits = new BindingSource(components);
            lblUnit = new Label();
            lblSubType = new Label();
            cboEnergySubType = new ComboBox();
            bsEnergySubType = new BindingSource(components);
            lblCalculationType = new Label();
            cboCalculationType = new ComboBox();
            bsCalculationType = new BindingSource(components);
            txtColor = new TextBox();
            lblColor = new Label();
            label1 = new Label();
            txtSortOrder = new TextBox();
            txtDescription = new TextBox();
            lblDescription = new Label();
            colorDialog1 = new ColorDialog();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgCostCategories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCostCategories).BeginInit();
            gbCostCategory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsTariffGroups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsUnits).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergySubType).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCalculationType).BeginInit();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAddCostCategory, tsbSaveCostCategory, tbsCancel, tsbDeleteCostCategory, tsbRefreshCostCategory, tsbCloseCostCategory });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbAddCostCategory
            // 
            tsbAddCostCategory.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(tsbAddCostCategory, "tsbAddCostCategory");
            tsbAddCostCategory.Name = "tsbAddCostCategory";
            tsbAddCostCategory.Click += tsbAddCostCategory_Click;
            // 
            // tsbSaveCostCategory
            // 
            tsbSaveCostCategory.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(tsbSaveCostCategory, "tsbSaveCostCategory");
            tsbSaveCostCategory.Name = "tsbSaveCostCategory";
            tsbSaveCostCategory.Click += tsbSave_Click;
            // 
            // tbsCancel
            // 
            tbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(tbsCancel, "tbsCancel");
            tbsCancel.Name = "tbsCancel";
            tbsCancel.Click += tbsCancel_Click;
            // 
            // tsbDeleteCostCategory
            // 
            tsbDeleteCostCategory.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(tsbDeleteCostCategory, "tsbDeleteCostCategory");
            tsbDeleteCostCategory.Name = "tsbDeleteCostCategory";
            tsbDeleteCostCategory.Click += tsbDeleteCostCategory_Click;
            // 
            // tsbRefreshCostCategory
            // 
            tsbRefreshCostCategory.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(tsbRefreshCostCategory, "tsbRefreshCostCategory");
            tsbRefreshCostCategory.Name = "tsbRefreshCostCategory";
            tsbRefreshCostCategory.Click += tsbRefreshCostCategory_Click;
            // 
            // tsbCloseCostCategory
            // 
            tsbCloseCostCategory.Alignment = ToolStripItemAlignment.Right;
            tsbCloseCostCategory.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbCloseCostCategory, "tsbCloseCostCategory");
            tsbCloseCostCategory.Name = "tsbCloseCostCategory";
            tsbCloseCostCategory.Click += tsbCloseCostCategory_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // cboEnergyType
            // 
            cboEnergyType.DataSource = bsEnergyTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            cboEnergyType.ValueMember = "Id";
            cboEnergyType.SelectedIndexChanged += cboEnergyType_SelectedIndexChanged;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // dgCostCategories
            // 
            dgCostCategories.AllowUserToAddRows = false;
            dgCostCategories.AllowUserToDeleteRows = false;
            dgCostCategories.AutoGenerateColumns = false;
            dgCostCategories.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgCostCategories.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn, sortOrderDataGridViewTextBoxColumn, UnitName });
            dgCostCategories.DataSource = bsCostCategories;
            resources.ApplyResources(dgCostCategories, "dgCostCategories");
            dgCostCategories.Name = "dgCostCategories";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // sortOrderDataGridViewTextBoxColumn
            // 
            sortOrderDataGridViewTextBoxColumn.DataPropertyName = "SortOrder";
            resources.ApplyResources(sortOrderDataGridViewTextBoxColumn, "sortOrderDataGridViewTextBoxColumn");
            sortOrderDataGridViewTextBoxColumn.Name = "sortOrderDataGridViewTextBoxColumn";
            // 
            // UnitName
            // 
            UnitName.DataPropertyName = "UnitName";
            resources.ApplyResources(UnitName, "UnitName");
            UnitName.Name = "UnitName";
            UnitName.ReadOnly = true;
            // 
            // bsCostCategories
            // 
            bsCostCategories.DataSource = typeof(EnergyUse.Models.CostCategory);
            bsCostCategories.CurrentChanged += bsCostCategories_CurrentChanged;
            // 
            // gbCostCategory
            // 
            resources.ApplyResources(gbCostCategory, "gbCostCategory");
            gbCostCategory.Controls.Add(lblEndDate);
            gbCostCategory.Controls.Add(dtEndDate);
            gbCostCategory.Controls.Add(lblStartDate);
            gbCostCategory.Controls.Add(dtStartDate);
            gbCostCategory.Controls.Add(chkNotCalculateOverReturnValue);
            gbCostCategory.Controls.Add(label2);
            gbCostCategory.Controls.Add(chkCanBeNegative);
            gbCostCategory.Controls.Add(chkCalculateVat);
            gbCostCategory.Controls.Add(label4);
            gbCostCategory.Controls.Add(label3);
            gbCostCategory.Controls.Add(cboTariffGroup);
            gbCostCategory.Controls.Add(cboUnit);
            gbCostCategory.Controls.Add(lblUnit);
            gbCostCategory.Controls.Add(lblSubType);
            gbCostCategory.Controls.Add(cboEnergySubType);
            gbCostCategory.Controls.Add(lblCalculationType);
            gbCostCategory.Controls.Add(cboCalculationType);
            gbCostCategory.Controls.Add(txtColor);
            gbCostCategory.Controls.Add(lblColor);
            gbCostCategory.Controls.Add(label1);
            gbCostCategory.Controls.Add(txtSortOrder);
            gbCostCategory.Controls.Add(txtDescription);
            gbCostCategory.Controls.Add(lblDescription);
            gbCostCategory.Name = "gbCostCategory";
            gbCostCategory.TabStop = false;
            // 
            // lblEndDate
            // 
            resources.ApplyResources(lblEndDate, "lblEndDate");
            lblEndDate.Name = "lblEndDate";
            // 
            // dtEndDate
            // 
            resources.ApplyResources(dtEndDate, "dtEndDate");
            dtEndDate.DataBindings.Add(new Binding("Value", bsCostCategories, "End", true));
            dtEndDate.Format = DateTimePickerFormat.Short;
            dtEndDate.Name = "dtEndDate";
            dtEndDate.Value = new DateTime(2022, 10, 1, 0, 0, 0, 0);
            // 
            // lblStartDate
            // 
            resources.ApplyResources(lblStartDate, "lblStartDate");
            lblStartDate.Name = "lblStartDate";
            // 
            // dtStartDate
            // 
            resources.ApplyResources(dtStartDate, "dtStartDate");
            dtStartDate.DataBindings.Add(new Binding("Value", bsCostCategories, "Start", true));
            dtStartDate.Format = DateTimePickerFormat.Short;
            dtStartDate.Name = "dtStartDate";
            dtStartDate.Value = new DateTime(2022, 10, 1, 0, 0, 0, 0);
            // 
            // chkNotCalculateOverReturnValue
            // 
            resources.ApplyResources(chkNotCalculateOverReturnValue, "chkNotCalculateOverReturnValue");
            chkNotCalculateOverReturnValue.DataBindings.Add(new Binding("Checked", bsCostCategories, "NotCalculateOverReturn", true));
            chkNotCalculateOverReturnValue.Name = "chkNotCalculateOverReturnValue";
            chkNotCalculateOverReturnValue.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // chkCanBeNegative
            // 
            resources.ApplyResources(chkCanBeNegative, "chkCanBeNegative");
            chkCanBeNegative.DataBindings.Add(new Binding("Checked", bsCostCategories, "CanNotBeNegative", true));
            chkCanBeNegative.Name = "chkCanBeNegative";
            chkCanBeNegative.UseVisualStyleBackColor = true;
            // 
            // chkCalculateVat
            // 
            resources.ApplyResources(chkCalculateVat, "chkCalculateVat");
            chkCalculateVat.DataBindings.Add(new Binding("Checked", bsCostCategories, "CalculateVat", true));
            chkCalculateVat.Name = "chkCalculateVat";
            chkCalculateVat.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // cboTariffGroup
            // 
            cboTariffGroup.DataBindings.Add(new Binding("SelectedValue", bsCostCategories, "TariffGroupId", true));
            cboTariffGroup.DataSource = bsTariffGroups;
            cboTariffGroup.DisplayMember = "Description";
            cboTariffGroup.FormattingEnabled = true;
            resources.ApplyResources(cboTariffGroup, "cboTariffGroup");
            cboTariffGroup.Name = "cboTariffGroup";
            cboTariffGroup.ValueMember = "Id";
            // 
            // bsTariffGroups
            // 
            bsTariffGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // cboUnit
            // 
            cboUnit.DataBindings.Add(new Binding("SelectedValue", bsCostCategories, "UnitId", true));
            cboUnit.DataSource = bsUnits;
            cboUnit.DisplayMember = "Description";
            cboUnit.FormattingEnabled = true;
            resources.ApplyResources(cboUnit, "cboUnit");
            cboUnit.Name = "cboUnit";
            cboUnit.ValueMember = "Id";
            // 
            // bsUnits
            // 
            bsUnits.DataSource = typeof(EnergyUse.Models.Unit);
            // 
            // lblUnit
            // 
            resources.ApplyResources(lblUnit, "lblUnit");
            lblUnit.Name = "lblUnit";
            // 
            // lblSubType
            // 
            resources.ApplyResources(lblSubType, "lblSubType");
            lblSubType.Name = "lblSubType";
            // 
            // cboEnergySubType
            // 
            cboEnergySubType.DataBindings.Add(new Binding("SelectedValue", bsCostCategories, "EnergySubTypeId", true));
            cboEnergySubType.DataSource = bsEnergySubType;
            cboEnergySubType.DisplayMember = "Description";
            cboEnergySubType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergySubType, "cboEnergySubType");
            cboEnergySubType.Name = "cboEnergySubType";
            cboEnergySubType.ValueMember = "Id";
            // 
            // bsEnergySubType
            // 
            bsEnergySubType.DataSource = typeof(EnergyUse.Models.EnergySubType);
            // 
            // lblCalculationType
            // 
            resources.ApplyResources(lblCalculationType, "lblCalculationType");
            lblCalculationType.Name = "lblCalculationType";
            // 
            // cboCalculationType
            // 
            cboCalculationType.DataBindings.Add(new Binding("SelectedValue", bsCostCategories, "CalculationTypeId", true));
            cboCalculationType.DataSource = bsCalculationType;
            cboCalculationType.DisplayMember = "Description";
            cboCalculationType.FormattingEnabled = true;
            resources.ApplyResources(cboCalculationType, "cboCalculationType");
            cboCalculationType.Name = "cboCalculationType";
            cboCalculationType.ValueMember = "Id";
            // 
            // bsCalculationType
            // 
            bsCalculationType.DataSource = typeof(EnergyUse.Models.CalculationType);
            // 
            // txtColor
            // 
            resources.ApplyResources(txtColor, "txtColor");
            txtColor.Name = "txtColor";
            txtColor.ReadOnly = true;
            txtColor.Tag = "ColorLow";
            // 
            // lblColor
            // 
            resources.ApplyResources(lblColor, "lblColor");
            lblColor.Name = "lblColor";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // txtSortOrder
            // 
            txtSortOrder.DataBindings.Add(new Binding("Text", bsCostCategories, "SortOrder", true));
            resources.ApplyResources(txtSortOrder, "txtSortOrder");
            txtSortOrder.Name = "txtSortOrder";
            // 
            // txtDescription
            // 
            txtDescription.DataBindings.Add(new Binding("Text", bsCostCategories, "Name", true));
            resources.ApplyResources(txtDescription, "txtDescription");
            txtDescription.Name = "txtDescription";
            // 
            // lblDescription
            // 
            resources.ApplyResources(lblDescription, "lblDescription");
            lblDescription.Name = "lblDescription";
            // 
            // frmCostcategories
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbCostCategory);
            Controls.Add(dgCostCategories);
            Controls.Add(cboEnergyType);
            Controls.Add(lblEnergyType);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Name = "frmCostcategories";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmCostcategories_FormClosing;
            Load += frmCostcategories_Load;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgCostCategories).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCostCategories).EndInit();
            gbCostCategory.ResumeLayout(false);
            gbCostCategory.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsTariffGroups).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsUnits).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergySubType).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCalculationType).EndInit();
            ResumeLayout(false);
            PerformLayout();
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