namespace WinFormsEF
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endRateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.rateValueDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.expectedPriceChangeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.priceChangeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.energyTypeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.costCategoryDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.energyTypesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addressesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.metersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.costCategoriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tarifGroupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.energyRatesAndTarifsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.predefinedPeriodsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.correctionFactorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vatTarifsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.calculatedUnitPriceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.paymentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.backUpAndRestoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMeterRatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ratesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maintenanceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalculateCurrentSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.recalculateAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setupNewFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.createDemoDataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.graphToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ratesToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.reportsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settlementToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rateReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbImportData = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbUsageGraph = new System.Windows.Forms.ToolStripButton();
            this.tsbCompareChart = new System.Windows.Forms.ToolStripButton();
            this.tsbRates = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbPdfReport = new System.Windows.Forms.ToolStripButton();
            this.tsbPayBackTime = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.tbTop = new System.Windows.Forms.GroupBox();
            this.CboEnergyType = new System.Windows.Forms.ComboBox();
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.CboAddress = new System.Windows.Forms.ComboBox();
            this.bsAddresses = new System.Windows.Forms.BindingSource(this.components);
            this.lblAddress = new System.Windows.Forms.Label();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.helpProvider1 = new System.Windows.Forms.HelpProvider();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.tbTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(this.splitContainer1, "splitContainer1");
            this.splitContainer1.Cursor = System.Windows.Forms.Cursors.VSplit;
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(this.splitContainer1.Panel1, "splitContainer1.Panel1");
            this.helpProvider1.SetShowHelp(this.splitContainer1.Panel1, ((bool)(resources.GetObject("splitContainer1.Panel1.ShowHelp"))));
            this.splitContainer1.Panel1.Tag = "Panel1";
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(this.splitContainer1.Panel2, "splitContainer1.Panel2");
            this.helpProvider1.SetShowHelp(this.splitContainer1.Panel2, ((bool)(resources.GetObject("splitContainer1.Panel2.ShowHelp"))));
            this.splitContainer1.Panel2.Tag = "Panel2";
            this.helpProvider1.SetShowHelp(this.splitContainer1, ((bool)(resources.GetObject("splitContainer1.ShowHelp"))));
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            this.splitContainer1.Paint += new System.Windows.Forms.PaintEventHandler(this.splitContainer1_Paint);
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // startRateDataGridViewTextBoxColumn
            // 
            this.startRateDataGridViewTextBoxColumn.DataPropertyName = "StartRate";
            resources.ApplyResources(this.startRateDataGridViewTextBoxColumn, "startRateDataGridViewTextBoxColumn");
            this.startRateDataGridViewTextBoxColumn.Name = "startRateDataGridViewTextBoxColumn";
            // 
            // endRateDataGridViewTextBoxColumn
            // 
            this.endRateDataGridViewTextBoxColumn.DataPropertyName = "EndRate";
            resources.ApplyResources(this.endRateDataGridViewTextBoxColumn, "endRateDataGridViewTextBoxColumn");
            this.endRateDataGridViewTextBoxColumn.Name = "endRateDataGridViewTextBoxColumn";
            // 
            // rateValueDataGridViewTextBoxColumn
            // 
            this.rateValueDataGridViewTextBoxColumn.DataPropertyName = "RateValue";
            resources.ApplyResources(this.rateValueDataGridViewTextBoxColumn, "rateValueDataGridViewTextBoxColumn");
            this.rateValueDataGridViewTextBoxColumn.Name = "rateValueDataGridViewTextBoxColumn";
            // 
            // expectedPriceChangeDataGridViewTextBoxColumn
            // 
            this.expectedPriceChangeDataGridViewTextBoxColumn.DataPropertyName = "ExpectedPriceChange";
            resources.ApplyResources(this.expectedPriceChangeDataGridViewTextBoxColumn, "expectedPriceChangeDataGridViewTextBoxColumn");
            this.expectedPriceChangeDataGridViewTextBoxColumn.Name = "expectedPriceChangeDataGridViewTextBoxColumn";
            // 
            // priceChangeDataGridViewTextBoxColumn
            // 
            this.priceChangeDataGridViewTextBoxColumn.DataPropertyName = "PriceChange";
            resources.ApplyResources(this.priceChangeDataGridViewTextBoxColumn, "priceChangeDataGridViewTextBoxColumn");
            this.priceChangeDataGridViewTextBoxColumn.Name = "priceChangeDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(this.descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // energyTypeDataGridViewTextBoxColumn
            // 
            this.energyTypeDataGridViewTextBoxColumn.DataPropertyName = "EnergyType";
            resources.ApplyResources(this.energyTypeDataGridViewTextBoxColumn, "energyTypeDataGridViewTextBoxColumn");
            this.energyTypeDataGridViewTextBoxColumn.Name = "energyTypeDataGridViewTextBoxColumn";
            // 
            // costCategoryDataGridViewTextBoxColumn
            // 
            this.costCategoryDataGridViewTextBoxColumn.DataPropertyName = "CostCategory";
            resources.ApplyResources(this.costCategoryDataGridViewTextBoxColumn, "costCategoryDataGridViewTextBoxColumn");
            this.costCategoryDataGridViewTextBoxColumn.Name = "costCategoryDataGridViewTextBoxColumn";
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.graphToolStripMenuItem,
            this.reportsToolStripMenuItem,
            this.infoToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            this.helpProvider1.SetShowHelp(this.menuStrip1, ((bool)(resources.GetObject("menuStrip1.ShowHelp"))));
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.backUpAndRestoreToolStripMenuItem,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.maintenanceToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generalToolStripMenuItem,
            this.energyTypesToolStripMenuItem,
            this.addressesToolStripMenuItem,
            this.metersToolStripMenuItem,
            this.costCategoriesToolStripMenuItem,
            this.tarifGroupsToolStripMenuItem,
            this.energyRatesAndTarifsToolStripMenuItem,
            this.predefinedPeriodsToolStripMenuItem,
            this.correctionFactorsToolStripMenuItem,
            this.nettingToolStripMenuItem,
            this.vatTarifsToolStripMenuItem,
            this.calculatedUnitPriceToolStripMenuItem,
            this.paymentsToolStripMenuItem});
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(this.settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // generalToolStripMenuItem
            // 
            this.generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            resources.ApplyResources(this.generalToolStripMenuItem, "generalToolStripMenuItem");
            this.generalToolStripMenuItem.Click += new System.EventHandler(this.generalToolStripMenuItem_Click);
            // 
            // energyTypesToolStripMenuItem
            // 
            this.energyTypesToolStripMenuItem.Name = "energyTypesToolStripMenuItem";
            resources.ApplyResources(this.energyTypesToolStripMenuItem, "energyTypesToolStripMenuItem");
            this.energyTypesToolStripMenuItem.Click += new System.EventHandler(this.energyTypesToolStripMenuItem_Click);
            // 
            // addressesToolStripMenuItem
            // 
            this.addressesToolStripMenuItem.Name = "addressesToolStripMenuItem";
            resources.ApplyResources(this.addressesToolStripMenuItem, "addressesToolStripMenuItem");
            this.addressesToolStripMenuItem.Click += new System.EventHandler(this.addressesToolStripMenuItem_Click);
            // 
            // metersToolStripMenuItem
            // 
            this.metersToolStripMenuItem.Name = "metersToolStripMenuItem";
            resources.ApplyResources(this.metersToolStripMenuItem, "metersToolStripMenuItem");
            this.metersToolStripMenuItem.Click += new System.EventHandler(this.metersToolStripMenuItem_Click);
            // 
            // costCategoriesToolStripMenuItem
            // 
            this.costCategoriesToolStripMenuItem.Name = "costCategoriesToolStripMenuItem";
            resources.ApplyResources(this.costCategoriesToolStripMenuItem, "costCategoriesToolStripMenuItem");
            this.costCategoriesToolStripMenuItem.Click += new System.EventHandler(this.costCategoriesToolStripMenuItem_Click);
            // 
            // tarifGroupsToolStripMenuItem
            // 
            this.tarifGroupsToolStripMenuItem.Name = "tarifGroupsToolStripMenuItem";
            resources.ApplyResources(this.tarifGroupsToolStripMenuItem, "tarifGroupsToolStripMenuItem");
            this.tarifGroupsToolStripMenuItem.Click += new System.EventHandler(this.tarifGroupsToolStripMenuItem_Click);
            // 
            // energyRatesAndTarifsToolStripMenuItem
            // 
            this.energyRatesAndTarifsToolStripMenuItem.Name = "energyRatesAndTarifsToolStripMenuItem";
            resources.ApplyResources(this.energyRatesAndTarifsToolStripMenuItem, "energyRatesAndTarifsToolStripMenuItem");
            this.energyRatesAndTarifsToolStripMenuItem.Click += new System.EventHandler(this.energyRatesAndTarifsToolStripMenuItem_Click);
            // 
            // predefinedPeriodsToolStripMenuItem
            // 
            this.predefinedPeriodsToolStripMenuItem.Name = "predefinedPeriodsToolStripMenuItem";
            resources.ApplyResources(this.predefinedPeriodsToolStripMenuItem, "predefinedPeriodsToolStripMenuItem");
            this.predefinedPeriodsToolStripMenuItem.Click += new System.EventHandler(this.predefinedPeriodsToolStripMenuItem_Click);
            // 
            // correctionFactorsToolStripMenuItem
            // 
            this.correctionFactorsToolStripMenuItem.Name = "correctionFactorsToolStripMenuItem";
            resources.ApplyResources(this.correctionFactorsToolStripMenuItem, "correctionFactorsToolStripMenuItem");
            this.correctionFactorsToolStripMenuItem.Click += new System.EventHandler(this.correctionFactorsToolStripMenuItem_Click);
            // 
            // nettingToolStripMenuItem
            // 
            this.nettingToolStripMenuItem.Name = "nettingToolStripMenuItem";
            resources.ApplyResources(this.nettingToolStripMenuItem, "nettingToolStripMenuItem");
            this.nettingToolStripMenuItem.Click += new System.EventHandler(this.nettingToolStripMenuItem_Click);
            // 
            // vatTarifsToolStripMenuItem
            // 
            this.vatTarifsToolStripMenuItem.Name = "vatTarifsToolStripMenuItem";
            resources.ApplyResources(this.vatTarifsToolStripMenuItem, "vatTarifsToolStripMenuItem");
            this.vatTarifsToolStripMenuItem.Click += new System.EventHandler(this.vatTarifsToolStripMenuItem_Click);
            // 
            // calculatedUnitPriceToolStripMenuItem
            // 
            this.calculatedUnitPriceToolStripMenuItem.Name = "calculatedUnitPriceToolStripMenuItem";
            resources.ApplyResources(this.calculatedUnitPriceToolStripMenuItem, "calculatedUnitPriceToolStripMenuItem");
            this.calculatedUnitPriceToolStripMenuItem.Click += new System.EventHandler(this.calculatedUnitPriceToolStripMenuItem_Click);
            // 
            // paymentsToolStripMenuItem
            // 
            this.paymentsToolStripMenuItem.Name = "paymentsToolStripMenuItem";
            resources.ApplyResources(this.paymentsToolStripMenuItem, "paymentsToolStripMenuItem");
            this.paymentsToolStripMenuItem.Click += new System.EventHandler(this.paymentsToolStripMenuItem_Click);
            // 
            // backUpAndRestoreToolStripMenuItem
            // 
            this.backUpAndRestoreToolStripMenuItem.Name = "backUpAndRestoreToolStripMenuItem";
            resources.ApplyResources(this.backUpAndRestoreToolStripMenuItem, "backUpAndRestoreToolStripMenuItem");
            this.backUpAndRestoreToolStripMenuItem.Click += new System.EventHandler(this.backUpAndRestoreToolStripMenuItem_Click);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportMeterRatesToolStripMenuItem});
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            resources.ApplyResources(this.importToolStripMenuItem, "importToolStripMenuItem");
            // 
            // exportMeterRatesToolStripMenuItem
            // 
            this.exportMeterRatesToolStripMenuItem.Name = "exportMeterRatesToolStripMenuItem";
            resources.ApplyResources(this.exportMeterRatesToolStripMenuItem, "exportMeterRatesToolStripMenuItem");
            this.exportMeterRatesToolStripMenuItem.Click += new System.EventHandler(this.importMeterRatesToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ratesToolStripMenuItem});
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(this.exportToolStripMenuItem, "exportToolStripMenuItem");
            // 
            // ratesToolStripMenuItem
            // 
            this.ratesToolStripMenuItem.Name = "ratesToolStripMenuItem";
            resources.ApplyResources(this.ratesToolStripMenuItem, "ratesToolStripMenuItem");
            this.ratesToolStripMenuItem.Click += new System.EventHandler(this.ratesToolStripMenuItem_Click);
            // 
            // maintenanceToolStripMenuItem
            // 
            this.maintenanceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.recalculateCurrentSelectionToolStripMenuItem,
            this.recalculateAllToolStripMenuItem,
            this.setupNewFileToolStripMenuItem,
            this.createDemoDataToolStripMenuItem});
            this.maintenanceToolStripMenuItem.Name = "maintenanceToolStripMenuItem";
            resources.ApplyResources(this.maintenanceToolStripMenuItem, "maintenanceToolStripMenuItem");
            // 
            // recalculateCurrentSelectionToolStripMenuItem
            // 
            this.recalculateCurrentSelectionToolStripMenuItem.Name = "recalculateCurrentSelectionToolStripMenuItem";
            resources.ApplyResources(this.recalculateCurrentSelectionToolStripMenuItem, "recalculateCurrentSelectionToolStripMenuItem");
            this.recalculateCurrentSelectionToolStripMenuItem.Click += new System.EventHandler(this.recalculateCurrentSelectionToolStripMenuItem_Click);
            // 
            // recalculateAllToolStripMenuItem
            // 
            this.recalculateAllToolStripMenuItem.Name = "recalculateAllToolStripMenuItem";
            resources.ApplyResources(this.recalculateAllToolStripMenuItem, "recalculateAllToolStripMenuItem");
            this.recalculateAllToolStripMenuItem.Click += new System.EventHandler(this.recalculateAllToolStripMenuItem_Click);
            // 
            // setupNewFileToolStripMenuItem
            // 
            this.setupNewFileToolStripMenuItem.Name = "setupNewFileToolStripMenuItem";
            resources.ApplyResources(this.setupNewFileToolStripMenuItem, "setupNewFileToolStripMenuItem");
            this.setupNewFileToolStripMenuItem.Click += new System.EventHandler(this.setupNewFileToolStripMenuItem_Click);
            // 
            // createDemoDataToolStripMenuItem
            // 
            this.createDemoDataToolStripMenuItem.Name = "createDemoDataToolStripMenuItem";
            resources.ApplyResources(this.createDemoDataToolStripMenuItem, "createDemoDataToolStripMenuItem");
            this.createDemoDataToolStripMenuItem.Click += new System.EventHandler(this.createDemoDataToolStripMenuItem_Click);
            // 
            // graphToolStripMenuItem
            // 
            this.graphToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.usageToolStripMenuItem,
            this.compareToolStripMenuItem,
            this.ratesToolStripMenuItem1});
            this.graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            resources.ApplyResources(this.graphToolStripMenuItem, "graphToolStripMenuItem");
            // 
            // usageToolStripMenuItem
            // 
            this.usageToolStripMenuItem.Name = "usageToolStripMenuItem";
            resources.ApplyResources(this.usageToolStripMenuItem, "usageToolStripMenuItem");
            this.usageToolStripMenuItem.Click += new System.EventHandler(this.usageToolStripMenuItem_Click);
            // 
            // compareToolStripMenuItem
            // 
            this.compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            resources.ApplyResources(this.compareToolStripMenuItem, "compareToolStripMenuItem");
            this.compareToolStripMenuItem.Click += new System.EventHandler(this.compareToolStripMenuItem_Click);
            // 
            // ratesToolStripMenuItem1
            // 
            this.ratesToolStripMenuItem1.Name = "ratesToolStripMenuItem1";
            resources.ApplyResources(this.ratesToolStripMenuItem1, "ratesToolStripMenuItem1");
            this.ratesToolStripMenuItem1.Click += new System.EventHandler(this.ratesToolStripMenuItem1_Click);
            // 
            // reportsToolStripMenuItem
            // 
            this.reportsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settlementToolStripMenuItem,
            this.rateReportToolStripMenuItem});
            this.reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            resources.ApplyResources(this.reportsToolStripMenuItem, "reportsToolStripMenuItem");
            // 
            // settlementToolStripMenuItem
            // 
            this.settlementToolStripMenuItem.Name = "settlementToolStripMenuItem";
            resources.ApplyResources(this.settlementToolStripMenuItem, "settlementToolStripMenuItem");
            this.settlementToolStripMenuItem.Click += new System.EventHandler(this.settlementToolStripMenuItem_Click);
            // 
            // rateReportToolStripMenuItem
            // 
            this.rateReportToolStripMenuItem.Name = "rateReportToolStripMenuItem";
            resources.ApplyResources(this.rateReportToolStripMenuItem, "rateReportToolStripMenuItem");
            this.rateReportToolStripMenuItem.Click += new System.EventHandler(this.rateReportToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            resources.ApplyResources(this.infoToolStripMenuItem, "infoToolStripMenuItem");
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbData,
            this.toolStripSeparator1,
            this.tsbImportData,
            this.toolStripSeparator2,
            this.tsbUsageGraph,
            this.tsbCompareChart,
            this.tsbRates,
            this.toolStripSeparator3,
            this.tsbPdfReport,
            this.tsbPayBackTime,
            this.toolStripSeparator4,
            this.tsbRefresh,
            this.tsbClose});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            this.helpProvider1.SetShowHelp(this.toolStrip1, ((bool)(resources.GetObject("toolStrip1.ShowHelp"))));
            // 
            // tsbData
            // 
            this.tsbData.Image = global::WinFormsUI.Properties.Resources.square_24x24;
            resources.ApplyResources(this.tsbData, "tsbData");
            this.tsbData.Name = "tsbData";
            this.tsbData.Click += new System.EventHandler(this.tsbData_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbImportData
            // 
            this.tsbImportData.Image = global::WinFormsUI.Properties.Resources.download_24x24;
            resources.ApplyResources(this.tsbImportData, "tsbImportData");
            this.tsbImportData.Name = "tsbImportData";
            this.tsbImportData.Click += new System.EventHandler(this.tsbImportData_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsbUsageGraph
            // 
            this.tsbUsageGraph.Image = global::WinFormsUI.Properties.Resources.statistics_24x24;
            resources.ApplyResources(this.tsbUsageGraph, "tsbUsageGraph");
            this.tsbUsageGraph.Name = "tsbUsageGraph";
            this.tsbUsageGraph.Click += new System.EventHandler(this.tsbUsageGraph_Click);
            // 
            // tsbCompareChart
            // 
            this.tsbCompareChart.Image = global::WinFormsUI.Properties.Resources.statistics_24x24;
            resources.ApplyResources(this.tsbCompareChart, "tsbCompareChart");
            this.tsbCompareChart.Name = "tsbCompareChart";
            this.tsbCompareChart.Click += new System.EventHandler(this.tsbCompareChart_Click);
            // 
            // tsbRates
            // 
            this.tsbRates.Image = global::WinFormsUI.Properties.Resources.statistics_24x24;
            resources.ApplyResources(this.tsbRates, "tsbRates");
            this.tsbRates.Name = "tsbRates";
            this.tsbRates.Click += new System.EventHandler(this.tsbRates_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsbPdfReport
            // 
            this.tsbPdfReport.Image = global::WinFormsUI.Properties.Resources.paper_list_24x24;
            resources.ApplyResources(this.tsbPdfReport, "tsbPdfReport");
            this.tsbPdfReport.Name = "tsbPdfReport";
            this.tsbPdfReport.Click += new System.EventHandler(this.tsbPdfReport_Click);
            // 
            // tsbPayBackTime
            // 
            this.tsbPayBackTime.Image = global::WinFormsUI.Properties.Resources.calculator_24x24;
            resources.ApplyResources(this.tsbPayBackTime, "tsbPayBackTime");
            this.tsbPayBackTime.Name = "tsbPayBackTime";
            this.tsbPayBackTime.Click += new System.EventHandler(this.tsbPayBackTime_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(this.toolStripSeparator4, "toolStripSeparator4");
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(this.tsbRefresh, "tsbRefresh");
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbClose, "tsbClose");
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // tbTop
            // 
            resources.ApplyResources(this.tbTop, "tbTop");
            this.tbTop.Controls.Add(this.CboEnergyType);
            this.tbTop.Controls.Add(this.lblEnergyType);
            this.tbTop.Controls.Add(this.CboAddress);
            this.tbTop.Controls.Add(this.lblAddress);
            this.tbTop.Name = "tbTop";
            this.helpProvider1.SetShowHelp(this.tbTop, ((bool)(resources.GetObject("tbTop.ShowHelp"))));
            this.tbTop.TabStop = false;
            // 
            // CboEnergyType
            // 
            this.CboEnergyType.DataSource = this.bsEnergyTypes;
            this.CboEnergyType.DisplayMember = "Name";
            this.CboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.CboEnergyType, "CboEnergyType");
            this.CboEnergyType.Name = "CboEnergyType";
            this.helpProvider1.SetShowHelp(this.CboEnergyType, ((bool)(resources.GetObject("CboEnergyType.ShowHelp"))));
            this.CboEnergyType.ValueMember = "Id";
            this.CboEnergyType.SelectedIndexChanged += new System.EventHandler(this.cboEnergyType_SelectedIndexChanged);
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            this.helpProvider1.SetShowHelp(this.lblEnergyType, ((bool)(resources.GetObject("lblEnergyType.ShowHelp"))));
            // 
            // CboAddress
            // 
            this.CboAddress.DataSource = this.bsAddresses;
            this.CboAddress.DisplayMember = "Description";
            this.CboAddress.FormattingEnabled = true;
            resources.ApplyResources(this.CboAddress, "CboAddress");
            this.CboAddress.Name = "CboAddress";
            this.helpProvider1.SetShowHelp(this.CboAddress, ((bool)(resources.GetObject("CboAddress.ShowHelp"))));
            this.CboAddress.ValueMember = "Id";
            this.CboAddress.SelectedIndexChanged += new System.EventHandler(this.cmbAddress_SelectedIndexChanged);
            // 
            // bsAddresses
            // 
            this.bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            this.helpProvider1.SetShowHelp(this.lblAddress, ((bool)(resources.GetObject("lblAddress.ShowHelp"))));
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            this.helpProvider1.SetShowHelp(this.statusStrip1, ((bool)(resources.GetObject("statusStrip1.ShowHelp"))));
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tbTop);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.HelpButton = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.helpProvider1.SetShowHelp(this, ((bool)(resources.GetObject("$this.ShowHelp"))));
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.tbTop.ResumeLayout(false);
            this.tbTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startRateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endRateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn rateValueDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn expectedPriceChangeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn priceChangeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn energyTypeDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn costCategoryDataGridViewTextBoxColumn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem addressesToolStripMenuItem;
        private ToolStripMenuItem energyTypesToolStripMenuItem;
        private ToolStripMenuItem infoToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbData;
        private ToolStripButton tsbImportData;
        private ToolStripButton tsbUsageGraph;
        private ToolStripButton tsbCompareChart;
        private ToolStripButton tsbRates;
        private ToolStripButton tsbPdfReport;
        private ToolStripButton tsbPayBackTime;
        private ToolStripButton tsbRefresh;
        private GroupBox tbTop;
        private ComboBox CboEnergyType;
        private Label lblEnergyType;
        private ComboBox CboAddress;
        private Label lblAddress;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripSeparator toolStripSeparator4;
        private SplitContainer splitContainer1;
        private StatusStrip statusStrip1;
        private ToolStripButton tsbClose;
        private ToolTip toolTip1;
        private BindingSource bsAddresses;
        private BindingSource bsEnergyTypes;
        private ToolStripMenuItem generalToolStripMenuItem;
        private ToolStripMenuItem metersToolStripMenuItem;
        private ToolStripMenuItem costCategoriesToolStripMenuItem;
        private ToolStripMenuItem tarifGroupsToolStripMenuItem;
        private ToolStripMenuItem energyRatesAndTarifsToolStripMenuItem;
        private ToolStripMenuItem predefinedPeriodsToolStripMenuItem;
        private ToolStripMenuItem correctionFactorsToolStripMenuItem;
        private ToolStripMenuItem nettingToolStripMenuItem;
        private ToolStripMenuItem backUpAndRestoreToolStripMenuItem;
        private ToolStripMenuItem importToolStripMenuItem;
        private ToolStripMenuItem exportMeterRatesToolStripMenuItem;
        private ToolStripMenuItem exportToolStripMenuItem;
        private ToolStripMenuItem ratesToolStripMenuItem;
        private ToolStripMenuItem maintenanceToolStripMenuItem;
        private ToolStripMenuItem vatTarifsToolStripMenuItem;
        private HelpProvider helpProvider1;
        private ToolStripMenuItem recalculateCurrentSelectionToolStripMenuItem;
        private ToolStripMenuItem recalculateAllToolStripMenuItem;
        private ToolStripMenuItem setupNewFileToolStripMenuItem;
        private ToolStripMenuItem createDemoDataToolStripMenuItem;
        private ToolStripMenuItem graphToolStripMenuItem;
        private ToolStripMenuItem usageToolStripMenuItem;
        private ToolStripMenuItem compareToolStripMenuItem;
        private ToolStripMenuItem ratesToolStripMenuItem1;
        private ToolStripMenuItem calculatedUnitPriceToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem settlementToolStripMenuItem;
        private ToolStripMenuItem paymentsToolStripMenuItem;
        private ToolStripMenuItem rateReportToolStripMenuItem;
    }
}