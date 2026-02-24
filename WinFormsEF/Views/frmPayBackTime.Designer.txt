namespace WinFormsEF.Views
{
    partial class frmPayBackTime
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayBackTime));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle9 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle10 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle11 = new DataGridViewCellStyle();
            toolTip1 = new ToolTip(components);
            statusStrip1 = new StatusStrip();
            toolStripProgressBar1 = new ToolStripProgressBar();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            label6 = new Label();
            txtTotalCapacitySolarPanels = new TextBox();
            lblTotalCapacity = new Label();
            lblEnergyType = new Label();
            cboEnergyType = new ComboBox();
            bsEnergyTypes = new BindingSource(components);
            lblAddress = new Label();
            cmbAddress = new ComboBox();
            bsAddresses = new BindingSource(components);
            label1 = new Label();
            txtQualityReductionSolarPanels = new TextBox();
            label3 = new Label();
            lblMaxYears = new Label();
            nudMaxYears = new NumericUpDown();
            lblSubsidy = new Label();
            txtSubsidyAmount = new TextBox();
            lblPurchaseAmount = new Label();
            txtPurchaseAmount = new TextBox();
            dtpPurchaseDate = new DateTimePicker();
            lblDateOrPurchase = new Label();
            dgPayBackTime = new DataGridView();
            periodIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            startPeriodDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            endPeriodDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            valueProducedDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            valueConsumedDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            EstimateDirectUsed = new DataGridViewTextBoxColumn();
            MonetaryValueProduced = new DataGridViewTextBoxColumn();
            ValueProducedEstimateDirectUsed = new DataGridViewTextBoxColumn();
            MonetaryValueConsumed = new DataGridViewTextBoxColumn();
            NettoProduced = new DataGridViewTextBoxColumn();
            OtherCostProduced = new DataGridViewTextBoxColumn();
            OtherCostConsumed = new DataGridViewTextBoxColumn();
            TotalCost = new DataGridViewTextBoxColumn();
            ReturnOnInvestment = new DataGridViewTextBoxColumn();
            ReturnOnInvestmentTotal = new DataGridViewTextBoxColumn();
            Return = new DataGridViewTextBoxColumn();
            bsPayBackTimes = new BindingSource(components);
            toolStrip1 = new ToolStrip();
            tsbCalculate = new ToolStripButton();
            tsbClose = new ToolStripButton();
            label2 = new Label();
            txtAverageReturn = new TextBox();
            label4 = new Label();
            statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxYears).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgPayBackTime).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsPayBackTimes).BeginInit();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripProgressBar1, toolStripStatusLabel1 });
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // txtTotalCapacitySolarPanels
            // 
            resources.ApplyResources(txtTotalCapacitySolarPanels, "txtTotalCapacitySolarPanels");
            txtTotalCapacitySolarPanels.Name = "txtTotalCapacitySolarPanels";
            txtTotalCapacitySolarPanels.ReadOnly = true;
            txtTotalCapacitySolarPanels.Tag = "TotalCapacitySolarPanels";
            // 
            // lblTotalCapacity
            // 
            resources.ApplyResources(lblTotalCapacity, "lblTotalCapacity");
            lblTotalCapacity.Name = "lblTotalCapacity";
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(lblEnergyType, "lblEnergyType");
            lblEnergyType.Name = "lblEnergyType";
            // 
            // cboEnergyType
            // 
            cboEnergyType.DataSource = bsEnergyTypes;
            cboEnergyType.DisplayMember = "Name";
            cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(cboEnergyType, "cboEnergyType");
            cboEnergyType.Name = "cboEnergyType";
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblAddress
            // 
            resources.ApplyResources(lblAddress, "lblAddress");
            lblAddress.Name = "lblAddress";
            // 
            // cmbAddress
            // 
            cmbAddress.DataSource = bsAddresses;
            cmbAddress.DisplayMember = "Description";
            cmbAddress.FormattingEnabled = true;
            resources.ApplyResources(cmbAddress, "cmbAddress");
            cmbAddress.Name = "cmbAddress";
            cmbAddress.ValueMember = "Id";
            cmbAddress.SelectedIndexChanged += cmbAddress_SelectedIndexChanged;
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // txtQualityReductionSolarPanels
            // 
            resources.ApplyResources(txtQualityReductionSolarPanels, "txtQualityReductionSolarPanels");
            txtQualityReductionSolarPanels.Name = "txtQualityReductionSolarPanels";
            txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels";
            txtQualityReductionSolarPanels.TextChanged += txtQualityReductionSolarPanels_TextChanged;
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // lblMaxYears
            // 
            resources.ApplyResources(lblMaxYears, "lblMaxYears");
            lblMaxYears.Name = "lblMaxYears";
            // 
            // nudMaxYears
            // 
            resources.ApplyResources(nudMaxYears, "nudMaxYears");
            nudMaxYears.Name = "nudMaxYears";
            nudMaxYears.Tag = "PayBackMaxYearsToCalculate";
            nudMaxYears.Value = new decimal(new int[] { 5, 0, 0, 0 });
            nudMaxYears.ValueChanged += nudMaxYears_ValueChanged;
            // 
            // lblSubsidy
            // 
            resources.ApplyResources(lblSubsidy, "lblSubsidy");
            lblSubsidy.Name = "lblSubsidy";
            // 
            // txtSubsidyAmount
            // 
            resources.ApplyResources(txtSubsidyAmount, "txtSubsidyAmount");
            txtSubsidyAmount.Name = "txtSubsidyAmount";
            txtSubsidyAmount.TextChanged += txtSubsidyAmount_TextChanged;
            // 
            // lblPurchaseAmount
            // 
            resources.ApplyResources(lblPurchaseAmount, "lblPurchaseAmount");
            lblPurchaseAmount.Name = "lblPurchaseAmount";
            // 
            // txtPurchaseAmount
            // 
            resources.ApplyResources(txtPurchaseAmount, "txtPurchaseAmount");
            txtPurchaseAmount.Name = "txtPurchaseAmount";
            txtPurchaseAmount.TextChanged += txtPurchaseAmount_TextChanged;
            // 
            // dtpPurchaseDate
            // 
            dtpPurchaseDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(dtpPurchaseDate, "dtpPurchaseDate");
            dtpPurchaseDate.Name = "dtpPurchaseDate";
            dtpPurchaseDate.ValueChanged += dtpPurchaseDate_ValueChanged;
            // 
            // lblDateOrPurchase
            // 
            resources.ApplyResources(lblDateOrPurchase, "lblDateOrPurchase");
            lblDateOrPurchase.Name = "lblDateOrPurchase";
            // 
            // dgPayBackTime
            // 
            dgPayBackTime.AllowUserToAddRows = false;
            dgPayBackTime.AllowUserToDeleteRows = false;
            resources.ApplyResources(dgPayBackTime, "dgPayBackTime");
            dgPayBackTime.AutoGenerateColumns = false;
            dgPayBackTime.Columns.AddRange(new DataGridViewColumn[] { periodIdDataGridViewTextBoxColumn, startPeriodDataGridViewTextBoxColumn, endPeriodDataGridViewTextBoxColumn, valueProducedDataGridViewTextBoxColumn, valueConsumedDataGridViewTextBoxColumn, EstimateDirectUsed, MonetaryValueProduced, ValueProducedEstimateDirectUsed, MonetaryValueConsumed, NettoProduced, OtherCostProduced, OtherCostConsumed, TotalCost, ReturnOnInvestment, ReturnOnInvestmentTotal, Return });
            dgPayBackTime.DataSource = bsPayBackTimes;
            dgPayBackTime.Name = "dgPayBackTime";
            dgPayBackTime.ReadOnly = true;
            dgPayBackTime.RowTemplate.Height = 24;
            dgPayBackTime.CellFormatting += dgvPayBackTime_CellFormatting;
            // 
            // periodIdDataGridViewTextBoxColumn
            // 
            periodIdDataGridViewTextBoxColumn.DataPropertyName = "PeriodId";
            resources.ApplyResources(periodIdDataGridViewTextBoxColumn, "periodIdDataGridViewTextBoxColumn");
            periodIdDataGridViewTextBoxColumn.Name = "periodIdDataGridViewTextBoxColumn";
            periodIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // startPeriodDataGridViewTextBoxColumn
            // 
            startPeriodDataGridViewTextBoxColumn.DataPropertyName = "StartPeriod";
            resources.ApplyResources(startPeriodDataGridViewTextBoxColumn, "startPeriodDataGridViewTextBoxColumn");
            startPeriodDataGridViewTextBoxColumn.Name = "startPeriodDataGridViewTextBoxColumn";
            startPeriodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // endPeriodDataGridViewTextBoxColumn
            // 
            endPeriodDataGridViewTextBoxColumn.DataPropertyName = "EndPeriod";
            resources.ApplyResources(endPeriodDataGridViewTextBoxColumn, "endPeriodDataGridViewTextBoxColumn");
            endPeriodDataGridViewTextBoxColumn.Name = "endPeriodDataGridViewTextBoxColumn";
            endPeriodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // valueProducedDataGridViewTextBoxColumn
            // 
            valueProducedDataGridViewTextBoxColumn.DataPropertyName = "ValueProduced";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            valueProducedDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(valueProducedDataGridViewTextBoxColumn, "valueProducedDataGridViewTextBoxColumn");
            valueProducedDataGridViewTextBoxColumn.Name = "valueProducedDataGridViewTextBoxColumn";
            valueProducedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // valueConsumedDataGridViewTextBoxColumn
            // 
            valueConsumedDataGridViewTextBoxColumn.DataPropertyName = "ValueConsumed";
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle2.Format = "N2";
            valueConsumedDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle2;
            resources.ApplyResources(valueConsumedDataGridViewTextBoxColumn, "valueConsumedDataGridViewTextBoxColumn");
            valueConsumedDataGridViewTextBoxColumn.Name = "valueConsumedDataGridViewTextBoxColumn";
            valueConsumedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // EstimateDirectUsed
            // 
            EstimateDirectUsed.DataPropertyName = "EstimateDirectUsed";
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle3.Format = "N2";
            EstimateDirectUsed.DefaultCellStyle = dataGridViewCellStyle3;
            resources.ApplyResources(EstimateDirectUsed, "EstimateDirectUsed");
            EstimateDirectUsed.Name = "EstimateDirectUsed";
            EstimateDirectUsed.ReadOnly = true;
            // 
            // MonetaryValueProduced
            // 
            MonetaryValueProduced.DataPropertyName = "MonetaryValueProduced";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            MonetaryValueProduced.DefaultCellStyle = dataGridViewCellStyle4;
            resources.ApplyResources(MonetaryValueProduced, "MonetaryValueProduced");
            MonetaryValueProduced.Name = "MonetaryValueProduced";
            MonetaryValueProduced.ReadOnly = true;
            // 
            // ValueProducedEstimateDirectUsed
            // 
            ValueProducedEstimateDirectUsed.DataPropertyName = "ValueProducedEstimateDirectUsed";
            resources.ApplyResources(ValueProducedEstimateDirectUsed, "ValueProducedEstimateDirectUsed");
            ValueProducedEstimateDirectUsed.Name = "ValueProducedEstimateDirectUsed";
            ValueProducedEstimateDirectUsed.ReadOnly = true;
            // 
            // MonetaryValueConsumed
            // 
            MonetaryValueConsumed.DataPropertyName = "MonetaryValueConsumed";
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle5.Format = "N2";
            MonetaryValueConsumed.DefaultCellStyle = dataGridViewCellStyle5;
            resources.ApplyResources(MonetaryValueConsumed, "MonetaryValueConsumed");
            MonetaryValueConsumed.Name = "MonetaryValueConsumed";
            MonetaryValueConsumed.ReadOnly = true;
            // 
            // NettoProduced
            // 
            NettoProduced.DataPropertyName = "NettoProduced";
            resources.ApplyResources(NettoProduced, "NettoProduced");
            NettoProduced.Name = "NettoProduced";
            NettoProduced.ReadOnly = true;
            // 
            // OtherCostProduced
            // 
            OtherCostProduced.DataPropertyName = "OtherCostProduced";
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle6.Format = "N2";
            OtherCostProduced.DefaultCellStyle = dataGridViewCellStyle6;
            resources.ApplyResources(OtherCostProduced, "OtherCostProduced");
            OtherCostProduced.Name = "OtherCostProduced";
            OtherCostProduced.ReadOnly = true;
            // 
            // OtherCostConsumed
            // 
            OtherCostConsumed.DataPropertyName = "OtherCostConsumed";
            dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle7.Format = "N2";
            OtherCostConsumed.DefaultCellStyle = dataGridViewCellStyle7;
            resources.ApplyResources(OtherCostConsumed, "OtherCostConsumed");
            OtherCostConsumed.Name = "OtherCostConsumed";
            OtherCostConsumed.ReadOnly = true;
            // 
            // TotalCost
            // 
            TotalCost.DataPropertyName = "TotalCost";
            dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle8.Format = "N2";
            TotalCost.DefaultCellStyle = dataGridViewCellStyle8;
            resources.ApplyResources(TotalCost, "TotalCost");
            TotalCost.Name = "TotalCost";
            TotalCost.ReadOnly = true;
            // 
            // ReturnOnInvestment
            // 
            ReturnOnInvestment.DataPropertyName = "ReturnOnInvestment";
            dataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle9.Format = "N2";
            ReturnOnInvestment.DefaultCellStyle = dataGridViewCellStyle9;
            resources.ApplyResources(ReturnOnInvestment, "ReturnOnInvestment");
            ReturnOnInvestment.Name = "ReturnOnInvestment";
            ReturnOnInvestment.ReadOnly = true;
            // 
            // ReturnOnInvestmentTotal
            // 
            ReturnOnInvestmentTotal.DataPropertyName = "ReturnOnInvestmentTotal";
            dataGridViewCellStyle10.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle10.Format = "N2";
            ReturnOnInvestmentTotal.DefaultCellStyle = dataGridViewCellStyle10;
            resources.ApplyResources(ReturnOnInvestmentTotal, "ReturnOnInvestmentTotal");
            ReturnOnInvestmentTotal.Name = "ReturnOnInvestmentTotal";
            ReturnOnInvestmentTotal.ReadOnly = true;
            // 
            // Return
            // 
            Return.DataPropertyName = "Return";
            dataGridViewCellStyle11.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle11.Format = "N2";
            Return.DefaultCellStyle = dataGridViewCellStyle11;
            resources.ApplyResources(Return, "Return");
            Return.Name = "Return";
            Return.ReadOnly = true;
            // 
            // bsPayBackTimes
            // 
            bsPayBackTimes.DataSource = typeof(EnergyUse.Models.Common.PayBackTime);
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbCalculate, tsbClose });
            resources.ApplyResources(toolStrip1, "toolStrip1");
            toolStrip1.Name = "toolStrip1";
            // 
            // tsbCalculate
            // 
            tsbCalculate.Image = WinFormsUI.Properties.Resources.calculator_24x24;
            resources.ApplyResources(tsbCalculate, "tsbCalculate");
            tsbCalculate.Name = "tsbCalculate";
            tsbCalculate.Click += tsbCalculate_Click;
            // 
            // tsbClose
            // 
            tsbClose.Alignment = ToolStripItemAlignment.Right;
            tsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbClose, "tsbClose");
            tsbClose.Name = "tsbClose";
            tsbClose.Click += tsbClose_Click;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // txtAverageReturn
            // 
            resources.ApplyResources(txtAverageReturn, "txtAverageReturn");
            txtAverageReturn.Name = "txtAverageReturn";
            txtAverageReturn.Tag = "QualityReductionSolarPanels";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // frmPayBackTime
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(label2);
            Controls.Add(txtAverageReturn);
            Controls.Add(label4);
            Controls.Add(toolStrip1);
            Controls.Add(dgPayBackTime);
            Controls.Add(label6);
            Controls.Add(txtTotalCapacitySolarPanels);
            Controls.Add(lblTotalCapacity);
            Controls.Add(lblEnergyType);
            Controls.Add(cboEnergyType);
            Controls.Add(lblAddress);
            Controls.Add(cmbAddress);
            Controls.Add(label1);
            Controls.Add(txtQualityReductionSolarPanels);
            Controls.Add(label3);
            Controls.Add(lblMaxYears);
            Controls.Add(nudMaxYears);
            Controls.Add(lblSubsidy);
            Controls.Add(txtSubsidyAmount);
            Controls.Add(lblPurchaseAmount);
            Controls.Add(txtPurchaseAmount);
            Controls.Add(dtpPurchaseDate);
            Controls.Add(lblDateOrPurchase);
            Controls.Add(statusStrip1);
            Name = "frmPayBackTime";
            ShowIcon = false;
            ShowInTaskbar = false;
            Load += frmPayBackTime_Load;
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudMaxYears).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgPayBackTime).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsPayBackTimes).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip toolTip1;
        private StatusStrip statusStrip1;
        private BindingSource bsPayBackTimes;
        private BindingSource bsAddresses;
        private BindingSource bsEnergyTypes;
        private Label label6;
        private TextBox txtTotalCapacitySolarPanels;
        private Label lblTotalCapacity;
        private Label lblEnergyType;
        private ComboBox cboEnergyType;
        private Label lblAddress;
        private ComboBox cmbAddress;
        private Label label1;
        private TextBox txtQualityReductionSolarPanels;
        private Label label3;
        private Label lblMaxYears;
        private NumericUpDown nudMaxYears;
        private Label lblSubsidy;
        private TextBox txtSubsidyAmount;
        private Label lblPurchaseAmount;
        private TextBox txtPurchaseAmount;
        private DateTimePicker dtpPurchaseDate;
        private Label lblDateOrPurchase;
        private DataGridView dgPayBackTime;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbCalculate;
        private ToolStripButton tsbClose;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private Label label2;
        private TextBox txtAverageReturn;
        private Label label4;
        private DataGridViewTextBoxColumn periodIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startPeriodDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endPeriodDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn valueProducedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn valueConsumedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn EstimateDirectUsed;
        private DataGridViewTextBoxColumn MonetaryValueProduced;
        private DataGridViewTextBoxColumn ValueProducedEstimateDirectUsed;
        private DataGridViewTextBoxColumn MonetaryValueConsumed;
        private DataGridViewTextBoxColumn NettoProduced;
        private DataGridViewTextBoxColumn OtherCostProduced;
        private DataGridViewTextBoxColumn OtherCostConsumed;
        private DataGridViewTextBoxColumn TotalCost;
        private DataGridViewTextBoxColumn ReturnOnInvestment;
        private DataGridViewTextBoxColumn ReturnOnInvestmentTotal;
        private DataGridViewTextBoxColumn Return;
    }
}