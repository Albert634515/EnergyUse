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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayBackTime));
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTotalCapacitySolarPanels = new System.Windows.Forms.TextBox();
            this.lblTotalCapacity = new System.Windows.Forms.Label();
            this.lblEnergyType = new System.Windows.Forms.Label();
            this.cboEnergyType = new System.Windows.Forms.ComboBox();
            this.bsEnergyTypes = new System.Windows.Forms.BindingSource(this.components);
            this.lblAddress = new System.Windows.Forms.Label();
            this.cmbAddress = new System.Windows.Forms.ComboBox();
            this.bsAddresses = new System.Windows.Forms.BindingSource(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtQualityReductionSolarPanels = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMaxYears = new System.Windows.Forms.Label();
            this.nudMaxYears = new System.Windows.Forms.NumericUpDown();
            this.lblSubsidy = new System.Windows.Forms.Label();
            this.txtSubsidyAmount = new System.Windows.Forms.TextBox();
            this.lblPurchaseAmount = new System.Windows.Forms.Label();
            this.txtPurchaseAmount = new System.Windows.Forms.TextBox();
            this.dtpPurchaseDate = new System.Windows.Forms.DateTimePicker();
            this.lblDateOrPurchase = new System.Windows.Forms.Label();
            this.dgPayBackTime = new System.Windows.Forms.DataGridView();
            this.periodIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.startPeriodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.endPeriodDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnOnInvestment = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ReturnOnInvestmentTotal = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Return = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueProducedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.valueConsumedDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ValueProducedAndConsumed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonetaryValueProduced = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonetaryValueConsumed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MonetaryValueProducedAndConsumed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherCostProduced = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.OtherCostConsumed = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsPayBackTimes = new System.Windows.Forms.BindingSource(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbCalculate = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxYears)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayBackTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPayBackTimes)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripStatusLabel1});
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            resources.ApplyResources(this.toolStripProgressBar1, "toolStripProgressBar1");
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            resources.ApplyResources(this.toolStripStatusLabel1, "toolStripStatusLabel1");
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // txtTotalCapacitySolarPanels
            // 
            resources.ApplyResources(this.txtTotalCapacitySolarPanels, "txtTotalCapacitySolarPanels");
            this.txtTotalCapacitySolarPanels.Name = "txtTotalCapacitySolarPanels";
            this.txtTotalCapacitySolarPanels.ReadOnly = true;
            this.txtTotalCapacitySolarPanels.Tag = "TotalCapacitySolarPanels";
            // 
            // lblTotalCapacity
            // 
            resources.ApplyResources(this.lblTotalCapacity, "lblTotalCapacity");
            this.lblTotalCapacity.Name = "lblTotalCapacity";
            // 
            // lblEnergyType
            // 
            resources.ApplyResources(this.lblEnergyType, "lblEnergyType");
            this.lblEnergyType.Name = "lblEnergyType";
            // 
            // cboEnergyType
            // 
            this.cboEnergyType.DataSource = this.bsEnergyTypes;
            this.cboEnergyType.DisplayMember = "Name";
            this.cboEnergyType.FormattingEnabled = true;
            resources.ApplyResources(this.cboEnergyType, "cboEnergyType");
            this.cboEnergyType.Name = "cboEnergyType";
            // 
            // bsEnergyTypes
            // 
            this.bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // cmbAddress
            // 
            this.cmbAddress.DataSource = this.bsAddresses;
            this.cmbAddress.DisplayMember = "Description";
            this.cmbAddress.FormattingEnabled = true;
            resources.ApplyResources(this.cmbAddress, "cmbAddress");
            this.cmbAddress.Name = "cmbAddress";
            this.cmbAddress.ValueMember = "Id";
            this.cmbAddress.SelectedIndexChanged += new System.EventHandler(this.cmbAddress_SelectedIndexChanged);
            // 
            // bsAddresses
            // 
            this.bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtQualityReductionSolarPanels
            // 
            resources.ApplyResources(this.txtQualityReductionSolarPanels, "txtQualityReductionSolarPanels");
            this.txtQualityReductionSolarPanels.Name = "txtQualityReductionSolarPanels";
            this.txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels";
            this.txtQualityReductionSolarPanels.TextChanged += new System.EventHandler(this.txtQualityReductionSolarPanels_TextChanged);
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // lblMaxYears
            // 
            resources.ApplyResources(this.lblMaxYears, "lblMaxYears");
            this.lblMaxYears.Name = "lblMaxYears";
            // 
            // nudMaxYears
            // 
            resources.ApplyResources(this.nudMaxYears, "nudMaxYears");
            this.nudMaxYears.Name = "nudMaxYears";
            this.nudMaxYears.Tag = "PayBackMaxYearsToCalculate";
            this.nudMaxYears.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.nudMaxYears.ValueChanged += new System.EventHandler(this.nudMaxYears_ValueChanged);
            // 
            // lblSubsidy
            // 
            resources.ApplyResources(this.lblSubsidy, "lblSubsidy");
            this.lblSubsidy.Name = "lblSubsidy";
            // 
            // txtSubsidyAmount
            // 
            resources.ApplyResources(this.txtSubsidyAmount, "txtSubsidyAmount");
            this.txtSubsidyAmount.Name = "txtSubsidyAmount";
            this.txtSubsidyAmount.TextChanged += new System.EventHandler(this.txtSubsidyAmount_TextChanged);
            // 
            // lblPurchaseAmount
            // 
            resources.ApplyResources(this.lblPurchaseAmount, "lblPurchaseAmount");
            this.lblPurchaseAmount.Name = "lblPurchaseAmount";
            // 
            // txtPurchaseAmount
            // 
            resources.ApplyResources(this.txtPurchaseAmount, "txtPurchaseAmount");
            this.txtPurchaseAmount.Name = "txtPurchaseAmount";
            this.txtPurchaseAmount.TextChanged += new System.EventHandler(this.txtPurchaseAmount_TextChanged);
            // 
            // dtpPurchaseDate
            // 
            this.dtpPurchaseDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.dtpPurchaseDate, "dtpPurchaseDate");
            this.dtpPurchaseDate.Name = "dtpPurchaseDate";
            // 
            // lblDateOrPurchase
            // 
            resources.ApplyResources(this.lblDateOrPurchase, "lblDateOrPurchase");
            this.lblDateOrPurchase.Name = "lblDateOrPurchase";
            // 
            // dgPayBackTime
            // 
            this.dgPayBackTime.AllowUserToAddRows = false;
            this.dgPayBackTime.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgPayBackTime, "dgPayBackTime");
            this.dgPayBackTime.AutoGenerateColumns = false;
            this.dgPayBackTime.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.periodIdDataGridViewTextBoxColumn,
            this.startPeriodDataGridViewTextBoxColumn,
            this.endPeriodDataGridViewTextBoxColumn,
            this.ReturnOnInvestment,
            this.ReturnOnInvestmentTotal,
            this.Return,
            this.valueProducedDataGridViewTextBoxColumn,
            this.valueConsumedDataGridViewTextBoxColumn,
            this.ValueProducedAndConsumed,
            this.MonetaryValueProduced,
            this.MonetaryValueConsumed,
            this.MonetaryValueProducedAndConsumed,
            this.OtherCostProduced,
            this.OtherCostConsumed});
            this.dgPayBackTime.DataSource = this.bsPayBackTimes;
            this.dgPayBackTime.Name = "dgPayBackTime";
            this.dgPayBackTime.ReadOnly = true;
            this.dgPayBackTime.RowTemplate.Height = 24;
            this.dgPayBackTime.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvPayBackTime_CellFormatting);
            // 
            // periodIdDataGridViewTextBoxColumn
            // 
            this.periodIdDataGridViewTextBoxColumn.DataPropertyName = "PeriodId";
            resources.ApplyResources(this.periodIdDataGridViewTextBoxColumn, "periodIdDataGridViewTextBoxColumn");
            this.periodIdDataGridViewTextBoxColumn.Name = "periodIdDataGridViewTextBoxColumn";
            this.periodIdDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // startPeriodDataGridViewTextBoxColumn
            // 
            this.startPeriodDataGridViewTextBoxColumn.DataPropertyName = "StartPeriod";
            resources.ApplyResources(this.startPeriodDataGridViewTextBoxColumn, "startPeriodDataGridViewTextBoxColumn");
            this.startPeriodDataGridViewTextBoxColumn.Name = "startPeriodDataGridViewTextBoxColumn";
            this.startPeriodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // endPeriodDataGridViewTextBoxColumn
            // 
            this.endPeriodDataGridViewTextBoxColumn.DataPropertyName = "EndPeriod";
            resources.ApplyResources(this.endPeriodDataGridViewTextBoxColumn, "endPeriodDataGridViewTextBoxColumn");
            this.endPeriodDataGridViewTextBoxColumn.Name = "endPeriodDataGridViewTextBoxColumn";
            this.endPeriodDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ReturnOnInvestment
            // 
            this.ReturnOnInvestment.DataPropertyName = "ReturnOnInvestment";
            resources.ApplyResources(this.ReturnOnInvestment, "ReturnOnInvestment");
            this.ReturnOnInvestment.Name = "ReturnOnInvestment";
            this.ReturnOnInvestment.ReadOnly = true;
            // 
            // ReturnOnInvestmentTotal
            // 
            this.ReturnOnInvestmentTotal.DataPropertyName = "ReturnOnInvestmentTotal";
            resources.ApplyResources(this.ReturnOnInvestmentTotal, "ReturnOnInvestmentTotal");
            this.ReturnOnInvestmentTotal.Name = "ReturnOnInvestmentTotal";
            this.ReturnOnInvestmentTotal.ReadOnly = true;
            // 
            // Return
            // 
            this.Return.DataPropertyName = "Return";
            resources.ApplyResources(this.Return, "Return");
            this.Return.Name = "Return";
            this.Return.ReadOnly = true;
            // 
            // valueProducedDataGridViewTextBoxColumn
            // 
            this.valueProducedDataGridViewTextBoxColumn.DataPropertyName = "ValueProduced";
            resources.ApplyResources(this.valueProducedDataGridViewTextBoxColumn, "valueProducedDataGridViewTextBoxColumn");
            this.valueProducedDataGridViewTextBoxColumn.Name = "valueProducedDataGridViewTextBoxColumn";
            this.valueProducedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // valueConsumedDataGridViewTextBoxColumn
            // 
            this.valueConsumedDataGridViewTextBoxColumn.DataPropertyName = "ValueConsumed";
            resources.ApplyResources(this.valueConsumedDataGridViewTextBoxColumn, "valueConsumedDataGridViewTextBoxColumn");
            this.valueConsumedDataGridViewTextBoxColumn.Name = "valueConsumedDataGridViewTextBoxColumn";
            this.valueConsumedDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // ValueProducedAndConsumed
            // 
            this.ValueProducedAndConsumed.DataPropertyName = "ValueProducedAndConsumed";
            resources.ApplyResources(this.ValueProducedAndConsumed, "ValueProducedAndConsumed");
            this.ValueProducedAndConsumed.Name = "ValueProducedAndConsumed";
            this.ValueProducedAndConsumed.ReadOnly = true;
            // 
            // MonetaryValueProduced
            // 
            this.MonetaryValueProduced.DataPropertyName = "MonetaryValueProduced";
            resources.ApplyResources(this.MonetaryValueProduced, "MonetaryValueProduced");
            this.MonetaryValueProduced.Name = "MonetaryValueProduced";
            this.MonetaryValueProduced.ReadOnly = true;
            // 
            // MonetaryValueConsumed
            // 
            this.MonetaryValueConsumed.DataPropertyName = "MonetaryValueConsumed";
            resources.ApplyResources(this.MonetaryValueConsumed, "MonetaryValueConsumed");
            this.MonetaryValueConsumed.Name = "MonetaryValueConsumed";
            this.MonetaryValueConsumed.ReadOnly = true;
            // 
            // MonetaryValueProducedAndConsumed
            // 
            this.MonetaryValueProducedAndConsumed.DataPropertyName = "MonetaryValueProducedAndConsumed";
            resources.ApplyResources(this.MonetaryValueProducedAndConsumed, "MonetaryValueProducedAndConsumed");
            this.MonetaryValueProducedAndConsumed.Name = "MonetaryValueProducedAndConsumed";
            this.MonetaryValueProducedAndConsumed.ReadOnly = true;
            // 
            // OtherCostProduced
            // 
            this.OtherCostProduced.DataPropertyName = "OtherCostProduced";
            resources.ApplyResources(this.OtherCostProduced, "OtherCostProduced");
            this.OtherCostProduced.Name = "OtherCostProduced";
            this.OtherCostProduced.ReadOnly = true;
            // 
            // OtherCostConsumed
            // 
            this.OtherCostConsumed.DataPropertyName = "OtherCostConsumed";
            resources.ApplyResources(this.OtherCostConsumed, "OtherCostConsumed");
            this.OtherCostConsumed.Name = "OtherCostConsumed";
            this.OtherCostConsumed.ReadOnly = true;
            // 
            // bsPayBackTimes
            // 
            this.bsPayBackTimes.DataSource = typeof(EnergyUse.Models.Common.PayBackTime);
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCalculate,
            this.tsbClose});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbCalculate
            // 
            this.tsbCalculate.Image = global::WinFormsUI.Properties.Resources.calculator_24x24;
            resources.ApplyResources(this.tsbCalculate, "tsbCalculate");
            this.tsbCalculate.Name = "tsbCalculate";
            this.tsbCalculate.Click += new System.EventHandler(this.tsbCalculate_Click);
            // 
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbClose, "tsbClose");
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // frmPayBackTime
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.dgPayBackTime);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtTotalCapacitySolarPanels);
            this.Controls.Add(this.lblTotalCapacity);
            this.Controls.Add(this.lblEnergyType);
            this.Controls.Add(this.cboEnergyType);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.cmbAddress);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtQualityReductionSolarPanels);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblMaxYears);
            this.Controls.Add(this.nudMaxYears);
            this.Controls.Add(this.lblSubsidy);
            this.Controls.Add(this.txtSubsidyAmount);
            this.Controls.Add(this.lblPurchaseAmount);
            this.Controls.Add(this.txtPurchaseAmount);
            this.Controls.Add(this.dtpPurchaseDate);
            this.Controls.Add(this.lblDateOrPurchase);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmPayBackTime";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Load += new System.EventHandler(this.frmPayBackTime_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsEnergyTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nudMaxYears)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgPayBackTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPayBackTimes)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private ToolTip toolTip2;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbCalculate;
        private ToolStripButton tsbClose;
        private ToolStripProgressBar toolStripProgressBar1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private DataGridViewTextBoxColumn periodIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn startPeriodDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn endPeriodDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ReturnOnInvestment;
        private DataGridViewTextBoxColumn ReturnOnInvestmentTotal;
        private DataGridViewTextBoxColumn Return;
        private DataGridViewTextBoxColumn valueProducedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn valueConsumedDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn ValueProducedAndConsumed;
        private DataGridViewTextBoxColumn MonetaryValueProduced;
        private DataGridViewTextBoxColumn MonetaryValueConsumed;
        private DataGridViewTextBoxColumn MonetaryValueProducedAndConsumed;
        private DataGridViewTextBoxColumn OtherCostProduced;
        private DataGridViewTextBoxColumn OtherCostConsumed;
    }
}