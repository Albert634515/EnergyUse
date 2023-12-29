namespace WinFormsEF.Views
{
    public partial class frmCalculatedUnitPrice : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.CalculatedUnitPrice _unitOfWork;

        #endregion

        #region InitForm

        public frmCalculatedUnitPrice()
        {
            InitializeComponent();
            setBaseFormSettings();
            LoadComboEnergyTypes();
            LoadComboTarifGroups();
        }

        private void LoadComboEnergyTypes()
        {
            var energyTypes = _unitOfWork.EnergyTypeRepo.GetAll().ToList();
            bsEnergyTypes.DataSource = energyTypes;

            cboEnergyType.SelectedIndex = -1;
        }

        private void LoadComboTarifGroups()
        {
            var tarifGroups = _unitOfWork.TarifGroupRepo.GetAll().ToList();
            bsTarifGroups.DataSource = tarifGroups;

            cboTarifGroup.SelectedIndex = -1;
        }

        #endregion

        #region Events

        private void frmCalculatedUnitPrice_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = dgCalculatedUnitPrice.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = Managers.General.WarningUnsavedChanges(this);
        }

        private void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            initCalculatedUnitPrice();
        }

        private void cboTarifGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            initCalculatedUnitPrice();
        }

        #endregion

        #region Toolbar

        private void tsbAdd_Click(object sender, EventArgs e)
        {
            addCalculatedUnitPrice();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            setCalculatedUnitPrice();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            cancelCalculatedUnitPrice();
        }

        private void tsbDelete_Click(object sender, EventArgs e)
        {
            deleteCalculatedUnitPrice();
        }

        private void tsbRefresh_Click(object sender, EventArgs e)
        {
            refreshCalculatedUnitPrice();
        }

        private void tsbClose_Click(object sender, EventArgs e)
        {
            closeCalculatedUnitPrice();
        }

        #endregion

        #region Methods

        private void initCalculatedUnitPrice()
        {
            var energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
            var tarifGroup = (EnergyUse.Models.TariffGroup)cboTarifGroup.SelectedItem;

            _unitOfWork.CalculatedUnitPrices = new List<EnergyUse.Models.CalculatedUnitPrice>();

           if (energyType != null && tarifGroup != null)
                _unitOfWork.CalculatedUnitPrices = _unitOfWork.CalculatedUnitPriceRepo.SelectByEnergyTypeAndTarifGroup(energyType.Id, tarifGroup.Id).ToList();

            _unitOfWork.SetListSorted();
            bsCalculatedUnitPrice.DataSource = _unitOfWork.CalculatedUnitPrices;
        }

        private void addCalculatedUnitPrice()
        {
            if (!validateInput())
                return;

            EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
            EnergyUse.Models.TariffGroup tariffGroup = (EnergyUse.Models.TariffGroup)cboTarifGroup.SelectedItem;

            var entity = _unitOfWork.AddDefaultEntity(energyType.Id, tariffGroup.Id);

            bsCalculatedUnitPrice.DataSource = _unitOfWork.CalculatedUnitPrices;
            bsCalculatedUnitPrice.ResetBindings(false);

            bsCalculatedUnitPrice.Position = _unitOfWork.GetPosition(entity);
        }

        private void setCalculatedUnitPrice()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            dgCalculatedUnitPrice.Focus();

            _unitOfWork.Complete();
        }

        private void cancelCalculatedUnitPrice()
        {
            _unitOfWork.CancelChanges();
        }

        private void deleteCalculatedUnitPrice()
        {
            if (bsCalculatedUnitPrice.Current != null)
            {
                var message = Managers.Languages.GetResourceString("CalculatedUnitPriceAskDelete", "Are you sure you want to delete this unit price?");
                var message2 = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");
                if (MessageBox.Show(message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    var entity = (EnergyUse.Models.CalculatedUnitPrice)bsCalculatedUnitPrice.Current;
                    _unitOfWork.Delete(entity);

                    bsCalculatedUnitPrice.DataSource = _unitOfWork.CalculatedUnitPrices;
                    bsCalculatedUnitPrice.ResetBindings(false);
                }
            }
        }

        private void refreshCalculatedUnitPrice()
        {
            if (!validateInput())
                return;

            initCalculatedUnitPrice();
        }

        private void closeCalculatedUnitPrice()
        {
            Close();
        }

        private bool validateInput()
        {
            if (cboEnergyType.SelectedIndex == -1)
            {
                var message = Managers.Languages.GetResourceString("SelectEnergyType", "Select an energy type");
                MessageBox.Show(this, message);
                cboEnergyType.Focus();
                return false;
            }

            if (cboTarifGroup.SelectedIndex == -1)
            {
                var message = Managers.Languages.GetResourceString("SelectTarifGroup", "Select a tarif group");
                MessageBox.Show(this, message);
                cboTarifGroup.Focus();
                return false;
            }

            return true;
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.CalculatedUnitPrice(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (this.BackColor != Color.Empty)
                dgCalculatedUnitPrice.BackgroundColor = this.BackColor;
        }

        #endregion
    }
}