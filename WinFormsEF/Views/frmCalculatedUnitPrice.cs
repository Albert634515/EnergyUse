using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class frmCalculatedUnitPrice : Form
{
    #region FormProperties

    private CalculatedUnitPriceController _controller;

    #endregion

    #region InitForm

    public frmCalculatedUnitPrice()
    {
        _controller = new CalculatedUnitPriceController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();
        Shown += frmCalculatedUnitPrice_Shown;
    }

    private async void frmCalculatedUnitPrice_Shown(object sender, EventArgs e)
    {
        await setComboEnergyTypesAsync();
        await setComboTarifGroupsAsync();
    }

    private async Task setComboEnergyTypesAsync()
    {
        var energyTypes = (await _controller.UnitOfWork.EnergyTypeRepo.GetAll()).ToList();
        bsEnergyTypes.DataSource = energyTypes;

        cboEnergyType.SelectedIndex = -1;
    }

    private async Task setComboTarifGroupsAsync()
    {
        var tarifGroups = (await _controller.UnitOfWork.TarifGroupRepo.GetAll()).ToList();
        bsTarifGroups.DataSource = tarifGroups;

        cboTarifGroup.SelectedIndex = -1;
    }

    #endregion

    #region Events

    private void frmCalculatedUnitPrice_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = dgCalculatedUnitPrice.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    private async void cboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        await initCalculatedUnitPriceAsync();
    }

    private async void cboTarifGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        await initCalculatedUnitPriceAsync();
    }

    #endregion

    #region Toolbar

    private async void tsbAdd_Click(object sender, EventArgs e)
    {
        await addCalculatedUnitPriceAsync();
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

    private async void tsbRefresh_Click(object sender, EventArgs e)
    {
        await refreshCalculatedUnitPriceAsync();
    }

    private void tsbClose_Click(object sender, EventArgs e)
    {
        closeCalculatedUnitPrice();
    }

    private async Task initCalculatedUnitPriceAsync()
    {
        var energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
        var tarifGroup = (EnergyUse.Models.TariffGroup)cboTarifGroup.SelectedItem;

        _controller.UnitOfWork.CalculatedUnitPrices = new List<EnergyUse.Models.CalculatedUnitPrice>();

        if (energyType != null && tarifGroup != null)
            _controller.UnitOfWork.CalculatedUnitPrices = (await _controller.UnitOfWork.CalculatedUnitPriceRepo.SelectByEnergyTypeAndTarifGroup(energyType.Id, tarifGroup.Id)).ToList();

        _controller.UnitOfWork.SetListSorted();
        bsCalculatedUnitPrice.DataSource = _controller.UnitOfWork.CalculatedUnitPrices;
    }

    private async Task addCalculatedUnitPriceAsync()
    {
        if (!validateInput())
            return;

        EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)cboEnergyType.SelectedItem;
        EnergyUse.Models.TariffGroup tariffGroup = (EnergyUse.Models.TariffGroup)cboTarifGroup.SelectedItem;

        var entity = await _controller.UnitOfWork.AddDefaultEntity(energyType.Id, tariffGroup.Id);

        bsCalculatedUnitPrice.DataSource = _controller.UnitOfWork.CalculatedUnitPrices;
        bsCalculatedUnitPrice.ResetBindings(false);

        bsCalculatedUnitPrice.Position = _controller.UnitOfWork.GetPosition(entity);
    }

    private void setCalculatedUnitPrice()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        dgCalculatedUnitPrice.Focus();

        _controller.UnitOfWork.Complete();
    }

    private void cancelCalculatedUnitPrice()
    {
        _controller.UnitOfWork.CancelChanges();
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
                _controller.UnitOfWork.Delete(entity);

                bsCalculatedUnitPrice.DataSource = _controller.UnitOfWork.CalculatedUnitPrices;
                bsCalculatedUnitPrice.ResetBindings(false);
            }
        }
    }

    private async Task refreshCalculatedUnitPriceAsync()
    {
        if (!validateInput())
            return;

        await initCalculatedUnitPriceAsync();
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
        Managers.Settings.SetBaseFormSettings(this);
        if (this.BackColor != Color.Empty)
            dgCalculatedUnitPrice.BackgroundColor = this.BackColor;
    }

    #endregion
}
