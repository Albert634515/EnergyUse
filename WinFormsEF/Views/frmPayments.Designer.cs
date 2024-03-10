namespace WinFormsEF.Views
{
    partial class frmPayments
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayments));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            CboAddress = new ComboBox();
            bsAddresses = new BindingSource(components);
            lblAddress = new Label();
            CboPreSelectedPeriods = new ComboBox();
            bsPreDefinedPeriod = new BindingSource(components);
            LblPeriod = new Label();
            toolStrip1 = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbSave = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            tbsClose = new ToolStripButton();
            statusStrip1 = new StatusStrip();
            toolTip1 = new ToolTip(components);
            DgPayments = new DataGridView();
            bsPayments = new BindingSource(components);
            GbPayment = new GroupBox();
            TxtAmount = new TextBox();
            LblAmount = new Label();
            LblPayDate = new Label();
            DtPayDate = new DateTimePicker();
            TxtDescription = new TextBox();
            LblDescription = new Label();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            payDateDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            amountDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            addressIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            preDefinedPeriodIdDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsPreDefinedPeriod).BeginInit();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DgPayments).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsPayments).BeginInit();
            GbPayment.SuspendLayout();
            SuspendLayout();
            // 
            // CboAddress
            // 
            CboAddress.DataSource = bsAddresses;
            CboAddress.DisplayMember = "Description";
            CboAddress.FormattingEnabled = true;
            resources.ApplyResources(CboAddress, "CboAddress");
            CboAddress.Name = "CboAddress";
            CboAddress.ValueMember = "Id";
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // lblAddress
            // 
            resources.ApplyResources(lblAddress, "lblAddress");
            lblAddress.Name = "lblAddress";
            // 
            // CboPreSelectedPeriods
            // 
            CboPreSelectedPeriods.DataSource = bsPreDefinedPeriod;
            CboPreSelectedPeriods.FormattingEnabled = true;
            resources.ApplyResources(CboPreSelectedPeriods, "CboPreSelectedPeriods");
            CboPreSelectedPeriods.Name = "CboPreSelectedPeriods";
            CboPreSelectedPeriods.Tag = "LastPreSelectedPeriod";
            CboPreSelectedPeriods.SelectedIndexChanged += CboPreSelectedPeriods_SelectedIndexChanged;
            // 
            // bsPreDefinedPeriod
            // 
            bsPreDefinedPeriod.DataSource = typeof(EnergyUse.Models.PreDefinedPeriod);
            // 
            // LblPeriod
            // 
            resources.ApplyResources(LblPeriod, "LblPeriod");
            LblPeriod.Name = "LblPeriod";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAdd, tsbSave, tbsCancel, tsbDelete, tsbRefresh, tbsClose });
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
            // tbsClose
            // 
            tbsClose.Alignment = ToolStripItemAlignment.Right;
            tbsClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tbsClose, "tbsClose");
            tbsClose.Name = "tbsClose";
            tbsClose.Click += tbsClose_Click;
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // DgPayments
            // 
            DgPayments.AllowUserToAddRows = false;
            DgPayments.AllowUserToDeleteRows = false;
            DgPayments.AllowUserToOrderColumns = true;
            resources.ApplyResources(DgPayments, "DgPayments");
            DgPayments.AutoGenerateColumns = false;
            DgPayments.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DgPayments.Columns.AddRange(new DataGridViewColumn[] { idDataGridViewTextBoxColumn, descriptionDataGridViewTextBoxColumn, payDateDataGridViewTextBoxColumn, amountDataGridViewTextBoxColumn, addressIdDataGridViewTextBoxColumn, preDefinedPeriodIdDataGridViewTextBoxColumn });
            DgPayments.DataSource = bsPayments;
            DgPayments.Name = "DgPayments";
            // 
            // bsPayments
            // 
            bsPayments.DataSource = typeof(EnergyUse.Models.Payment);
            // 
            // GbPayment
            // 
            resources.ApplyResources(GbPayment, "GbPayment");
            GbPayment.Controls.Add(TxtAmount);
            GbPayment.Controls.Add(LblAmount);
            GbPayment.Controls.Add(LblPayDate);
            GbPayment.Controls.Add(DtPayDate);
            GbPayment.Controls.Add(TxtDescription);
            GbPayment.Controls.Add(LblDescription);
            GbPayment.Name = "GbPayment";
            GbPayment.TabStop = false;
            // 
            // TxtAmount
            // 
            TxtAmount.DataBindings.Add(new Binding("Text", bsPayments, "Amount", true));
            resources.ApplyResources(TxtAmount, "TxtAmount");
            TxtAmount.Name = "TxtAmount";
            // 
            // LblAmount
            // 
            resources.ApplyResources(LblAmount, "LblAmount");
            LblAmount.Name = "LblAmount";
            // 
            // LblPayDate
            // 
            resources.ApplyResources(LblPayDate, "LblPayDate");
            LblPayDate.Name = "LblPayDate";
            // 
            // DtPayDate
            // 
            DtPayDate.DataBindings.Add(new Binding("Value", bsPayments, "PayDate", true));
            DtPayDate.DataBindings.Add(new Binding("Text", bsPayments, "PayDate", true));
            DtPayDate.Format = DateTimePickerFormat.Short;
            resources.ApplyResources(DtPayDate, "DtPayDate");
            DtPayDate.Name = "DtPayDate";
            // 
            // TxtDescription
            // 
            TxtDescription.DataBindings.Add(new Binding("Text", bsPayments, "Description", true));
            resources.ApplyResources(TxtDescription, "TxtDescription");
            TxtDescription.Name = "TxtDescription";
            // 
            // LblDescription
            // 
            resources.ApplyResources(LblDescription, "LblDescription");
            LblDescription.Name = "LblDescription";
            // 
            // idDataGridViewTextBoxColumn
            // 
            idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // payDateDataGridViewTextBoxColumn
            // 
            payDateDataGridViewTextBoxColumn.DataPropertyName = "PayDate";
            resources.ApplyResources(payDateDataGridViewTextBoxColumn, "payDateDataGridViewTextBoxColumn");
            payDateDataGridViewTextBoxColumn.Name = "payDateDataGridViewTextBoxColumn";
            // 
            // amountDataGridViewTextBoxColumn
            // 
            amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            amountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            resources.ApplyResources(amountDataGridViewTextBoxColumn, "amountDataGridViewTextBoxColumn");
            amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            // 
            // addressIdDataGridViewTextBoxColumn
            // 
            addressIdDataGridViewTextBoxColumn.DataPropertyName = "AddressId";
            resources.ApplyResources(addressIdDataGridViewTextBoxColumn, "addressIdDataGridViewTextBoxColumn");
            addressIdDataGridViewTextBoxColumn.Name = "addressIdDataGridViewTextBoxColumn";
            // 
            // preDefinedPeriodIdDataGridViewTextBoxColumn
            // 
            preDefinedPeriodIdDataGridViewTextBoxColumn.DataPropertyName = "PreDefinedPeriodId";
            resources.ApplyResources(preDefinedPeriodIdDataGridViewTextBoxColumn, "preDefinedPeriodIdDataGridViewTextBoxColumn");
            preDefinedPeriodIdDataGridViewTextBoxColumn.Name = "preDefinedPeriodIdDataGridViewTextBoxColumn";
            // 
            // frmPayments
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(GbPayment);
            Controls.Add(DgPayments);
            Controls.Add(statusStrip1);
            Controls.Add(toolStrip1);
            Controls.Add(CboAddress);
            Controls.Add(lblAddress);
            Controls.Add(CboPreSelectedPeriods);
            Controls.Add(LblPeriod);
            Name = "frmPayments";
            ShowIcon = false;
            ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsPreDefinedPeriod).EndInit();
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DgPayments).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsPayments).EndInit();
            GbPayment.ResumeLayout(false);
            GbPayment.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public ComboBox CboAddress;
        private Label lblAddress;
        private ComboBox CboPreSelectedPeriods;
        private Label LblPeriod;
        private ToolStrip toolStrip1;
        private ToolStripButton tsbAdd;
        private ToolStripButton tsbSave;
        private ToolStripButton tbsCancel;
        private ToolStripButton tsbDelete;
        private ToolStripButton tsbRefresh;
        private ToolStripButton tbsClose;
        private StatusStrip statusStrip1;
        private ToolTip toolTip1;
        private DataGridView DgPayments;
        private BindingSource bsPayments;
        private BindingSource bsAddresses;
        private BindingSource bsPreDefinedPeriod;
        private GroupBox GbPayment;
        private TextBox TxtDescription;
        private Label LblDescription;
        private DateTimePicker DtPayDate;
        private Label LblPayDate;
        private Label LblAmount;
        private TextBox TxtAmount;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn payDateDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn addressIdDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn preDefinedPeriodIdDataGridViewTextBoxColumn;
    }
}