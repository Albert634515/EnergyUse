using EnergyUse.Core.Controllers;
using System.Data;

namespace WinFormsEF.Views;

public partial class frmSelectReportParameters : Form
{
    #region FormProperties

    public EnergyUse.Models.Address CurrentAddress { get; set; }
    public int ReturnValue { get; set; } = 0;

    private SelectReportParametersController _controller;
    private int _selectionLineCount { get; set; } = 0;
    private List<EnergyUse.Models.PreDefinedPeriod> _preDefinedPeriods { get; set; }

    #endregion

    #region InitForm

    public frmSelectReportParameters(EnergyUse.Models.Address address, EnergyUse.Common.Enums.ReportType defaultReport)
    {
        _controller = new SelectReportParametersController(Managers.Config.GetDbFileName());
        _controller.Initialize();

        InitializeComponent();
        setBaseFormSettings();

        _controller.InitSettings = true;

        CurrentAddress = address;

        setComboAddresses();
        setPreSelectePeriods();
        setReportLists(defaultReport);
        setEnergyTypeSelections(address.Id);
        setEnergyTypeLists(address);

        _controller.InitSettings = false;
    }

    private void setComboAddresses()
    {
        var addressList = _controller.UnitOfWork.AddressRepo.GetAll().ToList();
        bsAddresses.DataSource = addressList;
        addressComboBox.SelectedIndex = -1;

        if (CurrentAddress == null)
        {
            var defaultAddress = addressList.Where(x => x.DefaultAddress == true).FirstOrDefault();
            if (defaultAddress != null)
                CurrentAddress = defaultAddress;
        }

        if (CurrentAddress != null)
        {
            int position = addressList.IndexOf(addressList.Single(x => x.Description == CurrentAddress.Description));
            addressComboBox.SelectedIndex = position;
        }
    }

    private void setPreSelectePeriods()
    {
        _preDefinedPeriods = _controller.UnitOfWork.PreDefinedPeriodRepo.GetAll().ToList();

        preSelectedPeriodComboBox.DataSource = _preDefinedPeriods;
        preSelectedPeriodComboBox.DisplayMember = "Description";
        preSelectedPeriodComboBox.ValueMember = "Id";

        long lastSelectedPeriod = getSettingLastSelectedPeriod();
        if (lastSelectedPeriod > 0)
            preSelectedPeriodComboBox.SelectedIndex = _preDefinedPeriods.FindIndex(q => q.Id == lastSelectedPeriod);
        else
            preSelectedPeriodComboBox.SelectedIndex = -1;
    }

    private void setReportLists(EnergyUse.Common.Enums.ReportType defaultReport)
    {
        var reportList = EnergyUse.Core.Manager.LibSelectionItemList.GetReportTypeList();
        bsReportTypes.DataSource = reportList;

        reportComboBox.Text = defaultReport.ToString();
    }

    #endregion

    #region Events

    private void cmbAddress_SelectedIndexChanged(object sender, EventArgs e)
    {
        var address = getSelectedAddress();
        setEnergyTypeLists(address);
        setEnergyTypeSelections(address.Id);
    }

    private void cboPreSelectedPeriods_SelectedIndexChanged(object sender, EventArgs e)
    {
        setSelectionPeriods();
    }

    private void cmdRemove_Click(object sender, EventArgs e)
    {
        removeDateSelectionLine();
    }

    private void clearPeriodButton_Click(object sender, EventArgs e)
    {
        preSelectedPeriodComboBox.SelectedIndex = -1;
    }

    private void reportComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        setSettingsReportTypeBased();
    }

    #endregion

    #region ButtonEvents

    private void cmdAdd_Click(object sender, EventArgs e)
    {
        addDateSelectionLine();
    }

    private void cmdCancel_Click(object sender, EventArgs e)
    {
        ReturnValue = -1;
        Close();
    }

    private void cmdSelect_Click(object sender, EventArgs e)
    {
        if (validateInput() != false)
        {
            setSettingLastSelectedPeriod();
            ReturnValue = 2;
            Close();
        }
    }

    #endregion

    #region Toolbar

    #endregion

    #region Methods

    private void setSettingLastSelectedPeriod()
    {
        if (preSelectedPeriodComboBox.SelectedIndex != -1)
        {
            string fileKey = getKeyForLastLastSelectedPeriod();
            var preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)preSelectedPeriodComboBox.SelectedItem;

            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            libSettings.SaveSetting(fileKey, preDefinedPeriod.Id.ToString());
        }
    }

    private bool validateInput()
    {
        DateTime startDate, endDate;
        EnergyUse.Models.Address address;

        address = (EnergyUse.Models.Address)addressComboBox.SelectedItem;

        if (address == null || address.Id == 0)
        {
            var message = Managers.Languages.GetResourceString("SelectParametersSelectAddress", "Please select an address to calculate the settlement for");
            MessageBox.Show(this, message);
            addressComboBox.Focus();
            return false;
        }

        for (int i = 1; i <= _selectionLineCount; i++)
        {
            ucControls.ucDateSelection ucDateSelection = this.Controls.Find(getDateSelectionKey(i, address.Id), true).FirstOrDefault() as ucControls.ucDateSelection;
            if (ucDateSelection != null)
            {
                var energyType = (EnergyUse.Models.EnergyType)ucDateSelection.cboEnergyType.SelectedItem;
                if (energyType != null)
                {
                    startDate = ucDateSelection.dtpFrom.Value;
                    endDate = ucDateSelection.dtpTill.Value;

                    if (endDate < startDate)
                    {
                        var message = Managers.Languages.GetResourceString("SelectParametersEndDateLarger", "End date should be large or equal to start date");
                        MessageBox.Show(this, message);
                        ucDateSelection.dtpFrom.Focus();
                        return false;
                    }

                    if (ucDateSelection.cboTarifGroups.SelectedIndex == -1)
                    {
                        var message = Managers.Languages.GetResourceString("SelectTarifGroup", "Select an tarif group");
                        MessageBox.Show(this, message);
                        ucDateSelection.cboTarifGroups.Focus();
                        return false;
                    }
                }
            }
        }

        return true;
    }

    private void clearEnergyTypeSelections()
    {
        foreach (var ds in this.Controls.OfType<ucControls.ucDateSelection>())
            this.Controls.Remove(ds);

        _selectionLineCount = 0;
    }

    private void setEnergyTypeSelections(long addressId)
    {
        clearEnergyTypeSelections();

        if (addressId == 0)
            return;

        ucControls.ucDateSelection ucDateSelection;
        int dateSelectionCount = getDateSelectionCount(addressId);
        Point location = parametersGroupBox.Location;
        location.X -= 5;
        location.Y += parametersGroupBox.Height - 35;

        for (int i = 1; i <= dateSelectionCount; i++)
        {
            ucDateSelection = setDateSelectionLine(i, addressId, location);
            setSelectionPeriods();
            location.Y += ucDateSelection.Height;
        }
    }

    private int getDateSelectionCount(long addressId)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        int dateSelectionCount = libSettings.GetNumberOfEnergyTypesOnReport(addressId);
        if (dateSelectionCount == 0)
        {
            var energyTypes = _controller.UnitOfWork.EnergyTypeRepo.SelectByAddressId(addressId).ToList();
            dateSelectionCount = energyTypes.Count;
            libSettings.SaveSetting($"NumberOfEnergyTypesOnReport_A{addressId}", dateSelectionCount.ToString());
        }

        return dateSelectionCount;
    }

    private void setLocationAddRemoveButton(int height)
    {
        Point location = addButton.Location;
        location.Y += height;
        addButton.Location = location;
    }

    private void addDateSelectionLine()
    {
        Point location;
        EnergyUse.Models.Address address = (EnergyUse.Models.Address)addressComboBox.SelectedItem;
        ucControls.ucDateSelection ucDateSelectionLast;

        var lastdateSelectionName = getLastSelectedKey();
        ucDateSelectionLast = (ucControls.ucDateSelection)FindControl(this, lastdateSelectionName);
        location = parametersGroupBox.Location;
        location.X -= 5;
        location.Y = ucDateSelectionLast.Location.Y;

        if (ucDateSelectionLast != null)
        {
            setDateSelectionLine(_selectionLineCount + 1, address.Id, location);
        }
    }

    private ucControls.ucDateSelection setDateSelectionLine(int lineCount, long addressId, Point location)
    {
        ucControls.ucDateSelection ucDateSelection = new ucControls.ucDateSelection();
        ucDateSelection.EnergyTypeList = _controller.UnitOfWork.EnergyTypeRepo.SelectByAddressId(addressId).ToList();
        ucDateSelection.TarifGroupsList = _controller.UnitOfWork.TarifGroupRepo.GetAll().ToList();
        ucDateSelection.SetTarifGroups();

        ucDateSelection.Name = getDateSelectionKey(lineCount, addressId);
        location.Y += ucDateSelection.Height;
        ucDateSelection.Location = location;
        ucDateSelection.SetEnergyTypes(addressId);
        ucDateSelection.cmdRemove.Click += new System.EventHandler(this.cmdRemove_Click);

        if (lineCount == 1)
        {
            ucDateSelection.SetRemoveButtonVisibilty(false);
            ucDateSelection.SetDefaultEnergyType();
        }

        Controls.Add(ucDateSelection);
        location.Y += ucDateSelection.Height;
        this.Height += ucDateSelection.Height;

        _selectionLineCount++;

        setLocationAddRemoveButton(ucDateSelection.Height);
        setRemoveButtonVisibity(addressId);

        return ucDateSelection;
    }

    private void removeDateSelectionLine()
    {
        ucControls.ucDateSelection ucDateSelectionLast;
        var lastdateSelectionName = getLastSelectedKey();

        ucDateSelectionLast = (ucControls.ucDateSelection)FindControl(this, lastdateSelectionName);
        Controls.Remove(ucDateSelectionLast);
        ucDateSelectionLast.Dispose();

        setLocationAddRemoveButton(0 - ucDateSelectionLast.Height);
        Height -= ucDateSelectionLast.Height;
        _selectionLineCount--;

        if (_selectionLineCount != 1)
        {
            ucDateSelectionLast = (ucControls.ucDateSelection)FindControl(this, lastdateSelectionName);
            ucDateSelectionLast.SetRemoveButtonVisibilty(true);
        }
    }

    private string getDateSelectionKey(int lineCount, long addressId)
    {
        return $"ucDateSelection{lineCount}_A{addressId}";
    }

    private string getLastSelectedKey()
    {
        EnergyUse.Models.Address address = (EnergyUse.Models.Address)addressComboBox.SelectedItem;
        return $"ucDateSelection{_selectionLineCount}_A{address.Id}";
    }

    private void setRemoveButtonVisibity(long addressId)
    {
        ucControls.ucDateSelection ucDateSelection = new ucControls.ucDateSelection(); for (int i = 1; i < _selectionLineCount; i++)
        {
            ucDateSelection = this.Controls.Find(getDateSelectionKey(i, addressId), true).FirstOrDefault() as ucControls.ucDateSelection;
            if (ucDateSelection != null)
                ucDateSelection.SetRemoveButtonVisibilty(false);
        }
    }

    private void setEnergyTypeLists(EnergyUse.Models.Address address)
    {
        bool dataselectionFound;
        int maxSelectionCount = getDateSelectionCount(address.Id);
        int lineCount = 1;
        string dateSelectionKey;

        do
        {
            dataselectionFound = false;
            dateSelectionKey = getDateSelectionKey(lineCount, address.Id);

            ucControls.ucDateSelection ucDateSelection = this.Controls.Find(dateSelectionKey, true).FirstOrDefault() as ucControls.ucDateSelection;
            if (ucDateSelection != null)
            {
                ucDateSelection.SetEnergyTypes(address.Id);
                if (lineCount == 1)
                    ucDateSelection.SetDefaultEnergyType();

                if (address.TariffGroup != null)
                    ucDateSelection.SetTarifGroup(address.TariffGroup.Id);

                lineCount += 1;
                dataselectionFound = true;
            }
        } while (dataselectionFound);
    }

    private void setSelectionPeriods()
    {
        EnergyUse.Models.PreDefinedPeriod preDefinedPeriod;
        List<EnergyUse.Models.PreDefinedPeriodDate> preDefinedPeriodDateList;
        EnergyUse.Models.Address address = getSelectedAddress();
        int lineCount = 1;

        if (preSelectedPeriodComboBox.SelectedIndex == -1)
            return;

        preDefinedPeriod = (EnergyUse.Models.PreDefinedPeriod)preSelectedPeriodComboBox.SelectedItem;
        preDefinedPeriodDateList = _controller.UnitOfWork.PreDefinedPeriodDateRepo.GetByPeriodId(preDefinedPeriod.Id).ToList();

        foreach (var preDefinedPeriodDate in preDefinedPeriodDateList)
        {
            ucControls.ucDateSelection ucDateSelection = this.Controls.Find(getDateSelectionKey(lineCount, address.Id), true).FirstOrDefault() as ucControls.ucDateSelection;
            if (ucDateSelection != null)
            {
                ucDateSelection.dtpFrom.Value = preDefinedPeriodDate.StartDate;
                ucDateSelection.dtpTill.Value = preDefinedPeriodDate.EndDate;
                ucDateSelection.SetEnergyType(preDefinedPeriodDate.EnergyType.Id);

                if (preDefinedPeriodDate.TariffGroup != null && preDefinedPeriodDate.TariffGroup.Id > 0)
                    ucDateSelection.SetTarifGroup(preDefinedPeriodDate.TariffGroup.Id);
            }

            lineCount += 1;
        }
    }

    private EnergyUse.Models.Address getSelectedAddress()
    {
        var address = (EnergyUse.Models.Address)addressComboBox.SelectedItem;
        if (address == null)
            address = new EnergyUse.Models.Address();

        return address;
    }

    private long getSettingLastSelectedPeriod()
    {
        long lastSelectedPeriod = 0;

        var fileKey = getKeyForLastLastSelectedPeriod();
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        var setting = libSettings.GetSetting(fileKey);
        if (setting != null && setting.Id > 0)
            lastSelectedPeriod = Convert.ToInt64(setting.KeyValue);

        return lastSelectedPeriod;
    }

    private string getKeyForLastLastSelectedPeriod()
    {
        var currentAddress = (EnergyUse.Models.Address)addressComboBox.SelectedItem;
        var fileKey = string.Empty;

        if (currentAddress != null && currentAddress.Id > 0)
            fileKey = $"{currentAddress.Id}{preSelectedPeriodComboBox.Tag}";

        return fileKey;
    }

    private void setBaseFormSettings()
    {
        Managers.Settings.SetBaseFormSettings(this);
    }

    /// <summary>
    /// Set form settings based on set report type
    /// </summary>
    private void setSettingsReportTypeBased()
    {
        EnergyUse.Common.Enums.ReportType selectedReport = EnergyUse.Common.Enums.ReportType.None;
        Enum.TryParse(reportComboBox.Text, out selectedReport);

        showRatesCheckBox.Visible = true;
        if (selectedReport == EnergyUse.Common.Enums.ReportType.None)
            return;
        else if (selectedReport == EnergyUse.Common.Enums.ReportType.Rates)
        {
            showRatesCheckBox.Visible = false;
            showRatesCheckBox.Checked = true;
        }
        else if (selectedReport == EnergyUse.Common.Enums.ReportType.SettlementCompact)
            showRatesCheckBox.Checked = false;
    }

    private Control FindControl(Control parent, string name)
    {
        // Check the parent.
        if (parent.Name == name) return parent;

        // Recursively search the parent's children.
        foreach (Control ctl in parent.Controls)
        {
            Control found = FindControl(ctl, name);
            if (found != null) return found;
        }

        // If we still haven't found it, it's not here.
        return null;
    }

    public EnergyUse.Models.Common.ParameterSelection GetSelectedParameters()
    {
        bool dataselectionFound;
        EnergyUse.Models.TariffGroup tarifGroup;
        EnergyUse.Common.Enums.ReportType selectedReport = EnergyUse.Common.Enums.ReportType.None;
        Enum.TryParse(reportComboBox.Text, out selectedReport);

        var selectionCount = 1;

        var parameterSelection = new EnergyUse.Models.Common.ParameterSelection();

        parameterSelection.StartRange = DateTime.Now.AddMonths(-6);
        parameterSelection.EndRange = parameterSelection.StartRange.AddMonths(6);
        parameterSelection.PredictMissingData = predictMissingDataCheckBox.Checked;
        parameterSelection.ShowRates = showRatesCheckBox.Checked;
        parameterSelection.ReportType = selectedReport;

        var address = (EnergyUse.Models.Address)addressComboBox.SelectedItem;
        parameterSelection.AddressId = address.Id;

        var preSelectedPeriod = (EnergyUse.Models.PreDefinedPeriod)preSelectedPeriodComboBox.SelectedItem;
        if (preSelectedPeriod != null)
            parameterSelection.PreSelectedPeriodId = preSelectedPeriod.Id;

        do
        {
            dataselectionFound = false;

            ucControls.ucDateSelection ucDateSelection = this.Controls.Find(getDateSelectionKey(selectionCount, address.Id), true).FirstOrDefault() as ucControls.ucDateSelection;
            if (ucDateSelection != null)
            {
                var selectedEnergyType = new EnergyUse.Models.Common.SelectedEnergyType();
                selectedEnergyType.EnergyType = (EnergyUse.Models.EnergyType)ucDateSelection.cboEnergyType.SelectedItem;
                selectedEnergyType.StartRange = ucDateSelection.dtpFrom.Value;
                selectedEnergyType.EndRange = ucDateSelection.dtpTill.Value;

                tarifGroup = (EnergyUse.Models.TariffGroup)ucDateSelection.cboTarifGroups.SelectedItem;
                selectedEnergyType.TarifGroup = tarifGroup.Id;

                if (selectedEnergyType.EnergyType != null)
                    parameterSelection.SelectedEnergyTypeList.Add(selectedEnergyType);

                selectionCount += 1;
                dataselectionFound = true;
            }
        } while (dataselectionFound);

        return parameterSelection;
    }

    #endregion
}