using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmAddresses : Form
{
    #region FormProperties

    private AddressController _controller;

    #endregion

    #region InitForm

    public frmAddresses()
    {
        _controller = new AddressController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
        initializeForm();
    }

    private void initializeForm()
    {
        setAddresses();
        setTariffGroups();
    }

    private void setAddresses()
    {
        bsAddresses.DataSource = _controller.GetAllAdresses();
    }

    private void setTariffGroups()
    {
        generalTaxBindingSource.DataSource = _controller.GetTariffGroups((int)EnergyUse.Common.Enums.TariffGroupType.GeneralCosts);
        defaultEnergyBindingSource.DataSource = _controller.GetTariffGroups((int)EnergyUse.Common.Enums.TariffGroupType.EnergyCosts);
    }

    #endregion

    #region Events

    private void bsAddresses_CurrentChanged(object sender, EventArgs e)
    {
        getSettings();
    }

    private void frmAddresses_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = dgAddresses.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    #endregion

    #region Toolbar

    private void tsbAdd_Click(object sender, EventArgs e)
    {
        addAddress();
    }
    private void tsbSave_Click(object sender, EventArgs e)
    {
        setAddress();
    }

    private void tbsCancel_Click(object sender, EventArgs e)
    {
        cancelAddress();
    }

    private void tsbDelete_Click(object sender, EventArgs e)
    {
        deleteAddress();
    }

    private void tsbRefresh_Click(object sender, EventArgs e)
    {
        setAddresses();
    }

    private void tsbClose_Click(object sender, EventArgs e)
    {
        closeAddressForm();
    }

    #endregion

    #region Methods

    private bool validateInput()
    {
        EnergyUse.Models.Address currentAddress = (EnergyUse.Models.Address)bsAddresses.Current;

        if (string.IsNullOrWhiteSpace(txtDescription.Text))
        {
            var message = Managers.Languages.GetResourceString("DescriptionIsRequired", "Description is a required field");
            MessageBox.Show(this, message);
            return false;
        }

        int addressCount = _controller.UnitOfWork.AddressRepo.GetExistsByDescription(currentAddress.Description.Trim(), currentAddress.Id);
        if (addressCount > 0)
        {
            var message = Managers.Languages.GetResourceString("AddressWithDescription", "There already is an address with description %s");
            message = message.Replace("%s", currentAddress.Description.Trim());
            MessageBox.Show(this, message);
            return false;
        }

        return true;
    }

    private void addAddress()
    {
        var entity = _controller.AddDefaultEntity(Managers.Languages.GetResourceString("Newaddress", "New address"));

        bsAddresses.DataSource = _controller.UnitOfWork.Addresses;
        bsAddresses.ResetBindings(false);

        var index = _controller.UnitOfWork.Addresses.IndexOf(entity);
        bsAddresses.Position = index;
    }

    private void setAddress()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        dgAddresses.Focus();

        if (!validateInput())
            return;

        if (bsAddresses.Current != null)
        {
            _controller.UnitOfWork.Complete();

            //Reload saved address
            EnergyUse.Models.Address address = (EnergyUse.Models.Address)bsAddresses.Current;
            if (address.Id == 0)
                address = _controller.UnitOfWork.AddressRepo.GetByDescription(address.Description);

            setAddressSettingsTags(address);

            //Save additional address data to general settings table
            saveSettingTextBox(txtPurchaseAmount);
            saveSettingTextBox(txtSubsidyAmount);
            saveSettingTextBox(txtQualityReductionSolarPanels);
            saveSettingTextBox(txtTotalCapacitySolarPanels);
        }
    }

    private void saveSettingTextBox(TextBox sender)

    {
        var settingValue = Managers.Settings.GetSetting((TextBox)sender);
        _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
    }

    private void cancelAddress()
    {
        _controller.UnitOfWork.CancelChanges();
        setAddresses();
    }

    private void deleteAddress()
    {
        EnergyUse.Models.Address entity = (EnergyUse.Models.Address)bsAddresses.Current;
        _controller.UnitOfWork.Delete(entity);
        bsAddresses.DataSource = _controller.UnitOfWork.Addresses;
        bsAddresses.ResetBindings(false);
    }

    private void getSettings()
    {
        EnergyUse.Models.Address address = new EnergyUse.Models.Address();
        if (bsAddresses.Current != null)
            address = (EnergyUse.Models.Address)bsAddresses.Current;

        setAddressSettingsTags(address);

        Managers.Settings.GetSettingTextBox(txtPurchaseAmount);
        Managers.Settings.GetSettingTextBox(txtSubsidyAmount);
        Managers.Settings.GetSettingTextBox(txtQualityReductionSolarPanels);
    }

    private void closeAddressForm()
    {
        Close();
    }

    private void setAddressSettingsTags(EnergyUse.Models.Address address)
    {
        txtPurchaseAmount.Tag = "PurchaseAmount_A";
        txtSubsidyAmount.Tag = "SubsidyAmount_A";
        txtQualityReductionSolarPanels.Tag = "QualityReductionSolarPanels_A";
        txtTotalCapacitySolarPanels.Tag = "TotalCapacitySolarPanels_A";

        if (address.Id > 0)
        {
            txtPurchaseAmount.Tag = $"{txtPurchaseAmount.Tag}{address.Id}";
            txtSubsidyAmount.Tag = $"{txtSubsidyAmount.Tag}{address.Id}";
            txtQualityReductionSolarPanels.Tag = $"{txtQualityReductionSolarPanels.Tag}{address.Id}";
            txtTotalCapacitySolarPanels.Tag = $"{txtTotalCapacitySolarPanels.Tag}{address.Id}";
        }
    }

    protected void setBaseFormSettings()
    {
        _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.Address(Managers.Config.GetDbFileName());

        Managers.Settings.SetBaseFormSettings(this);
        if (BackColor != Color.Empty)
            dgAddresses.BackgroundColor = BackColor;
    }

    #endregion
}