using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmCorrectionFactor : Form
{
    #region FormProperties

    private CorrectionFactorController _controller;

    #endregion

    #region InitForm

    public frmCorrectionFactor()
    {
        _controller = new CorrectionFactorController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
        setComboEnergyTypes();
    }

    private async void setComboEnergyTypes()
    {
        var energyTypes = (await _controller.UnitOfWork.EnergyTypeRepo.GetAll()).ToList();
        cboEnergyType.DataSource = energyTypes;
        cboEnergyType.DisplayMember = "Name";
        cboEnergyType.ValueMember = "Id";

        cboEnergyType.SelectedIndex = -1;
    }

    private void LoadCorrectionFactors(long energyTypeId)
    {
        _controller.UnitOfWork.CorrectionFactors = _controller.UnitOfWork.CorrectionFactorRepo.SelectByEnergyType(energyTypeId).ToList();
        bsCorrectionFactors.DataSource = _controller.UnitOfWork.CorrectionFactors;
    }

    #endregion

    #region Events

    private void frmCorrectionFactor_Load(object sender, EventArgs e)
    {
        LoadCorrectionFactors(getCurrentEnergyType().Id);
    }

    private void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCorrectionFactors(getCurrentEnergyType().Id);
    }

    private void frmCorrectionFactor_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = dgCorrectionFactor.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    #endregion

    #region Toolbar

    private void tsbAdd_Click(object sender, EventArgs e)
    {
        addCorrectionFactor();
    }

    private void tsbSave_Click(object sender, EventArgs e)
    {
        setCorrectionFactor();
    }

    private void tbsCancel_Click(object sender, EventArgs e)
    {
        cancelCorrectionFactor();
    }

    private void tsbDelete_Click(object sender, EventArgs e)
    {
        deleteCorrectionFactor();
    }

    private void tsbRefresh_Click(object sender, EventArgs e)
    {
        LoadCorrectionFactors(getCurrentEnergyType().Id);
    }

    private void tsbClose_Click(object sender, EventArgs e)
    {
        closeCorrectionFactorForm();
    }

    #endregion

    #region Methods

    private void addCorrectionFactor()
    {
        var entity = _controller.UnitOfWork.AddDefaultEntity(getCurrentEnergyType().Id);
       
        bsCorrectionFactors.DataSource = _controller.UnitOfWork.CorrectionFactors;
        bsCorrectionFactors.ResetBindings(false);

        bsCorrectionFactors.Position = _controller.UnitOfWork.GetPosition(entity);
    }

    private void setCorrectionFactor()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        dgCorrectionFactor.Focus();

        _controller.UnitOfWork.Complete();
    }

    private void cancelCorrectionFactor()
    {
        _controller.UnitOfWork.CancelChanges();
        LoadCorrectionFactors(getCurrentEnergyType().Id);
    }

    private void deleteCorrectionFactor()
    {
        var entity = (EnergyUse.Models.CorrectionFactor)bsCorrectionFactors.Current;
        _controller.UnitOfWork.Delete(entity);
        bsCorrectionFactors.DataSource = _controller.UnitOfWork.CorrectionFactors;
        bsCorrectionFactors.ResetBindings(false);
    }

    private void closeCorrectionFactorForm()
    {
        Close();
    }

    private EnergyUse.Models.EnergyType getCurrentEnergyType()
    {
        EnergyUse.Models.EnergyType energyType;

        if (cboEnergyType.SelectedIndex != -1)
            energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
        else
            energyType = new EnergyUse.Models.EnergyType();

        return energyType;
    }

    private void setBaseFormSettings()
    {
        _controller.UnitOfWork = new EnergyUse.Core.UnitOfWork.CorrectionFactor(Managers.Config.GetDbFileName());

        Managers.Settings.SetBaseFormSettings(this);
        if (this.BackColor != Color.Empty)
            dgCorrectionFactor.BackgroundColor = this.BackColor;            
    }

    #endregion
}