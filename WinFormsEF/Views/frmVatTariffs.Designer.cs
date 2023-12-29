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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVatTariffs));
            this.bsVatTarif = new System.Windows.Forms.BindingSource(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.TsbAddVatTarif = new System.Windows.Forms.ToolStripButton();
            this.TsbSaveVatTarif = new System.Windows.Forms.ToolStripButton();
            this.TbsCancel = new System.Windows.Forms.ToolStripButton();
            this.TsbDeleteVatTarif = new System.Windows.Forms.ToolStripButton();
            this.TsbRefreshVatTarif = new System.Windows.Forms.ToolStripButton();
            this.TsbCloseVatTarif = new System.Windows.Forms.ToolStripButton();
            this.CboCostCategory = new System.Windows.Forms.ComboBox();
            this.bsCostCategories = new System.Windows.Forms.BindingSource(this.components);
            this.LblCostCategory = new System.Windows.Forms.Label();
            this.CboEnergyType = new System.Windows.Forms.ComboBox();
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.LblEnergyType = new System.Windows.Forms.Label();
            this.DgVatTarifs = new System.Windows.Forms.DataGridView();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.EndDate = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tarif = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GbTarif = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TxtRateValue = new System.Windows.Forms.TextBox();
            this.LblTarif = new System.Windows.Forms.Label();
            this.LblEndDate = new System.Windows.Forms.Label();
            this.DtEndDate = new System.Windows.Forms.DateTimePicker();
            this.LblStartDate = new System.Windows.Forms.Label();
            this.DtStartDate = new System.Windows.Forms.DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)(this.bsVatTarif)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCostCategories)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgVatTarifs)).BeginInit();
            this.GbTarif.SuspendLayout();
            this.SuspendLayout();
            // 
            // bsVatTarif
            // 
            this.bsVatTarif.DataSource = typeof(EnergyUse.Models.VatTarif);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TsbAddVatTarif,
            this.TsbSaveVatTarif,
            this.TbsCancel,
            this.TsbDeleteVatTarif,
            this.TsbRefreshVatTarif,
            this.TsbCloseVatTarif});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // TsbAddVatTarif
            // 
            this.TsbAddVatTarif.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(this.TsbAddVatTarif, "TsbAddVatTarif");
            this.TsbAddVatTarif.Name = "TsbAddVatTarif";
            this.TsbAddVatTarif.Click += new System.EventHandler(this.TsbAddVatTarif_Click);
            // 
            // TsbSaveVatTarif
            // 
            this.TsbSaveVatTarif.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(this.TsbSaveVatTarif, "TsbSaveVatTarif");
            this.TsbSaveVatTarif.Name = "TsbSaveVatTarif";
            this.TsbSaveVatTarif.Click += new System.EventHandler(this.TsbSaveVatTarif_Click);
            // 
            // TbsCancel
            // 
            this.TbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(this.TbsCancel, "TbsCancel");
            this.TbsCancel.Name = "TbsCancel";
            this.TbsCancel.Click += new System.EventHandler(this.TbsCancel_Click);
            // 
            // TsbDeleteVatTarif
            // 
            this.TsbDeleteVatTarif.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(this.TsbDeleteVatTarif, "TsbDeleteVatTarif");
            this.TsbDeleteVatTarif.Name = "TsbDeleteVatTarif";
            this.TsbDeleteVatTarif.Click += new System.EventHandler(this.TsbDeleteVatTarif_Click);
            // 
            // TsbRefreshVatTarif
            // 
            this.TsbRefreshVatTarif.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(this.TsbRefreshVatTarif, "TsbRefreshVatTarif");
            this.TsbRefreshVatTarif.Name = "TsbRefreshVatTarif";
            this.TsbRefreshVatTarif.Click += new System.EventHandler(this.TsbRefreshVatTarif_Click);
            // 
            // TsbCloseVatTarif
            // 
            this.TsbCloseVatTarif.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TsbCloseVatTarif.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.TsbCloseVatTarif, "TsbCloseVatTarif");
            this.TsbCloseVatTarif.Name = "TsbCloseVatTarif";
            this.TsbCloseVatTarif.Click += new System.EventHandler(this.TsbCloseVatTarif_Click);
            // 
            // CboCostCategory
            // 
            this.CboCostCategory.DataSource = this.bsCostCategories;
            this.CboCostCategory.DisplayMember = "Name";
            this.CboCostCategory.FormattingEnabled = true;
            resources.ApplyResources(this.CboCostCategory, "CboCostCategory");
            this.CboCostCategory.Name = "CboCostCategory";
            this.CboCostCategory.ValueMember = "Id";
            this.CboCostCategory.SelectedIndexChanged += new System.EventHandler(this.CboCostCategory_SelectedIndexChanged);
            // 
            // bsCostCategories
            // 
            this.bsCostCategories.DataSource = typeof(EnergyUse.Models.CostCategory);
            // 
            // LblCostCategory
            // 
            resources.ApplyResources(this.LblCostCategory, "LblCostCategory");
            this.LblCostCategory.Name = "LblCostCategory";
            // 
            // CboEnergyType
            // 
            this.CboEnergyType.DataSource = this.bsEnergyTypes;
            this.CboEnergyType.DisplayMember = "Name";
            this.CboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.CboEnergyType, "CboEnergyType");
            this.CboEnergyType.Name = "CboEnergyType";
            this.CboEnergyType.ValueMember = "Id";
            this.CboEnergyType.SelectedIndexChanged += new System.EventHandler(this.CboEnergyType_SelectedIndexChanged);
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // LblEnergyType
            // 
            resources.ApplyResources(this.LblEnergyType, "LblEnergyType");
            this.LblEnergyType.Name = "LblEnergyType";
            // 
            // DgVatTarifs
            // 
            this.DgVatTarifs.AllowUserToAddRows = false;
            this.DgVatTarifs.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.DgVatTarifs, "DgVatTarifs");
            this.DgVatTarifs.AutoGenerateColumns = false;
            this.DgVatTarifs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgVatTarifs.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.StartDate,
            this.EndDate,
            this.Tarif});
            this.DgVatTarifs.DataSource = this.bsVatTarif;
            this.DgVatTarifs.Name = "DgVatTarifs";
            this.DgVatTarifs.RowTemplate.Height = 29;
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // StartDate
            // 
            this.StartDate.DataPropertyName = "StartDate";
            resources.ApplyResources(this.StartDate, "StartDate");
            this.StartDate.Name = "StartDate";
            // 
            // EndDate
            // 
            this.EndDate.DataPropertyName = "EndDate";
            resources.ApplyResources(this.EndDate, "EndDate");
            this.EndDate.Name = "EndDate";
            // 
            // Tarif
            // 
            this.Tarif.DataPropertyName = "Tarif";
            resources.ApplyResources(this.Tarif, "Tarif");
            this.Tarif.Name = "Tarif";
            // 
            // GbTarif
            // 
            this.GbTarif.Controls.Add(this.label2);
            this.GbTarif.Controls.Add(this.TxtRateValue);
            this.GbTarif.Controls.Add(this.LblTarif);
            this.GbTarif.Controls.Add(this.LblEndDate);
            this.GbTarif.Controls.Add(this.DtEndDate);
            this.GbTarif.Controls.Add(this.LblStartDate);
            this.GbTarif.Controls.Add(this.DtStartDate);
            resources.ApplyResources(this.GbTarif, "GbTarif");
            this.GbTarif.Name = "GbTarif";
            this.GbTarif.TabStop = false;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // TxtRateValue
            // 
            this.TxtRateValue.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsVatTarif, "Tarif", true));
            resources.ApplyResources(this.TxtRateValue, "TxtRateValue");
            this.TxtRateValue.Name = "TxtRateValue";
            // 
            // LblTarif
            // 
            resources.ApplyResources(this.LblTarif, "LblTarif");
            this.LblTarif.Name = "LblTarif";
            // 
            // LblEndDate
            // 
            resources.ApplyResources(this.LblEndDate, "LblEndDate");
            this.LblEndDate.Name = "LblEndDate";
            // 
            // DtEndDate
            // 
            this.DtEndDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsVatTarif, "EndDate", true));
            this.DtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.DtEndDate, "DtEndDate");
            this.DtEndDate.Name = "DtEndDate";
            // 
            // LblStartDate
            // 
            resources.ApplyResources(this.LblStartDate, "LblStartDate");
            this.LblStartDate.Name = "LblStartDate";
            // 
            // DtStartDate
            // 
            this.DtStartDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsVatTarif, "StartDate", true));
            this.DtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.DtStartDate, "DtStartDate");
            this.DtStartDate.Name = "DtStartDate";
            // 
            // frmVatTariffs
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GbTarif);
            this.Controls.Add(this.DgVatTarifs);
            this.Controls.Add(this.CboCostCategory);
            this.Controls.Add(this.LblCostCategory);
            this.Controls.Add(this.CboEnergyType);
            this.Controls.Add(this.LblEnergyType);
            this.Controls.Add(this.toolStrip1);
            this.Name = "frmVatTariffs";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmVatTarifs_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.bsVatTarif)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsCostCategories)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DgVatTarifs)).EndInit();
            this.GbTarif.ResumeLayout(false);
            this.GbTarif.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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