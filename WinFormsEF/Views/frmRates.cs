using EnergyUse.Common.Enums;
using EnergyUse.Core.Controllers;

namespace WinFormsEF.Views;

public partial class FrmRates : Form
{
    #region FormProperties

    private RateController _controller;

    #endregion

    #region InitForm

    public FrmRates()
    {
        InitializeComponent();
        initializeForm();
    }

    private void initializeForm()
    {
        _controller = new RateController(Managers.Config.GetDbFileName());
        _controller.Initialize();
        CboCostCategory.DropDown += (_, _) =>
        {
            var groupId = (CboTarifGroup.SelectedItem as EnergyUse.Models.TariffGroup)?.Id ?? 0;
            var energyId = (CboEnergyType.SelectedItem as EnergyUse.Models.EnergyType)?.Id ?? 0;
            var allowed = _controller.UnitOfWork.CostCategoryRepo.SelectByTariffGroup(groupId, energyId).GetAwaiter().GetResult();
            if (allowed.Select(category => category.Id).SequenceEqual(
                CboCostCategory.Items.Cast<EnergyUse.Models.CostCategory>().Select(category => category.Id)))
                return;
            var categoryId = (CboCostCategory.SelectedItem as EnergyUse.Models.CostCategory)?.Id;
            setCostCategories(energyId);
            if (categoryId.HasValue)
                CboCostCategory.SelectedValue = categoryId.Value;
        };
        DgRates.DataBindingComplete += (_, _) => colourRateValidationRows();

        setBaseFormSettings();
        setComboEnergyTypes();
        setComboTarifGroups();
        CboTarifGroup.Visible = true;
        LblTarifGroup.Visible = true;
        setComboRateTypes();
    }

    private void FrmRates_Load(object sender, EventArgs e)
    {
        initRates();
    }

    private void setCostCategories(long eneryTypeId)
    {
        var groupId = (CboTarifGroup.SelectedItem as EnergyUse.Models.TariffGroup)?.Id ?? 0;
        var costCategories = _controller.UnitOfWork.CostCategoryRepo.SelectByTariffGroup(groupId, eneryTypeId).GetAwaiter().GetResult();
        bsCostCategories.DataSource = costCategories;
        CboCostCategory.DataSource = costCategories;

        CboCostCategory.SelectedIndex = -1;
    }

    private async void setComboEnergyTypes()
    {
        var energyTypes = (await _controller.UnitOfWork.EnergyTypeRepo.GetAll()).ToList();
        bsEnergyTypes.DataSource = energyTypes;

        CboEnergyType.SelectedIndex = -1;
    }

    private void setComboTarifGroups()
    {
        var tarifGroups = _controller.UnitOfWork.TarifGroupRepo.GetAll().GetAwaiter().GetResult().ToList();

        bsTarifGroups.DataSource = tarifGroups;

        CboTarifGroup.SelectedIndex = -1;
    }

    private void setComboRateTypes()
    {
        var rateTypes = WinFormsEF.Managers.SelectionItemList.GetRateTypeList();
        bsRateType.DataSource = rateTypes;

        CboRateType.SelectedIndex = -1;
    }

    #endregion

    #region Events

    private void bsRates_CurrentChanged(object sender, EventArgs e)
    {
        setRateIncExLabel();
    }

    private void CboEnergyType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboEnergyType.SelectedIndex != -1)
        {
            var energyType = (EnergyUse.Models.EnergyType)CboEnergyType.SelectedItem;
            setCostCategories(energyType.Id);
        }
        else
            setCostCategories(0);

        initAdditionalCategoryAndGroupInfo();
        initRates();
    }

    private void CboCostCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (CboCostCategory.SelectedIndex != -1 && CboEnergyType.SelectedIndex != -1)
        {
            var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;

            LblTarifGroup.Visible = true;
            CboTarifGroup.Visible = true;

            initAdditionalCategoryAndGroupInfo();
            initRates();
            AlwaysCalculatedWithLabel.Visible = false;
        }
    }

    private void CboTarifGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        setCostCategories((CboEnergyType.SelectedItem as EnergyUse.Models.EnergyType)?.Id ?? 0);
        initAdditionalCategoryAndGroupInfo();
        initRates();
    }

    private void TxtDescription_Validating(object sender, System.ComponentModel.CancelEventArgs e)
    {
        setAdditionalInfo();
    }

    private void TxtRate_TextChanged(object sender, EventArgs e)
    {
        setRateIncExLabel();
    }

    private void FrmRates_FormClosing(object sender, FormClosingEventArgs e)
    {
        _ = DgRates.Focus();

        if (_controller.UnitOfWork.HasChanges())
            e.Cancel = Managers.GeneralDialogs.WarningUnsavedChanges(this);
    }

    private void CboRateType_SelectedIndexChanged(object sender, EventArgs e)
    {
        changeRateType();
    }

    #endregion

    #region Toolbar

    private void TsbAdd_Click(object sender, EventArgs e)
    {
        addRate();
    }

    private void TsbSave_Click(object sender, EventArgs e)
    {
        saveRate();
    }

    private void TbsCancel_Click(object sender, EventArgs e)
    {
        cancelRate();
    }

    private void TsbDelete_Click(object sender, EventArgs e)
    {
        deleteRate();
    }

    private void TsbRefresh_Click(object sender, EventArgs e)
    {
        refreshRates();
    }

    private void TbsClose_Click(object sender, EventArgs e)
    {
        closeRates();
    }

    #endregion

    #region Methods

    private async void setRateIncExLabel()
    {
        rateTaxInfoLabel.Text = "_";

        if (bsRates.Current == null)
            return;

        var currentRate = (EnergyUse.Models.Rate)bsRates.Current;
        var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;

        var rateTaxInfo = await _controller.GetRateIncExTax(costCategory, currentRate);
        if (rateTaxInfo != null)
        {
            if (costCategory.CalculateVat)
                rateTaxInfoLabel.Text = rateTaxInfo.RateIncTax.ToString();
        }
    }

    private void changeRateType()
    {
        var rateType = RateType.UnKnown;
        var rate = (EnergyUse.Models.Rate)bsRates.Current;

        if (CboRateType.SelectedIndex != -1)
            rateType = (RateType)CboRateType.SelectedValue;

        //if rate != staffel, check staffel exist and ask to delete
        if (rateType != RateType.Staffel && rate != null)
        {
            var staffelList = _controller.UnitOfWork.StaffelRepo.SelectByRateId(rate.Id).GetAwaiter().GetResult();
            if (staffelList != null && staffelList.Any())
            {
                if (MessageBox.Show("", "There are still staffel records for current rate, but type is no longer of type staffel. Do you want to delete the staffel records?", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    _controller.UnitOfWork.StaffelRepo.DeleteByRateId(rate.Id);
            }
        }

        setStaffelVisibility(rateType, rate is null ? 0 : rate.Id);
    }

    private void setAdditionalInfo()
    {
        var additionalCategoryAndGroupInfo = (EnergyUse.Models.AdditionalCategoryAndGroupInfo)bsAdditionalCategoryAndGroupInfo.Current;
        if (additionalCategoryAndGroupInfo != null)
        {
            if (additionalCategoryAndGroupInfo.Id == 0)
                _controller.UnitOfWork.AdditionalCategoryAndGroupInfoRepo.Add(additionalCategoryAndGroupInfo);
        }
    }

    private void initRates()
    {
        var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;
        var energyType = (EnergyUse.Models.EnergyType)CboEnergyType.SelectedItem;
        var tarifGroup = (EnergyUse.Models.TariffGroup)CboTarifGroup.SelectedItem;

        _controller.UnitOfWork.RateList = new List<EnergyUse.Models.Rate>();

        if (costCategory != null && tarifGroup != null && tarifGroup.Id > 0)
            _controller.UnitOfWork.RateList = _controller.UnitOfWork.RateRepo.SelectByCostCategoryAndEnergyTypeAndTarifGroup(costCategory.Id, energyType.Id, tarifGroup.Id).GetAwaiter().GetResult().ToList();

        _controller.UnitOfWork.SetListSorted();
        bsRates.DataSource = _controller.UnitOfWork.RateList;
        bsRates.ResetBindings(false);
    }

    private async void initAdditionalCategoryAndGroupInfo()
    {
        var costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;
        var energyType = (EnergyUse.Models.EnergyType)CboEnergyType.SelectedItem;
        var tarifGroup = (EnergyUse.Models.TariffGroup)CboTarifGroup.SelectedItem;

        if (energyType != null && energyType.Id > 0
            && costCategory != null && costCategory.Id > 0
            && tarifGroup != null && tarifGroup.Id > 0)
        {
            var entity = await _controller.UnitOfWork.AdditionalCategoryAndGroupInfoRepo.SelectByPrimaryKey(energyType.Id, costCategory.Id, tarifGroup.Id);
            if (entity == null)
            {
                entity = new EnergyUse.Models.AdditionalCategoryAndGroupInfo
                {
                    EnergyType = energyType,
                    CostCategory = costCategory,
                    TariffGroup = tarifGroup
                };
                _controller.UnitOfWork.AdditionalCategoryAndGroupInfoRepo.Add(entity);
            }

            bsAdditionalCategoryAndGroupInfo.DataSource = entity;
        }
    }

    /// <summary>
    /// Show/hide price field or staffel depending on rate type
    /// </summary>
    /// <param name="rateType">Rate type</param>
    /// <param name="rateId">Rate id</param>
    private void setStaffelVisibility(RateType rateType, long rateId)
    {
        var showStaffel = rateType == RateType.Staffel;

        // Show hide rate
        LblRate.Visible = !showStaffel;
        TxtRate.Visible = !showStaffel;
        //if (txtRate.Visible == false)
        //    txtRate.Text = null;

        // Show hide ucStaffel
        if (ucStaffel1 != null)
        {
            ucStaffel1.Visible = showStaffel;

            if (ucStaffel1.Visible)
                ucStaffel1.InitStaffels(rateId);
        }
    }

    private void addRate()
    {
        if (!validateInput())
            return;

        EnergyUse.Models.CostCategory costCategory = (EnergyUse.Models.CostCategory)CboCostCategory.SelectedItem;
        EnergyUse.Models.EnergyType energyType = (EnergyUse.Models.EnergyType)CboEnergyType.SelectedItem;
        var tarifGroup = getCurrentTarifGroup();

        EnergyUse.Models.Rate entity = _controller.UnitOfWork.AddDefaultEntity(energyType.Id, costCategory.Id, tarifGroup.Id).GetAwaiter().GetResult();

        bsRates.DataSource = _controller.UnitOfWork.RateList;
        bsRates.ResetBindings(false);

        bsRates.Position = _controller.UnitOfWork.GetPosition(entity);
    }

    private void saveRate()
    {
        // Set focus on grid to force valdition and update of bindingsource form interfaces
        DgRates.Focus();

        if (!Validate())
            return;

        bsRates.EndEdit();
        if (bsRates.Current is not EnergyUse.Models.Rate rate)
            return;

        if (!confirmRatesAreValid())
            return;

        rate.PriceChange = _controller.GetPriceChange(rate).GetAwaiter().GetResult();

        _controller.UnitOfWork.Complete();
    }

    private bool confirmRatesAreValid()
    {
        var errors = _controller.GetRateValidationErrorsAsync(_controller.UnitOfWork.RateList)
            .GetAwaiter().GetResult();
        colourRateValidationRows(errors);
        if (errors.Count == 0)
            return true;

        var details = string.Join(Environment.NewLine + Environment.NewLine, errors.Select(item =>
            $"Rate {item.Key.Id}: {item.Key.StartRate:dd-MM-yyyy} through {item.Key.EndRate:dd-MM-yyyy}{Environment.NewLine}{item.Value}"));
        if (errors.Values.Any(error => error.Contains("This cost category is not allowed")))
        {
            MessageBox.Show(details, "Rate validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        return MessageBox.Show(
            $"The red rows contain rate validation errors.{Environment.NewLine}{Environment.NewLine}" +
            $"{details}{Environment.NewLine}{Environment.NewLine}Do you want to save the rates anyway?",
            "Rate validation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
            MessageBoxDefaultButton.Button2) == DialogResult.Yes;
    }

    private void colourRateValidationRows(Dictionary<EnergyUse.Models.Rate, string> errors = null)
    {
        errors ??= RateController.ValidateRates(_controller.UnitOfWork.RateList);
        foreach (DataGridViewRow row in DgRates.Rows)
        {
            var error = row.DataBoundItem is EnergyUse.Models.Rate rate
                ? errors.GetValueOrDefault(rate, string.Empty)
                : string.Empty;
            var invalid = !string.IsNullOrWhiteSpace(error);
            row.DefaultCellStyle.BackColor = invalid ? Color.MistyRose : Color.Empty;
            row.DefaultCellStyle.ForeColor = invalid ? Color.DarkRed : Color.Empty;
            row.DefaultCellStyle.SelectionBackColor = invalid ? Color.LightCoral : Color.Empty;
            row.DefaultCellStyle.SelectionForeColor = invalid ? Color.DarkRed : Color.Empty;
            row.ErrorText = error;
            foreach (DataGridViewCell cell in row.Cells)
                cell.ToolTipText = error;
        }
    }

    private void cancelRate()
    {
        _controller.UnitOfWork.CancelChanges();
        bsRates.ResetBindings(false);
    }


    private EnergyUse.Models.TariffGroup getCurrentTarifGroup()
    {
        return (EnergyUse.Models.TariffGroup)CboTarifGroup.SelectedItem
            ?? new EnergyUse.Models.TariffGroup();
    }

    private void deleteRate()
    {
        if (bsRates.Current != null)
        {
            var message = Managers.Languages.GetResourceString("RatesAskDelete", "Are you sure you want to delete this rate?");
            var message2 = Managers.Languages.GetResourceString("DeleteTitle", "Delete?");
            if (MessageBox.Show(message, message2, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var entity = (EnergyUse.Models.Rate)bsRates.Current;
                _controller.UnitOfWork.Delete(entity);

                bsRates.DataSource = _controller.UnitOfWork.RateList;
                bsRates.ResetBindings(false);
            }
        }
    }

    private void refreshRates()
    {
        if (!validateInput())
            return;

        initRates();
    }

    private void closeRates()
    {
        Close();
    }

    private bool validateInput()
    {
        if (CboCostCategory.SelectedIndex == -1)
        {
            var message = Managers.Languages.GetResourceString("SelectCategory", "Select a category");
            MessageBox.Show(this, message);
            CboCostCategory.Focus();
            return false;
        }

        if (CboEnergyType.SelectedIndex == -1)
        {
            var message = Managers.Languages.GetResourceString("SelectEnergyType", "Select an energy type");
            MessageBox.Show(this, message);
            CboEnergyType.Focus();
            return false;
        }

        return true;
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
        if (this.BackColor != Color.Empty)
            DgRates.BackgroundColor = this.BackColor;
    }

    #endregion

}
