using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmTariffGroups : Form
{
    #region FormProperties

    private TariffGroupController _controller;

    #endregion

    #region InitForm

    public frmTariffGroups()
    {
        _controller = new TariffGroupController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
        setTariffGroupTypeCombo("");
        setTarifGroups();
    }

    private void setTariffGroupTypeCombo(string currentValue)
    {
        var items = WinFormsEF.Managers.SelectionItemList.GetTariffGroupTypeList();
        bsTariffGroupTypes.DataSource = items;

        if (currentValue == null)
            currentValue = ((int)EnergyUse.Common.Enums.TariffGroupType.EnergyCosts).ToString();

        typeComboBox.SelectedIndex = typeComboBox.FindString(currentValue);
    }

    #endregion

    #region Events

    private void FrmTarifGroups_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = DgTarifGroups.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    #endregion

    #region Toolbar

    private void TsbAdd_Click(object sender, EventArgs e)
    {
        addTarifGroup();
    }

    private void TsbSave_Click(object sender, EventArgs e)
    {
        setTarifGroup();
    }

    private void TbsCancel_Click(object sender, EventArgs e)
    {
        cancelTarifGroup();
    }

    private void TsbDelete_Click(object sender, EventArgs e)
    {
        deleteTarifGroup();
    }

    private void TsbRefresh_Click(object sender, EventArgs e)
    {
        setTarifGroups();
    }

    private void TsbClose_Click(object sender, EventArgs e)
    {
        closeTarifGroup();
    }

    #endregion

    #region Methods

    private void setTarifGroups()
    {
        _controller.UnitOfWork.TariffGroups = _controller.UnitOfWork.TariffGroupRepo.GetAll().ToList();

        BsTarifGroups.DataSource = _controller.UnitOfWork.TariffGroups;
        BsTarifGroups.ResetBindings(false);
    }

    private void addTarifGroup()
    {
        var entity = _controller.UnitOfWork.SetDefaultEntity(Managers.Languages.GetResourceString("TarifGroupNewGroup", "New group"));

        BsTarifGroups.DataSource = _controller.UnitOfWork.TariffGroups;
        BsTarifGroups.ResetBindings(false);

        BsTarifGroups.Position = _controller.UnitOfWork.GetPosition(entity);
    }

    private void cancelTarifGroup()
    {
        _controller.UnitOfWork.CancelChanges();
        setTarifGroups();
    }

    private void setTarifGroup()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        DgTarifGroups.Focus();

        _controller.UnitOfWork.Complete();
    }

    private void deleteTarifGroup()
    {
        if (BsTarifGroups.Current != null)
        {
            var message = Managers.Languages.GetResourceString("TariffGroupsAskDelete", "Are you sure you want to delete this tarif group?");
            var message2 = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");
            if (MessageBox.Show(message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var entity = (EnergyUse.Models.TariffGroup)BsTarifGroups.Current;
                _controller.UnitOfWork.Delete(entity);

                BsTarifGroups.DataSource = _controller.UnitOfWork.TariffGroups;
                BsTarifGroups.ResetBindings(false);
            }
        }
    }

    private void closeTarifGroup()
    {
        Close();
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
        if (BackColor != Color.Empty)
            DgTarifGroups.BackgroundColor = BackColor;
    }

    #endregion
}