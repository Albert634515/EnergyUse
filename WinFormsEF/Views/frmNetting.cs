using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmNetting : Form
{
    #region FormProperties

    private NettingController _controller;

    #endregion

    #region InitForm

    public frmNetting()
    {
        InitializeComponent();
        initializeForm();

    }

    private void initializeForm()
    {
        _controller = new NettingController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        _controller.InitSettings = true;

        SetBaseFormSettings();
        setComboEnergyTypes();

        _controller.InitSettings = false;
    }

    private void setComboEnergyTypes()
    {
        var energyTypes = _controller.UnitOfWork.EnergyTypeRepo.GetAll().ToList();
        cboEnergyType.DataSource = energyTypes;
        cboEnergyType.DisplayMember = "Name";
        cboEnergyType.ValueMember = "Id";

        var setting = _controller.GetSetting(cboEnergyType.Tag.ToString());
        Managers.Settings.SetSettingCombo(cboEnergyType, setting, "");

        if (cboEnergyType.SelectedIndex != -1)
            RefreshNetting();
    }

    #endregion

    #region Events

    private void frmNetting_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = dgNetting.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    #endregion

    #region Toolbar

    private void tsbAddNetting_Click(object sender, EventArgs e)
    {
        AddNetting();
    }

    private void tsbSave_Click(object sender, EventArgs e)
    {
        SaveNetting();
    }

    private void tbsCancel_Click(object sender, EventArgs e)
    {
        CancelNetting();
    }

    private void tsbDeleteNetting_Click(object sender, EventArgs e)
    {
        DeleteNetting();
    }

    private void tsbRefreshNetting_Click(object sender, EventArgs e)
    {
        RefreshNetting();
    }

    private void tsbCloseNetting_Click(object sender, EventArgs e)
    {
        CloseNetting();
    }

    #endregion

    #region Events

    private void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_controller.InitSettings == false)
        {
            RefreshNetting();

            var settingValue = Managers.Settings.GetSetting((ComboBox)sender);
            _controller.SaveSetting(settingValue.Item1, settingValue.Item2);
        }
    }

    #endregion

    #region Methods

    private void AddNetting()
    {
        if (!validateInput())
            return;

        var energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
        var entity = _controller.UnitOfWork.AddDefaultEntity(energyType.Id);

        bsNetting.DataSource = _controller.UnitOfWork.Nettings;
        bsNetting.Position = _controller.UnitOfWork.GetPosition(entity);
    }

    private void SaveNetting()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        dgNetting.Focus();

        _controller.UnitOfWork.Complete();
    }

    private void CancelNetting()
    {
        _controller.UnitOfWork.CancelChanges();
        RefreshNetting();
    }

    private void DeleteNetting()
    {
        EnergyUse.Models.Netting entity = (EnergyUse.Models.Netting)bsNetting.Current;
        _controller.UnitOfWork.Delete(entity);
        bsNetting.DataSource = _controller.UnitOfWork.Nettings;
        bsNetting.ResetBindings(false);
    }

    private void RefreshNetting()
    {
        if (!validateInput())
            return;

        var energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
        InitNetting(energyType.Id);
    }

    private void CloseNetting()
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

        return true;
    }

    private void InitNetting(long energyTypeId)
    {
        if (energyTypeId == 0)
            _controller.UnitOfWork.Nettings = new List<EnergyUse.Models.Netting>();
        else
            _controller.UnitOfWork.Nettings = _controller.UnitOfWork.NettingRepo.SelectByEnergyType(energyTypeId).ToList();

        _controller.UnitOfWork.SetListSorted();

        bsNetting.DataSource = _controller.UnitOfWork.Nettings;
        bsNetting.ResetBindings(false);
    }

    private void SetBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
        if (this.BackColor != Color.Empty)
            dgNetting.BackgroundColor = this.BackColor;
    }

    #endregion
}