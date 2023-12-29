namespace WinFormsEF.Views
{
    public partial class frmAddresses : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.Address _unitOfWork;
        
        #endregion

        #region InitForm

        public frmAddresses()
        {
            InitializeComponent();
            setBaseFormSettings();
            LoadForm();
        }

        private void LoadForm()
        {
            LoadAddresses();
        }

        private void LoadAddresses()
        {
            _unitOfWork.Addresses = _unitOfWork.AddressRepo.GetAll().ToList();
            bsAddresses.DataSource = _unitOfWork.Addresses;
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

            if (_unitOfWork.HasChanges())
                e.Cancel = Managers.General.WarningUnsavedChanges(this);
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
            LoadAddresses();
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

            int addressCount = _unitOfWork.AddressRepo.GetExistsByDescription(currentAddress.Description.Trim(), currentAddress.Id);
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
            var entity = _unitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("Newaddress", "New address"));

            bsAddresses.DataSource = _unitOfWork.Addresses;
            bsAddresses.ResetBindings(false);

            var index = _unitOfWork.Addresses.IndexOf(entity);
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
                _unitOfWork.Complete();

                EnergyUse.Models.Address address = (EnergyUse.Models.Address)bsAddresses.Current;
                if (address.Id == 0)
                    address = _unitOfWork.AddressRepo.GetByDescription(address.Description);

                setAddressSettingsTags(address);

                Managers.Settings.SaveSettingTextBox(txtPurchaseAmount);
                Managers.Settings.SaveSettingTextBox(txtSubsidyAmount);
                Managers.Settings.SaveSettingTextBox(txtQualityReductionSolarPanels);
                Managers.Settings.SaveSettingTextBox(txtTotalCapacitySolarPanels);
            }
        }

        private void cancelAddress()
        {
            _unitOfWork.CancelChanges();
            LoadAddresses();
        }

        private void deleteAddress()
        {
            EnergyUse.Models.Address entity = (EnergyUse.Models.Address)bsAddresses.Current;
            _unitOfWork.Delete(entity);
            bsAddresses.DataSource = _unitOfWork.Addresses;
            bsAddresses.ResetBindings(false);
        }

        private void getSettings()
        {
            EnergyUse.Models.Address address = new EnergyUse.Models.Address();
            if (bsAddresses.Current != null)
                address = (EnergyUse.Models.Address)bsAddresses.Current;

            setAddressSettingsTags(address);

            Managers.Settings.LoadSettingTextBox(txtPurchaseAmount);
            Managers.Settings.LoadSettingTextBox(txtSubsidyAmount);
            Managers.Settings.LoadSettingTextBox(txtQualityReductionSolarPanels);
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

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Address(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (BackColor != Color.Empty)
                dgAddresses.BackgroundColor = BackColor;            
        }

        #endregion
    }
}