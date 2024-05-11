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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAddresses));
            statusStrip1 = new StatusStrip();
            toolStrip1 = new ToolStrip();
            tsbAdd = new ToolStripButton();
            tsbSave = new ToolStripButton();
            tbsCancel = new ToolStripButton();
            tsbDelete = new ToolStripButton();
            tsbRefresh = new ToolStripButton();
            tsbClose = new ToolStripButton();
            toolTip1 = new ToolTip(components);
            dgAddresses = new DataGridView();
            descriptionDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            idDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            bsAddresses = new BindingSource(components);
            lblRate = new Label();
            txtDescription = new TextBox();
            lblHasSolarpanels = new Label();
            gbSolar = new GroupBox();
            lblSubsidy = new Label();
            txtSubsidyAmount = new TextBox();
            lblPurchaseAmount = new Label();
            txtPurchaseAmount = new TextBox();
            label6 = new Label();
            label4 = new Label();
            chkSolarPanelsAvailable = new CheckBox();
            label2 = new Label();
            txtTotalCapacitySolarPanels = new TextBox();
            txtQualityReductionSolarPanels = new TextBox();
            lblTotalCapacity = new Label();
            txtStreet = new TextBox();
            lblCity = new Label();
            lblStreet = new Label();
            chkDefaultAddress = new CheckBox();
            gbAddress = new GroupBox();
            txtPostcalCode = new TextBox();
            lblPostcalCode = new Label();
            label1 = new Label();
            txtCity = new TextBox();
            txtHouseNo = new TextBox();
            toolTip2 = new ToolTip(components);
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgAddresses).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).BeginInit();
            gbSolar.SuspendLayout();
            gbAddress.SuspendLayout();
            SuspendLayout();
            // 
            // statusStrip1
            // 
            statusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(statusStrip1, "statusStrip1");
            statusStrip1.Name = "statusStrip1";
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { tsbAdd, tsbSave, tbsCancel, tsbDelete, tsbRefresh, tsbClose });
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
            // tsbClose
            // 
            tsbClose.Alignment = ToolStripItemAlignment.Right;
            tsbClose.Image = WinFormsUI.Properties.Resources.crossed_24x24;
            resources.ApplyResources(tsbClose, "tsbClose");
            tsbClose.Name = "tsbClose";
            tsbClose.Click += tsbClose_Click;
            // 
            // dgAddresses
            // 
            dgAddresses.AllowUserToAddRows = false;
            dgAddresses.AllowUserToDeleteRows = false;
            resources.ApplyResources(dgAddresses, "dgAddresses");
            dgAddresses.AutoGenerateColumns = false;
            dgAddresses.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgAddresses.Columns.AddRange(new DataGridViewColumn[] { descriptionDataGridViewTextBoxColumn, idDataGridViewTextBoxColumn });
            dgAddresses.DataSource = bsAddresses;
            dgAddresses.Name = "dgAddresses";
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
            idDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // bsAddresses
            // 
            bsAddresses.DataSource = typeof(EnergyUse.Models.Address);
            bsAddresses.CurrentChanged += bsAddresses_CurrentChanged;
            // 
            // lblRate
            // 
            resources.ApplyResources(lblRate, "lblRate");
            lblRate.Name = "lblRate";
            // 
            // txtDescription
            // 
            txtDescription.DataBindings.Add(new Binding("Text", bsAddresses, "Description", true));
            resources.ApplyResources(txtDescription, "txtDescription");
            txtDescription.Name = "txtDescription";
            // 
            // lblHasSolarpanels
            // 
            resources.ApplyResources(lblHasSolarpanels, "lblHasSolarpanels");
            lblHasSolarpanels.Name = "lblHasSolarpanels";
            // 
            // gbSolar
            // 
            resources.ApplyResources(gbSolar, "gbSolar");
            gbSolar.Controls.Add(lblHasSolarpanels);
            gbSolar.Controls.Add(lblSubsidy);
            gbSolar.Controls.Add(txtSubsidyAmount);
            gbSolar.Controls.Add(lblPurchaseAmount);
            gbSolar.Controls.Add(txtPurchaseAmount);
            gbSolar.Controls.Add(label6);
            gbSolar.Controls.Add(label4);
            gbSolar.Controls.Add(chkSolarPanelsAvailable);
            gbSolar.Controls.Add(label2);
            gbSolar.Controls.Add(txtTotalCapacitySolarPanels);
            gbSolar.Controls.Add(txtQualityReductionSolarPanels);
            gbSolar.Controls.Add(lblTotalCapacity);
            gbSolar.Name = "gbSolar";
            gbSolar.TabStop = false;
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
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            // 
            // chkSolarPanelsAvailable
            // 
            resources.ApplyResources(chkSolarPanelsAvailable, "chkSolarPanelsAvailable");
            chkSolarPanelsAvailable.Name = "chkSolarPanelsAvailable";
            chkSolarPanelsAvailable.Tag = "";
            chkSolarPanelsAvailable.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            // 
            // txtTotalCapacitySolarPanels
            // 
            txtTotalCapacitySolarPanels.DataBindings.Add(new Binding("Text", bsAddresses, "TotalCapacity", true));
            resources.ApplyResources(txtTotalCapacitySolarPanels, "txtTotalCapacitySolarPanels");
            txtTotalCapacitySolarPanels.Name = "txtTotalCapacitySolarPanels";
            txtTotalCapacitySolarPanels.Tag = "";
            // 
            // txtQualityReductionSolarPanels
            // 
            resources.ApplyResources(txtQualityReductionSolarPanels, "txtQualityReductionSolarPanels");
            txtQualityReductionSolarPanels.Name = "txtQualityReductionSolarPanels";
            txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels";
            // 
            // lblTotalCapacity
            // 
            resources.ApplyResources(lblTotalCapacity, "lblTotalCapacity");
            lblTotalCapacity.Name = "lblTotalCapacity";
            // 
            // txtStreet
            // 
            txtStreet.DataBindings.Add(new Binding("Text", bsAddresses, "Street", true));
            resources.ApplyResources(txtStreet, "txtStreet");
            txtStreet.Name = "txtStreet";
            // 
            // lblCity
            // 
            resources.ApplyResources(lblCity, "lblCity");
            lblCity.Name = "lblCity";
            // 
            // lblStreet
            // 
            resources.ApplyResources(lblStreet, "lblStreet");
            lblStreet.Name = "lblStreet";
            // 
            // chkDefaultAddress
            // 
            resources.ApplyResources(chkDefaultAddress, "chkDefaultAddress");
            chkDefaultAddress.DataBindings.Add(new Binding("Checked", bsAddresses, "DefaultAddress", true));
            chkDefaultAddress.Name = "chkDefaultAddress";
            chkDefaultAddress.UseVisualStyleBackColor = true;
            // 
            // gbAddress
            // 
            resources.ApplyResources(gbAddress, "gbAddress");
            gbAddress.Controls.Add(txtPostcalCode);
            gbAddress.Controls.Add(lblPostcalCode);
            gbAddress.Controls.Add(label1);
            gbAddress.Controls.Add(txtCity);
            gbAddress.Controls.Add(txtHouseNo);
            gbAddress.Controls.Add(txtStreet);
            gbAddress.Controls.Add(lblCity);
            gbAddress.Controls.Add(lblStreet);
            gbAddress.Name = "gbAddress";
            gbAddress.TabStop = false;
            // 
            // txtPostcalCode
            // 
            txtPostcalCode.DataBindings.Add(new Binding("Text", bsAddresses, "PostalCode", true));
            resources.ApplyResources(txtPostcalCode, "txtPostcalCode");
            txtPostcalCode.Name = "txtPostcalCode";
            // 
            // lblPostcalCode
            // 
            resources.ApplyResources(lblPostcalCode, "lblPostcalCode");
            lblPostcalCode.Name = "lblPostcalCode";
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            // 
            // txtCity
            // 
            txtCity.DataBindings.Add(new Binding("Text", bsAddresses, "City", true));
            resources.ApplyResources(txtCity, "txtCity");
            txtCity.Name = "txtCity";
            // 
            // txtHouseNo
            // 
            txtHouseNo.DataBindings.Add(new Binding("Text", bsAddresses, "HouseNumber", true));
            resources.ApplyResources(txtHouseNo, "txtHouseNo");
            txtHouseNo.Name = "txtHouseNo";
            // 
            // frmAddresses
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(gbSolar);
            Controls.Add(lblRate);
            Controls.Add(gbAddress);
            Controls.Add(txtDescription);
            Controls.Add(dgAddresses);
            Controls.Add(chkDefaultAddress);
            Controls.Add(toolStrip1);
            Controls.Add(statusStrip1);
            Name = "frmAddresses";
            ShowIcon = false;
            ShowInTaskbar = false;
            FormClosing += frmAddresses_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgAddresses).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsAddresses).EndInit();
            gbSolar.ResumeLayout(false);
            gbSolar.PerformLayout();
            gbAddress.ResumeLayout(false);
            gbAddress.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
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