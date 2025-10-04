using EnergyUse.Common.Enums;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.WinForms;
using System.Data;

namespace WinFormsEF.ucControls;

public partial class ucChartRatesLiveCharts : UserControl
{
    #region ChartProperties

    private bool _initSettings;
    private EnergyUse.Core.Graphs.LiveCharts.Rates _chartRates { get; set; }
    private EnergyUse.Core.UnitOfWork.Graphs _unitOfWork { get; set; }
    private EnergyUse.Models.Address _currentAddress { get; set; }
    private EnergyUse.Models.EnergyType _currentEnergyType { get; set; }

    private CartesianChart _cartesianChart;

    #endregion

    public ucChartRatesLiveCharts(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType)
    {
        InitializeComponent();

        _initSettings = true;
        _unitOfWork = new EnergyUse.Core.UnitOfWork.Graphs(Managers.Config.GetDbFileName());
        _currentAddress = selectedAddress;
        _currentEnergyType = selectedEnergyType;

        setCostCategory(_currentEnergyType);
        _initSettings = false;

        ResetChart();
    }

    #region ButtonEvents

    private void cmdReset_Click(object sender, EventArgs e)
    {
        ResetChart();
    }

    private void cmdExport_Click(object sender, EventArgs e)
    {
        exportToExcel(_currentEnergyType);
    }

    #endregion

    #region Events

    private void chkListCostCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_initSettings == true) { return; }

        setCurrentCheckListBox(chkListCostCategory);

        ResetChart();
    }

    private void chkListCostCategory_ItemCheck(object sender, ItemCheckEventArgs e)
    {
        if (_initSettings == true) { return; }

        CheckedListBox clb = (CheckedListBox)sender;
        // Switch off event handler
        clb.ItemCheck -= chkListCostCategory_ItemCheck;
        clb.SetItemCheckState(e.Index, e.NewValue);
        // Switch on event handler
        clb.ItemCheck += chkListCostCategory_ItemCheck;

        SetChart();
    }

    private void dtpFrom_ValueChanged(object sender, EventArgs e)
    {
        if (_initSettings == true) { return; }

        setCurrentDtpTag(dtpFrom);
    }

    private void dtpTill_ValueChanged(object sender, EventArgs e)
    {
        if (_initSettings == true) { return; }

        setCurrentDtpTag(dtpTill);
    }

    private void rbType_CheckedChanged(object sender, EventArgs e)
    {
        if (_initSettings == true) { return; }

        _initSettings = true;
        setCostCategory(_currentEnergyType);
        _initSettings = false;

        SetChart();
    }

    #endregion

    #region Methods

    public void SetChart()
    {
        if (_currentEnergyType != null)
        {
            ParameterGraph graphParameter = new ParameterGraph();
            graphParameter.Address = _currentAddress;
            graphParameter.EnergyTypeList = new() { _currentEnergyType };
            graphParameter.DbName = Managers.Config.GetDbFileName();
            graphParameter.ShowType = GetShowType();
            graphParameter.From = dtpFrom.Value;
            graphParameter.Till = dtpTill.Value;
            graphParameter.CostCategoryList = getSelectedCostCategories();
            graphParameter.TarifGroupId = 1;

            Cursor.Current = Cursors.WaitCursor;
            if (rbUnit.Checked)
                getChartSeriesPerCostCategoryAndUnit(graphParameter);
            else
                getChartSeriesPerCostCategory(graphParameter);
            Cursor.Current = Cursors.Default;
        }
    }

    private void addChart(Period periodType, List<ISeries> serieslist)
    {
        if (serieslist.Count == 0)
            return;

        string title = Managers.Languages.GetResourceString("ChartRatesTitle", "Rates");

        panel1.Controls.Clear();
        _cartesianChart = Managers.LiveCharts.GetDefaultChart(periodType, serieslist, title, !_currentEnergyType.HasEnergyReturn);

        panel1.Controls.Add(_cartesianChart);
    }

    private void getChartSeriesPerCostCategory(ParameterGraph graphParameter)
    {
        _chartRates = new EnergyUse.Core.Graphs.LiveCharts.Rates(graphParameter);

        addChart(Period.Day, _chartRates.GetSeries());
    }

    private void getChartSeriesPerCostCategoryAndUnit(ParameterGraph graphParameter)
    {
        _chartRates = new EnergyUse.Core.Graphs.LiveCharts.Rates(graphParameter);

        addChart(Period.Day, _chartRates.GetSeries());
    }

    private List<EnergyUse.Models.CostCategory> getSelectedCostCategories()
    {
        List<EnergyUse.Models.CostCategory> costCategories = new List<EnergyUse.Models.CostCategory>();
        for (int i = 0; i < chkListCostCategory.Items.Count; i++)
        {
            CheckState st = chkListCostCategory.GetItemCheckState(i);
            if (st != CheckState.Checked)
                continue;

            costCategories.Add((EnergyUse.Models.CostCategory)chkListCostCategory.Items[i]);
        }

        return costCategories;
    }

    public void ResetCurrentAddress(EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
    {
        _currentAddress = address;
        _currentEnergyType = energyType;
        ResetChart();
    }

    public void ResetCurrentEnergyType(EnergyUse.Models.EnergyType energyType)
    {
        _currentEnergyType = energyType;
        setCostCategory(_currentEnergyType);
        ResetChart();
    }

    private void setCostCategory(EnergyUse.Models.EnergyType energyType)
    {
        var currentSettingId = chkListCostCategory.Tag.ToString();
        var setting = getCurrentSetting(currentSettingId);
        List<EnergyUse.Models.CostCategory> costCategories = new();
        if (energyType != null)
            costCategories = _unitOfWork.CostCategoryRepo.SelectByEnergyTypeId(energyType.Id).ToList();

        if (rbUnit.Checked)
            costCategories = costCategories.Where(x => x.EnergySubType.Id >= 1 && x.EnergySubType.Id <= 4).ToList();

        chkListCostCategory.Items.Clear();
        foreach (var costCategory in costCategories)
        {
            var categoryChecked = true;
            if (setting != null)
                categoryChecked = setting.KeyValue.Contains($"{costCategory.Id};");

            chkListCostCategory.Items.Add(costCategory, categoryChecked);
        }

        ((ListBox)chkListCostCategory).DisplayMember = "Name";
    }

    public void ResetChart()
    {
        if (_initSettings) { return; }

        setDefaultPeriodSettings();

        SetChart();
    }

    private void setDefaultPeriodSettings()
    {
        var currentDateFrom = getCurrentDateByDatePicker(dtpFrom);
        if (currentDateFrom == DateTime.MinValue)
            currentDateFrom = new DateTime(DateTime.Now.AddYears(-4).Year, 1, 1); ;

        var currentDateTill = getCurrentDateByDatePicker(dtpTill);
        if (currentDateTill == DateTime.MinValue)
            currentDateTill = new DateTime(DateTime.Now.Year, 12, 31);

        dtpFrom.Value = currentDateFrom;
        dtpTill.Value = currentDateTill;
    }

    private DateTime getCurrentDateByDatePicker(DateTimePicker dateTimePicker)
    {
        var currentSettingId = dateTimePicker.Tag.ToString();
        var setting = getCurrentSetting(currentSettingId);

        if (setting != null)
        {
            var year = int.Parse(setting.KeyValue.Substring(0, 4));
            var month = int.Parse(setting.KeyValue.Substring(4, 2));
            var day = int.Parse(setting.KeyValue.Substring(6, 2));

            return new DateTime(year, month, day);
        }
        else
            return DateTime.MinValue;
    }

    private void setCurrentCheckListBox(CheckedListBox chkListCostCategory)
    {
        var currentSettingId = chkListCostCategory.Tag.ToString();
        var currentValue = string.Empty;
        var selectedCostCategories = getSelectedCostCategories();
        foreach (var costCategory in selectedCostCategories)
            currentValue += $"{costCategory.Id};";

        setCurrentSetting(currentSettingId, currentValue);
    }

    private void setCurrentDtpTag(DateTimePicker dtp)
    {
        var currentSettingId = dtp.Tag.ToString();
        var currentValue = dtp.Value.ToString("yyyyMMdd");

        setCurrentSetting(currentSettingId, currentValue);
    }

    private EnergyUse.Models.Setting getCurrentSetting(string settingId)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        return libSettings.GetSetting(settingId);
    }

    private void setCurrentSetting(string settingId, string currentValue)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
        libSettings.SaveSetting(settingId, currentValue);
    }

    private ShowType GetShowType()
    {
        if (rbRate.Checked)
            return ShowType.Rate;
        else if (rbUnit.Checked)
            return ShowType.Unit;
        else
            return ShowType.Unknown;
    }

    private void exportToExcel(EnergyUse.Models.EnergyType energyType)
    {
        if (_chartRates == null) return;

        var dataList = _chartRates.GetDataList();
        if (dataList.Count == 0)
        {
            var message = Managers.Languages.GetResourceString("NoDataToExport", "No data to export");
            MessageBox.Show(this, message);
        }
        else
        {
            var fileName = Managers.GeneralDialogs.GetExportFileName("ChartRates", energyType);
            EnergyUse.Core.Manager.LibExport.ExportChartRatesToExcel(fileName, energyType, dataList);
        }
    }

    #endregion
}