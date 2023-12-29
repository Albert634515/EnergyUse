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
            CmdPreviousEnergyTypes = new Button();
            GbNewEnergyTypes = new GroupBox();
            cmdNextNewEnergyTypes = new Button();
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
            resources.ApplyResources(StatusStrip1, "StatusStrip1");
            StatusStrip1.ImageScalingSize = new Size(20, 20);
            StatusStrip1.Name = "StatusStrip1";
            toolTip2.SetToolTip(StatusStrip1, resources.GetString("StatusStrip1.ToolTip"));
            ToolTip1.SetToolTip(StatusStrip1, resources.GetString("StatusStrip1.ToolTip1"));
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
            ToolTip1.SetToolTip(TabControl, resources.GetString("TabControl.ToolTip"));
            toolTip2.SetToolTip(TabControl, resources.GetString("TabControl.ToolTip1"));
            // 
            // TabPageSelectFile
            // 
            resources.ApplyResources(TabPageSelectFile, "TabPageSelectFile");
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
            TabPageSelectFile.Name = "TabPageSelectFile";
            toolTip2.SetToolTip(TabPageSelectFile, resources.GetString("TabPageSelectFile.ToolTip"));
            ToolTip1.SetToolTip(TabPageSelectFile, resources.GetString("TabPageSelectFile.ToolTip1"));
            TabPageSelectFile.UseVisualStyleBackColor = true;
            // 
            // CmdSelectExportDirectory
            // 
            resources.ApplyResources(CmdSelectExportDirectory, "CmdSelectExportDirectory");
            CmdSelectExportDirectory.Image = WinFormsUI.Properties.Resources.open_file_icon;
            CmdSelectExportDirectory.Name = "CmdSelectExportDirectory";
            toolTip2.SetToolTip(CmdSelectExportDirectory, resources.GetString("CmdSelectExportDirectory.ToolTip"));
            ToolTip1.SetToolTip(CmdSelectExportDirectory, resources.GetString("CmdSelectExportDirectory.ToolTip1"));
            CmdSelectExportDirectory.UseVisualStyleBackColor = true;
            CmdSelectExportDirectory.Click += CmdSelectExportDirectory_Click;
            // 
            // txtTargetDirectory
            // 
            resources.ApplyResources(txtTargetDirectory, "txtTargetDirectory");
            txtTargetDirectory.Name = "txtTargetDirectory";
            txtTargetDirectory.Tag = "";
            ToolTip1.SetToolTip(txtTargetDirectory, resources.GetString("txtTargetDirectory.ToolTip"));
            toolTip2.SetToolTip(txtTargetDirectory, resources.GetString("txtTargetDirectory.ToolTip1"));
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.Name = "label3";
            ToolTip1.SetToolTip(label3, resources.GetString("label3.ToolTip"));
            toolTip2.SetToolTip(label3, resources.GetString("label3.ToolTip1"));
            // 
            // LblIntro
            // 
            resources.ApplyResources(LblIntro, "LblIntro");
            LblIntro.Name = "LblIntro";
            ToolTip1.SetToolTip(LblIntro, resources.GetString("LblIntro.ToolTip"));
            toolTip2.SetToolTip(LblIntro, resources.GetString("LblIntro.ToolTip1"));
            // 
            // PnFileTypeSelection
            // 
            resources.ApplyResources(PnFileTypeSelection, "PnFileTypeSelection");
            PnFileTypeSelection.Controls.Add(RbExistingFile);
            PnFileTypeSelection.Controls.Add(RbNewFile);
            PnFileTypeSelection.Name = "PnFileTypeSelection";
            ToolTip1.SetToolTip(PnFileTypeSelection, resources.GetString("PnFileTypeSelection.ToolTip"));
            toolTip2.SetToolTip(PnFileTypeSelection, resources.GetString("PnFileTypeSelection.ToolTip1"));
            // 
            // RbExistingFile
            // 
            resources.ApplyResources(RbExistingFile, "RbExistingFile");
            RbExistingFile.Name = "RbExistingFile";
            toolTip2.SetToolTip(RbExistingFile, resources.GetString("RbExistingFile.ToolTip"));
            ToolTip1.SetToolTip(RbExistingFile, resources.GetString("RbExistingFile.ToolTip1"));
            RbExistingFile.UseVisualStyleBackColor = true;
            RbExistingFile.CheckedChanged += RbNewFile_CheckedChanged;
            // 
            // RbNewFile
            // 
            resources.ApplyResources(RbNewFile, "RbNewFile");
            RbNewFile.Name = "RbNewFile";
            toolTip2.SetToolTip(RbNewFile, resources.GetString("RbNewFile.ToolTip"));
            ToolTip1.SetToolTip(RbNewFile, resources.GetString("RbNewFile.ToolTip1"));
            RbNewFile.UseVisualStyleBackColor = true;
            RbNewFile.CheckedChanged += RbNewFile_CheckedChanged;
            // 
            // CmdNextSelectFile
            // 
            resources.ApplyResources(CmdNextSelectFile, "CmdNextSelectFile");
            CmdNextSelectFile.Name = "CmdNextSelectFile";
            toolTip2.SetToolTip(CmdNextSelectFile, resources.GetString("CmdNextSelectFile.ToolTip"));
            ToolTip1.SetToolTip(CmdNextSelectFile, resources.GetString("CmdNextSelectFile.ToolTip1"));
            CmdNextSelectFile.UseVisualStyleBackColor = true;
            CmdNextSelectFile.Click += CmdNextSelectFile_Click;
            // 
            // LblAddBaseData
            // 
            resources.ApplyResources(LblAddBaseData, "LblAddBaseData");
            LblAddBaseData.Name = "LblAddBaseData";
            ToolTip1.SetToolTip(LblAddBaseData, resources.GetString("LblAddBaseData.ToolTip"));
            toolTip2.SetToolTip(LblAddBaseData, resources.GetString("LblAddBaseData.ToolTip1"));
            // 
            // ChkAddBaseData
            // 
            resources.ApplyResources(ChkAddBaseData, "ChkAddBaseData");
            ChkAddBaseData.Checked = true;
            ChkAddBaseData.CheckState = CheckState.Checked;
            ChkAddBaseData.Name = "ChkAddBaseData";
            toolTip2.SetToolTip(ChkAddBaseData, resources.GetString("ChkAddBaseData.ToolTip"));
            ToolTip1.SetToolTip(ChkAddBaseData, resources.GetString("ChkAddBaseData.ToolTip1"));
            ChkAddBaseData.UseVisualStyleBackColor = true;
            // 
            // LblSetAsDefaultFile
            // 
            resources.ApplyResources(LblSetAsDefaultFile, "LblSetAsDefaultFile");
            LblSetAsDefaultFile.Name = "LblSetAsDefaultFile";
            ToolTip1.SetToolTip(LblSetAsDefaultFile, resources.GetString("LblSetAsDefaultFile.ToolTip"));
            toolTip2.SetToolTip(LblSetAsDefaultFile, resources.GetString("LblSetAsDefaultFile.ToolTip1"));
            // 
            // ChkSetAsDefaultFile
            // 
            resources.ApplyResources(ChkSetAsDefaultFile, "ChkSetAsDefaultFile");
            ChkSetAsDefaultFile.Checked = true;
            ChkSetAsDefaultFile.CheckState = CheckState.Checked;
            ChkSetAsDefaultFile.Name = "ChkSetAsDefaultFile";
            toolTip2.SetToolTip(ChkSetAsDefaultFile, resources.GetString("ChkSetAsDefaultFile.ToolTip"));
            ToolTip1.SetToolTip(ChkSetAsDefaultFile, resources.GetString("ChkSetAsDefaultFile.ToolTip1"));
            ChkSetAsDefaultFile.UseVisualStyleBackColor = true;
            // 
            // CmdSelectExportFile
            // 
            resources.ApplyResources(CmdSelectExportFile, "CmdSelectExportFile");
            CmdSelectExportFile.Image = WinFormsUI.Properties.Resources.open_file_icon;
            CmdSelectExportFile.Name = "CmdSelectExportFile";
            toolTip2.SetToolTip(CmdSelectExportFile, resources.GetString("CmdSelectExportFile.ToolTip"));
            ToolTip1.SetToolTip(CmdSelectExportFile, resources.GetString("CmdSelectExportFile.ToolTip1"));
            CmdSelectExportFile.UseVisualStyleBackColor = true;
            CmdSelectExportFile.Click += CmdSelectExportFile_Click;
            // 
            // TxtNewDbFile
            // 
            resources.ApplyResources(TxtNewDbFile, "TxtNewDbFile");
            TxtNewDbFile.Name = "TxtNewDbFile";
            TxtNewDbFile.Tag = "";
            ToolTip1.SetToolTip(TxtNewDbFile, resources.GetString("TxtNewDbFile.ToolTip"));
            toolTip2.SetToolTip(TxtNewDbFile, resources.GetString("TxtNewDbFile.ToolTip1"));
            // 
            // LblDbFile
            // 
            resources.ApplyResources(LblDbFile, "LblDbFile");
            LblDbFile.Name = "LblDbFile";
            ToolTip1.SetToolTip(LblDbFile, resources.GetString("LblDbFile.ToolTip"));
            toolTip2.SetToolTip(LblDbFile, resources.GetString("LblDbFile.ToolTip1"));
            // 
            // TabPageNewAddress
            // 
            resources.ApplyResources(TabPageNewAddress, "TabPageNewAddress");
            TabPageNewAddress.Controls.Add(gbNewAddress);
            TabPageNewAddress.Controls.Add(CmdNextNewAddress);
            TabPageNewAddress.Controls.Add(CmPreviousTabNewAddress);
            TabPageNewAddress.Name = "TabPageNewAddress";
            toolTip2.SetToolTip(TabPageNewAddress, resources.GetString("TabPageNewAddress.ToolTip"));
            ToolTip1.SetToolTip(TabPageNewAddress, resources.GetString("TabPageNewAddress.ToolTip1"));
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
            ToolTip1.SetToolTip(gbNewAddress, resources.GetString("gbNewAddress.ToolTip"));
            toolTip2.SetToolTip(gbNewAddress, resources.GetString("gbNewAddress.ToolTip1"));
            // 
            // chkDefaultAddress
            // 
            resources.ApplyResources(chkDefaultAddress, "chkDefaultAddress");
            chkDefaultAddress.Checked = true;
            chkDefaultAddress.CheckState = CheckState.Checked;
            chkDefaultAddress.DataBindings.Add(new Binding("Checked", bsAddress, "DefaultAddress", true));
            chkDefaultAddress.Name = "chkDefaultAddress";
            toolTip2.SetToolTip(chkDefaultAddress, resources.GetString("chkDefaultAddress.ToolTip"));
            ToolTip1.SetToolTip(chkDefaultAddress, resources.GetString("chkDefaultAddress.ToolTip1"));
            chkDefaultAddress.UseVisualStyleBackColor = true;
            // 
            // bsAddress
            // 
            bsAddress.DataSource = typeof(EnergyUse.Models.Address);
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
            ToolTip1.SetToolTip(gbAddress, resources.GetString("gbAddress.ToolTip"));
            toolTip2.SetToolTip(gbAddress, resources.GetString("gbAddress.ToolTip1"));
            // 
            // txtPostcalCode
            // 
            resources.ApplyResources(txtPostcalCode, "txtPostcalCode");
            txtPostcalCode.DataBindings.Add(new Binding("Text", bsAddress, "PostalCode", true));
            txtPostcalCode.Name = "txtPostcalCode";
            ToolTip1.SetToolTip(txtPostcalCode, resources.GetString("txtPostcalCode.ToolTip"));
            toolTip2.SetToolTip(txtPostcalCode, resources.GetString("txtPostcalCode.ToolTip1"));
            // 
            // lblPostcalCode
            // 
            resources.ApplyResources(lblPostcalCode, "lblPostcalCode");
            lblPostcalCode.Name = "lblPostcalCode";
            ToolTip1.SetToolTip(lblPostcalCode, resources.GetString("lblPostcalCode.ToolTip"));
            toolTip2.SetToolTip(lblPostcalCode, resources.GetString("lblPostcalCode.ToolTip1"));
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.Name = "label1";
            ToolTip1.SetToolTip(label1, resources.GetString("label1.ToolTip"));
            toolTip2.SetToolTip(label1, resources.GetString("label1.ToolTip1"));
            // 
            // txtCity
            // 
            resources.ApplyResources(txtCity, "txtCity");
            txtCity.DataBindings.Add(new Binding("Text", bsAddress, "City", true));
            txtCity.Name = "txtCity";
            ToolTip1.SetToolTip(txtCity, resources.GetString("txtCity.ToolTip"));
            toolTip2.SetToolTip(txtCity, resources.GetString("txtCity.ToolTip1"));
            // 
            // txtHouseNo
            // 
            resources.ApplyResources(txtHouseNo, "txtHouseNo");
            txtHouseNo.DataBindings.Add(new Binding("Text", bsAddress, "HouseNumber", true));
            txtHouseNo.Name = "txtHouseNo";
            ToolTip1.SetToolTip(txtHouseNo, resources.GetString("txtHouseNo.ToolTip"));
            toolTip2.SetToolTip(txtHouseNo, resources.GetString("txtHouseNo.ToolTip1"));
            // 
            // txtStreet
            // 
            resources.ApplyResources(txtStreet, "txtStreet");
            txtStreet.DataBindings.Add(new Binding("Text", bsAddress, "Street", true));
            txtStreet.Name = "txtStreet";
            ToolTip1.SetToolTip(txtStreet, resources.GetString("txtStreet.ToolTip"));
            toolTip2.SetToolTip(txtStreet, resources.GetString("txtStreet.ToolTip1"));
            // 
            // lblCity
            // 
            resources.ApplyResources(lblCity, "lblCity");
            lblCity.Name = "lblCity";
            ToolTip1.SetToolTip(lblCity, resources.GetString("lblCity.ToolTip"));
            toolTip2.SetToolTip(lblCity, resources.GetString("lblCity.ToolTip1"));
            // 
            // lblStreet
            // 
            resources.ApplyResources(lblStreet, "lblStreet");
            lblStreet.Name = "lblStreet";
            ToolTip1.SetToolTip(lblStreet, resources.GetString("lblStreet.ToolTip"));
            toolTip2.SetToolTip(lblStreet, resources.GetString("lblStreet.ToolTip1"));
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
            ToolTip1.SetToolTip(gbSolar, resources.GetString("gbSolar.ToolTip"));
            toolTip2.SetToolTip(gbSolar, resources.GetString("gbSolar.ToolTip1"));
            // 
            // lblHasSolarpanels
            // 
            resources.ApplyResources(lblHasSolarpanels, "lblHasSolarpanels");
            lblHasSolarpanels.Name = "lblHasSolarpanels";
            ToolTip1.SetToolTip(lblHasSolarpanels, resources.GetString("lblHasSolarpanels.ToolTip"));
            toolTip2.SetToolTip(lblHasSolarpanels, resources.GetString("lblHasSolarpanels.ToolTip1"));
            // 
            // lblSubsidy
            // 
            resources.ApplyResources(lblSubsidy, "lblSubsidy");
            lblSubsidy.Name = "lblSubsidy";
            ToolTip1.SetToolTip(lblSubsidy, resources.GetString("lblSubsidy.ToolTip"));
            toolTip2.SetToolTip(lblSubsidy, resources.GetString("lblSubsidy.ToolTip1"));
            // 
            // txtSubsidyAmount
            // 
            resources.ApplyResources(txtSubsidyAmount, "txtSubsidyAmount");
            txtSubsidyAmount.Name = "txtSubsidyAmount";
            ToolTip1.SetToolTip(txtSubsidyAmount, resources.GetString("txtSubsidyAmount.ToolTip"));
            toolTip2.SetToolTip(txtSubsidyAmount, resources.GetString("txtSubsidyAmount.ToolTip1"));
            // 
            // lblPurchaseAmount
            // 
            resources.ApplyResources(lblPurchaseAmount, "lblPurchaseAmount");
            lblPurchaseAmount.Name = "lblPurchaseAmount";
            ToolTip1.SetToolTip(lblPurchaseAmount, resources.GetString("lblPurchaseAmount.ToolTip"));
            toolTip2.SetToolTip(lblPurchaseAmount, resources.GetString("lblPurchaseAmount.ToolTip1"));
            // 
            // txtPurchaseAmount
            // 
            resources.ApplyResources(txtPurchaseAmount, "txtPurchaseAmount");
            txtPurchaseAmount.Name = "txtPurchaseAmount";
            ToolTip1.SetToolTip(txtPurchaseAmount, resources.GetString("txtPurchaseAmount.ToolTip"));
            toolTip2.SetToolTip(txtPurchaseAmount, resources.GetString("txtPurchaseAmount.ToolTip1"));
            // 
            // label6
            // 
            resources.ApplyResources(label6, "label6");
            label6.Name = "label6";
            ToolTip1.SetToolTip(label6, resources.GetString("label6.ToolTip"));
            toolTip2.SetToolTip(label6, resources.GetString("label6.ToolTip1"));
            // 
            // label4
            // 
            resources.ApplyResources(label4, "label4");
            label4.Name = "label4";
            ToolTip1.SetToolTip(label4, resources.GetString("label4.ToolTip"));
            toolTip2.SetToolTip(label4, resources.GetString("label4.ToolTip1"));
            // 
            // chkSolarPanelsAvailable
            // 
            resources.ApplyResources(chkSolarPanelsAvailable, "chkSolarPanelsAvailable");
            chkSolarPanelsAvailable.DataBindings.Add(new Binding("Checked", bsAddress, "SolarPanelsAvailable", true));
            chkSolarPanelsAvailable.Name = "chkSolarPanelsAvailable";
            chkSolarPanelsAvailable.Tag = "";
            toolTip2.SetToolTip(chkSolarPanelsAvailable, resources.GetString("chkSolarPanelsAvailable.ToolTip"));
            ToolTip1.SetToolTip(chkSolarPanelsAvailable, resources.GetString("chkSolarPanelsAvailable.ToolTip1"));
            chkSolarPanelsAvailable.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.Name = "label2";
            ToolTip1.SetToolTip(label2, resources.GetString("label2.ToolTip"));
            toolTip2.SetToolTip(label2, resources.GetString("label2.ToolTip1"));
            // 
            // txtTotalCapacitySolarPanels
            // 
            resources.ApplyResources(txtTotalCapacitySolarPanels, "txtTotalCapacitySolarPanels");
            txtTotalCapacitySolarPanels.DataBindings.Add(new Binding("Text", bsAddress, "TotalCapacity", true));
            txtTotalCapacitySolarPanels.Name = "txtTotalCapacitySolarPanels";
            txtTotalCapacitySolarPanels.Tag = "";
            ToolTip1.SetToolTip(txtTotalCapacitySolarPanels, resources.GetString("txtTotalCapacitySolarPanels.ToolTip"));
            toolTip2.SetToolTip(txtTotalCapacitySolarPanels, resources.GetString("txtTotalCapacitySolarPanels.ToolTip1"));
            // 
            // txtQualityReductionSolarPanels
            // 
            resources.ApplyResources(txtQualityReductionSolarPanels, "txtQualityReductionSolarPanels");
            txtQualityReductionSolarPanels.Name = "txtQualityReductionSolarPanels";
            txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels";
            ToolTip1.SetToolTip(txtQualityReductionSolarPanels, resources.GetString("txtQualityReductionSolarPanels.ToolTip"));
            toolTip2.SetToolTip(txtQualityReductionSolarPanels, resources.GetString("txtQualityReductionSolarPanels.ToolTip1"));
            // 
            // lblTotalCapacity
            // 
            resources.ApplyResources(lblTotalCapacity, "lblTotalCapacity");
            lblTotalCapacity.Name = "lblTotalCapacity";
            ToolTip1.SetToolTip(lblTotalCapacity, resources.GetString("lblTotalCapacity.ToolTip"));
            toolTip2.SetToolTip(lblTotalCapacity, resources.GetString("lblTotalCapacity.ToolTip1"));
            // 
            // lblRate
            // 
            resources.ApplyResources(lblRate, "lblRate");
            lblRate.Name = "lblRate";
            ToolTip1.SetToolTip(lblRate, resources.GetString("lblRate.ToolTip"));
            toolTip2.SetToolTip(lblRate, resources.GetString("lblRate.ToolTip1"));
            // 
            // txtDescription
            // 
            resources.ApplyResources(txtDescription, "txtDescription");
            txtDescription.DataBindings.Add(new Binding("Text", bsAddress, "Description", true));
            txtDescription.Name = "txtDescription";
            ToolTip1.SetToolTip(txtDescription, resources.GetString("txtDescription.ToolTip"));
            toolTip2.SetToolTip(txtDescription, resources.GetString("txtDescription.ToolTip1"));
            // 
            // CmdNextNewAddress
            // 
            resources.ApplyResources(CmdNextNewAddress, "CmdNextNewAddress");
            CmdNextNewAddress.Name = "CmdNextNewAddress";
            toolTip2.SetToolTip(CmdNextNewAddress, resources.GetString("CmdNextNewAddress.ToolTip"));
            ToolTip1.SetToolTip(CmdNextNewAddress, resources.GetString("CmdNextNewAddress.ToolTip1"));
            CmdNextNewAddress.UseVisualStyleBackColor = true;
            CmdNextNewAddress.Click += CmdNextNewAddress_Click;
            // 
            // CmPreviousTabNewAddress
            // 
            resources.ApplyResources(CmPreviousTabNewAddress, "CmPreviousTabNewAddress");
            CmPreviousTabNewAddress.Name = "CmPreviousTabNewAddress";
            toolTip2.SetToolTip(CmPreviousTabNewAddress, resources.GetString("CmPreviousTabNewAddress.ToolTip"));
            ToolTip1.SetToolTip(CmPreviousTabNewAddress, resources.GetString("CmPreviousTabNewAddress.ToolTip1"));
            CmPreviousTabNewAddress.UseVisualStyleBackColor = true;
            CmPreviousTabNewAddress.Click += CmPreviousTabNewAddress_Click;
            // 
            // TabPageSelectEnergyTypes
            // 
            resources.ApplyResources(TabPageSelectEnergyTypes, "TabPageSelectEnergyTypes");
            TabPageSelectEnergyTypes.Controls.Add(CmdPreviousEnergyTypes);
            TabPageSelectEnergyTypes.Controls.Add(GbNewEnergyTypes);
            TabPageSelectEnergyTypes.Controls.Add(cmdNextNewEnergyTypes);
            TabPageSelectEnergyTypes.Controls.Add(LblHelpNewEnergyTypes);
            TabPageSelectEnergyTypes.Name = "TabPageSelectEnergyTypes";
            toolTip2.SetToolTip(TabPageSelectEnergyTypes, resources.GetString("TabPageSelectEnergyTypes.ToolTip"));
            ToolTip1.SetToolTip(TabPageSelectEnergyTypes, resources.GetString("TabPageSelectEnergyTypes.ToolTip1"));
            TabPageSelectEnergyTypes.UseVisualStyleBackColor = true;
            // 
            // CmdPreviousEnergyTypes
            // 
            resources.ApplyResources(CmdPreviousEnergyTypes, "CmdPreviousEnergyTypes");
            CmdPreviousEnergyTypes.Name = "CmdPreviousEnergyTypes";
            toolTip2.SetToolTip(CmdPreviousEnergyTypes, resources.GetString("CmdPreviousEnergyTypes.ToolTip"));
            ToolTip1.SetToolTip(CmdPreviousEnergyTypes, resources.GetString("CmdPreviousEnergyTypes.ToolTip1"));
            CmdPreviousEnergyTypes.UseVisualStyleBackColor = true;
            CmdPreviousEnergyTypes.Click += CmdPreviousEnergyTypes_Click;
            // 
            // GbNewEnergyTypes
            // 
            resources.ApplyResources(GbNewEnergyTypes, "GbNewEnergyTypes");
            GbNewEnergyTypes.Name = "GbNewEnergyTypes";
            GbNewEnergyTypes.TabStop = false;
            ToolTip1.SetToolTip(GbNewEnergyTypes, resources.GetString("GbNewEnergyTypes.ToolTip"));
            toolTip2.SetToolTip(GbNewEnergyTypes, resources.GetString("GbNewEnergyTypes.ToolTip1"));
            // 
            // cmdNextNewEnergyTypes
            // 
            resources.ApplyResources(cmdNextNewEnergyTypes, "cmdNextNewEnergyTypes");
            cmdNextNewEnergyTypes.Name = "cmdNextNewEnergyTypes";
            toolTip2.SetToolTip(cmdNextNewEnergyTypes, resources.GetString("cmdNextNewEnergyTypes.ToolTip"));
            ToolTip1.SetToolTip(cmdNextNewEnergyTypes, resources.GetString("cmdNextNewEnergyTypes.ToolTip1"));
            cmdNextNewEnergyTypes.UseVisualStyleBackColor = true;
            cmdNextNewEnergyTypes.Click += cmdNextNewEnergyTypes_Click;
            // 
            // LblHelpNewEnergyTypes
            // 
            resources.ApplyResources(LblHelpNewEnergyTypes, "LblHelpNewEnergyTypes");
            LblHelpNewEnergyTypes.Name = "LblHelpNewEnergyTypes";
            ToolTip1.SetToolTip(LblHelpNewEnergyTypes, resources.GetString("LblHelpNewEnergyTypes.ToolTip"));
            toolTip2.SetToolTip(LblHelpNewEnergyTypes, resources.GetString("LblHelpNewEnergyTypes.ToolTip1"));
            // 
            // TabPageSetupFile
            // 
            resources.ApplyResources(TabPageSetupFile, "TabPageSetupFile");
            TabPageSetupFile.Controls.Add(lblSetupFile);
            TabPageSetupFile.Controls.Add(cmdPreviousSetupFile);
            TabPageSetupFile.Controls.Add(LblSetupNewFile);
            TabPageSetupFile.Controls.Add(CmdCreateDb);
            TabPageSetupFile.Name = "TabPageSetupFile";
            toolTip2.SetToolTip(TabPageSetupFile, resources.GetString("TabPageSetupFile.ToolTip"));
            ToolTip1.SetToolTip(TabPageSetupFile, resources.GetString("TabPageSetupFile.ToolTip1"));
            TabPageSetupFile.UseVisualStyleBackColor = true;
            // 
            // lblSetupFile
            // 
            resources.ApplyResources(lblSetupFile, "lblSetupFile");
            lblSetupFile.Name = "lblSetupFile";
            ToolTip1.SetToolTip(lblSetupFile, resources.GetString("lblSetupFile.ToolTip"));
            toolTip2.SetToolTip(lblSetupFile, resources.GetString("lblSetupFile.ToolTip1"));
            // 
            // cmdPreviousSetupFile
            // 
            resources.ApplyResources(cmdPreviousSetupFile, "cmdPreviousSetupFile");
            cmdPreviousSetupFile.Name = "cmdPreviousSetupFile";
            toolTip2.SetToolTip(cmdPreviousSetupFile, resources.GetString("cmdPreviousSetupFile.ToolTip"));
            ToolTip1.SetToolTip(cmdPreviousSetupFile, resources.GetString("cmdPreviousSetupFile.ToolTip1"));
            cmdPreviousSetupFile.UseVisualStyleBackColor = true;
            cmdPreviousSetupFile.Click += cmdPreviousSetupFile_Click;
            // 
            // LblSetupNewFile
            // 
            resources.ApplyResources(LblSetupNewFile, "LblSetupNewFile");
            LblSetupNewFile.Name = "LblSetupNewFile";
            ToolTip1.SetToolTip(LblSetupNewFile, resources.GetString("LblSetupNewFile.ToolTip"));
            toolTip2.SetToolTip(LblSetupNewFile, resources.GetString("LblSetupNewFile.ToolTip1"));
            // 
            // CmdCreateDb
            // 
            resources.ApplyResources(CmdCreateDb, "CmdCreateDb");
            CmdCreateDb.Name = "CmdCreateDb";
            toolTip2.SetToolTip(CmdCreateDb, resources.GetString("CmdCreateDb.ToolTip"));
            ToolTip1.SetToolTip(CmdCreateDb, resources.GetString("CmdCreateDb.ToolTip1"));
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
            ToolTip1.SetToolTip(this, resources.GetString("$this.ToolTip"));
            toolTip2.SetToolTip(this, resources.GetString("$this.ToolTip1"));
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