namespace WinFormsEF.Views
{
    public partial class frmVatTariffs : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.VatTarif _unitOfWork;

        #endregion

        #region InitForm

        public frmVatTariffs()
        {
            InitializeComponent();
            setBaseFormSettings();
            LoadComboEnergyTypes();
        }

        private void LoadComboEnergyTypes()
        {
            var energyTypes = _unitOfWork.EnergyTypeRepo.GetAll().ToList();
            bsEnergyTypes.DataSource = energyTypes;

            CboEnergyType.SelectedIndex = -1;
        }

        private void LoadCostCategories(long eneryTypeId)
        {
            var costCategories = _unitOfWork.CostCategoryRepo.SelectByEnergyTypeIdVatCalculated(eneryTypeId).ToList();
            bsCostCategories.DataSource = costCategories;
            CboCostCategory.DataSource = costCategories;

            CboCostCategory.SelectedIndex = -1;
        }

        #endregion

        #region Events

        private void CboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CboEnergyType.SelectedIndex != -1)
            {
                var energyType = (EnergyUse.Models.EnergyType)CboEnergyType.SelectedItem;
                LoadCostCategories(energyType.Id);
            }
            else
                LoadCostCategories(0);

            loadVatTarifs();
        }

        private void CboCostCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadVatTarifs();
        }

        private void FrmVatTarifs_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = DgVatTarifs.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = e.Cancel = Managers.General.WarningUnsavedChanges(this);
        }

        #endregion

        #region Toolbar

        private void TsbAddVatTarif_Click(object sender, EventArgs e)
        {
            addVatTarif();
        }

        private void TsbSaveVatTarif_Click(object sender, EventArgs e)
        {
            setVatTarif();
        }

        private void TbsCancel_Click(object sender, EventArgs e)
        {
            cancelVatTarif();
           loadVatTarifs();
        }

        private void TsbDeleteVatTarif_Click(object sender, EventArgs e)
        {
            deleteVatTarif();
        }

        private void TsbRefreshVatTarif_Click(object sender, EventArgs e)
        {
            loadVatTarifs();
        }

        private void TsbCloseVatTarif_Click(object sender, EventArgs e)
        {
            closeVatTarifs();
        }

        #endregion

        #region Methods

        private void addVatTarif()
        {
            if (!validateInput())
                return;

            var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;
            var entity = _unitOfWork.AddDefaultEntity(costCategory.Id);

            bsVatTarif.DataSource = _unitOfWork.VatTarifs;

            bsVatTarif.Position = _unitOfWork.GetPosition(entity);

            DtStartDate.Focus();
        }

        private void setVatTarif()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            DgVatTarifs.Focus();

            var currentVatTarif = (EnergyUse.Models.VatTarif)bsVatTarif.Current;
            currentVatTarif.StartDate = currentVatTarif.StartDate.Date;
            currentVatTarif.EndDate = currentVatTarif.EndDate.Date;

            if (!validateInput())
            {
                return;
            }

            _unitOfWork.Complete();
            loadVatTarifs();
        }

        private void cancelVatTarif()
        {
            _unitOfWork.CancelChanges();
        }

        private void deleteVatTarif()
        {
            var entity = (EnergyUse.Models.VatTarif)bsVatTarif.Current;
            _unitOfWork.Delete(entity);

            bsVatTarif.DataSource = _unitOfWork.VatTarifs;
            bsVatTarif.ResetBindings(false);
        }

        private bool validateInput()
        {
            var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;
            if (costCategory == null)
            {
                var message = Managers.Languages.GetResourceString("SelectCategory", "First select a category");
                MessageBox.Show(this, message);
                return false;
            }

            return true;
        }

        private void loadVatTarifs()
        {
            _unitOfWork.VatTarifs = new List<EnergyUse.Models.VatTarif>();
            var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;

            if (costCategory != null)
                _unitOfWork.VatTarifs = _unitOfWork.VatTarifRepo.GetByCostCategoryId(costCategory.Id).ToList();

            _unitOfWork.SetListSorted();
            bsVatTarif.DataSource = _unitOfWork.VatTarifs;
        }

        private void closeVatTarifs()
        {
            Close();
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.VatTarif(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (BackColor != Color.Empty)
                DgVatTarifs.BackgroundColor = BackColor;
        }

        #endregion
    }
}