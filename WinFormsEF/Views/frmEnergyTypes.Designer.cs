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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEnergyTypes));
            statusStrip1 = new StatusStrip();
            toolStrip1 = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbSave = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            tsbClose = new ToolStripButton();
            toolTip1 = new ToolTip(components);
            dgEnergyTypes = new DataGridView();
            EnergyTypeName = new DataGridViewTextBoxColumn();
            UnitName = new DataGridViewTextBoxColumn();
            bsEnergyTypes = new BindingSource(components);
            gbEnergyType = new GroupBox();
            lblUnit = new Label();
            cboUnit = new ComboBox();
            bsUnits = new BindingSource(components);
            chkHasEnergyReturn = new CheckBox();
            chkHasNormalAndLow = new CheckBox();
            chkDefaultType = new CheckBox();
            txtName = new TextBox();
            lblName = new Label();
            gbColors = new GroupBox();
            txtColor = new TextBox();
            lblColor = new Label();
            txtReturnDeliveryHigh = new TextBox();
            txtReturnDeliveryLow = new TextBox();
            txtColorHigh = new TextBox();
            txtColorLow = new TextBox();
            lblReturnDeliveryHigh = new Label();
            lblReturnDeliveryLow = new Label();
            lblColorHigh = new Label();
            lblColorLow = new Label();
            nameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            colorDialog1 = new ColorDialog();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nameDataGridViewTextBoxColumn1 = new DataGridViewTextBoxColumn();
            hasNormalAndLowDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            hasEnergyReturnDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            defaultTypeDataGridViewCheckBoxColumn = new DataGridViewCheckBoxColumn();
            unitIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            unitDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            unitNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            additionalCategoryAndGroupInfosDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            correctionFactorsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            costCategoriesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            meterReadingsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            metersDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            nettingsDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            preDefinedPeriodDatesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ratesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            calculatedUnitPricesDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            gbEnergyType.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsUnits).BeginInit();
            gbColors.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAdd, tsbSave, tbsCancel, tsbDelete, tsbRefresh, tsbClose });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbAdd
            // 
            tsbAdd.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(tsbAdd, "tsbAdd");
            tsbAdd.Name = "tsbAdd";
            tsbAdd.Click += tsbAdd_Click;
            // 
            // tsbSave
            // 
            tsbSave.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(tsbSave, "tsbSave");
            tsbSave.Name = "tsbSave";
            tsbSave.Click += tsbSave_Click;
            // 
            // tbsCancel
            // 
            tbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(tbsCancel, "tbsCancel");
            tbsCancel.Name = "tbsCancel";
            tbsCancel.Click += tbsCancel_Click;
            // 
            // tsbDelete
            // 
            tsbDelete.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(tsbDelete, "tsbDelete");
            tsbDelete.Name = "tsbDelete";
            tsbDelete.Click += tsbDelete_Click;
            // 
            // tsbRefresh
            // 
            tsbRefresh.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(tsbRefresh, "tsbRefresh");
            tsbRefresh.Name = "tsbRefresh";
            tsbRefresh.Click += tsbRefresh_Click;
            // 
            // tsbClose
            // 
            tsbClose.Alignment = ToolStripItemAlignment.Right;
            tsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbClose, "tsbClose");
            tsbClose.Name = "tsbClose";
            tsbClose.Click += TsbClose_Click;
            // 
            // dgEnergyTypes
            // 
            dgEnergyTypes.AllowUserToAddRows = false;
            dgEnergyTypes.AllowUserToDeleteRows = false;
            dgEnergyTypes.AutoGenerateColumns = false;
            dgEnergyTypes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgEnergyTypes.Columns.AddRange(new DataGridViewColumn[] { EnergyTypeName, UnitName, idDataGridViewTextBoxColumn, nameDataGridViewTextBoxColumn1, hasNormalAndLowDataGridViewCheckBoxColumn, hasEnergyReturnDataGridViewCheckBoxColumn, defaultTypeDataGridViewCheckBoxColumn, unitIdDataGridViewTextBoxColumn, unitDataGridViewTextBoxColumn, unitNameDataGridViewTextBoxColumn, additionalCategoryAndGroupInfosDataGridViewTextBoxColumn, correctionFactorsDataGridViewTextBoxColumn, costCategoriesDataGridViewTextBoxColumn, meterReadingsDataGridViewTextBoxColumn, metersDataGridViewTextBoxColumn, nettingsDataGridViewTextBoxColumn, preDefinedPeriodDatesDataGridViewTextBoxColumn, ratesDataGridViewTextBoxColumn, calculatedUnitPricesDataGridViewTextBoxColumn });
            dgEnergyTypes.DataSource = bsEnergyTypes;
            resources.ApplyResources(dgEnergyTypes, "dgEnergyTypes");
            dgEnergyTypes.Name = "dgEnergyTypes";
            // 
            // EnergyTypeName
            // 
            EnergyTypeName.DataPropertyName = "Name";
            resources.ApplyResources(EnergyTypeName, "EnergyTypeName");
            EnergyTypeName.Name = "EnergyTypeName";
            // 
            // UnitName
            // 
            UnitName.DataPropertyName = "UnitName";
            resources.ApplyResources(UnitName, "UnitName");
            UnitName.Name = "UnitName";
            UnitName.ReadOnly = true;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            bsEnergyTypes.CurrentChanged += bsEnergyTypes_CurrentChanged;
            // 
            // gbEnergyType
            // 
            resources.ApplyResources(gbEnergyType, "gbEnergyType");
            gbEnergyType.Controls.Add(lblUnit);
            gbEnergyType.Controls.Add(cboUnit);
            gbEnergyType.Controls.Add(chkHasEnergyReturn);
            gbEnergyType.Controls.Add(chkHasNormalAndLow);
            gbEnergyType.Controls.Add(chkDefaultType);
            gbEnergyType.Controls.Add(txtName);
            gbEnergyType.Controls.Add(lblName);
            gbEnergyType.Name = "gbEnergyType";
            gbEnergyType.TabStop = false;
            // 
            // lblUnit
            // 
            resources.ApplyResources(lblUnit, "lblUnit");
            lblUnit.Name = "lblUnit";
            // 
            // cboUnit
            // 
            cboUnit.DataBindings.Add(new Binding("SelectedValue", bsEnergyTypes, "UnitId", true));
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
            // chkHasEnergyReturn
            // 
            resources.ApplyResources(chkHasEnergyReturn, "chkHasEnergyReturn");
            chkHasEnergyReturn.DataBindings.Add(new Binding("Checked", bsEnergyTypes, "HasEnergyReturn", true));
            chkHasEnergyReturn.Name = "chkHasEnergyReturn";
            chkHasEnergyReturn.UseVisualStyleBackColor = true;
            chkHasEnergyReturn.CheckedChanged += chkHasEnergyReturn_CheckedChanged;
            // 
            // chkHasNormalAndLow
            // 
            resources.ApplyResources(chkHasNormalAndLow, "chkHasNormalAndLow");
            chkHasNormalAndLow.DataBindings.Add(new Binding("Checked", bsEnergyTypes, "HasNormalAndLow", true));
            chkHasNormalAndLow.Name = "chkHasNormalAndLow";
            chkHasNormalAndLow.UseVisualStyleBackColor = true;
            chkHasNormalAndLow.CheckedChanged += chkHasNormalAndLow_CheckedChanged;
            // 
            // chkDefaultType
            // 
            resources.ApplyResources(chkDefaultType, "chkDefaultType");
            chkDefaultType.DataBindings.Add(new Binding("Checked", bsEnergyTypes, "DefaultType", true));
            chkDefaultType.Name = "chkDefaultType";
            chkDefaultType.UseVisualStyleBackColor = true;
            // 
            // txtName
            // 
            txtName.DataBindings.Add(new Binding("Text", bsEnergyTypes, "Name", true));
            resources.ApplyResources(txtName, "txtName");
            txtName.Name = "txtName";
            // 
            // lblName
            // 
            resources.ApplyResources(lblName, "lblName");
            lblName.Name = "lblName";
            // 
            // gbColors
            // 
            resources.ApplyResources(gbColors, "gbColors");
            gbColors.Controls.Add(txtColor);
            gbColors.Controls.Add(lblColor);
            gbColors.Controls.Add(txtReturnDeliveryHigh);
            gbColors.Controls.Add(txtReturnDeliveryLow);
            gbColors.Controls.Add(txtColorHigh);
            gbColors.Controls.Add(txtColorLow);
            gbColors.Controls.Add(lblReturnDeliveryHigh);
            gbColors.Controls.Add(lblReturnDeliveryLow);
            gbColors.Controls.Add(lblColorHigh);
            gbColors.Controls.Add(lblColorLow);
            gbColors.Name = "gbColors";
            gbColors.TabStop = false;
            // 
            // txtColor
            // 
            resources.ApplyResources(txtColor, "txtColor");
            txtColor.Name = "txtColor";
            txtColor.ReadOnly = true;
            txtColor.Tag = "Color";
            txtColor.Click += txtColor_Click;
            // 
            // lblColor
            // 
            resources.ApplyResources(lblColor, "lblColor");
            lblColor.Name = "lblColor";
            // 
            // txtReturnDeliveryHigh
            // 
            resources.ApplyResources(txtReturnDeliveryHigh, "txtReturnDeliveryHigh");
            txtReturnDeliveryHigh.Name = "txtReturnDeliveryHigh";
            txtReturnDeliveryHigh.ReadOnly = true;
            txtReturnDeliveryHigh.Tag = "ReturnNormal";
            txtReturnDeliveryHigh.Click += txtReturnDeliveryHigh_Click;
            // 
            // txtReturnDeliveryLow
            // 
            resources.ApplyResources(txtReturnDeliveryLow, "txtReturnDeliveryLow");
            txtReturnDeliveryLow.Name = "txtReturnDeliveryLow";
            txtReturnDeliveryLow.ReadOnly = true;
            txtReturnDeliveryLow.Tag = "ReturnLow";
            txtReturnDeliveryLow.Click += txtReturnDeliveryLow_Click;
            // 
            // txtColorHigh
            // 
            resources.ApplyResources(txtColorHigh, "txtColorHigh");
            txtColorHigh.Name = "txtColorHigh";
            txtColorHigh.ReadOnly = true;
            txtColorHigh.Tag = "ColorNormal";
            txtColorHigh.Click += txtColorHigh_Click;
            // 
            // txtColorLow
            // 
            resources.ApplyResources(txtColorLow, "txtColorLow");
            txtColorLow.Name = "txtColorLow";
            txtColorLow.ReadOnly = true;
            txtColorLow.Tag = "ColorLow";
            txtColorLow.Click += txtColorLow_Click;
            // 
            // lblReturnDeliveryHigh
            // 
            resources.ApplyResources(lblReturnDeliveryHigh, "lblReturnDeliveryHigh");
            lblReturnDeliveryHigh.Name = "lblReturnDeliveryHigh";
            // 
            // lblReturnDeliveryLow
            // 
            resources.ApplyResources(lblReturnDeliveryLow, "lblReturnDeliveryLow");
            lblReturnDeliveryLow.Name = "lblReturnDeliveryLow";
            // 
            // lblColorHigh
            // 
            resources.ApplyResources(lblColorHigh, "lblColorHigh");
            lblColorHigh.Name = "lblColorHigh";
            // 
            // lblColorLow
            // 
            resources.ApplyResources(lblColorLow, "lblColorLow");
            lblColorLow.Name = "lblColorLow";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            nameDataGridViewTextBoxColumn.DataPropertyName = "Name";
            resources.ApplyResources(nameDataGridViewTextBoxColumn, "nameDataGridViewTextBoxColumn");
            nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn1
            // 
            nameDataGridViewTextBoxColumn1.DataPropertyName = "Name";
            resources.ApplyResources(nameDataGridViewTextBoxColumn1, "nameDataGridViewTextBoxColumn1");
            nameDataGridViewTextBoxColumn1.Name = "nameDataGridViewTextBoxColumn1";
            // 
            // hasNormalAndLowDataGridViewCheckBoxColumn
            // 
            hasNormalAndLowDataGridViewCheckBoxColumn.DataPropertyName = "HasNormalAndLow";
            resources.ApplyResources(hasNormalAndLowDataGridViewCheckBoxColumn, "hasNormalAndLowDataGridViewCheckBoxColumn");
            hasNormalAndLowDataGridViewCheckBoxColumn.Name = "hasNormalAndLowDataGridViewCheckBoxColumn";
            // 
            // hasEnergyReturnDataGridViewCheckBoxColumn
            // 
            hasEnergyReturnDataGridViewCheckBoxColumn.DataPropertyName = "HasEnergyReturn";
            resources.ApplyResources(hasEnergyReturnDataGridViewCheckBoxColumn, "hasEnergyReturnDataGridViewCheckBoxColumn");
            hasEnergyReturnDataGridViewCheckBoxColumn.Name = "hasEnergyReturnDataGridViewCheckBoxColumn";
            // 
            // defaultTypeDataGridViewCheckBoxColumn
            // 
            defaultTypeDataGridViewCheckBoxColumn.DataPropertyName = "DefaultType";
            resources.ApplyResources(defaultTypeDataGridViewCheckBoxColumn, "defaultTypeDataGridViewCheckBoxColumn");
            defaultTypeDataGridViewCheckBoxColumn.Name = "defaultTypeDataGridViewCheckBoxColumn";
            // 
            // unitIdDataGridViewTextBoxColumn
            // 
            unitIdDataGridViewTextBoxColumn.DataPropertyName = "UnitId";
            resources.ApplyResources(unitIdDataGridViewTextBoxColumn, "unitIdDataGridViewTextBoxColumn");
            unitIdDataGridViewTextBoxColumn.Name = "unitIdDataGridViewTextBoxColumn";
            // 
            // unitDataGridViewTextBoxColumn
            // 
            unitDataGridViewTextBoxColumn.DataPropertyName = "Unit";
            resources.ApplyResources(unitDataGridViewTextBoxColumn, "unitDataGridViewTextBoxColumn");
            unitDataGridViewTextBoxColumn.Name = "unitDataGridViewTextBoxColumn";
            // 
            // unitNameDataGridViewTextBoxColumn
            // 
            unitNameDataGridViewTextBoxColumn.DataPropertyName = "UnitName";
            resources.ApplyResources(unitNameDataGridViewTextBoxColumn, "unitNameDataGridViewTextBoxColumn");
            unitNameDataGridViewTextBoxColumn.Name = "unitNameDataGridViewTextBoxColumn";
            unitNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // additionalCategoryAndGroupInfosDataGridViewTextBoxColumn
            // 
            additionalCategoryAndGroupInfosDataGridViewTextBoxColumn.DataPropertyName = "AdditionalCategoryAndGroupInfos";
            resources.ApplyResources(additionalCategoryAndGroupInfosDataGridViewTextBoxColumn, "additionalCategoryAndGroupInfosDataGridViewTextBoxColumn");
            additionalCategoryAndGroupInfosDataGridViewTextBoxColumn.Name = "additionalCategoryAndGroupInfosDataGridViewTextBoxColumn";
            // 
            // correctionFactorsDataGridViewTextBoxColumn
            // 
            correctionFactorsDataGridViewTextBoxColumn.DataPropertyName = "CorrectionFactors";
            resources.ApplyResources(correctionFactorsDataGridViewTextBoxColumn, "correctionFactorsDataGridViewTextBoxColumn");
            correctionFactorsDataGridViewTextBoxColumn.Name = "correctionFactorsDataGridViewTextBoxColumn";
            // 
            // costCategoriesDataGridViewTextBoxColumn
            // 
            costCategoriesDataGridViewTextBoxColumn.DataPropertyName = "CostCategories";
            resources.ApplyResources(costCategoriesDataGridViewTextBoxColumn, "costCategoriesDataGridViewTextBoxColumn");
            costCategoriesDataGridViewTextBoxColumn.Name = "costCategoriesDataGridViewTextBoxColumn";
            // 
            // meterReadingsDataGridViewTextBoxColumn
            // 
            meterReadingsDataGridViewTextBoxColumn.DataPropertyName = "MeterReadings";
            resources.ApplyResources(meterReadingsDataGridViewTextBoxColumn, "meterReadingsDataGridViewTextBoxColumn");
            meterReadingsDataGridViewTextBoxColumn.Name = "meterReadingsDataGridViewTextBoxColumn";
            // 
            // metersDataGridViewTextBoxColumn
            // 
            metersDataGridViewTextBoxColumn.DataPropertyName = "Meters";
            resources.ApplyResources(metersDataGridViewTextBoxColumn, "metersDataGridViewTextBoxColumn");
            metersDataGridViewTextBoxColumn.Name = "metersDataGridViewTextBoxColumn";
            // 
            // nettingsDataGridViewTextBoxColumn
            // 
            nettingsDataGridViewTextBoxColumn.DataPropertyName = "Nettings";
            resources.ApplyResources(nettingsDataGridViewTextBoxColumn, "nettingsDataGridViewTextBoxColumn");
            nettingsDataGridViewTextBoxColumn.Name = "nettingsDataGridViewTextBoxColumn";
            // 
            // preDefinedPeriodDatesDataGridViewTextBoxColumn
            // 
            preDefinedPeriodDatesDataGridViewTextBoxColumn.DataPropertyName = "PreDefinedPeriodDates";
            resources.ApplyResources(preDefinedPeriodDatesDataGridViewTextBoxColumn, "preDefinedPeriodDatesDataGridViewTextBoxColumn");
            preDefinedPeriodDatesDataGridViewTextBoxColumn.Name = "preDefinedPeriodDatesDataGridViewTextBoxColumn";
            // 
            // ratesDataGridViewTextBoxColumn
            // 
            ratesDataGridViewTextBoxColumn.DataPropertyName = "Rates";
            resources.ApplyResources(ratesDataGridViewTextBoxColumn, "ratesDataGridViewTextBoxColumn");
            ratesDataGridViewTextBoxColumn.Name = "ratesDataGridViewTextBoxColumn";
            // 
            // calculatedUnitPricesDataGridViewTextBoxColumn
            // 
            calculatedUnitPricesDataGridViewTextBoxColumn.DataPropertyName = "CalculatedUnitPrices";
            resources.ApplyResources(calculatedUnitPricesDataGridViewTextBoxColumn, "calculatedUnitPricesDataGridViewTextBoxColumn");
            calculatedUnitPricesDataGridViewTextBoxColumn.Name = "calculatedUnitPricesDataGridViewTextBoxColumn";
            // 
            // frmEnergyTypes
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbColors);
            Controls.Add(gbEnergyType);
            Controls.Add(dgEnergyTypes);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Name = "frmEnergyTypes";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmEnergyTypes_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            gbEnergyType.ResumeLayout(false);
            gbEnergyType.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsUnits).EndInit();
            gbColors.ResumeLayout(false);
            gbColors.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn1;
        private DataGridViewCheckBoxColumn hasNormalAndLowDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn hasEnergyReturnDataGridViewCheckBoxColumn;
        private DataGridViewCheckBoxColumn defaultTypeDataGridViewCheckBoxColumn;
        private DataGridViewTextBoxColumn unitIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn unitDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn unitNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn additionalCategoryAndGroupInfosDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn correctionFactorsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn costCategoriesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn meterReadingsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn metersDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn nettingsDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn preDefinedPeriodDatesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ratesDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn calculatedUnitPricesDataGridViewTextBoxColumn;
    }
}