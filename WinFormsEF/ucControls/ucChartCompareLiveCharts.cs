using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.WinForms;
using System.Globalization;

namespace WinFormsEF.ucControls
{
    public partial class ucChartCompareLiveCharts : UserControl
    {
        #region ChartProperties

        private bool _initSettings;
        private EnergyUse.Core.Graphs.LiveCharts.Compare _chartCompare { get; set; }
        private EnergyUse.Models.Address _currentAddress { get; set; }
        private EnergyUse.Models.EnergyType _currentEnergyType { get; set; }
        private CartesianChart _cartesianChart { get; set; }

        #endregion

        #region InitControl

        public ucChartCompareLiveCharts(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType)
        {
            InitializeComponent();

            _initSettings = true;

            _currentAddress = selectedAddress;
            _currentEnergyType = selectedEnergyType;

            ResetChart();

            _initSettings = false;
        }

        #endregion

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

        private void cboPeriodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentComboValue(CboPeriodType);
            SetChart();
        }

        private void cboStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentComboValue(cboStartYear, CboPeriodType.Text);
            SetChart();
        }

        private void cboEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentComboValue(cboEndYear, CboPeriodType.Text);
            SetChart();
        }

        private void cboNumbers_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentComboValue(cboNumbers, CboPeriodType.Text);

            string periodType = CboPeriodType.Text;

            if (periodType.ToUpper() == "DAY")
                setNumber2Combo(cboNumbers.SelectedIndex + 1);

            SetChart();
        }

        private void cboNumbers2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentComboValue(cboNumbers2, CboPeriodType.Text);
            SetChart();
        }


        private void rateRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

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

        private void SubCategoryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentPanelValue(ShowByPanel, SubCategoryRadioButton);
            SetChart();
        }

        private void TotalsRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentPanelValue(ShowByPanel, TotalsRadioButton);
            SetChart();
        }

        private void chkPredictMissingData_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentCheckBox(chkPredictMissingData);
            SetChart();
        }

        private void chkShowStacked_CheckedChanged(object sender, EventArgs e)
        {
            if (_initSettings) { return; }

            setCurrentCheckBox(chkShowStacked);
            SetChart();
        }

        #endregion

        #region GetChartSeriesPerPeriod

        private void getChartSeriesPerPeriod(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Compare(graphParameter);

            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartCompare.GetSeries());

            setLabels(_chartCompare.GetResultLabelsPerPeriod(_currentEnergyType));
        }

        private void getChartSeriesPerPeriodBySubCategory(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Compare(graphParameter);

            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartCompare.GetSeries());

            setLabels(_chartCompare.GetResultLabelsPerPeriod(_currentEnergyType));
        }

        private void getChartSeriesPerPeriodByTotal(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Compare(graphParameter);

            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartCompare.GetSeries());

            setLabels(_chartCompare.GetResultLabelsPerPeriod(_currentEnergyType));
        }

        #endregion

        #region Methods

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

        private void addChart(Period periodType, ShowType showType, List<ISeries> serieslist)
        {
            if (serieslist.Count == 0)
                return;

            string title = getChartTitle(periodType, cboNumbers.SelectedIndex + 1);

            pnChartContainer.Controls.Clear();
            _cartesianChart = Managers.LiveCharts.GetCompareChart(Period.Year, serieslist, title, Managers.LiveCharts.GetYaxisLabel(showType, _currentEnergyType), !_currentEnergyType.HasEnergyReturn);


            pnChartContainer.Controls.Add(_cartesianChart);
        }

        public void SetChart()
        {
            int daysInMonth;

            if (_initSettings == true) return;

            if (cboEndYear.SelectedIndex == -1 || cboStartYear.SelectedIndex == -1)
                return;

            try
            {
                daysInMonth = DateTime.DaysInMonth((int)cboEndYear.SelectedItem, DateTime.Now.Month);

                ParameterGraph graphParameter = new();
                graphParameter.Address = _currentAddress;
                graphParameter.EnergyTypeList = new() { _currentEnergyType };
                graphParameter.DbName = Managers.Config.GetDbFileName();
                graphParameter.ShowStacked = chkShowStacked.Checked;
                graphParameter.PredictMissingData = chkPredictMissingData.Checked;
                graphParameter.YearStart = (int)cboStartYear.SelectedItem;
                graphParameter.YearEnd = (int)cboEndYear.SelectedItem;
                graphParameter.PeriodType = getSelectedPeriodType();
                graphParameter.ShowBy = getSelectedShowBy();
                graphParameter.ShowType = getSelectedShowType();
                graphParameter.From = new DateTime((int)cboStartYear.SelectedItem, 1, 1);
                graphParameter.Till = new DateTime((int)cboEndYear.SelectedItem, 12, daysInMonth);


                if (_currentAddress != null)
                    graphParameter.TarifGroupId = _currentAddress.TariffGroup.Id;

                if (_currentEnergyType != null)
                {
                    Cursor = Cursors.WaitCursor;

                    if (graphParameter.PeriodType == Period.Day || graphParameter.PeriodType == Period.Month)
                        graphParameter.Month = cboNumbers.SelectedIndex + 1;
                    if (graphParameter.PeriodType == Period.Week)
                        graphParameter.Week = cboNumbers.SelectedIndex + 1;
                    if (cboNumbers2.SelectedIndex > -1)
                        graphParameter.Day = int.Parse(cboNumbers2.Text);

                    if (graphParameter.ShowBy == ShowBy.Category)
                        getChartSeriesPerPeriod(graphParameter);
                    else if (graphParameter.ShowBy == ShowBy.SubCategory)
                        getChartSeriesPerPeriodBySubCategory(graphParameter);
                    else if (graphParameter.ShowBy == ShowBy.Total)
                        getChartSeriesPerPeriodByTotal(graphParameter);
                }
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

        public void ResetCurrentAddress(EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
        {
            _currentAddress = address;
            _currentEnergyType = energyType;

            ResetChart();
        }

        public void ResetCurrentEnergyType(EnergyUse.Models.EnergyType energyType)
        {
            _currentEnergyType = energyType;

            setDefaultEnergyTypeSettings();
            ResetChart();
        }

        public void ResetChart()
        {
            _initSettings = true;

            setComboPeriodTypes(getCurrentSettingValue(CboPeriodType.Tag.ToString()));
            setNumberCombo();
            setDefaultPeriodSettings();

            _initSettings = false;

            setDefaultPanelTypeValue();
            setDefaultPanelShowByValue();
            setDefaultCheckBoxSetting(chkPredictMissingData);
            setDefaultCheckBoxSetting(chkShowStacked);

            SetChart();
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

        private void setComboPeriodTypes(string currentValue)
        {
            var periodTypes = WinFormsEF.Managers.SelectionItemList.GetPeriodList();
            bsPeriodType.DataSource = periodTypes;

            if (currentValue == null)
                currentValue = "Year";

            CboPeriodType.SelectedIndex = CboPeriodType.FindString(currentValue);
        }

        private void setNumberCombo()
        {
            int minRange, maxRange, selectedIndex;

            Period periodType = getSelectedPeriodType();
            selectedIndex = -1;

            lblNumber2.Visible = false;
            cboNumbers2.Visible = false;
            cboNumbers2.DataSource = null;
            cboNumbers2.Items.Clear();
            cboNumbers.DataSource = null;
            cboNumbers.Items.Clear();

            switch (periodType)
            {
                case Period.Day:
                    minRange = 1;
                    maxRange = 12;
                    selectedIndex = DateTime.Now.Month;

                    LblNumber.Text = "Month:";
                    LblNumber.Visible = true;
                    cboNumbers.Visible = true;
                    break;
                case Period.Week:
                    minRange = 1;
                    maxRange = 53;
                    selectedIndex = DateTime.Now.GetWeekNumber();

                    LblNumber.Text = "Week:";
                    LblNumber.Visible = true;
                    cboNumbers.Visible = true;
                    break;
                case Period.Month:
                    minRange = 1;
                    maxRange = 12;
                    selectedIndex = DateTime.Now.Month;

                    LblNumber.Text = "Month:";
                    LblNumber.Visible = true;
                    cboNumbers.Visible = true;
                    break;
                case Period.Year:
                    minRange = 0;
                    maxRange = 0;

                    LblNumber.Visible = false;
                    cboNumbers.Visible = false;
                    break;
                default:
                    minRange = 0;
                    maxRange = 0;

                    LblNumber.Visible = false;
                    cboNumbers.Visible = false;
                    break;
            }

            // Number range 1
            if (periodType == Period.Month)
                setComboWitMonths(minRange, maxRange, selectedIndex);
            else if (periodType == Period.Day)
            {
                setComboWitMonths(minRange, maxRange, DateTime.Now.Month);
                setNumber2Combo(DateTime.Now.Month);
            }
            else
            {
                setComboWithRange(minRange, maxRange, selectedIndex);
                setComboYears();
            }
        }

        private void setComboYears()
        {
            int minYear, maxYear;

            minYear = LibGraphGeneral.GetStartPeriod(Period.Year);
            if (minYear == 0)
                minYear = 1900;

            maxYear = LibGraphGeneral.GetEndPeriod(Period.Year);
            if (maxYear == 0 || maxYear < minYear)
                maxYear = DateTime.Now.Year;

            cboStartYear.Items.Clear();
            cboEndYear.Items.Clear();
            for (int i = minYear; i <= maxYear; i++)
            {
                cboStartYear.Items.Add(i);
                cboEndYear.Items.Add(i);
            }

            cboEndYear.SelectedItem = maxYear;

            cboStartYear.SelectedItem = maxYear - 10;
            if (cboStartYear.SelectedIndex == -1)
                cboStartYear.SelectedItem = minYear;
        }

        private void setNumber2Combo(int month)
        {
            int daysInMonth;

            daysInMonth = 1;

            if (month > 0)
                daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, month);
            if (month == 2)
                daysInMonth = 29;

            cboNumbers2.Items.Clear();
            for (int i = 1; i <= daysInMonth; i++)
                cboNumbers2.Items.Add(i);
            cboNumbers2.SelectedItem = DateTime.Now.Day;

            lblNumber2.Text = "Day:";
            lblNumber2.Visible = true;
            cboNumbers2.Visible = true;
        }

        private void setComboWithRange(int startRange, int endRange, int selectedIndex)
        {
            cboNumbers.DataSource = null;
            cboNumbers.Items.Clear();
            for (int i = startRange; i <= endRange; i++)
                cboNumbers.Items.Add(i);
            cboNumbers.SelectedItem = selectedIndex;
        }

        private void setComboWitMonths(int startRange, int endRange, int selectedIndex)
        {
            string monthName;
            SortedDictionary<int, string> monthList = new();

            cboNumbers.DataSource = null;
            cboNumbers.Items.Clear();

            for (int i = startRange; i <= endRange; i++)
            {
                monthName = new DateTime(DateTime.Now.Year, i, 1).ToString("MMM", CultureInfo.InvariantCulture);
                monthList.Add(i, monthName);
            }
            cboNumbers.DataSource = new BindingSource(monthList, null);

            cboNumbers.DisplayMember = "Value";
            cboNumbers.ValueMember = "Key";

            cboNumbers.SelectedIndex = selectedIndex - 1;
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

            switch (periodType)
            {
                case Period.Day:
                    LblNumber.Visible = true;
                    cboNumbers.Visible = true;
                    break;
                case Period.Week:
                    LblNumber.Visible = true;
                    cboNumbers.Visible = true;
                    break;
                case Period.Month:
                    LblNumber.Visible = true;
                    cboNumbers.Visible = true;
                    break;
                case Period.Year:
                    LblNumber.Visible = false;
                    cboNumbers.Visible = false;
                    break;
                default:
                    LblNumber.Visible = false;
                    cboNumbers.Visible = false;
                    break;
            }

            setComboYears();
            setNumberCombo();

            // Set last save defaults
            setComboToDefault(cboStartYear);
            setComboToDefault(cboEndYear);
            setComboToDefault(cboNumbers);
            setComboToDefault(cboNumbers2);
        }

        private void setDefaultCheckBoxSetting(CheckBox checkBox)
        {
            Period periodType = getSelectedPeriodType();
            var currentSettingId = getCurrentSettingIdByPeriodType(checkBox.Tag.ToString(), periodType);
            var setting = getCurrentSetting(currentSettingId);

            if ((setting != null && setting.KeyValue.ToLower() == "true") || setting == null)
                checkBox.Checked = true;
            else
                checkBox.Checked = false;
        }

        private void setComboToDefault(ComboBox comboBox)
        {
            if (comboBox.Visible)
            {
                var currentSetting = getCurrentSettingValue($"{comboBox.Tag.ToString()}{CboPeriodType.Text.ToUpper()}");
                if (!string.IsNullOrWhiteSpace(currentSetting))
                    comboBox.SelectedIndex = comboBox.FindString(currentSetting);
            }
        }

        private void setCurrentPanelValue(Panel panel, RadioButton radioButton)
        {
            Period periodType = getSelectedPeriodType();
            var currentSettingId = getCurrentSettingIdByPeriodType(panel.Tag.ToString(), periodType);
            var currentValue = radioButton.Tag.ToString();

            if (!string.IsNullOrWhiteSpace(currentValue))
                setCurrentSetting(currentSettingId, currentValue);
        }

        /// <summary>
        /// Store current combo value to settings table
        /// </summary>
        /// <param name="combobox">Current combo box</param>
        private void setCurrentComboValue(ComboBox combobox, string periodType = "")
        {
            var currentSettingId = $"{combobox.Tag.ToString()}{periodType.ToUpper()}";
            var currentValue = combobox.Text.ToUpper();

            if (!string.IsNullOrWhiteSpace(currentValue))
                setCurrentSetting(currentSettingId, currentValue);
        }

        private void setCurrentCheckBox(CheckBox checkBox)
        {
            var currentPeriodType = getSelectedPeriodType();
            var currentSettingId = getCurrentSettingIdByPeriodType(checkBox.Tag.ToString(), currentPeriodType);
            var currentValue = checkBox.Checked.ToString();

            setCurrentSetting(currentSettingId, currentValue);
        }

        private string getCurrentSettingIdByPeriodType(string tag, Period periodType)
        {
            var currentSettingId = tag;
            if (periodType != Period.Unknown)
                currentSettingId += $"_{periodType.ToString().ToUpper()}";

            return currentSettingId;
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

        private bool validateRefresh()
        {
            if (_currentAddress == null)
            {
                MessageBox.Show(this, "Please select an address");
                return false;
            }

            if (_currentEnergyType == null)
            {
                MessageBox.Show(this, "Please select an energy type");
                return false;
            }

            return true;
        }

        private ShowType getSelectedShowType()
        {
            if (RateRadioButton.Checked)
                return ShowType.Rate;
            else if (ValueRadioButton.Checked)
                return ShowType.Value;
            else if (EfficiencyRadioButton.Checked)
                return ShowType.Efficiency;
            else
                return ShowType.Unknown;
        }

        private ShowBy getSelectedShowBy()
        {
            if (CategoryRadioButton.Checked)
                return ShowBy.Category;
            else if (SubCategoryRadioButton.Checked)
                return ShowBy.SubCategory;
            else
                return ShowBy.Total;
        }

        private Period getSelectedPeriodType()
        {
            var selectedItem = new SelectionItem();
            if (CboPeriodType.SelectedIndex != -1)
                selectedItem = (SelectionItem)CboPeriodType.SelectedItem;

            return LibGraphGeneral.GetPeriodType(selectedItem.Key);
        }

        private string getChartTitle(Period periodType, int index)
        {
            string monthName, chartTitle;

            if (index > 0 && periodType == Period.Month)
            {
                monthName = new DateTime(DateTime.Now.Year, index, 1).ToString("MMM", CultureInfo.InvariantCulture);
                chartTitle = Managers.Languages.GetResourceString("ChartCompareTitleMonth", "Compare data per %s for %m");
                chartTitle = chartTitle.Replace("%m", monthName);
            }
            else
                chartTitle = Managers.Languages.GetResourceString("ChartCompareTitle", "Compare data per %s");

            chartTitle = chartTitle.Replace("%s", periodType.ToString());

            return chartTitle;
        }

        private void exportToExcel(EnergyUse.Models.EnergyType energyType)
        {
            if (_chartCompare == null) return;

            var dataList = _chartCompare.GetDataList();
            if (dataList.Count == 0)
            {
                var message = Managers.Languages.GetResourceString("NoDataToExport", "No data to export");
                MessageBox.Show(this, message);
            }
            else
            {
                var fileName = Managers.GeneralDialogs.GetExportFileName("ChartDefault", energyType);
                EnergyUse.Core.Manager.LibExport.ExportCompareChartToExcel(fileName, energyType, dataList);
            }
        }

        #endregion
    }
}