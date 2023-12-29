using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.WinForms;
using OfficeOpenXml;
using System.Data;
using System.Globalization;

namespace WinFormsEF.ucControls
{
    public partial class ucChartCompareLiveCharts : UserControl
    {
        #region ChartProperties

        private bool initSettings;
        private EnergyUse.Core.Graphs.LiveCharts.Compare _chartCompare { get; set; }
        private EnergyUse.Models.Address CurrentAddress { get; set; }
        private EnergyUse.Models.EnergyType CurrentEnergyType { get; set; }
        private CartesianChart cartesianChart { get; set; }

        #endregion

        #region InitControl

        public ucChartCompareLiveCharts(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType)
        {
            InitializeComponent();

            initSettings = true;
            CurrentAddress = selectedAddress;
            CurrentEnergyType = selectedEnergyType;

            setComboPeriodTypes();
            ResetChart();

            initSettings = false;            
        }

        private void setComboPeriodTypes()
        {
            var periodTypes = WinFormsEF.Managers.SelectionItemList.GetPeriodList();
            bsPeriodType.DataSource = periodTypes;

            CboPeriodType.SelectedIndex = CboPeriodType.FindString("Year");
        }

        #endregion

        #region ButtonEvents

        private void cmdReset_Click(object sender, EventArgs e)
        {
            ResetChart();
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            ExportToExcel(CurrentEnergyType);
        }

        #endregion

        #region Events

        private void cboPeriodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            initSettings = true;
            setDefaultPeriodSettings();
            initSettings = false;

            LoadChart();
        }

        private void cboNumbers_SelectedValueChanged(object sender, EventArgs e)
        {
            string periodType;

            periodType = CboPeriodType.Text;

            if (periodType.ToUpper() == "DAY")
                setNumber2Combo(cboNumbers.SelectedIndex + 1);

            LoadChart();
        }

        private void cboStartYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void cboEndYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void cboNumbers2_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void rbShowType_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void chkShowStacked_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        #endregion

        #region GetChartSeriesPerPeriod

        private void GetChartSeriesPerPeriod(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Compare(graphParameter);

            AddChart(graphParameter.PeriodType, graphParameter.ShowType, _chartCompare.GetSeries());

            SetLabels(_chartCompare.GetResultLabelsPerPeriod(CurrentEnergyType));
        }

        private void GetChartSeriesPerPeriodBySubCategory(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Compare(graphParameter);

            AddChart(graphParameter.PeriodType, graphParameter.ShowType, _chartCompare.GetSeries());

            SetLabels(_chartCompare.GetResultLabelsPerPeriod(CurrentEnergyType));
        }

        private void GetChartSeriesPerPeriodByTotal(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Compare(graphParameter);

            AddChart(graphParameter.PeriodType, graphParameter.ShowType, _chartCompare.GetSeries());

            SetLabels(_chartCompare.GetResultLabelsPerPeriod(CurrentEnergyType));
        }

        #endregion

        #region Methods

        private void SetLabels(Dictionary<string, ResultLabel> labels)
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

        private void AddChart(Period periodType, ShowType showType, List<ISeries> serieslist)
        {
            if (serieslist.Count == 0)
                return;

            string title = GetChartTitle(periodType, cboNumbers.SelectedIndex + 1);

            pnChartContainer.Controls.Clear();
            cartesianChart = Managers.LiveCharts.GetCompareChart(Period.Year, serieslist, title, Managers.LiveCharts.GetYaxisLabel(showType, CurrentEnergyType), !CurrentEnergyType.HasEnergyReturn);
            

            pnChartContainer.Controls.Add(cartesianChart);
        }

        public void LoadChart()
        {
            int daysInMonth;

            if (initSettings == true) return;

            if (cboEndYear.SelectedIndex == -1 || cboStartYear.SelectedIndex == -1)
                return;

            try
            {         
                daysInMonth = DateTime.DaysInMonth((int)cboEndYear.SelectedItem, DateTime.Now.Month);

                ParameterGraph graphParameter = new();
                graphParameter.Address = CurrentAddress;
                graphParameter.EnergyTypeList = new() { CurrentEnergyType };
                graphParameter.DbName = Managers.Config.GetDbFileName();
                graphParameter.ShowStacked = chkShowStacked.Checked;
                graphParameter.PredictMissingData = chkPredictMissingData.Checked;
                graphParameter.YearStart = (int)cboStartYear.SelectedItem;
                graphParameter.YearEnd = (int)cboEndYear.SelectedItem;
                graphParameter.PeriodType = GetSelectedPeriodType();
                graphParameter.ShowBy = GetSelectedShowBy();
                graphParameter.ShowType = GetSelectedShowType();
                graphParameter.From = new DateTime((int)cboStartYear.SelectedItem, 1, 1);
                graphParameter.Till = new DateTime((int)cboEndYear.SelectedItem, 12, daysInMonth);               


                if (CurrentAddress != null)
                    graphParameter.TarifGroupId = CurrentAddress.TariffGroup.Id;

                if (CurrentEnergyType != null)
                {
                    Cursor = Cursors.WaitCursor;

                    if (graphParameter.PeriodType == Period.Day || graphParameter.PeriodType == Period.Month)
                        graphParameter.Month = cboNumbers.SelectedIndex + 1;
                    if (graphParameter.PeriodType == Period.Week)
                        graphParameter.Week = cboNumbers.SelectedIndex + 1;
                    if (cboNumbers2.SelectedIndex > -1)
                        graphParameter.Day = int.Parse(cboNumbers2.Text);

                    if (graphParameter.ShowBy == ShowBy.Category)
                        GetChartSeriesPerPeriod(graphParameter);
                    else if (graphParameter.ShowBy == ShowBy.SubCategory)
                        GetChartSeriesPerPeriodBySubCategory(graphParameter);
                    else if (graphParameter.ShowBy == ShowBy.Total)
                        GetChartSeriesPerPeriodByTotal(graphParameter);
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
            CurrentAddress = address;
            CurrentEnergyType = energyType;
    
            ResetChart();
        }

        public void ResetCurrentEnergyType(EnergyUse.Models.EnergyType energyType)
        {
            CurrentEnergyType = energyType;
   
            SetDefaultEnergyTypeSettings();
            ResetChart();
        }

        public void ResetChart()
        {
            initSettings = true;
            setNumberCombo();
            setDefaultPeriodSettings();
            initSettings = false;

            if (CurrentEnergyType != null)
            {
                rbEfficiency.Visible = CurrentEnergyType.HasEnergyReturn;
                if (rbEfficiency.Checked)
                    rbRate.Checked = true;
            }

            LoadChart();
        }

        private void setNumberCombo()
        {
            int minRange, maxRange, selectedIndex;

            Period periodType = GetSelectedPeriodType();
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

        private void SetDefaultEnergyTypeSettings()
        {
            if (CurrentEnergyType != null)
            {
                rbEfficiency.Visible = CurrentEnergyType.HasEnergyReturn;
                lblProduction.Visible = CurrentEnergyType.HasEnergyReturn;
                if (rbEfficiency.Checked)
                    rbRate.Checked = true;
            }
        }

        private void setDefaultPeriodSettings()
        {
            Period periodType = GetSelectedPeriodType();

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
        }

        private bool ValidateRefresh()
        {
            if (CurrentAddress == null)
            {
                MessageBox.Show(this, "Please select an address");
                return false;
            }

            if (CurrentEnergyType == null)
            {
                MessageBox.Show(this, "Please select an energy type");
                return false;
            }

            return true;
        }

        private ShowType GetSelectedShowType()
        {
            if (rbRate.Checked)
                return ShowType.Rate;
            else if (rbValue.Checked)
                return ShowType.Value;
            else if (rbEfficiency.Checked)
                return ShowType.Efficiency;
            else
                return ShowType.Unknown;
        }

        private ShowBy GetSelectedShowBy()
        {
            if (rbCategory.Checked)
                return ShowBy.Category;
            else if (rbSubCategory.Checked)
                return ShowBy.SubCategory;
            else
                return ShowBy.Total;
        }

        private Period GetSelectedPeriodType()
        {
            var selectedItem = new SelectionItem();
            if (CboPeriodType.SelectedIndex != -1)
                selectedItem = (SelectionItem)CboPeriodType.SelectedItem;

            return LibGraphGeneral.GetPeriodType(selectedItem.Key);
        }

        private string GetChartTitle(Period periodType, int index)
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

        private void ExportToExcel(EnergyUse.Models.EnergyType energyType)
        {
            int totalCols;
            string message;

            var exportFileName = $"ChartCompare_{DateTime.Now:yyyyMMddHHmmss}_{energyType.Name}.xlsx";
            var exportDirectory = string.Empty;
            var dataList = _chartCompare.GetDataList();

            if (dataList != null && dataList.Count == 0)
            {
                message = Managers.Languages.GetResourceString("NoDataToExport", "No data to export");
                MessageBox.Show(this, message);
            }
            else
            {
                SaveFileDialog sf = new();
                sf.FileName = Path.Combine(exportDirectory, exportFileName);

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    exportDirectory = Path.GetDirectoryName(sf.FileName);
                    exportFileName = Path.GetFileName(sf.FileName);
                }

                if (string.IsNullOrWhiteSpace(exportDirectory))
                    return;

                if (!Directory.Exists(Path.GetDirectoryName(exportDirectory)))
                    Directory.CreateDirectory(Path.GetDirectoryName(exportDirectory));

                System.IO.FileInfo f = new(Path.Combine(exportDirectory, exportFileName));
                if (f.Exists)
                {
                    message = Managers.Languages.GetResourceString("FileExistsOverWrite", "File already exists, do you want overwrite?");
                    DialogResult dialogResult = MessageBox.Show(message, Managers.Languages.GetResourceString("FileAlreadyExists", "File already exists"), MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                        f.Delete();
                    else
                        return;
                }

                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                using (ExcelPackage excelPackage = new())
                {
                    ExcelWorksheet energieExport = excelPackage.Workbook.Worksheets.Add("ChartCompareData");

                    if (energyType.HasEnergyReturn && energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                        .Select(s => new
                        {
                            PeriodType = s.PeriodType,
                            Value_X = s.ValueX,
                            Value_X_Date = s.ValueXDate.ToShortDateString(),
                            Value_Y_Low = s.ValueYLow,
                            Value_Y_Normal = s.ValueYNormal,
                            Value_Y_Return_Low = s.ValueYReturnLow,
                            Value_Y_Return_Normal = s.ValueYReturnNormal,
                            Value_Y_Monetary = s.ValueMonetaryY,
                            Value_Y_Monetary_Low = s.ValueYMonetaryLow,
                            ValueY_Monetary_Normal = s.ValueYMonetaryNormal,
                            Value_Y_Monetary_Return_Low = s.ValueYMonetaryReturnLow,
                            Value_Y_Monetary_Return_Normal = s.ValueYMonetaryReturnNormal,
                            Rate_Low = s.RateLow,
                            Rate_Normal = s.RateNormal,
                            Rate_ReturnLow = s.RateReturnLow,
                            Rate_Return_Normal = s.RateReturnNormal,
                            CorrectionFactor = s.CorrectionFactor
                        });
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }
                    else if (energyType.HasEnergyReturn && !energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                        .Select(s => new
                        {
                            PeriodType = s.PeriodType,
                            Value_X = s.ValueX,
                            Value_Y_Normal = s.ValueYNormal,
                            Value_Y_Return_Normal = s.ValueYReturnNormal,
                            Value_Y_Monetary = s.ValueMonetaryY,
                            ValueY_Monetary_Normal = s.ValueYMonetaryNormal,
                            Value_Y_Monetary_Return_Normal = s.ValueYMonetaryReturnNormal,
                            Rate_Normal = s.RateNormal,
                            Rate_Return_Normal = s.RateReturnNormal,
                            CorrectionFactor = s.CorrectionFactor
                        });
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }
                    else if (!energyType.HasEnergyReturn && !energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                            .Select(s => new
                            {
                                PeriodType = s.PeriodType,
                                Value_X = s.ValueX,
                                Value_X_Date = s.ValueXDate.ToString("yyyy-mm-dd"),
                                Value_Y_Normal = s.ValueYNormal,
                                Value_Y_Monetary = s.ValueMonetaryY,
                                ValueY_Monetary_Normal = s.ValueYMonetaryNormal,
                                Rate_Normal = s.RateNormal,
                                CorrectionFactor = s.CorrectionFactor
                            });
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }
                    else if (!energyType.HasEnergyReturn && energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                        .Select(s => new
                        {
                            PeriodType = s.PeriodType,
                            Value_X = s.ValueX,
                            Value_X_Date = s.ValueXDate.ToString("yyyy-mm-dd"),
                            Value_Y_Low = s.ValueYLow,
                            Value_Y_Normal = s.ValueYNormal,
                            Value_Y_Monetary = s.ValueMonetaryY,
                            Value_Y_Monetary_Low = s.ValueYMonetaryLow,
                            ValueY_Monetary_Normal = s.ValueYMonetaryNormal,
                            Rate_Low = s.RateLow,
                            Rate_Normal = s.RateNormal,
                            CorrectionFactor = s.CorrectionFactor
                        });
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }
                    else
                    {
                        var exportResult = dataList;
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }

                    // Clean up column headers
                    totalCols = energieExport.Dimension.End.Column;
                    for (int i = 1; i <= totalCols; i++)
                        energieExport.Cells[1, i].Value = energieExport.Cells[1, i].Value.ToString().Replace("_", " ");

                    energieExport.Cells[energieExport.Dimension.Address].AutoFitColumns();
                    excelPackage.SaveAs(f);

                    EnergyUse.Common.Libs.LibGeneral.OpenCreatedFile(Path.Combine(exportDirectory, exportFileName));
                }
            }
        }

        #endregion
    }
}