namespace WinFormsEF.Views
{
    partial class FrmSetupNewFile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSetupNewFile));
            ToolTip1 = new ToolTip(components);
            StatusStrip1 = new StatusStrip();
            TabControl = new TabControl();
            TabPageSelectFile = new TabPage();
            CmdSelectExportDirectory = new Button();
            txtTargetDirectory = new TextBox();
            label3 = new Label();
            LblIntro = new Label();
            PnFileTypeSelection = new Panel();
            RbExistingFile = new RadioButton();
            RbNewFile = new RadioButton();
            CmdNextSelectFile = new Button();
            LblAddBaseData = new Label();
            ChkAddBaseData = new CheckBox();
            LblSetAsDefaultFile = new Label();
            ChkSetAsDefaultFile = new CheckBox();
            CmdSelectExportFile = new Button();
            TxtNewDbFile = new TextBox();
            LblDbFile = new Label();
            TabPageNewAddress = new TabPage();
            gbNewAddress = new GroupBox();
            chkDefaultAddress = new CheckBox();
            bsAddress = new BindingSource(components);
            gbAddress = new GroupBox();
            txtPostcalCode = new TextBox();
            lblPostcalCode = new Label();
            label1 = new Label();
            txtCity = new TextBox();
            txtHouseNo = new TextBox();
            txtStreet = new TextBox();
            lblCity = new Label();
            lblStreet = new Label();
            gbSolar = new GroupBox();
            lblHasSolarpanels = new Label();
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
            lblRate = new Label();
            txtDescription = new TextBox();
            CmdNextNewAddress = new Button();
            CmPreviousTabNewAddress = new Button();
            TabPageSelectEnergyTypes = new TabPage();
            cmdNextNewEnergyTypes = new Button();
            CmdPreviousEnergyTypes = new Button();
            GbNewEnergyTypes = new GroupBox();
            LblHelpNewEnergyTypes = new Label();
            TabPageSetupFile = new TabPage();
            lblSetupFile = new Label();
            cmdPreviousSetupFile = new Button();
            LblSetupNewFile = new Label();
            CmdCreateDb = new Button();
            bsAddresss = new BindingSource(components);
            toolTip2 = new ToolTip(components);
            bsEnergyTypes = new BindingSource(components);
            TabControl.SuspendLayout();
            TabPageSelectFile.SuspendLayout();
            PnFileTypeSelection.SuspendLayout();
            TabPageNewAddress.SuspendLayout();
            gbNewAddress.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddress).BeginInit();
            gbAddress.SuspendLayout();
            gbSolar.SuspendLayout();
            TabPageSelectEnergyTypes.SuspendLayout();
            TabPageSetupFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddresss).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).BeginInit();
            SuspendLayout();
            // 
            // StatusStrip1
            // 
            StatusStrip1.ImageScalingSize = new Size(20, 20);
            resources.ApplyResources(StatusStrip1, "StatusStrip1");
            StatusStrip1.Name = "StatusStrip1";
            // 
            // TabControl
            // 
            resources.ApplyResources(TabControl, "TabControl");
            TabControl.Controls.Add(TabPageSelectFile);
            TabControl.Controls.Add(TabPageNewAddress);
            TabControl.Controls.Add(TabPageSelectEnergyTypes);
            TabControl.Controls.Add(TabPageSetupFile);
            TabControl.Name = "TabControl";
            TabControl.SelectedIndex = 0;
            // 
            // TabPageSelectFile
            // 
            TabPageSelectFile.Controls.Add(CmdSelectExportDirectory);
            TabPageSelectFile.Controls.Add(txtTargetDirectory);
            TabPageSelectFile.Controls.Add(label3);
            TabPageSelectFile.Controls.Add(LblIntro);
            TabPageSelectFile.Controls.Add(PnFileTypeSelection);
            TabPageSelectFile.Controls.Add(CmdNextSelectFile);
            TabPageSelectFile.Controls.Add(LblAddBaseData);
            TabPageSelectFile.Controls.Add(ChkAddBaseData);
            TabPageSelectFile.Controls.Add(LblSetAsDefaultFile);
            TabPageSelectFile.Controls.Add(ChkSetAsDefaultFile);
            TabPageSelectFile.Controls.Add(CmdSelectExportFile);
            TabPageSelectFile.Controls.Add(TxtNewDbFile);
            TabPageSelectFile.Controls.Add(LblDbFile);
            resources.ApplyResources(TabPageSelectFile, "TabPageSelectFile");
            TabPageSelectFile.Name = "TabPageSelectFile";
            TabPageSelectFile.UseVisualStyleBackColor = true;
            // 
            // CmdSelectExportDirectory
            // 
            CmdSelectExportDirectory.Image = WinFormsUI.Properties.Resources.open_file_icon;
            resources.ApplyResources(CmdSelectExportDirectory, "CmdSelectExportDirectory");
            CmdSelectExportDirectory.Name = "CmdSelectExportDirectory";
            CmdSelectExportDirectory.UseVisualStyleBackColor = true;
            CmdSelectExportDirectory.Click += CmdSelectExportDirectory_Click;
            // 
            // txtTargetDirectory
            // 
            resources.ApplyResources(txtTargetDirectory, "txtTargetDirectory");
            txtTargetDirectory.Name = "txtTargetDirectory";
            txtTargetDirectory.Tag = "";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            // 
            // LblIntro
            // 
            resources.ApplyResources(LblIntro, "LblIntro");
            LblIntro.Name = "LblIntro";
            // 
            // PnFileTypeSelection
            // 
            PnFileTypeSelection.Controls.Add(RbExistingFile);
            PnFileTypeSelection.Controls.Add(RbNewFile);
            resources.ApplyResources(PnFileTypeSelection, "PnFileTypeSelection");
            PnFileTypeSelection.Name = "PnFileTypeSelection";
            // 
            // RbExistingFile
            // 
            resources.ApplyResources(RbExistingFile, "RbExistingFile");
            RbExistingFile.Name = "RbExistingFile";
            RbExistingFile.UseVisualStyleBackColor = true;
            RbExistingFile.CheckedChanged += RbNewFile_CheckedChanged;
            // 
            // RbNewFile
            // 
            resources.ApplyResources(RbNewFile, "RbNewFile");
            RbNewFile.Name = "RbNewFile";
            RbNewFile.UseVisualStyleBackColor = true;
            RbNewFile.CheckedChanged += RbNewFile_CheckedChanged;
            // 
            // CmdNextSelectFile
            // 
            resources.ApplyResources(CmdNextSelectFile, "CmdNextSelectFile");
            CmdNextSelectFile.Name = "CmdNextSelectFile";
            CmdNextSelectFile.UseVisualStyleBackColor = true;
            CmdNextSelectFile.Click += CmdNextSelectFile_Click;
            // 
            // LblAddBaseData
            // 
            resources.ApplyResources(LblAddBaseData, "LblAddBaseData");
            LblAddBaseData.Name = "LblAddBaseData";
            // 
            // ChkAddBaseData
            // 
            resources.ApplyResources(ChkAddBaseData, "ChkAddBaseData");
            ChkAddBaseData.Checked = true;
            ChkAddBaseData.CheckState = CheckState.Checked;
            ChkAddBaseData.Name = "ChkAddBaseData";
            ChkAddBaseData.UseVisualStyleBackColor = true;
            // 
            // LblSetAsDefaultFile
            // 
            resources.ApplyResources(LblSetAsDefaultFile, "LblSetAsDefaultFile");
            LblSetAsDefaultFile.Name = "LblSetAsDefaultFile";
            // 
            // ChkSetAsDefaultFile
            // 
            resources.ApplyResources(ChkSetAsDefaultFile, "ChkSetAsDefaultFile");
            ChkSetAsDefaultFile.Checked = true;
            ChkSetAsDefaultFile.CheckState = CheckState.Checked;
            ChkSetAsDefaultFile.Name = "ChkSetAsDefaultFile";
            ChkSetAsDefaultFile.UseVisualStyleBackColor = true;
            // 
            // CmdSelectExportFile
            // 
            CmdSelectExportFile.Image = WinFormsUI.Properties.Resources.open_file_icon;
            resources.ApplyResources(CmdSelectExportFile, "CmdSelectExportFile");
            CmdSelectExportFile.Name = "CmdSelectExportFile";
            CmdSelectExportFile.UseVisualStyleBackColor = true;
            CmdSelectExportFile.Click += CmdSelectExportFile_Click;
            // 
            // TxtNewDbFile
            // 
            resources.ApplyResources(TxtNewDbFile, "TxtNewDbFile");
            TxtNewDbFile.Name = "TxtNewDbFile";
            TxtNewDbFile.Tag = "";
            // 
            // LblDbFile
            // 
            resources.ApplyResources(LblDbFile, "LblDbFile");
            LblDbFile.Name = "LblDbFile";
            // 
            // TabPageNewAddress
            // 
            TabPageNewAddress.Controls.Add(gbNewAddress);
            TabPageNewAddress.Controls.Add(CmdNextNewAddress);
            TabPageNewAddress.Controls.Add(CmPreviousTabNewAddress);
            resources.ApplyResources(TabPageNewAddress, "TabPageNewAddress");
            TabPageNewAddress.Name = "TabPageNewAddress";
            TabPageNewAddress.UseVisualStyleBackColor = true;
            // 
            // gbNewAddress
            // 
            resources.ApplyResources(gbNewAddress, "gbNewAddress");
            gbNewAddress.Controls.Add(chkDefaultAddress);
            gbNewAddress.Controls.Add(gbAddress);
            gbNewAddress.Controls.Add(gbSolar);
            gbNewAddress.Controls.Add(lblRate);
            gbNewAddress.Controls.Add(txtDescription);
            gbNewAddress.Name = "gbNewAddress";
            gbNewAddress.TabStop = false;
            // 
            // chkDefaultAddress
            // 
            resources.ApplyResources(chkDefaultAddress, "chkDefaultAddress");
            chkDefaultAddress.Checked = true;
            chkDefaultAddress.CheckState = CheckState.Checked;
            chkDefaultAddress.DataBindings.Add(new Binding("Checked", bsAddress, "DefaultAddress", true));
            chkDefaultAddress.Name = "chkDefaultAddress";
            chkDefaultAddress.UseVisualStyleBackColor = true;
            // 
            // bsAddress
            // 
            bsAddress.DataSource = typeof(EnergyUse.Models.Address);
            // 
            // gbAddress
            // 
            gbAddress.Controls.Add(txtPostcalCode);
            gbAddress.Controls.Add(lblPostcalCode);
            gbAddress.Controls.Add(label1);
            gbAddress.Controls.Add(txtCity);
            gbAddress.Controls.Add(txtHouseNo);
            gbAddress.Controls.Add(txtStreet);
            gbAddress.Controls.Add(lblCity);
            gbAddress.Controls.Add(lblStreet);
            resources.ApplyResources(gbAddress, "gbAddress");
            gbAddress.Name = "gbAddress";
            gbAddress.TabStop = false;
            // 
            // txtPostcalCode
            // 
            txtPostcalCode.DataBindings.Add(new Binding("Text", bsAddress, "PostalCode", true));
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
            txtCity.DataBindings.Add(new Binding("Text", bsAddress, "City", true));
            resources.ApplyResources(txtCity, "txtCity");
            txtCity.Name = "txtCity";
            // 
            // txtHouseNo
            // 
            txtHouseNo.DataBindings.Add(new Binding("Text", bsAddress, "HouseNumber", true));
            resources.ApplyResources(txtHouseNo, "txtHouseNo");
            txtHouseNo.Name = "txtHouseNo";
            // 
            // txtStreet
            // 
            txtStreet.DataBindings.Add(new Binding("Text", bsAddress, "Street", true));
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
            // gbSolar
            // 
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
            resources.ApplyResources(gbSolar, "gbSolar");
            gbSolar.Name = "gbSolar";
            gbSolar.TabStop = false;
            // 
            // lblHasSolarpanels
            // 
            resources.ApplyResources(lblHasSolarpanels, "lblHasSolarpanels");
            lblHasSolarpanels.Name = "lblHasSolarpanels";
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
            chkSolarPanelsAvailable.DataBindings.Add(new Binding("Checked", bsAddress, "SolarPanelsAvailable", true));
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
            txtTotalCapacitySolarPanels.DataBindings.Add(new Binding("Text", bsAddress, "TotalCapacity", true));
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
            // lblRate
            // 
            resources.ApplyResources(lblRate, "lblRate");
            lblRate.Name = "lblRate";
            // 
            // txtDescription
            // 
            txtDescription.DataBindings.Add(new Binding("Text", bsAddress, "Description", true));
            resources.ApplyResources(txtDescription, "txtDescription");
            txtDescription.Name = "txtDescription";
            // 
            // CmdNextNewAddress
            // 
            resources.ApplyResources(CmdNextNewAddress, "CmdNextNewAddress");
            CmdNextNewAddress.Name = "CmdNextNewAddress";
            CmdNextNewAddress.UseVisualStyleBackColor = true;
            CmdNextNewAddress.Click += CmdNextNewAddress_Click;
            // 
            // CmPreviousTabNewAddress
            // 
            resources.ApplyResources(CmPreviousTabNewAddress, "CmPreviousTabNewAddress");
            CmPreviousTabNewAddress.Name = "CmPreviousTabNewAddress";
            CmPreviousTabNewAddress.UseVisualStyleBackColor = true;
            CmPreviousTabNewAddress.Click += CmPreviousTabNewAddress_Click;
            // 
            // TabPageSelectEnergyTypes
            // 
            TabPageSelectEnergyTypes.Controls.Add(cmdNextNewEnergyTypes);
            TabPageSelectEnergyTypes.Controls.Add(CmdPreviousEnergyTypes);
            TabPageSelectEnergyTypes.Controls.Add(GbNewEnergyTypes);
            TabPageSelectEnergyTypes.Controls.Add(LblHelpNewEnergyTypes);
            resources.ApplyResources(TabPageSelectEnergyTypes, "TabPageSelectEnergyTypes");
            TabPageSelectEnergyTypes.Name = "TabPageSelectEnergyTypes";
            TabPageSelectEnergyTypes.UseVisualStyleBackColor = true;
            // 
            // cmdNextNewEnergyTypes
            // 
            resources.ApplyResources(cmdNextNewEnergyTypes, "cmdNextNewEnergyTypes");
            cmdNextNewEnergyTypes.Name = "cmdNextNewEnergyTypes";
            cmdNextNewEnergyTypes.UseVisualStyleBackColor = true;
            cmdNextNewEnergyTypes.Click += cmdNextNewEnergyTypes_Click;
            // 
            // CmdPreviousEnergyTypes
            // 
            resources.ApplyResources(CmdPreviousEnergyTypes, "CmdPreviousEnergyTypes");
            CmdPreviousEnergyTypes.Name = "CmdPreviousEnergyTypes";
            CmdPreviousEnergyTypes.UseVisualStyleBackColor = true;
            CmdPreviousEnergyTypes.Click += CmdPreviousEnergyTypes_Click;
            // 
            // GbNewEnergyTypes
            // 
            resources.ApplyResources(GbNewEnergyTypes, "GbNewEnergyTypes");
            GbNewEnergyTypes.Name = "GbNewEnergyTypes";
            GbNewEnergyTypes.TabStop = false;
            // 
            // LblHelpNewEnergyTypes
            // 
            resources.ApplyResources(LblHelpNewEnergyTypes, "LblHelpNewEnergyTypes");
            LblHelpNewEnergyTypes.Name = "LblHelpNewEnergyTypes";
            // 
            // TabPageSetupFile
            // 
            TabPageSetupFile.Controls.Add(lblSetupFile);
            TabPageSetupFile.Controls.Add(cmdPreviousSetupFile);
            TabPageSetupFile.Controls.Add(LblSetupNewFile);
            TabPageSetupFile.Controls.Add(CmdCreateDb);
            resources.ApplyResources(TabPageSetupFile, "TabPageSetupFile");
            TabPageSetupFile.Name = "TabPageSetupFile";
            TabPageSetupFile.UseVisualStyleBackColor = true;
            // 
            // lblSetupFile
            // 
            resources.ApplyResources(lblSetupFile, "lblSetupFile");
            lblSetupFile.Name = "lblSetupFile";
            // 
            // cmdPreviousSetupFile
            // 
            resources.ApplyResources(cmdPreviousSetupFile, "cmdPreviousSetupFile");
            cmdPreviousSetupFile.Name = "cmdPreviousSetupFile";
            cmdPreviousSetupFile.UseVisualStyleBackColor = true;
            cmdPreviousSetupFile.Click += cmdPreviousSetupFile_Click;
            // 
            // LblSetupNewFile
            // 
            resources.ApplyResources(LblSetupNewFile, "LblSetupNewFile");
            LblSetupNewFile.Name = "LblSetupNewFile";
            // 
            // CmdCreateDb
            // 
            resources.ApplyResources(CmdCreateDb, "CmdCreateDb");
            CmdCreateDb.Name = "CmdCreateDb";
            CmdCreateDb.UseVisualStyleBackColor = true;
            CmdCreateDb.Click += CmdCreateNewDb_Click;
            // 
            // bsEnergyTypes
            // 
            bsEnergyTypes.DataSource = typeof(EnergyUse.Models.EnergyType);
            // 
            // FrmSetupNewFile
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(TabControl);
            Controls.Add(StatusStrip1);
            Name = "FrmSetupNewFile";
            ShowIcon = false;
            ShowInTaskbar = false;
            TopMost = true;
            Load += FrmSetupNewFile_Load;
            TabControl.ResumeLayout(false);
            TabPageSelectFile.ResumeLayout(false);
            TabPageSelectFile.PerformLayout();
            PnFileTypeSelection.ResumeLayout(false);
            PnFileTypeSelection.PerformLayout();
            TabPageNewAddress.ResumeLayout(false);
            gbNewAddress.ResumeLayout(false);
            gbNewAddress.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddress).EndInit();
            gbAddress.ResumeLayout(false);
            gbAddress.PerformLayout();
            gbSolar.ResumeLayout(false);
            gbSolar.PerformLayout();
            TabPageSelectEnergyTypes.ResumeLayout(false);
            TabPageSelectEnergyTypes.PerformLayout();
            TabPageSetupFile.ResumeLayout(false);
            TabPageSetupFile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)bsAddresss).EndInit();
            ((System.ComponentModel.ISupportInitialize)bsEnergyTypes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolTip ToolTip1;
        private StatusStrip StatusStrip1;
        private TabControl TabControl;
        private TabPage TabPageSelectFile;
        private Label LblIntro;
        private Panel PnFileTypeSelection;
        private RadioButton RbExistingFile;
        private RadioButton RbNewFile;
        private Button CmdNextSelectFile;
        private Label LblAddBaseData;
        private CheckBox ChkAddBaseData;
        private Label LblSetAsDefaultFile;
        private CheckBox ChkSetAsDefaultFile;
        private Button CmdSelectExportFile;
        private TextBox TxtNewDbFile;
        private Label LblDbFile;
        private TabPage TabPageNewAddress;
        private Button CmdNextNewAddress;
        private Button CmPreviousTabNewAddress;
        private TabPage TabPageSelectEnergyTypes;
        private GroupBox GbNewEnergyTypes;
        private Label LblHelpNewEnergyTypes;
        private TabPage TabPageSetupFile;
        private Button cmdPreviousSetupFile;
        private Label LblSetupNewFile;
        private Button CmdCreateDb;
        private Button cmdNextNewEnergyTypes;
        private Button CmdPreviousEnergyTypes;
        private GroupBox gbNewAddress;
        private CheckBox chkDefaultAddress;
        private GroupBox gbAddress;
        private TextBox txtPostcalCode;
        private BindingSource bsAddresss;
        private Label lblPostcalCode;
        private Label label1;
        private TextBox txtCity;
        private TextBox txtHouseNo;
        private TextBox txtStreet;
        private Label lblCity;
        private Label lblStreet;
        private GroupBox gbSolar;
        private Label lblHasSolarpanels;
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
        private Label lblRate;
        private TextBox txtDescription;
        private ToolTip toolTip2;
        private BindingSource bsAddress;
        private BindingSource bsEnergyTypes;
        private Label lblSetupFile;
        private Button CmdSelectExportDirectory;
        private TextBox txtTargetDirectory;
        private Label label3;
    }
}