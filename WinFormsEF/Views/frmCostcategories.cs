namespace WinFormsEF.Views
{
    public partial class frmCostcategories : Form
    {
        #region FormProperties

        private EnergyUse.Core.UnitOfWork.CostCategory _unitOfWork;

        #endregion

        #region InitForm

        public frmCostcategories()
        {
            InitializeComponent();
            setBaseFormSettings();

            LoadComboData();
        }

        private void LoadComboData()
        {
            bsEnergyTypes.DataSource = _unitOfWork.EnergyTypeRepo.GetAll();
            bsCalculationType.DataSource = _unitOfWork.CalculationTypeRepo.GetAll();
            bsEnergySubType.DataSource = _unitOfWork.EnergySubTypeRepo.GetAll();
            bsTariffGroups.DataSource = _unitOfWork.TariffGroupRepo.GetAll();
            bsUnits.DataSource = _unitOfWork.UnitRepo.GetAll();

            cboCalculationType.SelectedIndex = -1;
            cboUnit.SelectedIndex = -1;
            cboEnergySubType.SelectedIndex = -1;
            cboTariffGroup.SelectedIndex = -1;
            cboEnergyType.SelectedIndex = -1;
        }

        //private void SetCurrentCostCategory(int energyTypeId, long costCategoryId)
        //{
        //    if (costCategoryId == 0)
        //        _unitOfWork.CostCategories.Add(_unitOfWork.CostCategoryRepo.GetDefault(energyTypeId));
        //    else
        //        _unitOfWork.CostCategories.Add(_unitOfWork.CostCategoryRepo.Get(costCategoryId));

        //    bsCostCategories.DataSource = _unitOfWork.CostCategories;
        //}

        #endregion

        #region Events

        private void frmCostcategories_Load(object sender, EventArgs e)
        {

        }

        private void bsCostCategories_CurrentChanged(object sender, EventArgs e)
        {
            if (bsCostCategories.Current != null)
            {
                var currentCategory = (EnergyUse.Models.CostCategory)bsCostCategories.Current;
                txtColor.Tag = $"CostCategory{currentCategory.Id}";
                Managers.Settings.LoadColorSetting(txtColor);
            }
        }

        private void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboEnergyType.SelectedIndex != -1)
            {
                EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
                initCostCategories((int)energyType.Id);
            }
        }

        private void frmCostcategories_FormClosing(object sender, FormClosingEventArgs e)
        {
            _ = dgCostCategories.Focus();

            if (_unitOfWork.HasChanges())
                e.Cancel = Managers.General.WarningUnsavedChanges(this);
        }

        #endregion

        #region Toolbar

        private void tsbAddCostCategory_Click(object sender, EventArgs e)
        {
            if (validateAdd())
                addCostCategory();
        }

        private void tsbSave_Click(object sender, EventArgs e)
        {
            saveCategories();
        }

        private void tbsCancel_Click(object sender, EventArgs e)
        {
            cancelCategories();
        }

        private void tsbDeleteCostCategory_Click(object sender, EventArgs e)
        {
            deleteCategory();
        }

        private void tsbRefreshCostCategory_Click(object sender, EventArgs e)
        {
            refreshCostCategory();
        }

        private void tsbCloseCostCategory_Click(object sender, EventArgs e)
        {
            closeCostCategory();
        }

        #endregion

        #region Methods

        private bool validateInput()
        {

            return true;
        }

        private bool validateAdd()
        {
            var currentEnergyType = (EnergyUse.Models.EnergyType)bsEnergyTypes.Current;
            if (currentEnergyType == null)
            {
                var message = Managers.Languages.GetResourceString("SelectEnergyType", "Select an energy type");
                MessageBox.Show(this, message);
                return false;
            }

            return true;
        }

        private void addCostCategory()
        {
            var currentEnergyType = (EnergyUse.Models.EnergyType)bsEnergyTypes.Current;
            var entity = _unitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("Newcategory", "New category"), currentEnergyType.Id);

            bsCostCategories.DataSource = _unitOfWork.CostCategories;
            bsCostCategories.ResetBindings(false);

            bsCostCategories.Position = _unitOfWork.GetPosition(entity);
        }

        private void saveCategories()
        {
            // Set focus on grid to force valdition and update of bindingsource form interfaces
            dgCostCategories.Focus();

            if (!validateInput())
            {
                return;
            }

            if (bsCostCategories.Current != null)
            {
                var currentCategory = (EnergyUse.Models.CostCategory)bsCostCategories.Current;
                var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
                if (currentCategory.Id > 0)
                    libSettings.SaveColorSetting(txtColor.Tag.ToString(), txtColor.BackColor);
            }

            _unitOfWork.Complete();
        }

        private void cancelCategories()
        {
            _unitOfWork.CancelChanges();
            refreshCostCategory();
        }

        private void deleteCategory()
        {
            var entity = (EnergyUse.Models.CostCategory)bsCostCategories.Current;
            _unitOfWork.Delete(entity);
            bsCostCategories.DataSource = _unitOfWork.CostCategories;
            bsEnergyTypes.ResetBindings(false);
        }

        private void refreshCostCategory()
        {
            if (!validateInput())
                return;

            EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
            initCostCategories((int)energyType.Id);
        }

        private void closeCostCategory()
        {
            Close();
        }

        private void initCostCategories(int energyTypeId)
        {
            _unitOfWork.CostCategories = new List<EnergyUse.Models.CostCategory>();

            if (energyTypeId > 0)
                _unitOfWork.CostCategories = _unitOfWork.CostCategoryRepo.SelectByEnergyTypeId(energyTypeId).ToList();

            bsCostCategories.DataSource = _unitOfWork.CostCategories;
            bsCostCategories.ResetBindings(false);
        }

        private void setBaseFormSettings()
        {
            _unitOfWork = new EnergyUse.Core.UnitOfWork.CostCategory(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (BackColor != Color.Empty)
                dgCostCategories.BackgroundColor = BackColor;
        }

        #endregion
    }
}