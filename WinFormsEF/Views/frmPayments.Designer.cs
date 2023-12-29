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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayments));
            this.CboAddress = new System.Windows.Forms.ComboBox();
            this.bsAddresses = new System.Windows.Forms.BindingSource(this.components);
            this.lblAddress = new System.Windows.Forms.Label();
            this.CboPreSelectedPeriods = new System.Windows.Forms.ComboBox();
            this.bsPreDefinedPeriod = new System.Windows.Forms.BindingSource(this.components);
            this.LblPeriod = new System.Windows.Forms.Label();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tbsClose = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.DgPayments = new System.Windows.Forms.DataGridView();
            this.bsPayments = new System.Windows.Forms.BindingSource(this.components);
            this.GbPayment = new System.Windows.Forms.GroupBox();
            this.TxtAmount = new System.Windows.Forms.TextBox();
            this.LblAmount = new System.Windows.Forms.Label();
            this.LblPayDate = new System.Windows.Forms.Label();
            this.DtPayDate = new System.Windows.Forms.DateTimePicker();
            this.TxtDescription = new System.Windows.Forms.TextBox();
            this.LblDescription = new System.Windows.Forms.Label();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.payDateDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amountDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.addressIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.preDefinedPeriodIdDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPreDefinedPeriod)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPayments)).BeginInit();
            this.GbPayment.SuspendLayout();
            this.SuspendLayout();
            // 
            // CboAddress
            // 
            this.CboAddress.DataSource = this.bsAddresses;
            this.CboAddress.DisplayMember = "Description";
            this.CboAddress.FormattingEnabled = true;
            resources.ApplyResources(this.CboAddress, "CboAddress");
            this.CboAddress.Name = "CboAddress";
            this.CboAddress.ValueMember = "Id";
            // 
            // bsAddresses
            // 
            this.bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // lblAddress
            // 
            resources.ApplyResources(this.lblAddress, "lblAddress");
            this.lblAddress.Name = "lblAddress";
            // 
            // CboPreSelectedPeriods
            // 
            this.CboPreSelectedPeriods.DataSource = this.bsPreDefinedPeriod;
            this.CboPreSelectedPeriods.FormattingEnabled = true;
            resources.ApplyResources(this.CboPreSelectedPeriods, "CboPreSelectedPeriods");
            this.CboPreSelectedPeriods.Name = "CboPreSelectedPeriods";
            this.CboPreSelectedPeriods.Tag = "LastPreSelectedPeriod";
            this.CboPreSelectedPeriods.SelectedIndexChanged += new System.EventHandler(this.CboPreSelectedPeriods_SelectedIndexChanged);
            // 
            // bsPreDefinedPeriod
            // 
            this.bsPreDefinedPeriod.DataSource = typeof(EnergyUse.Models.PreDefinedPeriod);
            // 
            // LblPeriod
            // 
            resources.ApplyResources(this.LblPeriod, "LblPeriod");
            this.LblPeriod.Name = "LblPeriod";
            // 
            // toolStrip1
            // 
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbSave,
            this.tbsCancel,
            this.tsbDelete,
            this.tsbRefresh,
            this.tbsClose});
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // tsbAdd
            // 
            this.tsbAdd.Image = global::WinFormsUI.Properties.Resources.add_24x24;
            resources.ApplyResources(this.tsbAdd, "tsbAdd");
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Click += new System.EventHandler(this.tsbAdd_Click);
            // 
            // tsbSave
            // 
            this.tsbSave.Image = global::WinFormsUI.Properties.Resources.tick_mark_24x24;
            resources.ApplyResources(this.tsbSave, "tsbSave");
            this.tsbSave.Name = "tsbSave";
            this.tsbSave.Click += new System.EventHandler(this.tsbSave_Click);
            // 
            // tbsCancel
            // 
            this.tbsCancel.Image = global::WinFormsUI.Properties.Resources.clear_24x24;
            resources.ApplyResources(this.tbsCancel, "tbsCancel");
            this.tbsCancel.Name = "tbsCancel";
            this.tbsCancel.Click += new System.EventHandler(this.tbsCancel_Click);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = global::WinFormsUI.Properties.Resources.delete_24x24;
            resources.ApplyResources(this.tsbDelete, "tsbDelete");
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Click += new System.EventHandler(this.tsbDelete_Click);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::WinFormsUI.Properties.Resources.clock_24x24;
            resources.ApplyResources(this.tsbRefresh, "tsbRefresh");
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Click += new System.EventHandler(this.tsbRefresh_Click);
            // 
            // tbsClose
            // 
            this.tbsClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tbsClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tbsClose, "tbsClose");
            this.tbsClose.Name = "tbsClose";
            this.tbsClose.Click += new System.EventHandler(this.tbsClose_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
            // 
            // DgPayments
            // 
            this.DgPayments.AllowUserToAddRows = false;
            this.DgPayments.AllowUserToDeleteRows = false;
            this.DgPayments.AllowUserToOrderColumns = true;
            resources.ApplyResources(this.DgPayments, "DgPayments");
            this.DgPayments.AutoGenerateColumns = false;
            this.DgPayments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DgPayments.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.idDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.payDateDataGridViewTextBoxColumn,
            this.amountDataGridViewTextBoxColumn,
            this.addressIdDataGridViewTextBoxColumn,
            this.preDefinedPeriodIdDataGridViewTextBoxColumn});
            this.DgPayments.DataSource = this.bsPayments;
            this.DgPayments.Name = "DgPayments";
            this.DgPayments.RowTemplate.Height = 29;
            // 
            // bsPayments
            // 
            this.bsPayments.DataSource = typeof(EnergyUse.Models.Payment);
            // 
            // GbPayment
            // 
            resources.ApplyResources(this.GbPayment, "GbPayment");
            this.GbPayment.Controls.Add(this.TxtAmount);
            this.GbPayment.Controls.Add(this.LblAmount);
            this.GbPayment.Controls.Add(this.LblPayDate);
            this.GbPayment.Controls.Add(this.DtPayDate);
            this.GbPayment.Controls.Add(this.TxtDescription);
            this.GbPayment.Controls.Add(this.LblDescription);
            this.GbPayment.Name = "GbPayment";
            this.GbPayment.TabStop = false;
            // 
            // TxtAmount
            // 
            this.TxtAmount.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPayments, "Amount", true));
            resources.ApplyResources(this.TxtAmount, "TxtAmount");
            this.TxtAmount.Name = "TxtAmount";
            // 
            // LblAmount
            // 
            resources.ApplyResources(this.LblAmount, "LblAmount");
            this.LblAmount.Name = "LblAmount";
            // 
            // LblPayDate
            // 
            resources.ApplyResources(this.LblPayDate, "LblPayDate");
            this.LblPayDate.Name = "LblPayDate";
            // 
            // DtPayDate
            // 
            this.DtPayDate.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.bsPayments, "PayDate", true));
            this.DtPayDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPayments, "PayDate", true));
            this.DtPayDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            resources.ApplyResources(this.DtPayDate, "DtPayDate");
            this.DtPayDate.Name = "DtPayDate";
            // 
            // TxtDescription
            // 
            this.TxtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsPayments, "Description", true));
            resources.ApplyResources(this.TxtDescription, "TxtDescription");
            this.TxtDescription.Name = "TxtDescription";
            // 
            // LblDescription
            // 
            resources.ApplyResources(this.LblDescription, "LblDescription");
            this.LblDescription.Name = "LblDescription";
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(this.descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // payDateDataGridViewTextBoxColumn
            // 
            this.payDateDataGridViewTextBoxColumn.DataPropertyName = "PayDate";
            resources.ApplyResources(this.payDateDataGridViewTextBoxColumn, "payDateDataGridViewTextBoxColumn");
            this.payDateDataGridViewTextBoxColumn.Name = "payDateDataGridViewTextBoxColumn";
            // 
            // amountDataGridViewTextBoxColumn
            // 
            this.amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            resources.ApplyResources(this.amountDataGridViewTextBoxColumn, "amountDataGridViewTextBoxColumn");
            this.amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            // 
            // addressIdDataGridViewTextBoxColumn
            // 
            this.addressIdDataGridViewTextBoxColumn.DataPropertyName = "AddressId";
            resources.ApplyResources(this.addressIdDataGridViewTextBoxColumn, "addressIdDataGridViewTextBoxColumn");
            this.addressIdDataGridViewTextBoxColumn.Name = "addressIdDataGridViewTextBoxColumn";
            // 
            // preDefinedPeriodIdDataGridViewTextBoxColumn
            // 
            this.preDefinedPeriodIdDataGridViewTextBoxColumn.DataPropertyName = "PreDefinedPeriodId";
            resources.ApplyResources(this.preDefinedPeriodIdDataGridViewTextBoxColumn, "preDefinedPeriodIdDataGridViewTextBoxColumn");
            this.preDefinedPeriodIdDataGridViewTextBoxColumn.Name = "preDefinedPeriodIdDataGridViewTextBoxColumn";
            // 
            // frmPayments
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GbPayment);
            this.Controls.Add(this.DgPayments);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.CboAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.CboPreSelectedPeriods);
            this.Controls.Add(this.LblPeriod);
            this.Name = "frmPayments";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPreDefinedPeriod)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DgPayments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsPayments)).EndInit();
            this.GbPayment.ResumeLayout(false);
            this.GbPayment.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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