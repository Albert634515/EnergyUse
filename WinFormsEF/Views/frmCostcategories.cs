using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views
{
    public partial class frmCostcategories : Form
    {
        #region FormProperties

        private CostcategoriesController _controller;

        #endregion

        #region InitForm

        public frmCostcategories()
        {
            _controller = new CostcategoriesController(Managers.Config.GetDbFileName());
            _controller.Initialize();

            InitializeComponent();
            setBaseFormSettings();

            LoadComboData();
        }

        private void LoadComboData()
        {
            bsEnergyTypes.DataSource = _controller.UnitOfWork.EnergyTypeRepo.GetAll();
            bsCalculationType.DataSource = _controller.UnitOfWork.CalculationTypeRepo.GetAll();
            bsEnergySubType.DataSource = _controller.UnitOfWork.EnergySubTypeRepo.GetAll();
            bsTariffGroups.DataSource = _controller.UnitOfWork.TariffGroupRepo.GetAll();
            bsUnits.DataSource = _controller.UnitOfWork.UnitRepo.GetAll();

            cboCalculationType.SelectedIndex = -1;
            cboUnit.SelectedIndex = -1;
            cboEnergySubType.SelectedIndex = -1;
            cboTariffGroup.SelectedIndex = -1;
            cboEnergyType.SelectedIndex = -1;
        }

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

            if (_controller.UnitOfWork.HasChanges())
                e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
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
            var entity = _controller.UnitOfWork.AddDefaultEntity(Managers.Languages.GetResourceString("Newcategory", "New category"), currentEnergyType.Id);

            bsCostCategories.DataSource = _controller.UnitOfWork.CostCategories;
            bsCostCategories.ResetBindings(false);

            bsCostCategories.Position = _controller.UnitOfWork.GetPosition(entity);
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

            _controller.UnitOfWork.Complete();
        }

        private void cancelCategories()
        {
            _controller.UnitOfWork.CancelChanges();
            refreshCostCategory();
        }

        private void deleteCategory()
        {
            var entity = (EnergyUse.Models.CostCategory)bsCostCategories.Current;
            _controller.UnitOfWork.Delete(entity);
            bsCostCategories.DataSource = _controller.UnitOfWork.CostCategories;
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
            _controller.UnitOfWork.CostCategories = new List<EnergyUse.Models.CostCategory>();

            if (energyTypeId > 0)
                _controller.UnitOfWork.CostCategories = _controller.UnitOfWork.CostCategoryRepo.SelectByEnergyTypeId(energyTypeId).ToList();

            bsCostCategories.DataSource = _controller.UnitOfWork.CostCategories;
            bsCostCategories.ResetBindings(false);
        }

        private void setBaseFormSettings()
        {
            _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.CostCategory(Managers.Config.GetDbFileName());

            Managers.Settings.SetBaseFormSettings(this);
            if (BackColor != Color.Empty)
                dgCostCategories.BackgroundColor = BackColor;
        }

        #endregion
    }
}