using EnergyUse.Common.Libs;
using EnergyUse.Core.Controllers;
using EnergyUse.Models;

namespace WinFormsEF.Views;

public partial class FrmSetupNewFile : Form
{
    #region FormProperties

    private SetupNewFileController _controller;

    #endregion

    #region InitForm

    public FrmSetupNewFile()
    {
        _controller = new SetupNewFileController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
    }

    private void FrmSetupNewFile_Load(object sender, EventArgs e)
    {
        TabControl.TabPages.Remove(TabPageNewAddress);
        TabControl.TabPages.Remove(TabPageSelectEnergyTypes);
        TabControl.TabPages.Remove(TabPageSetupFile);

        bsAddress.DataSource = EnergyUse.Core.Manager.LibBaseData.GetDemoAddress(1);
    }

    #endregion

    #region ButtonEventsNavigation

    private void CmdNextSelectFile_Click(object sender, EventArgs e)
    {
        if (!ValidateFileSelect())
            return;

        if (RbNewFile.Checked)
            navigateNextTab(1);
        else
            navigateNextTab(3);
    }

    private bool ValidateFileSelect()
    {
        if (string.IsNullOrWhiteSpace(txtTargetDirectory.Text))
        {
            MessageBox.Show(this, "Selected a target directory to place the file in.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(TxtNewDbFile.Text))
        {
            MessageBox.Show(this, "Enter a file name.");
            return false;
        }

        if (!string.IsNullOrWhiteSpace(TxtNewDbFile.Text) && !TxtNewDbFile.Text.EndsWith(".db"))
        {
            MessageBox.Show(this, "File name should end with the file extension .db");
            return false;
        }

        var selectedFile = Path.Combine(txtTargetDirectory.Text, TxtNewDbFile.Text);
        if (File.Exists(selectedFile) && RbNewFile.Checked)
        {
            if (MessageBox.Show(this, "Selected file already exits, are you sure you want to create a new file and overwrite selected file?", "Overwrite existing?", MessageBoxButtons.YesNo) == DialogResult.No)
                return false;
        }

        return true;
    }

    private void CmPreviousTabNewAddress_Click(object sender, EventArgs e)
    {
        navigateNextTab(0);
    }

    private void CmdNextNewAddress_Click(object sender, EventArgs e)
    {
        navigateNextTab(2);
    }

    private void CmdPreviousEnergyTypes_Click(object sender, EventArgs e)
    {
        navigateNextTab(1);
    }

    private void cmdNextNewEnergyTypes_Click(object sender, EventArgs e)
    {
        navigateNextTab(3);
    }

    private void cmdPreviousSetupFile_Click(object sender, EventArgs e)
    {
        if (RbNewFile.Checked)
            navigateNextTab(2);
        else
            navigateNextTab(0);
    }

    private void CmdCreateNewDb_Click(object sender, EventArgs e)
    {
        var targetFile = Path.Combine(txtTargetDirectory.Text.Trim(), TxtNewDbFile.Text.Trim());
        var currentFile = Managers.Config.GetDbFileName().Trim();

        if (!validateNewSetup())
            return;

        Cursor = Cursors.WaitCursor;

        if (!Directory.Exists(Path.GetDirectoryName(targetFile)))
            Directory.CreateDirectory(Path.GetDirectoryName(targetFile));

        if (RbNewFile.Checked)
        {
            // Write new db to app config
            Managers.Config.SetDbFileName(targetFile);

            //Reconnect
            _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.SetupNewFile(targetFile);

            // Set as current in app config during creation to add new data
            setAsDefaultFile(targetFile);

            setUpNewAddress();
            setUpNewMeters();

            if (ChkAddBaseData.Checked)
                setUpCostCategory();
        }

        if (ChkSetAsDefaultFile.Checked || RbExistingFile.Checked)
            setAsDefaultFile(targetFile);
        else if (ChkSetAsDefaultFile.Checked == false && !string.IsNullOrWhiteSpace(currentFile))
            setAsDefaultFile(currentFile);
        else
            setAsDefaultFile(targetFile);

        Cursor = Cursors.Default;

        var message = "Database has been setup";
        MessageBox.Show(this, message);
        Close();
    }

    #endregion

    #region Events

    private void RbNewFile_CheckedChanged(object sender, EventArgs e)
    {
        LblAddBaseData.Visible = !RbExistingFile.Checked;
        ChkAddBaseData.Visible = !RbExistingFile.Checked;

        LblSetupNewFile.Visible = !RbExistingFile.Checked;
        lblSetupFile.Visible = RbExistingFile.Checked;
        lblSetupFile.Location = LblSetupNewFile.Location;
    }

    private void CmdSelectExportDirectory_Click(object sender, EventArgs e)
    {
        FolderBrowserDialog folderDlg = new FolderBrowserDialog();
        folderDlg.ShowNewFolderButton = true;
        //folderDlg.RootFolder = txtTargetDirectory.Text;

        DialogResult result = folderDlg.ShowDialog();
        if (result == DialogResult.OK)
            txtTargetDirectory.Text = folderDlg.SelectedPath;
    }

    private void CmdSelectExportFile_Click(object sender, EventArgs e)
    {
        try
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Path.GetDirectoryName(TxtNewDbFile.Text);
            openFileDialog.Title = "Select Energy Use Db file";
            openFileDialog.Filter = "Energy Use Db file|*.db";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtTargetDirectory.Text = Path.GetDirectoryName(openFileDialog.FileName);
                TxtNewDbFile.Text = Path.GetFileName(openFileDialog.FileName);
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    #endregion

    #region Toolbar

    #endregion

    #region Methods

    private void navigateNextTab(int nextTabId)
    {
        TabControl.TabPages.Remove(TabControl.SelectedTab);

        if (nextTabId == 0)
            TabControl.TabPages.Add(TabPageSelectFile);
        else if (nextTabId == 1)
            TabControl.TabPages.Add(TabPageNewAddress);
        else if (nextTabId == 2)
            TabControl.TabPages.Add(TabPageSelectEnergyTypes);
        else if (nextTabId == 3)
            TabControl.TabPages.Add(TabPageSetupFile);
    }

    private void setDefaultDataDirectory()
    {
        var currentFile = Managers.Config.GetDbFileName();
        if (!string.IsNullOrWhiteSpace(currentFile))
            txtTargetDirectory.Text = Path.GetDirectoryName(LibGeneral.GetDefaultDataFile(currentFile));
        else
            txtTargetDirectory.Text = Path.Combine(System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EnergyUse");
    }

    private void setDefaultDataFile()
    {
        var currentFile = Managers.Config.GetDbFileName();
        if (!string.IsNullOrWhiteSpace(currentFile))
            TxtNewDbFile.Text = Path.GetFileName(LibGeneral.GetDefaultDataFile(currentFile));
        else
            TxtNewDbFile.Text = "EnergyUse.db";
    }

    private bool validateNewSetup()
    {
        var targetFile = Path.Combine(txtTargetDirectory.Text.Trim(), TxtNewDbFile.Text.Trim());

        if (RbNewFile.Checked && string.IsNullOrWhiteSpace(targetFile))
        {
            MessageBox.Show(this, "Please first select an new database filename");
            return false;
        }

        if (RbNewFile.Checked && File.Exists(targetFile))
        {
            if (MessageBox.Show(this, "File already exists, overwrite?", "Overwrite?", MessageBoxButtons.YesNo) == DialogResult.No)
                return false;
        }

        if (RbExistingFile.Checked && string.IsNullOrWhiteSpace(targetFile))
        {
            MessageBox.Show(this, "Please first select an existing database filename");
            return false;
        }

        if (RbExistingFile.Checked && !File.Exists(targetFile))
        {
            if (MessageBox.Show(this, "File does not exists?", "File not found?", MessageBoxButtons.YesNo) == DialogResult.No)
                return false;
        }

        return true;
    }

    private void setAsDefaultFile(string defaultFile)
    {
        if (!File.Exists(defaultFile))
            MessageBox.Show(this, $"File {defaultFile} does not exits or is not acessable, set as default is not possible");
        else
            Managers.Config.SetDbFileName(defaultFile);
    }

    private void setBaseFormSettings()
    {
        var sourceDb = Managers.Config.GetDbFileName();

        if (!string.IsNullOrEmpty(sourceDb) && File.Exists(sourceDb))
        {
            _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.SetupNewFile(Managers.Config.GetDbFileName());
            Managers.Settings.SetBaseFormSettings(this);
        }

        RbNewFile.Checked = true;
        setDefaultDataDirectory();
        setDefaultDataFile();
        setAddressSettingsTags();
        addDefaultEnergyTypes();
    }

    private void addDefaultEnergyTypes(bool hasNormalAndLow = true, bool hasEnergyReturn = false)
    {
        Point location = new Point(5, 20);

        var energyTypeList = EnergyUse.Core.Manager.LibBaseData.GetDefaultEnergyTypes(hasNormalAndLow, hasEnergyReturn);
        bsEnergyTypes.DataSource = energyTypeList;
        foreach (var energyType in energyTypeList)
        {
            var newLabel = new Label();
            newLabel.Text = energyType.Name;
            newLabel.Location = location;
            newLabel.Font = new Font(Label.DefaultFont, FontStyle.Bold);
            location.Y += newLabel.Height + 10;

            GbNewEnergyTypes.Controls.Add(newLabel);
        }
    }

    private void setUpCostCategory()
    {
        var costCategories = getListOfNewCostCategories();
        var tmpCostCategoryId = 0;
        foreach (var costCategory in costCategories)
        {
            tmpCostCategoryId++;

            costCategory.Id = tmpCostCategoryId;
            _controller.UnitOfWork.CostCategoriesRepo.Add(costCategory);
        }

        _controller.UnitOfWork.Complete();
    }

    private void setUpNewAddress()
    {
        var address = (Address)bsAddress.Current;
        var tarifGroupDefault = _controller.UnitOfWork.TarifGroupRepo.SelectByDescription("Default");

        address.TariffGroupId = tarifGroupDefault.Id;
        address.DefaultAddress = true;
        _controller.UnitOfWork.AddressRepo.Add(address);
        _controller.UnitOfWork.Complete();
    }

    private void setupMeter(EnergyType energyType, long addressId)
    {
        Meter meter = new Meter();
        meter.Description = $"{energyType.Name} meter";
        meter.Number = $"Meter no. for {energyType.Name}";
        meter.AddressId = addressId;
        meter.EnergyTypeId = energyType.Id;
        _controller.UnitOfWork.MeterRepo.Add(meter);
        _controller.UnitOfWork.Complete();
    }

    private void setUpNewMeters()
    {
        var energyTypeList = (List<EnergyType>)bsEnergyTypes.DataSource;
        var address = (Address)bsAddress.Current;
        foreach (var energyType in energyTypeList)
        {
            setupMeter(energyType, address.Id);
        }
    }

    private List<CostCategory> getListOfNewCostCategories()
    {
        var costCategories = new List<CostCategory>();
        var calTypePU = _controller.UnitOfWork.CalculationTypeRepo.SelectByDescription("Per Unit");
        var calTypePd = _controller.UnitOfWork.CalculationTypeRepo.SelectByDescription("Per Day");
        var energyTypeElectricity = _controller.UnitOfWork.EnergyTypeRepo.SelectByName("Electricity");
        var energySubType = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Normal");
        var energySubTypeOther = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Other");
        var tarifGroupDefault = _controller.UnitOfWork.TarifGroupRepo.SelectByDescription("Default");
        var tarifGroupGeneral = _controller.UnitOfWork.TarifGroupRepo.SelectByDescription("General");

        CostCategory costCategory;
        if (energyTypeElectricity != null)
        {
            costCategory = new CostCategory();
            costCategory.Name = "Energy rate normal";
            costCategory.SortOrder = 1;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubType.Id;
            costCategory.UnitId = "kWh";
            costCategory.TariffGroupId = tarifGroupDefault.Id;
            costCategories.Add(costCategory);

            energySubType = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Low");
            costCategory = new CostCategory();
            costCategory.Name = "Energy rate low";
            costCategory.SortOrder = 2;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubType.Id;
            costCategory.UnitId = "kWh";
            costCategory.TariffGroupId = tarifGroupDefault.Id;
            costCategories.Add(costCategory);

            energySubType = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("ReturnNormal");
            costCategory = new CostCategory();
            costCategory.Name = "Return delivery normal rate";
            costCategory.SortOrder = 3;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubType.Id;
            costCategory.UnitId = "kWh";
            costCategory.TariffGroupId = tarifGroupDefault.Id;
            costCategories.Add(costCategory);

            energySubType = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("ReturnLow");
            costCategory = new CostCategory();
            costCategory.Name = "Return delivery low rate";
            costCategory.SortOrder = 4;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubType.Id;
            costCategory.UnitId = "kWh";
            costCategory.TariffGroupId = tarifGroupDefault.Id;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Delivery costs";
            costCategory.SortOrder = 7;
            costCategory.CalculationTypeId = calTypePd.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "Day";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Network costs";
            costCategory.SortOrder = 8;
            costCategory.CalculationTypeId = calTypePd.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "Day";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Reduction energy tax";
            costCategory.SortOrder = 9;
            costCategory.CalculationTypeId = calTypePd.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "Day";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Energy tax";
            costCategory.SortOrder = 5;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeElectricity.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "kWh";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategory.CanNotBeNegative = true;
            costCategory.NotCalculateOverReturn = true;
            costCategories.Add(costCategory);
        }

        var energyTypeGas = _controller.UnitOfWork.EnergyTypeRepo.SelectByName("Gas");
        if (energyTypeGas != null)
        {
            energySubType = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Normal");
            costCategory = new CostCategory();
            costCategory.Name = "Energie tarief gas";
            costCategory.SortOrder = 2;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeGas.Id;
            costCategory.EnergySubTypeId = energySubType.Id;
            costCategory.UnitId = "m3";
            costCategory.TariffGroupId = tarifGroupDefault.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Vaste leveringskosten";
            costCategory.SortOrder = 4;
            costCategory.CalculationTypeId = calTypePd.Id;
            costCategory.EnergyTypeId = energyTypeGas.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "Day";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Netbeheerkosten";
            costCategory.SortOrder = 5;
            costCategory.CalculationTypeId = calTypePd.Id;
            costCategory.EnergyTypeId = energyTypeGas.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "Day";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Opslag duurzame energie";
            costCategory.SortOrder = 3;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeGas.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "m3";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Energy tax";
            costCategory.SortOrder = 2;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeGas.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "m3";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);

            costCategory = new CostCategory();
            costCategory.Name = "Opslag duurzame energie";
            costCategory.SortOrder = 6;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeGas.Id;
            costCategory.EnergySubTypeId = energySubTypeOther.Id;
            costCategory.UnitId = "kWh";
            costCategory.TariffGroupId = tarifGroupGeneral.Id;
            costCategory.CalculateVat = true;
            costCategory.CanNotBeNegative = true;
            costCategory.NotCalculateOverReturn = true;
            costCategories.Add(costCategory);
        }

        var energyTypeWater = _controller.UnitOfWork.EnergyTypeRepo.SelectByName("Water");
        if (energyTypeWater != null)
        {
            energySubType = _controller.UnitOfWork.EnergySubTypeRepo.SelectByDescription("Normal");
            costCategory = new CostCategory();
            costCategory.Name = "Energie tarief water";
            costCategory.SortOrder = 3;
            costCategory.CalculationTypeId = calTypePU.Id;
            costCategory.EnergyTypeId = energyTypeWater.Id;
            costCategory.EnergySubTypeId = energySubType.Id;
            costCategory.UnitId = "m3";
            costCategory.TariffGroupId = tarifGroupDefault.Id;
            costCategory.CalculateVat = true;
            costCategories.Add(costCategory);
        }

        return costCategories;
    }

    private void setAddressSettingsTags()
    {
        var address = (Address)bsAddresss.Current;

        txtPurchaseAmount.Tag = "PurchaseAmount_A";
        txtSubsidyAmount.Tag = "SubsidyAmount_A";
        txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels_A";

        if (address != null && address.Id > 0)
        {
            txtPurchaseAmount.Tag = $"{txtPurchaseAmount.Tag}{address.Id}";
            txtSubsidyAmount.Tag = $"{txtSubsidyAmount.Tag}{address.Id}";
            txtQualityReductionSolarPanels.Tag = $"{txtQualityReductionSolarPanels.Tag}{address.Id}";
        }
    }

    #endregion
}