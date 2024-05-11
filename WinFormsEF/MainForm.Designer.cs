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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            splitContainer1 = new SplitContainer();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            startRateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            endRateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            rateValueDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            expectedPriceChangeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            priceChangeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            energyTypeDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            costCategoryDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            generalToolStripMenuItem = new ToolStripMenuItem();
            energyTypesToolStripMenuItem = new ToolStripMenuItem();
            addressesToolStripMenuItem = new ToolStripMenuItem();
            metersToolStripMenuItem = new ToolStripMenuItem();
            costCategoriesToolStripMenuItem = new ToolStripMenuItem();
            tarifGroupsToolStripMenuItem = new ToolStripMenuItem();
            energyRatesAndTarifsToolStripMenuItem = new ToolStripMenuItem();
            predefinedPeriodsToolStripMenuItem = new ToolStripMenuItem();
            correctionFactorsToolStripMenuItem = new ToolStripMenuItem();
            nettingToolStripMenuItem = new ToolStripMenuItem();
            vatTarifsToolStripMenuItem = new ToolStripMenuItem();
            calculatedUnitPriceToolStripMenuItem = new ToolStripMenuItem();
            paymentsToolStripMenuItem = new ToolStripMenuItem();
            backUpAndRestoreToolStripMenuItem = new ToolStripMenuItem();
            importToolStripMenuItem = new ToolStripMenuItem();
            exportMeterRatesToolStripMenuItem = new ToolStripMenuItem();
            exportToolStripMenuItem = new ToolStripMenuItem();
            ratesToolStripMenuItem = new ToolStripMenuItem();
            maintenanceToolStripMenuItem = new ToolStripMenuItem();
            recalculateCurrentSelectionToolStripMenuItem = new ToolStripMenuItem();
            recalculateAllToolStripMenuItem = new ToolStripMenuItem();
            setupNewFileToolStripMenuItem = new ToolStripMenuItem();
            createDemoDataToolStripMenuItem = new ToolStripMenuItem();
            graphToolStripMenuItem = new ToolStripMenuItem();
            usageToolStripMenuItem = new ToolStripMenuItem();
            compareToolStripMenuItem = new ToolStripMenuItem();
            ratesToolStripMenuItem1 = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            settlementToolStripMenuItem = new ToolStripMenuItem();
            rateReportToolStripMenuItem = new ToolStripMenuItem();
            infoToolStripMenuItem = new ToolStripMenuItem();
            toolStrip1 = new ToolStrip();
            tsbData = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            tsbImportData = new ToolStripButton();
            toolStripSeparator2 = new ToolStripSeparator();
            tsbUsageGraph = new ToolStripButton();
            tsbCompareChart = new ToolStripButton();
            tsbRates = new ToolStripButton();
            toolStripSeparator3 = new ToolStripSeparator();
            tsbPdfReport = new ToolStripButton();
            tsbPayBackTime = new ToolStripButton();
            toolStripSeparator4 = new ToolStripSeparator();
            tsbRefresh = new ToolStripButton();
            tsbClose = new ToolStripButton();
            tbTop = new GroupBox();
            CboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            lblEnergyType = new Label();
            CboAddress = new ComboBox();
            bsAddresses = new BindingSource(components);
            lblAddress = new Label();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            helpProvider1 = new HelpProvider();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.SuspendLayout();
            menuStrip1.SuspendLayout();
            toolStrip1.SuspendLayout();
            tbTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            resources.ApplyResources(splitContainer1, "splitContainer1");
            splitContainer1.Cursor = Cursors.VSplit;
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            resources.ApplyResources(splitContainer1.Panel1, "splitContainer1.Panel1");
            helpProvider1.SetShowHelp(splitContainer1.Panel1, (bool)resources.GetObject("splitContainer1.Panel1.ShowHelp"));
            splitContainer1.Panel1.Tag = "Panel1";
            // 
            // splitContainer1.Panel2
            // 
            resources.ApplyResources(splitContainer1.Panel2, "splitContainer1.Panel2");
            helpProvider1.SetShowHelp(splitContainer1.Panel2, (bool)resources.GetObject("splitContainer1.Panel2.ShowHelp"));
            splitContainer1.Panel2.Tag = "Panel2";
            helpProvider1.SetShowHelp(splitContainer1, (bool)resources.GetObject("splitContainer1.ShowHelp"));
            splitContainer1.SplitterMoved += splitContainer1_SplitterMoved;
            splitContainer1.Paint += splitContainer1_Paint;
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
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
            resources.ApplyResources(rateValueDataGridViewTextBoxColumn, "rateValueDataGridViewTextBoxColumn");
            rateValueDataGridViewTextBoxColumn.Name = "rateValueDataGridViewTextBoxColumn";
            // 
            // expectedPriceChangeDataGridViewTextBoxColumn
            // 
            expectedPriceChangeDataGridViewTextBoxColumn.DataPropertyName = "ExpectedPriceChange";
            resources.ApplyResources(expectedPriceChangeDataGridViewTextBoxColumn, "expectedPriceChangeDataGridViewTextBoxColumn");
            expectedPriceChangeDataGridViewTextBoxColumn.Name = "expectedPriceChangeDataGridViewTextBoxColumn";
            // 
            // priceChangeDataGridViewTextBoxColumn
            // 
            priceChangeDataGridViewTextBoxColumn.DataPropertyName = "PriceChange";
            resources.ApplyResources(priceChangeDataGridViewTextBoxColumn, "priceChangeDataGridViewTextBoxColumn");
            priceChangeDataGridViewTextBoxColumn.Name = "priceChangeDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // energyTypeDataGridViewTextBoxColumn
            // 
            energyTypeDataGridViewTextBoxColumn.DataPropertyName = "EnergyType";
            resources.ApplyResources(energyTypeDataGridViewTextBoxColumn, "energyTypeDataGridViewTextBoxColumn");
            energyTypeDataGridViewTextBoxColumn.Name = "energyTypeDataGridViewTextBoxColumn";
            // 
            // costCategoryDataGridViewTextBoxColumn
            // 
            costCategoryDataGridViewTextBoxColumn.DataPropertyName = "CostCategory";
            resources.ApplyResources(costCategoryDataGridViewTextBoxColumn, "costCategoryDataGridViewTextBoxColumn");
            costCategoryDataGridViewTextBoxColumn.Name = "costCategoryDataGridViewTextBoxColumn";
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(20, 20);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, graphToolStripMenuItem, reportsToolStripMenuItem, infoToolStripMenuItem });
            resources.ApplyResources(menuStrip1, "menuStrip1");
            menuStrip1.Name = "menuStrip1";
            helpProvider1.SetShowHelp(menuStrip1, (bool)resources.GetObject("menuStrip1.ShowHelp"));
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settingsToolStripMenuItem, backUpAndRestoreToolStripMenuItem, importToolStripMenuItem, exportToolStripMenuItem, maintenanceToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { generalToolStripMenuItem, energyTypesToolStripMenuItem, addressesToolStripMenuItem, metersToolStripMenuItem, costCategoriesToolStripMenuItem, tarifGroupsToolStripMenuItem, energyRatesAndTarifsToolStripMenuItem, predefinedPeriodsToolStripMenuItem, correctionFactorsToolStripMenuItem, nettingToolStripMenuItem, vatTarifsToolStripMenuItem, calculatedUnitPriceToolStripMenuItem, paymentsToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            resources.ApplyResources(settingsToolStripMenuItem, "settingsToolStripMenuItem");
            // 
            // generalToolStripMenuItem
            // 
            generalToolStripMenuItem.Name = "generalToolStripMenuItem";
            resources.ApplyResources(generalToolStripMenuItem, "generalToolStripMenuItem");
            generalToolStripMenuItem.Click += generalToolStripMenuItem_Click;
            // 
            // energyTypesToolStripMenuItem
            // 
            energyTypesToolStripMenuItem.Name = "energyTypesToolStripMenuItem";
            resources.ApplyResources(energyTypesToolStripMenuItem, "energyTypesToolStripMenuItem");
            energyTypesToolStripMenuItem.Click += energyTypesToolStripMenuItem_Click;
            // 
            // addressesToolStripMenuItem
            // 
            addressesToolStripMenuItem.Name = "addressesToolStripMenuItem";
            resources.ApplyResources(addressesToolStripMenuItem, "addressesToolStripMenuItem");
            addressesToolStripMenuItem.Click += addressesToolStripMenuItem_Click;
            // 
            // metersToolStripMenuItem
            // 
            metersToolStripMenuItem.Name = "metersToolStripMenuItem";
            resources.ApplyResources(metersToolStripMenuItem, "metersToolStripMenuItem");
            metersToolStripMenuItem.Click += metersToolStripMenuItem_Click;
            // 
            // costCategoriesToolStripMenuItem
            // 
            costCategoriesToolStripMenuItem.Name = "costCategoriesToolStripMenuItem";
            resources.ApplyResources(costCategoriesToolStripMenuItem, "costCategoriesToolStripMenuItem");
            costCategoriesToolStripMenuItem.Click += costCategoriesToolStripMenuItem_Click;
            // 
            // tarifGroupsToolStripMenuItem
            // 
            tarifGroupsToolStripMenuItem.Name = "tarifGroupsToolStripMenuItem";
            resources.ApplyResources(tarifGroupsToolStripMenuItem, "tarifGroupsToolStripMenuItem");
            tarifGroupsToolStripMenuItem.Click += tarifGroupsToolStripMenuItem_Click;
            // 
            // energyRatesAndTarifsToolStripMenuItem
            // 
            energyRatesAndTarifsToolStripMenuItem.Name = "energyRatesAndTarifsToolStripMenuItem";
            resources.ApplyResources(energyRatesAndTarifsToolStripMenuItem, "energyRatesAndTarifsToolStripMenuItem");
            energyRatesAndTarifsToolStripMenuItem.Click += energyRatesAndTarifsToolStripMenuItem_Click;
            // 
            // predefinedPeriodsToolStripMenuItem
            // 
            predefinedPeriodsToolStripMenuItem.Name = "predefinedPeriodsToolStripMenuItem";
            resources.ApplyResources(predefinedPeriodsToolStripMenuItem, "predefinedPeriodsToolStripMenuItem");
            predefinedPeriodsToolStripMenuItem.Click += predefinedPeriodsToolStripMenuItem_Click;
            // 
            // correctionFactorsToolStripMenuItem
            // 
            correctionFactorsToolStripMenuItem.Name = "correctionFactorsToolStripMenuItem";
            resources.ApplyResources(correctionFactorsToolStripMenuItem, "correctionFactorsToolStripMenuItem");
            correctionFactorsToolStripMenuItem.Click += correctionFactorsToolStripMenuItem_Click;
            // 
            // nettingToolStripMenuItem
            // 
            nettingToolStripMenuItem.Name = "nettingToolStripMenuItem";
            resources.ApplyResources(nettingToolStripMenuItem, "nettingToolStripMenuItem");
            nettingToolStripMenuItem.Click += nettingToolStripMenuItem_Click;
            // 
            // vatTarifsToolStripMenuItem
            // 
            vatTarifsToolStripMenuItem.Name = "vatTarifsToolStripMenuItem";
            resources.ApplyResources(vatTarifsToolStripMenuItem, "vatTarifsToolStripMenuItem");
            vatTarifsToolStripMenuItem.Click += vatTarifsToolStripMenuItem_Click;
            // 
            // calculatedUnitPriceToolStripMenuItem
            // 
            calculatedUnitPriceToolStripMenuItem.Name = "calculatedUnitPriceToolStripMenuItem";
            resources.ApplyResources(calculatedUnitPriceToolStripMenuItem, "calculatedUnitPriceToolStripMenuItem");
            calculatedUnitPriceToolStripMenuItem.Click += calculatedUnitPriceToolStripMenuItem_Click;
            // 
            // paymentsToolStripMenuItem
            // 
            paymentsToolStripMenuItem.Name = "paymentsToolStripMenuItem";
            resources.ApplyResources(paymentsToolStripMenuItem, "paymentsToolStripMenuItem");
            paymentsToolStripMenuItem.Click += paymentsToolStripMenuItem_Click;
            // 
            // backUpAndRestoreToolStripMenuItem
            // 
            backUpAndRestoreToolStripMenuItem.Name = "backUpAndRestoreToolStripMenuItem";
            resources.ApplyResources(backUpAndRestoreToolStripMenuItem, "backUpAndRestoreToolStripMenuItem");
            backUpAndRestoreToolStripMenuItem.Click += backUpAndRestoreToolStripMenuItem_Click;
            // 
            // importToolStripMenuItem
            // 
            importToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { exportMeterRatesToolStripMenuItem });
            importToolStripMenuItem.Name = "importToolStripMenuItem";
            resources.ApplyResources(importToolStripMenuItem, "importToolStripMenuItem");
            // 
            // exportMeterRatesToolStripMenuItem
            // 
            exportMeterRatesToolStripMenuItem.Name = "exportMeterRatesToolStripMenuItem";
            resources.ApplyResources(exportMeterRatesToolStripMenuItem, "exportMeterRatesToolStripMenuItem");
            exportMeterRatesToolStripMenuItem.Click += importMeterRatesToolStripMenuItem_Click;
            // 
            // exportToolStripMenuItem
            // 
            exportToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { ratesToolStripMenuItem });
            exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            resources.ApplyResources(exportToolStripMenuItem, "exportToolStripMenuItem");
            // 
            // ratesToolStripMenuItem
            // 
            ratesToolStripMenuItem.Name = "ratesToolStripMenuItem";
            resources.ApplyResources(ratesToolStripMenuItem, "ratesToolStripMenuItem");
            ratesToolStripMenuItem.Click += ratesToolStripMenuItem_Click;
            // 
            // maintenanceToolStripMenuItem
            // 
            maintenanceToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { recalculateCurrentSelectionToolStripMenuItem, recalculateAllToolStripMenuItem, setupNewFileToolStripMenuItem, createDemoDataToolStripMenuItem });
            maintenanceToolStripMenuItem.Name = "maintenanceToolStripMenuItem";
            resources.ApplyResources(maintenanceToolStripMenuItem, "maintenanceToolStripMenuItem");
            // 
            // recalculateCurrentSelectionToolStripMenuItem
            // 
            recalculateCurrentSelectionToolStripMenuItem.Name = "recalculateCurrentSelectionToolStripMenuItem";
            resources.ApplyResources(recalculateCurrentSelectionToolStripMenuItem, "recalculateCurrentSelectionToolStripMenuItem");
            recalculateCurrentSelectionToolStripMenuItem.Click += recalculateCurrentSelectionToolStripMenuItem_Click;
            // 
            // recalculateAllToolStripMenuItem
            // 
            recalculateAllToolStripMenuItem.Name = "recalculateAllToolStripMenuItem";
            resources.ApplyResources(recalculateAllToolStripMenuItem, "recalculateAllToolStripMenuItem");
            recalculateAllToolStripMenuItem.Click += recalculateAllToolStripMenuItem_Click;
            // 
            // setupNewFileToolStripMenuItem
            // 
            setupNewFileToolStripMenuItem.Name = "setupNewFileToolStripMenuItem";
            resources.ApplyResources(setupNewFileToolStripMenuItem, "setupNewFileToolStripMenuItem");
            setupNewFileToolStripMenuItem.Click += setupNewFileToolStripMenuItem_Click;
            // 
            // createDemoDataToolStripMenuItem
            // 
            createDemoDataToolStripMenuItem.Name = "createDemoDataToolStripMenuItem";
            resources.ApplyResources(createDemoDataToolStripMenuItem, "createDemoDataToolStripMenuItem");
            createDemoDataToolStripMenuItem.Click += createDemoDataToolStripMenuItem_Click;
            // 
            // graphToolStripMenuItem
            // 
            graphToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { usageToolStripMenuItem, compareToolStripMenuItem, ratesToolStripMenuItem1 });
            graphToolStripMenuItem.Name = "graphToolStripMenuItem";
            resources.ApplyResources(graphToolStripMenuItem, "graphToolStripMenuItem");
            // 
            // usageToolStripMenuItem
            // 
            usageToolStripMenuItem.Name = "usageToolStripMenuItem";
            resources.ApplyResources(usageToolStripMenuItem, "usageToolStripMenuItem");
            usageToolStripMenuItem.Click += usageToolStripMenuItem_Click;
            // 
            // compareToolStripMenuItem
            // 
            compareToolStripMenuItem.Name = "compareToolStripMenuItem";
            resources.ApplyResources(compareToolStripMenuItem, "compareToolStripMenuItem");
            compareToolStripMenuItem.Click += compareToolStripMenuItem_Click;
            // 
            // ratesToolStripMenuItem1
            // 
            ratesToolStripMenuItem1.Name = "ratesToolStripMenuItem1";
            resources.ApplyResources(ratesToolStripMenuItem1, "ratesToolStripMenuItem1");
            ratesToolStripMenuItem1.Click += ratesToolStripMenuItem1_Click;
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { settlementToolStripMenuItem, rateReportToolStripMenuItem });
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            resources.ApplyResources(reportsToolStripMenuItem, "reportsToolStripMenuItem");
            // 
            // settlementToolStripMenuItem
            // 
            settlementToolStripMenuItem.Name = "settlementToolStripMenuItem";
            resources.ApplyResources(settlementToolStripMenuItem, "settlementToolStripMenuItem");
            settlementToolStripMenuItem.Click += settlementToolStripMenuItem_Click;
            // 
            // rateReportToolStripMenuItem
            // 
            rateReportToolStripMenuItem.Name = "rateReportToolStripMenuItem";
            resources.ApplyResources(rateReportToolStripMenuItem, "rateReportToolStripMenuItem");
            rateReportToolStripMenuItem.Click += rateReportToolStripMenuItem_Click;
            // 
            // infoToolStripMenuItem
            // 
            infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            resources.ApplyResources(infoToolStripMenuItem, "infoToolStripMenuItem");
            infoToolStripMenuItem.Click += infoToolStripMenuItem_Click;
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbData, toolStripSeparator1, tsbImportData, toolStripSeparator2, tsbUsageGraph, tsbCompareChart, tsbRates, toolStripSeparator3, tsbPdfReport, tsbPayBackTime, toolStripSeparator4, tsbRefresh, tsbClose });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            helpProvider1.SetShowHelp(toolStrip1, (bool)resources.GetObject("toolStrip1.ShowHelp"));
            // 
            // tsbData
            // 
            tsbData.Image = WinFormsUI.Properties.Resources.square_24x24;
            resources.ApplyResources(tsbData, "tsbData");
            tsbData.Name = "tsbData";
            tsbData.Click += tsbData_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(toolStripSeparator1, "toolStripSeparator1");
            // 
            // tsbImportData
            // 
            tsbImportData.Image = WinFormsUI.Properties.Resources.download_24x24;
            resources.ApplyResources(tsbImportData, "tsbImportData");
            tsbImportData.Name = "tsbImportData";
            tsbImportData.Click += tsbImportData_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(toolStripSeparator2, "toolStripSeparator2");
            // 
            // tsbUsageGraph
            // 
            tsbUsageGraph.Image = WinFormsUI.Properties.Resources.statistics_24x24;
            resources.ApplyResources(tsbUsageGraph, "tsbUsageGraph");
            tsbUsageGraph.Name = "tsbUsageGraph";
            tsbUsageGraph.Click += tsbUsageGraph_Click;
            // 
            // tsbCompareChart
            // 
            tsbCompareChart.Image = WinFormsUI.Properties.Resources.statistics_24x24;
            resources.ApplyResources(tsbCompareChart, "tsbCompareChart");
            tsbCompareChart.Name = "tsbCompareChart";
            tsbCompareChart.Click += tsbCompareChart_Click;
            // 
            // tsbRates
            // 
            tsbRates.Image = WinFormsUI.Properties.Resources.statistics_24x24;
            resources.ApplyResources(tsbRates, "tsbRates");
            tsbRates.Name = "tsbRates";
            tsbRates.Click += tsbRates_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(toolStripSeparator3, "toolStripSeparator3");
            // 
            // tsbPdfReport
            // 
            tsbPdfReport.Image = WinFormsUI.Properties.Resources.paper_list_24x24;
            resources.ApplyResources(tsbPdfReport, "tsbPdfReport");
            tsbPdfReport.Name = "tsbPdfReport";
            tsbPdfReport.Click += tsbPdfReport_Click;
            // 
            // tsbPayBackTime
            // 
            tsbPayBackTime.Image = WinFormsUI.Properties.Resources.calculator_24x24;
            resources.ApplyResources(tsbPayBackTime, "tsbPayBackTime");
            tsbPayBackTime.Name = "tsbPayBackTime";
            tsbPayBackTime.Click += tsbPayBackTime_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            resources.ApplyResources(toolStripSeparator4, "toolStripSeparator4");
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
            tsbClose.Click += tsbClose_Click;
            // 
            // tbTop
            // 
            resources.ApplyResources(tbTop, "tbTop");
            tbTop.Controls.Add(CboEnergyType);
            tbTop.Controls.Add(lblEnergyType);
            tbTop.Controls.Add(CboAddress);
            tbTop.Controls.Add(lblAddress);
            tbTop.Name = "tbTop";
            helpProvider1.SetShowHelp(tbTop, (bool)resources.GetObject("tbTop.ShowHelp"));
            tbTop.TabStop = false;
            // 
            // CboEnergyType
            // 
            CboEnergyType.DataSource = bsEnergyTypes;
            CboEnergyType.DisplayMember = "Name";
            CboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(CboEnergyType, "CboEnergyType");
            CboEnergyType.Name = "CboEnergyType";
            helpProvider1.SetShowHelp(CboEnergyType, (bool)resources.GetObject("CboEnergyType.ShowHelp"));
            CboEnergyType.ValueMember = "Id";
            CboEnergyType.SelectedIndexChanged += cboEnergyType_SelectedIndexChanged;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            helpProvider1.SetShowHelp(lblEnergyType, (bool)resources.GetObject("lblEnergyType.ShowHelp"));
            // 
            // CboAddress
            // 
            CboAddress.DataSource = bsAddresses;
            CboAddress.DisplayMember = "Description";
            CboAddress.FormattingEnabled = true;
            resources.ApplyResources(CboAddress, "CboAddress");
            CboAddress.Name = "CboAddress";
            helpProvider1.SetShowHelp(CboAddress, (bool)resources.GetObject("CboAddress.ShowHelp"));
            CboAddress.ValueMember = "Id";
            CboAddress.SelectedIndexChanged += cmbAddress_SelectedIndexChanged;
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // lblAddress
            // 
            resources.ApplyResources(lblAddress, "lblAddress");
            lblAddress.Name = "lblAddress";
            helpProvider1.SetShowHelp(lblAddress, (bool)resources.GetObject("lblAddress.ShowHelp"));
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            helpProvider1.SetShowHelp(statusStrip1, (bool)resources.GetObject("statusStrip1.ShowHelp"));
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(tbTop);
            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            HelpButton = true;
            MainMenuStrip = menuStrip1;
            Name = "MainForm";
            helpProvider1.SetShowHelp(this, (bool)resources.GetObject("$this.ShowHelp"));
            WindowState = FormWindowState.Maximized;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            tbTop.ResumeLayout(false);
            tbTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ResumeLayout(false);
            PerformLayout();
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