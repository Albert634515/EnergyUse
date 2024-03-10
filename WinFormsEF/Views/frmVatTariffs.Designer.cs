namespace WinFormsEF.Views
{
    partial class frmVatTariffs
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVatTariffs));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            bsVatTarif = new BindingSource(components);
            toolTip1 = new ToolTip(components);
            toolStrip1 = new ToolStrip();
            TsbAddVatTarif = new ToolStripButton();
            TsbSaveVatTarif = new ToolStripButton();
            TbsCancel = new ToolStripButton();
            TsbDeleteVatTarif = new ToolStripButton();
            TsbRefreshVatTarif = new ToolStripButton();
            TsbCloseVatTarif = new ToolStripButton();
            CboCostCategory = new ComboBox();
            bsCostCategories = new BindingSource(components);
            LblCostCategory = new Label();
            CboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            LblEnergyType = new Label();
            DgVatTarifs = new DataGridView();
            GbTarif = new GroupBox();
            label2 = new Label();
            TxtRateValue = new TextBox();
            LblTarif = new Label();
            LblEndDate = new Label();
            DtEndDate = new DateTimePicker();
            LblStartDate = new Label();
            DtStartDate = new DateTimePicker();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            StartDate = new DataGridViewTextBoxColumn();
            EndDate = new DataGridViewTextBoxColumn();
            Tarif = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)bsVatTarif).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsCostCategories).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)DgVatTarifs).BeginInit();
            GbTarif.SuspendLayout();
            SuspendLayout();
            // 
            // bsVatTarif
            // 
            bsVatTarif.DataSource = typeof(EnergyUse.Models.VatTarif);
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { TsbAddVatTarif, TsbSaveVatTarif, TbsCancel, TsbDeleteVatTarif, TsbRefreshVatTarif, TsbCloseVatTarif });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // TsbAddVatTarif
            // 
            TsbAddVatTarif.Image = WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(TsbAddVatTarif, "TsbAddVatTarif");
            TsbAddVatTarif.Name = "TsbAddVatTarif";
            TsbAddVatTarif.Click += TsbAddVatTarif_Click;
            // 
            // TsbSaveVatTarif
            // 
            TsbSaveVatTarif.Image = WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(TsbSaveVatTarif, "TsbSaveVatTarif");
            TsbSaveVatTarif.Name = "TsbSaveVatTarif";
            TsbSaveVatTarif.Click += TsbSaveVatTarif_Click;
            // 
            // TbsCancel
            // 
            TbsCancel.Image = WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(TbsCancel, "TbsCancel");
            TbsCancel.Name = "TbsCancel";
            TbsCancel.Click += TbsCancel_Click;
            // 
            // TsbDeleteVatTarif
            // 
            TsbDeleteVatTarif.Image = WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(TsbDeleteVatTarif, "TsbDeleteVatTarif");
            TsbDeleteVatTarif.Name = "TsbDeleteVatTarif";
            TsbDeleteVatTarif.Click += TsbDeleteVatTarif_Click;
            // 
            // TsbRefreshVatTarif
            // 
            TsbRefreshVatTarif.Image = WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(TsbRefreshVatTarif, "TsbRefreshVatTarif");
            TsbRefreshVatTarif.Name = "TsbRefreshVatTarif";
            TsbRefreshVatTarif.Click += TsbRefreshVatTarif_Click;
            // 
            // TsbCloseVatTarif
            // 
            TsbCloseVatTarif.Alignment = ToolStripItemAlignment.Right;
            TsbCloseVatTarif.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(TsbCloseVatTarif, "TsbCloseVatTarif");
            TsbCloseVatTarif.Name = "TsbCloseVatTarif";
            TsbCloseVatTarif.Click += TsbCloseVatTarif_Click;
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
            // DgVatTarifs
            // 
            DgVatTarifs.AllowUserToAddRows = false;
            DgVatTarifs.AllowUserToDeleteRows = false;
            resources.ApplyResources(DgVatTarifs, "DgVatTarifs");
            DgVatTarifs.AutoGenerateColumns = false;
            DgVatTarifs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgVatTarifs.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, StartDate, EndDate, Tarif });
            DgVatTarifs.DataSource = bsVatTarif;
            DgVatTarifs.Name = "DgVatTarifs";
            // 
            // GbTarif
            // 
            GbTarif.Controls.Add(label2);
            GbTarif.Controls.Add(TxtRateValue);
            GbTarif.Controls.Add(LblTarif);
            GbTarif.Controls.Add(LblEndDate);
            GbTarif.Controls.Add(DtEndDate);
            GbTarif.Controls.Add(LblStartDate);
            GbTarif.Controls.Add(DtStartDate);
            resources.ApplyResources(GbTarif, "GbTarif");
            GbTarif.Name = "GbTarif";
            GbTarif.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // TxtRateValue
            // 
            TxtRateValue.DataBindings.Add(new Binding("Text", bsVatTarif, "Tarif", true));
            resources.ApplyResources(TxtRateValue, "TxtRateValue");
            TxtRateValue.Name = "TxtRateValue";
            // 
            // LblTarif
            // 
            resources.ApplyResources(LblTarif, "LblTarif");
            LblTarif.Name = "LblTarif";
            // 
            // LblEndDate
            // 
            resources.ApplyResources(LblEndDate, "LblEndDate");
            LblEndDate.Name = "LblEndDate";
            // 
            // DtEndDate
            // 
            DtEndDate.DataBindings.Add(new Binding("Value", bsVatTarif, "EndDate", true));
            DtEndDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DtEndDate, "DtEndDate");
            DtEndDate.Name = "DtEndDate";
            // 
            // LblStartDate
            // 
            resources.ApplyResources(LblStartDate, "LblStartDate");
            LblStartDate.Name = "LblStartDate";
            // 
            // DtStartDate
            // 
            DtStartDate.DataBindings.Add(new Binding("Value", bsVatTarif, "StartDate", true));
            DtStartDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DtStartDate, "DtStartDate");
            DtStartDate.Name = "DtStartDate";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // StartDate
            // 
            StartDate.DataPropertyName = "StartDate";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            dataGridViewCellStyle1.NullValue = null;
            StartDate.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(StartDate, "StartDate");
            StartDate.Name = "StartDate";
            // 
            // EndDate
            // 
            EndDate.DataPropertyName = "EndDate";
            resources.ApplyResources(EndDate, "EndDate");
            EndDate.Name = "EndDate";
            // 
            // Tarif
            // 
            Tarif.DataPropertyName = "Tarif";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            Tarif.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(Tarif, "Tarif");
            Tarif.Name = "Tarif";
            // 
            // frmVatTariffs
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(GbTarif);
            Controls.Add(DgVatTarifs);
            Controls.Add(CboCostCategory);
            Controls.Add(LblCostCategory);
            Controls.Add(CboEnergyType);
            Controls.Add(LblEnergyType);
            Controls.Add(toolStrip1);
            Name = "frmVatTariffs";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += FrmVatTarifs_FormClosing;
            ((System.ComponentModel.ISupportInitialize)bsVatTarif).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsCostCategories).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)DgVatTarifs).EndInit();
            GbTarif.ResumeLayout(false);
            GbTarif.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource bsVatTarif;
        private ToolTip toolTip1;
        private ToolStripButton TsbCloseVatTarif;
        private ToolStripButton TsbRefreshVatTarif;
        private ToolStripButton TsbDeleteVatTarif;
        private ToolStripButton TsbSaveVatTarif;
        private ToolStripButton TsbAddVatTarif;
        private ToolStrip toolStrip1;
        private ComboBox CboCostCategory;
        private Label LblCostCategory;
        private ComboBox CboEnergyType;
        private Label LblEnergyType;
        private BindingSource bsEnergyTypes;
        private BindingSource bsCostCategories;
        private DataGridViewTextBoxColumn TarifDataGridViewTextBoxColumn;
        private DataGridView DgVatTarifs;
        private GroupBox GbTarif;
        private Label LblStartDate;
        private DateTimePicker DtStartDate;
        private Label LblEndDate;
        private DateTimePicker DtEndDate;
        private Label LblTarif;
        private TextBox TxtRateValue;
        private Label label2;
        private ToolStripButton TbsCancel;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn StartDate;
        private DataGridViewTextBoxColumn EndDate;
        private DataGridViewTextBoxColumn Tarif;
    }
}