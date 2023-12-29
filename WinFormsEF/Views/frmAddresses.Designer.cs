namespace WinFormsEF.Views
{
    partial class frmAddresses
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddresses));
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbSave = new System.Windows.Forms.ToolStripButton();
            this.tbsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsbClose = new System.Windows.Forms.ToolStripButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.dgAddresses = new System.Windows.Forms.DataGridView();
            this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.idDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.bsAddresses = new System.Windows.Forms.BindingSource(this.components);
            this.lblRate = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.lblHasSolarpanels = new System.Windows.Forms.Label();
            this.gbSolar = new System.Windows.Forms.GroupBox();
            this.lblSubsidy = new System.Windows.Forms.Label();
            this.txtSubsidyAmount = new System.Windows.Forms.TextBox();
            this.lblPurchaseAmount = new System.Windows.Forms.Label();
            this.txtPurchaseAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.chkSolarPanelsAvailable = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalCapacitySolarPanels = new System.Windows.Forms.TextBox();
            this.txtQualityReductionSolarPanels = new System.Windows.Forms.TextBox();
            this.lblTotalCapacity = new System.Windows.Forms.Label();
            this.txtStreet = new System.Windows.Forms.TextBox();
            this.lblCity = new System.Windows.Forms.Label();
            this.lblStreet = new System.Windows.Forms.Label();
            this.chkDefaultAddress = new System.Windows.Forms.CheckBox();
            this.gbAddress = new System.Windows.Forms.GroupBox();
            this.txtPostcalCode = new System.Windows.Forms.TextBox();
            this.lblPostcalCode = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this.txtHouseNo = new System.Windows.Forms.TextBox();
            this.toolTip2 = new System.Windows.Forms.ToolTip(this.components);
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).BeginInit();
            this.gbSolar.SuspendLayout();
            this.gbAddress.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            resources.ApplyResources(this.statusStrip1, "statusStrip1");
            this.statusStrip1.Name = "statusStrip1";
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
            this.tsbClose});
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
            // tsbClose
            // 
            this.tsbClose.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tsbClose.Image = global::WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(this.tsbClose, "tsbClose");
            this.tsbClose.Name = "tsbClose";
            this.tsbClose.Click += new System.EventHandler(this.tsbClose_Click);
            // 
            // dgAddresses
            // 
            this.dgAddresses.AllowUserToAddRows = false;
            this.dgAddresses.AllowUserToDeleteRows = false;
            resources.ApplyResources(this.dgAddresses, "dgAddresses");
            this.dgAddresses.AutoGenerateColumns = false;
            this.dgAddresses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgAddresses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.descriptionDataGridViewTextBoxColumn,
            this.idDataGridViewTextBoxColumn});
            this.dgAddresses.DataSource = this.bsAddresses;
            this.dgAddresses.Name = "dgAddresses";
            this.dgAddresses.RowTemplate.Height = 29;
            // 
            // descriptionDataGridViewTextBoxColumn
            // 
            this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
            resources.ApplyResources(this.descriptionDataGridViewTextBoxColumn, "descriptionDataGridViewTextBoxColumn");
            this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
            // 
            // idDataGridViewTextBoxColumn
            // 
            this.idDataGridViewTextBoxColumn.DataPropertyName = "Id";
            resources.ApplyResources(this.idDataGridViewTextBoxColumn, "idDataGridViewTextBoxColumn");
            this.idDataGridViewTextBoxColumn.Name = "idDataGridViewTextBoxColumn";
            this.idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsAddresses
            // 
            this.bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            this.bsAddresses.CurrentChanged += new System.EventHandler(this.bsAddresses_CurrentChanged);
            // 
            // lblRate
            // 
            resources.ApplyResources(this.lblRate, "lblRate");
            this.lblRate.Name = "lblRate";
            // 
            // txtDescription
            // 
            this.txtDescription.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAddresses, "Description", true));
            resources.ApplyResources(this.txtDescription, "txtDescription");
            this.txtDescription.Name = "txtDescription";
            // 
            // lblHasSolarpanels
            // 
            resources.ApplyResources(this.lblHasSolarpanels, "lblHasSolarpanels");
            this.lblHasSolarpanels.Name = "lblHasSolarpanels";
            // 
            // gbSolar
            // 
            resources.ApplyResources(this.gbSolar, "gbSolar");
            this.gbSolar.Controls.Add(this.lblHasSolarpanels);
            this.gbSolar.Controls.Add(this.lblSubsidy);
            this.gbSolar.Controls.Add(this.txtSubsidyAmount);
            this.gbSolar.Controls.Add(this.lblPurchaseAmount);
            this.gbSolar.Controls.Add(this.txtPurchaseAmount);
            this.gbSolar.Controls.Add(this.label6);
            this.gbSolar.Controls.Add(this.label4);
            this.gbSolar.Controls.Add(this.chkSolarPanelsAvailable);
            this.gbSolar.Controls.Add(this.label2);
            this.gbSolar.Controls.Add(this.txtTotalCapacitySolarPanels);
            this.gbSolar.Controls.Add(this.txtQualityReductionSolarPanels);
            this.gbSolar.Controls.Add(this.lblTotalCapacity);
            this.gbSolar.Name = "gbSolar";
            this.gbSolar.TabStop = false;
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
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // chkSolarPanelsAvailable
            // 
            resources.ApplyResources(this.chkSolarPanelsAvailable, "chkSolarPanelsAvailable");
            this.chkSolarPanelsAvailable.Name = "chkSolarPanelsAvailable";
            this.chkSolarPanelsAvailable.Tag = "";
            this.chkSolarPanelsAvailable.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtTotalCapacitySolarPanels
            // 
            this.txtTotalCapacitySolarPanels.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAddresses, "TotalCapacity", true));
            resources.ApplyResources(this.txtTotalCapacitySolarPanels, "txtTotalCapacitySolarPanels");
            this.txtTotalCapacitySolarPanels.Name = "txtTotalCapacitySolarPanels";
            this.txtTotalCapacitySolarPanels.Tag = "";
            // 
            // txtQualityReductionSolarPanels
            // 
            resources.ApplyResources(this.txtQualityReductionSolarPanels, "txtQualityReductionSolarPanels");
            this.txtQualityReductionSolarPanels.Name = "txtQualityReductionSolarPanels";
            this.txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels";
            // 
            // lblTotalCapacity
            // 
            resources.ApplyResources(this.lblTotalCapacity, "lblTotalCapacity");
            this.lblTotalCapacity.Name = "lblTotalCapacity";
            // 
            // txtStreet
            // 
            this.txtStreet.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAddresses, "Street", true));
            resources.ApplyResources(this.txtStreet, "txtStreet");
            this.txtStreet.Name = "txtStreet";
            // 
            // lblCity
            // 
            resources.ApplyResources(this.lblCity, "lblCity");
            this.lblCity.Name = "lblCity";
            // 
            // lblStreet
            // 
            resources.ApplyResources(this.lblStreet, "lblStreet");
            this.lblStreet.Name = "lblStreet";
            // 
            // chkDefaultAddress
            // 
            resources.ApplyResources(this.chkDefaultAddress, "chkDefaultAddress");
            this.chkDefaultAddress.DataBindings.Add(new System.Windows.Forms.Binding("Checked", this.bsAddresses, "DefaultAddress", true));
            this.chkDefaultAddress.Name = "chkDefaultAddress";
            this.chkDefaultAddress.UseVisualStyleBackColor = true;
            // 
            // gbAddress
            // 
            resources.ApplyResources(this.gbAddress, "gbAddress");
            this.gbAddress.Controls.Add(this.txtPostcalCode);
            this.gbAddress.Controls.Add(this.lblPostcalCode);
            this.gbAddress.Controls.Add(this.label1);
            this.gbAddress.Controls.Add(this.txtCity);
            this.gbAddress.Controls.Add(this.txtHouseNo);
            this.gbAddress.Controls.Add(this.txtStreet);
            this.gbAddress.Controls.Add(this.lblCity);
            this.gbAddress.Controls.Add(this.lblStreet);
            this.gbAddress.Name = "gbAddress";
            this.gbAddress.TabStop = false;
            // 
            // txtPostcalCode
            // 
            this.txtPostcalCode.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAddresses, "PostalCode", true));
            resources.ApplyResources(this.txtPostcalCode, "txtPostcalCode");
            this.txtPostcalCode.Name = "txtPostcalCode";
            // 
            // lblPostcalCode
            // 
            resources.ApplyResources(this.lblPostcalCode, "lblPostcalCode");
            this.lblPostcalCode.Name = "lblPostcalCode";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCity
            // 
            this.txtCity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAddresses, "City", true));
            resources.ApplyResources(this.txtCity, "txtCity");
            this.txtCity.Name = "txtCity";
            // 
            // txtHouseNo
            // 
            this.txtHouseNo.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsAddresses, "HouseNumber", true));
            resources.ApplyResources(this.txtHouseNo, "txtHouseNo");
            this.txtHouseNo.Name = "txtHouseNo";
            // 
            // frmAddresses
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbSolar);
            this.Controls.Add(this.lblRate);
            this.Controls.Add(this.gbAddress);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.dgAddresses);
            this.Controls.Add(this.chkDefaultAddress);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Name = "frmAddresses";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAddresses_FormClosing);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgAddresses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAddresses)).EndInit();
            this.gbSolar.ResumeLayout(false);
            this.gbSolar.PerformLayout();
            this.gbAddress.ResumeLayout(false);
            this.gbAddress.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private BindingSource bsAddresses;
        private DataGridView dgAddresses;
        private ToolTip toolTip2;
        private Label lblRate;
        private TextBox txtDescription;
        private Label lblHasSolarpanels;
        private GroupBox gbSolar;
        private Label lblSubsidy;
        private TextBox txtSubsidyAmount;
        private Label lblPurchaseAmount;
        private TextBox txtPurchaseAmount;
        private Label label6;
        private Label label4;
        private CheckBox chkSolarPanelsAvailable;
        private Label label2;
        private TextBox txtTotalCapacitySolarPanels;
        private TextBox txtQualityReductionSolarPanels;
        private Label lblTotalCapacity;
        private TextBox txtStreet;
        private Label lblCity;
        private Label lblStreet;
        private CheckBox chkDefaultAddress;
        private GroupBox gbAddress;
        private TextBox txtPostcalCode;
        private Label lblPostcalCode;
        private Label label1;
        private TextBox txtCity;
        private TextBox txtHouseNo;
        private ToolStripButton tbsCancel;
        private DataGridViewTextBoxColumn descriptionDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
    }
}