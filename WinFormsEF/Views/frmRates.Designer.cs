namespace WinFormsEF.Views
{
    partial class FrmRates
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmRates));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            ToolStrip1 = new ToolStrip();
            TsbAdd = new ToolStripButton();
            TsbSave = new ToolStripButton();
            TbsCancel = new ToolStripButton();
            TsbDelete = new ToolStripButton();
            TsbRefresh = new ToolStripButton();
            TbsClose = new ToolStripButton();
            StatusStrip1 = new StatusStrip();
            bsRates = new BindingSource(components);
            bsAdditionalCategoryAndGroupInfo = new BindingSource(components);
            CboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            LblEnergyType = new Label();
            CboCostCategory = new ComboBox();
            bsCostCategories = new BindingSource(components);
            LblCostCategory = new Label();
            CboTarifGroup = new ComboBox();
            bsTarifGroups = new BindingSource(components);
            LblTarifGroup = new Label();
            LblRateSource = new Label();
            TxtDescription = new TextBox();
            DgRates = new DataGridView();
            GbRate = new GroupBox();
            LblRate = new Label();
            CboRateType = new ComboBox();
            bsRateType = new BindingSource(components);
            LblRateType = new Label();
            LblPriceChangeHint = new Label();
            LblPriceChange = new Label();
            TxtPriceChange = new TextBox();
            LblExpectedPriceChangeHint = new Label();
            LblExchangeRate = new Label();
            TxtExpectedPriceChange = new TextBox();
            TxtRate = new TextBox();
            LblDescription = new Label();
            TxtRateDescription = new TextBox();
            DtpEndRate = new DateTimePicker();
            DtpStartRate = new DateTimePicker();
            LblRange = new Label();
            toolTip1 = new ToolTip(components);
            LblAlwaysCalculatedWith = new Label();
            startRateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            endRateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            rateValueDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            expectedPriceChangeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceChangeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ToolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsRates).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsAdditionalCategoryAndGroupInfo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsCostCategories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DgRates).BeginInit();
            GbRate.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsRateType).BeginInit();
            SuspendLayout();
            // 
            // ToolStrip1
            // 
            ToolStrip1.ImageScalingSize = new Size(20, 20);
            ToolStrip1.Items.AddRange(new ToolStripItem[] { TsbAdd, TsbSave, TbsCancel, TsbDelete, TsbRefresh, TbsClose });
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
            // TbsClose
            // 
            TbsClose.Alignment = ToolStripItemAlignment.Right;
            TbsClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(TbsClose, "TbsClose");
            TbsClose.Name = "TbsClose";
            TbsClose.Click += TbsClose_Click;
            // 
            // StatusStrip1
            // 
            StatusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(StatusStrip1, "StatusStrip1");
            StatusStrip1.Name = "StatusStrip1";
            // 
            // bsRates
            // 
            bsRates.DataSource = typeof(EnergyUse.Models.Rate);
            // 
            // bsAdditionalCategoryAndGroupInfo
            // 
            bsAdditionalCategoryAndGroupInfo.DataSource = typeof(EnergyUse.Models.AdditionalCategoryAndGroupInfo);
            // 
            // CboEnergyType
            // 
            CboEnergyType.DataSource = bsEnergyTypes;
            CboEnergyType.DisplayMember = "Name";
            CboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(CboEnergyType, "CboEnergyType");
            CboEnergyType.Name = "CboEnergyType";
            CboEnergyType.ValueMember = "Id";
            CboEnergyType.SelectedIndexChanged += CboEnergyType_SelectedIndexChanged;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // LblEnergyType
            // 
            resources.ApplyResources(LblEnergyType, "LblEnergyType");
            LblEnergyType.Name = "LblEnergyType";
            // 
            // CboCostCategory
            // 
            CboCostCategory.DataSource = bsCostCategories;
            CboCostCategory.DisplayMember = "Name";
            CboCostCategory.FormattingEnabled = true;
            resources.ApplyResources(CboCostCategory, "CboCostCategory");
            CboCostCategory.Name = "CboCostCategory";
            CboCostCategory.ValueMember = "Id";
            CboCostCategory.SelectedIndexChanged += CboCostCategory_SelectedIndexChanged;
            // 
            // bsCostCategories
            // 
            bsCostCategories.DataSource = typeof(EnergyUse.Models.CostCategory);
            // 
            // LblCostCategory
            // 
            resources.ApplyResources(LblCostCategory, "LblCostCategory");
            LblCostCategory.Name = "LblCostCategory";
            // 
            // CboTarifGroup
            // 
            CboTarifGroup.DataSource = bsTarifGroups;
            CboTarifGroup.DisplayMember = "Description";
            CboTarifGroup.FormattingEnabled = true;
            resources.ApplyResources(CboTarifGroup, "CboTarifGroup");
            CboTarifGroup.Name = "CboTarifGroup";
            CboTarifGroup.ValueMember = "Id";
            CboTarifGroup.SelectedIndexChanged += CboTarifGroup_SelectedIndexChanged;
            // 
            // bsTarifGroups
            // 
            bsTarifGroups.DataSource = typeof(EnergyUse.Models.TariffGroup);
            // 
            // LblTarifGroup
            // 
            resources.ApplyResources(LblTarifGroup, "LblTarifGroup");
            LblTarifGroup.Name = "LblTarifGroup";
            // 
            // LblRateSource
            // 
            resources.ApplyResources(LblRateSource, "LblRateSource");
            LblRateSource.Name = "LblRateSource";
            // 
            // TxtDescription
            // 
            resources.ApplyResources(TxtDescription, "TxtDescription");
            TxtDescription.DataBindings.Add(new Binding("Text", bsAdditionalCategoryAndGroupInfo, "Description", true));
            TxtDescription.Name = "TxtDescription";
            TxtDescription.Validating += TxtDescription_Validating;
            // 
            // DgRates
            // 
            DgRates.AllowUserToAddRows = false;
            DgRates.AllowUserToDeleteRows = false;
            resources.ApplyResources(DgRates, "DgRates");
            DgRates.AutoGenerateColumns = false;
            DgRates.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgRates.Columns.AddRange(new DataGridViewColumn[] { startRateDataGridViewTextBoxColumn, endRateDataGridViewTextBoxColumn, rateValueDataGridViewTextBoxColumn, expectedPriceChangeDataGridViewTextBoxColumn, priceChangeDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn });
            DgRates.DataSource = bsRates;
            DgRates.Name = "DgRates";
            // 
            // GbRate
            // 
            resources.ApplyResources(GbRate, "GbRate");
            GbRate.Controls.Add(LblRate);
            GbRate.Controls.Add(CboRateType);
            GbRate.Controls.Add(LblRateType);
            GbRate.Controls.Add(LblPriceChangeHint);
            GbRate.Controls.Add(LblPriceChange);
            GbRate.Controls.Add(TxtPriceChange);
            GbRate.Controls.Add(LblExpectedPriceChangeHint);
            GbRate.Controls.Add(LblExchangeRate);
            GbRate.Controls.Add(TxtExpectedPriceChange);
            GbRate.Controls.Add(TxtRate);
            GbRate.Controls.Add(LblDescription);
            GbRate.Controls.Add(TxtRateDescription);
            GbRate.Controls.Add(DtpEndRate);
            GbRate.Controls.Add(DtpStartRate);
            GbRate.Controls.Add(LblRange);
            GbRate.Name = "GbRate";
            GbRate.TabStop = false;
            // 
            // LblRate
            // 
            resources.ApplyResources(LblRate, "LblRate");
            LblRate.Name = "LblRate";
            // 
            // CboRateType
            // 
            CboRateType.DataBindings.Add(new Binding("SelectedValue", bsRates, "RateTypeId", true));
            CboRateType.DataSource = bsRateType;
            CboRateType.DisplayMember = "Description";
            CboRateType.FormattingEnabled = true;
            resources.ApplyResources(CboRateType, "CboRateType");
            CboRateType.Name = "CboRateType";
            CboRateType.ValueMember = "Id";
            CboRateType.SelectedIndexChanged += CboRateType_SelectedIndexChanged;
            // 
            // bsRateType
            // 
            bsRateType.DataSource = typeof(EnergyUse.Models.Common.SelectionItem);
            // 
            // LblRateType
            // 
            resources.ApplyResources(LblRateType, "LblRateType");
            LblRateType.Name = "LblRateType";
            // 
            // LblPriceChangeHint
            // 
            resources.ApplyResources(LblPriceChangeHint, "LblPriceChangeHint");
            LblPriceChangeHint.Name = "LblPriceChangeHint";
            // 
            // LblPriceChange
            // 
            resources.ApplyResources(LblPriceChange, "LblPriceChange");
            LblPriceChange.Name = "LblPriceChange";
            // 
            // TxtPriceChange
            // 
            TxtPriceChange.BackColor = SystemColors.ControlLight;
            TxtPriceChange.DataBindings.Add(new Binding("Text", bsRates, "PriceChange", true));
            resources.ApplyResources(TxtPriceChange, "TxtPriceChange");
            TxtPriceChange.Name = "TxtPriceChange";
            TxtPriceChange.ReadOnly = true;
            // 
            // LblExpectedPriceChangeHint
            // 
            resources.ApplyResources(LblExpectedPriceChangeHint, "LblExpectedPriceChangeHint");
            LblExpectedPriceChangeHint.Name = "LblExpectedPriceChangeHint";
            // 
            // LblExchangeRate
            // 
            resources.ApplyResources(LblExchangeRate, "LblExchangeRate");
            LblExchangeRate.Name = "LblExchangeRate";
            // 
            // TxtExpectedPriceChange
            // 
            TxtExpectedPriceChange.DataBindings.Add(new Binding("Text", bsRates, "ExpectedPriceChange", true));
            resources.ApplyResources(TxtExpectedPriceChange, "TxtExpectedPriceChange");
            TxtExpectedPriceChange.Name = "TxtExpectedPriceChange";
            toolTip1.SetToolTip(TxtExpectedPriceChange, resources.GetString("TxtExpectedPriceChange.ToolTip"));
            // 
            // TxtRate
            // 
            TxtRate.DataBindings.Add(new Binding("Text", bsRates, "RateValue", true));
            resources.ApplyResources(TxtRate, "TxtRate");
            TxtRate.Name = "TxtRate";
            // 
            // LblDescription
            // 
            resources.ApplyResources(LblDescription, "LblDescription");
            LblDescription.Name = "LblDescription";
            // 
            // TxtRateDescription
            // 
            TxtRateDescription.DataBindings.Add(new Binding("Text", bsRates, "Description", true));
            resources.ApplyResources(TxtRateDescription, "TxtRateDescription");
            TxtRateDescription.Name = "TxtRateDescription";
            // 
            // DtpEndRate
            // 
            DtpEndRate.DataBindings.Add(new Binding("Value", bsRates, "EndRate", true));
            DtpEndRate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DtpEndRate, "DtpEndRate");
            DtpEndRate.Name = "DtpEndRate";
            // 
            // DtpStartRate
            // 
            DtpStartRate.DataBindings.Add(new Binding("Value", bsRates, "StartRate", true));
            DtpStartRate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DtpStartRate, "DtpStartRate");
            DtpStartRate.Name = "DtpStartRate";
            DtpStartRate.Value = new DateTime(2022, 9, 24, 0, 0, 0, 0);
            // 
            // LblRange
            // 
            resources.ApplyResources(LblRange, "LblRange");
            LblRange.Name = "LblRange";
            // 
            // LblAlwaysCalculatedWith
            // 
            resources.ApplyResources(LblAlwaysCalculatedWith, "LblAlwaysCalculatedWith");
            LblAlwaysCalculatedWith.Name = "LblAlwaysCalculatedWith";
            // 
            // startRateDataGridViewTextBoxColumn
            // 
            startRateDataGridViewTextBoxColumn.DataPropertyName = "StartRate";
            resources.ApplyResources(startRateDataGridViewTextBoxColumn, "startRateDataGridViewTextBoxColumn");
            startRateDataGridViewTextBoxColumn.Name = "startRateDataGridViewTextBoxColumn";
            // 
            // endRateDataGridViewTextBoxColumn
            // 
            endRateDataGridViewTextBoxColumn.DataPropertyName = "EndRate";
            resources.ApplyResources(endRateDataGridViewTextBoxColumn, "endRateDataGridViewTextBoxColumn");
            endRateDataGridViewTextBoxColumn.Name = "endRateDataGridViewTextBoxColumn";
            // 
            // rateValueDataGridViewTextBoxColumn
            // 
            rateValueDataGridViewTextBoxColumn.DataPropertyName = "RateValue";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N5";
            rateValueDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(rateValueDataGridViewTextBoxColumn, "rateValueDataGridViewTextBoxColumn");
            rateValueDataGridViewTextBoxColumn.Name = "rateValueDataGridViewTextBoxColumn";
            // 
            // expectedPriceChangeDataGridViewTextBoxColumn
            // 
            expectedPriceChangeDataGridViewTextBoxColumn.DataPropertyName = "ExpectedPriceChange";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N3";
            expectedPriceChangeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(expectedPriceChangeDataGridViewTextBoxColumn, "expectedPriceChangeDataGridViewTextBoxColumn");
            expectedPriceChangeDataGridViewTextBoxColumn.Name = "expectedPriceChangeDataGridViewTextBoxColumn";
            // 
            // priceChangeDataGridViewTextBoxColumn
            // 
            priceChangeDataGridViewTextBoxColumn.DataPropertyName = "PriceChange";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N3";
            priceChangeDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(priceChangeDataGridViewTextBoxColumn, "priceChangeDataGridViewTextBoxColumn");
            priceChangeDataGridViewTextBoxColumn.Name = "priceChangeDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // FrmRates
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(LblAlwaysCalculatedWith);
            Controls.Add(GbRate);
            Controls.Add(DgRates);
            Controls.Add(LblRateSource);
            Controls.Add(TxtDescription);
            Controls.Add(CboTarifGroup);
            Controls.Add(LblTarifGroup);
            Controls.Add(CboCostCategory);
            Controls.Add(LblCostCategory);
            Controls.Add(CboEnergyType);
            Controls.Add(LblEnergyType);
            Controls.Add(StatusStrip1);
            Controls.Add(ToolStrip1);
            Name = "FrmRates";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += FrmRates_FormClosing;
            Load += FrmRates_Load;
            ToolStrip1.ResumeLayout(false);
            ToolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsRates).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsAdditionalCategoryAndGroupInfo).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsCostCategories).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsTarifGroups).EndInit();
            ((System.ComponentModel.ISupportInitialize)DgRates).EndInit();
            GbRate.ResumeLayout(false);
            GbRate.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsRateType).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip ToolStrip1;
        private ToolStripButton TsbAdd;
        private ToolStripButton TsbSave;
        private ToolStripButton TsbDelete;
        private ToolStripButton TsbRefresh;
        private ToolStripButton TbsClose;
        private StatusStrip StatusStrip1;
        private BindingSource bsRates;
        private BindingSource bsAdditionalCategoryAndGroupInfo;
        private ComboBox CboEnergyType;
        private Label LblEnergyType;
        private ComboBox CboCostCategory;
        private Label LblCostCategory;
        private ComboBox CboTarifGroup;
        private Label LblTarifGroup;
        private Label LblRateSource;
        private TextBox TxtDescription;
        private DataGridView DgRates;
        private BindingSource bsCostCategories;
        private BindingSource bsTarifGroups;
        private BindingSource bsEnergyTypes;
        private GroupBox GbRate;
        private DateTimePicker DtpEndRate;
        private DateTimePicker DtpStartRate;
        private Label LblRange;
        private Label LblDescription;
        private TextBox TxtRateDescription;
        private Label LblRate;
        private TextBox TxtRate;
        private Label LblPriceChangeHint;
        private Label LblPriceChange;
        private TextBox TxtPriceChange;
        private Label LblExpectedPriceChangeHint;
        private Label LblExchangeRate;
        private TextBox TxtExpectedPriceChange;
        private ToolTip toolTip1;
        private Label LblAlwaysCalculatedWith;
        private ToolStripButton TbsCancel;
        private ComboBox CboRateType;
        private Label LblRateType;
        private BindingSource bsRateType;
        private ucControls.ucStaffel ucStaffel1;
        private DataGridViewTextBoxColumn startRateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endRateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rateValueDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn expectedPriceChangeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceChangeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    }
}