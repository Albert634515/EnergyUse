using EnergyUse.Common.Enums;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.WinForms;

namespace WinFormsEF.ucControls
{
    public partial class ucChartDefaultLiveCharts : UserControl
    {
        #region ChartProperties

        private bool _initSettings { get; set; }
        private List<EnergyUse.Models.EnergyType> _energyTypes { get; set; } = new();
        private EnergyUse.Core.Graphs.LiveCharts.Default _chartDefaultPerPeriod { get; set; }
        private EnergyUse.Models.Address _currentAddress { get; set; }
        private EnergyUse.Models.EnergyType _currentEnergyType { get; set; }
        private CartesianChart _cartesianChart { get; set; }

        #endregion

        #region InitControl

        public ucChartDefaultLiveCharts(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType, List<EnergyUse.Models.EnergyType> energyTypes)
        {
            InitializeComponent();
            initializeChart(selectedAddress, selectedEnergyType, energyTypes);
        }

        private void initializeChart(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType, List<EnergyUse.Models.EnergyType> energyTypes)
        {
            _initSettings = true;

            _currentAddress = selectedAddress;
            _currentEnergyType = selectedEnergyType;
            _energyTypes = energyTypes;

            setComboCompareWith();

            _initSettings = false;

            resetChart();
        }

        private void setComboPeriodTypes(string currentValue)
        {
            var periodTypes = WinFormsEF.Managers.SelectionItemList.GetPeriodList();
            bsPeriodType.DataSource = periodTypes;

            if (currentValue == null)
                currentValue = "Year";

            CboPeriodType.SelectedIndex = CboPeriodType.FindString(currentValue);
        }

        private void setComboCompareWith()
        {
            bsEnergyTypes.DataSource = _energyTypes;
            CboCompareWith.SelectedIndex = -1;
            CboCompareWith.SelectedItem = null;
            CboCompareWith.ResetText();
        }

        private void setDefaultEnergyTypeSettings()
        {
            if (_currentEnergyType != null)
            {
                EfficiencyRadioButton.Visible = _currentEnergyType.HasEnergyReturn;
                lblProduction.Visible = _currentEnergyType.HasEnergyReturn;
                if (EfficiencyRadioButton.Checked)
                    RateRadioButton.Checked = true;
            }
        }

        private void setDefaultPeriodSettings()
        {
            Period periodType = getSelectedPeriodType();

            var currentDateFrom = getCurrentDateByDatePicker(DtpFrom, periodType);
            if (currentDateFrom == DateTime.MinValue)
                currentDateFrom = getDefaulStartDateByPeriod(periodType);

            var currentDateTill = getCurrentDateByDatePicker(DtpTill, periodType);
            if (currentDateTill == DateTime.MinValue)
                currentDateTill = getDefaulTillDateByPeriod(periodType);

            DtpFrom.Value = currentDateFrom;
            DtpTill.Value = currentDateTill;
        }

        private void setDefaultCheckBoxSetting(CheckBox checkBox)
        {
            Period periodType = getSelectedPeriodType();
            var currentSettingId = getCurrentCheckBoxSettingIdByPeriodType(checkBox, periodType);
            var setting = getCurrentSetting(currentSettingId);

            if ((setting != null && setting.KeyValue.ToLower() == "true") || setting == null)
                checkBox.Checked = true;
            else
                checkBox.Checked = false;
        }

        #endregion

        #region ButtonEvents

        private void CmdReset_Click(object sender, EventArgs e)
        {
            resetChart();
        }

        private void CmdExport_Click(object sender, EventArgs e)
        {
            exportToExcel(_currentEnergyType);
        }

        #endregion

        #region Events

        private void cboPeriodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentComboValue(CboPeriodType);
            setDefaultPeriodSettings();
            SetChart();
        }

        private void cboCompareWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentComboValue(CboCompareWith, CboPeriodType.Text);
        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentDateTimePicker(DtpFrom);
        }

        private void DtpTill_ValueChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentDateTimePicker(DtpTill);
        }

        private void rateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings == true) { return; }

            setCurrentPanelValue(TypePanel, RateRadioButton);
            SetChart();
        }

        private void valueRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentPanelValue(TypePanel, ValueRadioButton);
            SetChart();
        }

        private void efficiencyRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentPanelValue(TypePanel, EfficiencyRadioButton);
            SetChart();
        }

        private void categoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentPanelValue(ShowByPanel, CategoryRadioButton);
            SetChart();
        }

        private void subCategoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentPanelValue(ShowByPanel, SubCategoryRadioButton);
            SetChart();
        }

        private void totalsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentPanelValue(ShowByPanel, TotalsRadioButton);
            SetChart();
        }

        private void chkShowStacked_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentCheckBox(chkShowStacked);
            SetChart();
        }

        private void chkShowAvg_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentCheckBox(chkShowAvg);
            SetChart();
        }

        private void chkPredictMissingData_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentCheckBox(chkPredictMissingData);
            SetChart();
        }

        #endregion

        #region Methods

        public void SetChart()
        {
            if (_currentEnergyType == null)
                return;

            ParameterGraph graphParameter = new ParameterGraph();
            graphParameter.Address = _currentAddress;
            graphParameter.EnergyTypeList = getSelectedTypes();
            graphParameter.DbName = Managers.Config.GetDbFileName();
            graphParameter.ShowStacked = chkShowStacked.Checked;
            graphParameter.ShowAvg = chkShowAvg.Checked;
            graphParameter.PredictMissingData = chkPredictMissingData.Checked;
            graphParameter.From = DtpFrom.Value;
            graphParameter.Till = DtpTill.Value;
            graphParameter.PeriodType = getSelectedPeriodType();
            graphParameter.ShowBy = getSelectedShowBy();
            graphParameter.ShowType = getSelectedShowType();

            try
            {
                if (_currentAddress != null && _currentAddress.TariffGroup != null)
                    graphParameter.TarifGroupId = _currentAddress.TariffGroup.Id;

                Cursor = Cursors.WaitCursor;

                if (graphParameter.ShowBy == ShowBy.Category)
                    getChartSeriesPerPeriod(graphParameter);
                else if (graphParameter.ShowBy == ShowBy.SubCategory)
                    getChartSeriesPeriodBySubCategory(graphParameter);
                else if (graphParameter.ShowBy == ShowBy.Total)
                    getChartSeriesPerPeriodBySubTotal(graphParameter);

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void resetChart()
        {
            _initSettings = true;

            setComboPeriodTypes(getCurrentSettingValue(CboPeriodType.Tag.ToString()));
            setDefaultPeriodSettings();

            if (_currentEnergyType != null)
            {
                EfficiencyRadioButton.Visible = _currentEnergyType.HasEnergyReturn;
                if (EfficiencyRadioButton.Checked)
                    RateRadioButton.Checked = true;
            }

            setDefaultPanelTypeValue();
            setDefaultPanelShowByValue();
            setDefaultCheckBoxSetting(chkPredictMissingData);
            setDefaultCheckBoxSetting(chkShowStacked);
            setDefaultCheckBoxSetting(chkShowAvg);

            _initSettings = false;

            SetChart();
        }

        private List<EnergyUse.Models.EnergyType> getSelectedTypes()
        {
            var selectedTypes = new List<EnergyUse.Models.EnergyType>() { _currentEnergyType };

            if (CboCompareWith.SelectedIndex != -1)
                selectedTypes.Add((EnergyUse.Models.EnergyType)CboCompareWith.SelectedItem);

            return selectedTypes;
        }

        private void getChartSeriesPerPeriod(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(_currentEnergyType));
            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartDefaultPerPeriod.GetSeries(), axisList);
        }

        private void getChartSeriesPeriodBySubCategory(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(_currentEnergyType));
            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartDefaultPerPeriod.GetSeries(), axisList);
        }

        private void getChartSeriesPerPeriodBySubTotal(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(_currentEnergyType));
            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartDefaultPerPeriod.GetSeries(), axisList);
        }

        private void getChartEmpty(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(_currentEnergyType));
            addChart(graphParameter.PeriodType, graphParameter.ShowType, new List<ISeries>(), axisList);
        }

        private List<LiveChartsCore.SkiaSharpView.Axis> getYAxis(ParameterGraph graphParameter)
        {
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = new();
            var axisPosition = AxisPosition.End;
            foreach (var item in graphParameter.EnergyTypeList)
            {
                axisPosition = axisPosition == AxisPosition.End ? AxisPosition.Start : AxisPosition.End;

                axisList.Add(Managers.LiveCharts.GetYAxis(!item.HasEnergyReturn, item.Unit.Description, axisPosition));
            }

            return axisList;
        }

        private void setLabels(Dictionary<string, ResultLabel> labels)
        {
            lblConsumption.Visible = false;
            lblProduction.Visible = false;
            lblNetto.Visible = false;

            if (labels.ContainsKey("Consumption"))
            {
                lblConsumption.Visible = labels["Consumption"].LabelVisibility;
                lblConsumption.ForeColor = labels["Consumption"].LabelForeColor;
                lblConsumption.BackColor = labels["Consumption"].LabelBackColor;
                lblConsumption.Text = labels["Consumption"].LabelText;
            }

            if (labels.ContainsKey("Production"))
            {
                lblProduction.Visible = labels["Production"].LabelVisibility;
                lblProduction.ForeColor = labels["Production"].LabelForeColor;
                lblProduction.BackColor = labels["Production"].LabelBackColor;
                lblProduction.Text = labels["Production"].LabelText;
            }

            if (labels.ContainsKey("Netto"))
            {
                lblNetto.Visible = labels["Netto"].LabelVisibility;
                lblNetto.ForeColor = labels["Netto"].LabelForeColor;
                lblNetto.BackColor = labels["Netto"].LabelBackColor;
                lblNetto.Text = labels["Netto"].LabelText;
            }

            lblProduction.Left = (lblConsumption.Left + lblConsumption.Width) + 10;
            lblNetto.Left = (lblProduction.Left + lblProduction.Width) + 10;
        }

        private void addChart(Period periodType, ShowType showType, List<ISeries> serieslist, List<LiveChartsCore.SkiaSharpView.Axis> axislist)
        {
            if (serieslist.Count == 0)
                return;

            string title = $"{Managers.Languages.GetResourceString("ChartDefaultTitle", "Data per")} {periodType}";

            pnChartContainer.Controls.Clear();
            _cartesianChart = Managers.LiveCharts.GetDefaultChart(periodType, serieslist, axislist, title, !_currentEnergyType.HasEnergyReturn, Managers.LiveCharts.GetYaxisLabel(showType, _currentEnergyType));

            pnChartContainer.Controls.Add(_cartesianChart);
        }

        public void ResetCurrentAddress(EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
        {
            _currentAddress = address;
            _currentEnergyType = energyType;

            resetChart();
        }

        public void ResetCurrentEnergyType(EnergyUse.Models.EnergyType energyType)
        {
            _currentEnergyType = energyType;

            setDefaultEnergyTypeSettings();
            resetChart();
        }

        private DateTime getDefaulStartDateByPeriod(Period periodType)
        {
            DateTime defaultStartDate;
            var startYear = LibGraphGeneral.GetStartPeriod(periodType);

            switch (periodType)
            {
                case Period.Day:
                    defaultStartDate = new DateTime(startYear, DateTime.Now.Month, 1);
                    break;
                case Period.Week:
                    defaultStartDate = new DateTime(startYear, DateTime.Now.Month, 1).AddMonths(-12);
                    break;
                case Period.Month:
                    defaultStartDate = new DateTime(startYear, DateTime.Now.Month, 1).AddMonths(-12);
                    break;
                case Period.Year:
                    defaultStartDate = new DateTime(startYear, DateTime.Now.Month, 1).AddYears(-10);
                    break;
                default:
                    defaultStartDate = new DateTime(startYear, DateTime.Now.Month, 1);
                    break;
            }

            return defaultStartDate;
        }

        private DateTime getDefaulTillDateByPeriod(Period periodType)
        {
            var defaultTillDate = DateTime.MinValue;
            var endYear = LibGraphGeneral.GetEndPeriod(periodType);
            var daysInMonth = DateTime.DaysInMonth(endYear, DateTime.Now.Month);

            switch (periodType)
            {
                case Period.Day:
                    defaultTillDate = new DateTime(endYear, DateTime.Now.Month, DateTime.Now.Day);
                    break;
                case Period.Week:
                    defaultTillDate = new DateTime(endYear, DateTime.Now.Month, daysInMonth);
                    break;
                case Period.Month:
                    defaultTillDate = new DateTime(endYear, DateTime.Now.Month, daysInMonth);
                    break;
                case Period.Year:
                    defaultTillDate = new DateTime(endYear, 12, 31);
                    break;
                default:
                    defaultTillDate = new DateTime(endYear, DateTime.Now.Month, daysInMonth);
                    break;
            }

            return defaultTillDate;
        }


        private ShowType getSelectedShowType()
        {
            if (RateRadioButton.Checked)
            {
                return ShowType.Rate;
            }
            else if (ValueRadioButton.Checked)
            {
                return ShowType.Value;
            }
            else if (EfficiencyRadioButton.Checked)
            {
                return ShowType.Efficiency;
            }
            else
            {
                return ShowType.Unknown;
            }
        }

        private ShowBy getSelectedShowBy()
        {
            if (CategoryRadioButton.Checked)
            {
                return ShowBy.Category;
            }
            else if (SubCategoryRadioButton.Checked)
            {
                return ShowBy.SubCategory;
            }
            else
            {
                return ShowBy.Total;
            }
        }

        private Period getSelectedPeriodType()
        {
            var selectedItem = new SelectionItem();
            if (CboPeriodType.SelectedIndex != -1)
                selectedItem = (SelectionItem)CboPeriodType.SelectedItem;

            return LibGraphGeneral.GetPeriodType(selectedItem.Key);
        }

        private void setDefaultPanelTypeValue()
        {
            Period periodType = getSelectedPeriodType();
            var currentSettingId = getCurrentSettingIdByPeriodType(TypePanel.Tag.ToString(), periodType);
            var setting = getCurrentSetting(currentSettingId);

            if (_currentEnergyType != null)
                EfficiencyRadioButton.Visible = _currentEnergyType.HasEnergyReturn;

            if (setting == null && EfficiencyRadioButton.Visible && EfficiencyRadioButton.Checked)
                RateRadioButton.Checked = true;

            if (setting != null && setting.KeyValue == RateRadioButton.Tag.ToString())
                RateRadioButton.Checked = true;
            else if (setting != null && setting.KeyValue == ValueRadioButton.Tag.ToString())
                ValueRadioButton.Checked = true;
            else if (setting != null && setting.KeyValue == EfficiencyRadioButton.Tag.ToString())
                EfficiencyRadioButton.Checked = true;
        }

        private void setDefaultPanelShowByValue()
        {
            Period periodType = getSelectedPeriodType();
            var currentSettingId = getCurrentSettingIdByPeriodType(ShowByPanel.Tag.ToString(), periodType);
            var setting = getCurrentSetting(currentSettingId);

            if (setting == null && EfficiencyRadioButton.Visible && EfficiencyRadioButton.Checked)
                RateRadioButton.Checked = true;

            if (setting != null && setting.KeyValue == CategoryRadioButton.Tag.ToString())
                CategoryRadioButton.Checked = true;
            else if (setting != null && setting.KeyValue == SubCategoryRadioButton.Tag.ToString())
                SubCategoryRadioButton.Checked = true;
            else if (setting != null && setting.KeyValue == TotalsRadioButton.Tag.ToString())
                TotalsRadioButton.Checked = true;
        }

        private string getCurrentSettingValue(string settingId)
        {
            var setting = getCurrentSetting(settingId);
            if (setting != null)
                return setting.KeyValue;
            else
                return "";
        }

        private DateTime getCurrentDateByDatePicker(DateTimePicker dateTimePicker, Period periodType)
        {
            var currentSettingId = getCurrentSettingIdByPeriodType(dateTimePicker.Tag.ToString(), periodType);
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

        private void setCurrentDateTimePicker(DateTimePicker dtp)
        {
            var currentValue = dtp.Value.ToString("yyyyMMdd");
            var currentPeriodType = getSelectedPeriodType();
            var currentSettingId = getCurrentSettingIdByPeriodType(dtp.Tag.ToString(), currentPeriodType);

            setCurrentSetting(currentSettingId, currentValue);
        }

        private string getCurrentSettingIdByPeriodType(string tag, Period periodType)
        {
            var currentSettingId = tag;
            if (periodType != Period.Unknown)
                currentSettingId += $"_{periodType.ToString().ToUpper()}";

            return currentSettingId;
        }

        private void setCurrentCheckBox(CheckBox checkBox)
        {
            var currentValue = checkBox.Checked.ToString();
            var currentPeriodType = getSelectedPeriodType();
            var currentSettingId = getCurrentCheckBoxSettingIdByPeriodType(checkBox, currentPeriodType);

            setCurrentSetting(currentSettingId, currentValue);
        }

        private string getCurrentCheckBoxSettingIdByPeriodType(CheckBox checkBox, Period periodType)
        {
            var currentSettingId = checkBox.Tag.ToString();
            if (periodType != Period.Unknown)
                currentSettingId += $"_{periodType.ToString().ToUpper()}";

            return currentSettingId;
        }

        /// <summary>
        /// Store current combo value to settings table
        /// </summary>
        /// <param name="combobox">Current combo box</param>
        private void setCurrentComboValue(ComboBox combobox, string periodType = "")
        {
            var currentValue = combobox.Text.ToUpper();
            var currentSettingId = $"{combobox.Tag.ToString()}{periodType.ToUpper()}";

            if (!string.IsNullOrWhiteSpace(currentValue))
                setCurrentSetting(currentSettingId, currentValue);
        }

        private void setCurrentPanelValue(Panel panel, RadioButton radioButton)
        {
            Period periodType = getSelectedPeriodType();
            var currentSettingId = getCurrentSettingIdByPeriodType(panel.Tag.ToString(), periodType);
            var currentValue = radioButton.Tag.ToString();

            if (!string.IsNullOrWhiteSpace(currentValue))
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

        private void exportToExcel(EnergyUse.Models.EnergyType currentEnergyType)
        {
            if (_chartDefaultPerPeriod == null) return;

            var dataList = _chartDefaultPerPeriod.GetDataList();
            if (dataList.Count == 0)
            {
                var message = Managers.Languages.GetResourceString("NoDataToExport", "No data to export");
                MessageBox.Show(this, message);
            }
            else
            {
                var fileName = Managers.GeneralDialogs.GetExportFileName("ChartDefault", currentEnergyType);
                EnergyUse.Core.Manager.LibExport.ExportDataDefaultLiveChartToExcel(fileName, currentEnergyType, dataList);
            }
        }

        #endregion

    }
}
