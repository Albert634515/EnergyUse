using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmVatTariffs : Form
{
    #region FormProperties

    private VatTariffController _controller;

    #endregion

    #region InitForm

    public frmVatTariffs()
    {
        InitializeComponent();
        initializeForm();
    }

    private void initializeForm()
    {
        _controller = new VatTariffController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        _controller.InitSettings = true;

        setBaseFormSettings();
        setComboEnergyTypes();

        _controller.InitSettings = false;
    }

    private async void setComboEnergyTypes()
    {
        var energyTypes = (await _controller.UnitOfWork.EnergyTypeRepo.GetAll()).ToList();
        bsEnergyTypes.DataSource = energyTypes;

        CboEnergyType.SelectedIndex = -1;
    }

    private void LoadCostCategories(long eneryTypeId)
    {
        var costCategories = _controller.UnitOfWork.CostCategoryRepo.SelectByEnergyTypeIdVatCalculated(eneryTypeId).ToList();
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

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
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
        var entity = _controller.UnitOfWork.AddDefaultEntity(costCategory.Id);

        bsVatTarif.DataSource = _controller.UnitOfWork.VatTarifs;

        bsVatTarif.Position = _controller.UnitOfWork.GetPosition(entity);

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

        _controller.UnitOfWork.Complete();
        loadVatTarifs();
    }

    private void cancelVatTarif()
    {
        _controller.UnitOfWork.CancelChanges();
    }

    private void deleteVatTarif()
    {
        var entity = (EnergyUse.Models.VatTarif)bsVatTarif.Current;
        _controller.UnitOfWork.Delete(entity);

        bsVatTarif.DataSource = _controller.UnitOfWork.VatTarifs;
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

    private async void loadVatTarifs()
    {
        _controller.UnitOfWork.VatTarifs = new List<EnergyUse.Models.VatTarif>();
        var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;

        if (costCategory != null)
            _controller.UnitOfWork.VatTarifs = await _controller.UnitOfWork.VatTarifRepo.GetByCostCategoryId(costCategory.Id);

        _controller.UnitOfWork.SetListSorted();
        bsVatTarif.DataSource = _controller.UnitOfWork.VatTarifs;
    }

    private void closeVatTarifs()
    {
        Close();
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
        if (BackColor != Color.Empty)
            DgVatTarifs.BackgroundColor = BackColor;
    }

    #endregion
}