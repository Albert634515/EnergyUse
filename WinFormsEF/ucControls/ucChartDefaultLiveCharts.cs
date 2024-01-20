using EnergyUse.Common.Enums;
using EnergyUse.Core.Manager;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView.WinForms;
using OfficeOpenXml;
using System.Data;
using System.Windows.Forms;

namespace WinFormsEF.ucControls
{
    public partial class ucChartDefaultLiveCharts : UserControl
    {
        #region ChartProperties

        private bool _InitSettings { get; set; }
        private EnergyUse.Core.Graphs.LiveCharts.Default _chartDefaultPerPeriod { get; set; }
        private EnergyUse.Models.Address CurrentAddress { get; set; }
        private EnergyUse.Models.EnergyType CurrentEnergyType { get; set; }
        private CartesianChart CartesianChart { get; set; }

        #endregion

        #region InitControl

        public ucChartDefaultLiveCharts(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType, List<EnergyUse.Models.EnergyType> energyTypes)
        {
            InitializeComponent();
            initializeChart(selectedAddress, selectedEnergyType, energyTypes);
        }

        private void initializeChart(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType, List<EnergyUse.Models.EnergyType> energyTypes)
        {
            _InitSettings = true;

            CurrentAddress = selectedAddress;
            CurrentEnergyType = selectedEnergyType;

            setComboPeriodTypes(getCurrentSetting(CboPeriodType.Tag.ToString()));
            setComboCompareWith(energyTypes);
            resetChart();

            _InitSettings = false;
        }

        private void setComboPeriodTypes(string currentValue)
        {
            var periodTypes = WinFormsEF.Managers.SelectionItemList.GetPeriodList();
            bsPeriodType.DataSource = periodTypes;

            if (currentValue == null)
                currentValue = "Year";

            CboPeriodType.SelectedIndex = CboPeriodType.FindString(currentValue);
        }

        private void setComboCompareWith(List<EnergyUse.Models.EnergyType> energyTypes)
        {
            bsEnergyTypes.DataSource = energyTypes;
            CboCompareWith.SelectedIndex = -1;
            CboCompareWith.SelectedItem = null;
            CboCompareWith.ResetText();
        }

        #endregion

        #region ButtonEvents

        private void CmdReset_Click(object sender, EventArgs e)
        {
            resetChart();
        }

        private void CmdExport_Click(object sender, EventArgs e)
        {
            exportToExcel(CurrentEnergyType);
        }

        #endregion

        #region Events

        private void cboPeriodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCurrentPeriodType();
            setDefaultPeriodSettings();
            LoadChart();
        }

        private void CboCompareWith_SelectedIndexChanged(object sender, EventArgs e)
        {
            setCurrentCompareWithType();
        }

        private void DtpFrom_ValueChanged(object sender, EventArgs e)
        {
            setCurrentDtpTag(DtpFrom);
        }

        private void DtpTill_ValueChanged(object sender, EventArgs e)
        {
            setCurrentDtpTag(DtpTill);
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void rbShowBy_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void chkShowStacked_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void chkShowAvg_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        private void chkPredictMissingData_CheckedChanged(object sender, EventArgs e)
        {
            LoadChart();
        }

        #endregion

        #region Methods

        public void LoadChart()
        {
            if (_InitSettings == true) return;

            if (CurrentEnergyType == null)
                return;

            ParameterGraph graphParameter = new ParameterGraph();
            graphParameter.Address = CurrentAddress;
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
                if (CurrentAddress != null && CurrentAddress.TariffGroup != null)
                    graphParameter.TarifGroupId = CurrentAddress.TariffGroup.Id;

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

        private List<EnergyUse.Models.EnergyType> getSelectedTypes()
        {
            var selectedTypes = new List<EnergyUse.Models.EnergyType>() { CurrentEnergyType };

            if (CboCompareWith.SelectedIndex != -1)
                selectedTypes.Add((EnergyUse.Models.EnergyType)CboCompareWith.SelectedItem);

            return selectedTypes;
        }

        private void getChartSeriesPerPeriod(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(CurrentEnergyType));
            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartDefaultPerPeriod.GetSeries(), axisList);
        }

        private void getChartSeriesPeriodBySubCategory(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(CurrentEnergyType));
            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartDefaultPerPeriod.GetSeries(), axisList);
        }

        private void getChartSeriesPerPeriodBySubTotal(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(CurrentEnergyType));
            addChart(graphParameter.PeriodType, graphParameter.ShowType, _chartDefaultPerPeriod.GetSeries(), axisList);
        }

        private void getChartEmpty(ParameterGraph graphParameter)
        {
            _chartDefaultPerPeriod = new EnergyUse.Core.Graphs.LiveCharts.Default(graphParameter);
            List<LiveChartsCore.SkiaSharpView.Axis> axisList = getYAxis(graphParameter);

            setLabels(_chartDefaultPerPeriod.GetResultLabelsPerPeriod(CurrentEnergyType));
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
            CartesianChart = Managers.LiveCharts.GetDefaultChart(periodType, serieslist, axislist, title, !CurrentEnergyType.HasEnergyReturn, Managers.LiveCharts.GetYaxisLabel(showType, CurrentEnergyType));

            pnChartContainer.Controls.Add(CartesianChart);
        }

        public void ResetCurrentAddress(EnergyUse.Models.Address address, EnergyUse.Models.EnergyType energyType)
        {
            CurrentAddress = address;
            CurrentEnergyType = energyType;

            resetChart();
        }

        public void ResetCurrentEnergyType(EnergyUse.Models.EnergyType energyType)
        {
            CurrentEnergyType = energyType;

            setDefaultEnergyTypeSettings();
            resetChart();
        }

        private void resetChart()
        {
            _InitSettings = true;
            setDefaultPeriodSettings();
            _InitSettings = false;

            if (CurrentEnergyType != null)
            {
                rbEfficiency.Visible = CurrentEnergyType.HasEnergyReturn;
                if (rbEfficiency.Checked)
                    rbRate.Checked = true;
            }

            LoadChart();
        }

        private void setDefaultEnergyTypeSettings()
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
            Period periodType = getSelectedPeriodType();            

            var currentDateFrom = getCurrentDateByDatePicker(DtpFrom);
            if (currentDateFrom == DateTime.MinValue)
                currentDateFrom = getDefaulStartDateByPeriod(periodType);

            var currentDateTill = getCurrentDateByDatePicker(DtpTill);
            if (currentDateTill == DateTime.MinValue)
                currentDateTill = getDefaulTillDateByPeriod(periodType);

            DtpFrom.Value = currentDateFrom;
            DtpTill.Value = currentDateTill;
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
                    defaultStartDate =  new DateTime(startYear, DateTime.Now.Month, 1);
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
            if (rbRate.Checked)
            {
                return ShowType.Rate;
            }
            else if (rbValue.Checked)
            {
                return ShowType.Value;
            }
            else if (rbEfficiency.Checked)
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
            if (rbCategory.Checked)
            {
                return ShowBy.Category;
            }
            else if (rbSubCategory.Checked)
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

        /// <summary>
        /// Store current period type to settings table
        /// </summary>
        /// <param name="periodType">Current selected period type</param>
        private void setCurrentPeriodType()
        {
            var currentValue = CboPeriodType.Text.ToUpper();
            var currentSettingId = CboPeriodType.Tag.ToString();
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

            if (!string.IsNullOrWhiteSpace(currentValue))
                libSettings.SaveSetting(currentSettingId, currentValue);
        }

        private string getCurrentSetting(string settingId)
        {
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetKey(settingId);
            if (setting != null)
                return setting.KeyValue;
            else
                return "";
        }

        /// <summary>
        /// Store value compare with
        /// </summary>
        /// <param name="periodType">Current selected compare type</param>
        private void setCurrentCompareWithType()
        {
            var currentValue = CboCompareWith.Text.ToUpper();
            var currentSettingId = CboCompareWith.Tag.ToString();
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

            if (!string.IsNullOrWhiteSpace(currentValue))
               libSettings.SaveSetting(currentSettingId, currentValue);
        }

        private DateTime getCurrentDateByDatePicker(DateTimePicker dateTimePicker)
        {
            var currentSettingId = getCurrentDtpTag(dateTimePicker);
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            var setting = libSettings.GetKey(currentSettingId);
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

        private void setCurrentDtpTag(DateTimePicker dtp)
        {
            var currentValue = dtp.Value.ToString("yyyyMMdd");
            var currentSettingId = getCurrentDtpTag(dtp);
            var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
            libSettings.SaveSetting(currentSettingId, currentValue);
        }

        private string getCurrentDtpTag(DateTimePicker dtp)
        {
            var currentPeriodType = CboPeriodType.Text.ToUpper();
            var currentSettingId = dtp.Tag.ToString();
            if (!string.IsNullOrWhiteSpace(currentPeriodType))
                currentSettingId += $"_{currentPeriodType}";

            return currentSettingId;
        }

        private void exportToExcel(EnergyUse.Models.EnergyType energyType)
        {
            int totalCols;
            string message;

            if (_chartDefaultPerPeriod == null) return;

            var exportFileName = $"ChartDefault_{DateTime.Now:yyyyMMddHHmmss}_{energyType.Name}.xlsx";
            var exportDirectory = string.Empty;

            var dataList = _chartDefaultPerPeriod.GetDataList();
            if (dataList.Count == 0)
            {
                message = Managers.Languages.GetResourceString("NoDataToExport", "No data to export");
                MessageBox.Show(this, message);
            }
            else
            {
                SaveFileDialog sf = new SaveFileDialog();
                sf.FileName = Path.Combine(exportDirectory, exportFileName);

                if (sf.ShowDialog() == DialogResult.OK)
                {
                    // Now here's our save folder
                    exportDirectory = Path.GetDirectoryName(sf.FileName);
                    exportFileName = Path.GetFileName(sf.FileName);
                }

                if (string.IsNullOrWhiteSpace(exportDirectory))
                    return;

                if (!Directory.Exists(Path.GetDirectoryName(exportDirectory)))
                    Directory.CreateDirectory(Path.GetDirectoryName(exportDirectory));

                System.IO.FileInfo f = new System.IO.FileInfo(Path.Combine(exportDirectory, exportFileName));
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
                using (ExcelPackage excelPackage = new ExcelPackage())
                {
                    ExcelWorksheet energieExport = excelPackage.Workbook.Worksheets.Add("ChartDefaultData");

                    if (energyType.HasEnergyReturn && energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                        .Select(s => new
                        {
                            s.PeriodType,
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
                            s.CorrectionFactor
                        });
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }
                    else if (energyType.HasEnergyReturn && !energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                        .Select(s => new
                        {
                            s.PeriodType,
                            Value_X = s.ValueX,
                            Value_Y_Normal = s.ValueYNormal,
                            Value_Y_Return_Normal = s.ValueYReturnNormal,
                            Value_Y_Monetary = s.ValueMonetaryY,
                            ValueY_Monetary_Normal = s.ValueYMonetaryNormal,
                            Value_Y_Monetary_Return_Normal = s.ValueYMonetaryReturnNormal,
                            Rate_Normal = s.RateNormal,
                            Rate_Return_Normal = s.RateReturnNormal,
                            s.CorrectionFactor
                        });
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }
                    else if (!energyType.HasEnergyReturn && !energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                            .Select(s => new
                            {
                                s.PeriodType,
                                Value_X = s.ValueX,
                                Value_X_Date = s.ValueXDate,
                                Value_Y_Normal = s.ValueYNormal,
                                Value_Y_Monetary = s.ValueMonetaryY,
                                ValueY_Monetary_Normal = s.ValueYMonetaryNormal,
                                Rate_Normal = s.RateNormal,
                                s.CorrectionFactor
                            });
                        energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                    }
                    else if (!energyType.HasEnergyReturn && energyType.HasNormalAndLow)
                    {
                        var exportResult = dataList
                        .Select(s => new
                        {
                            s.PeriodType,
                            Value_X = s.ValueX,
                            Value_X_Date = s.ValueXDate,
                            Value_Y_Low = s.ValueYLow,
                            Value_Y_Normal = s.ValueYNormal,
                            Value_Y_Monetary = s.ValueMonetaryY,
                            Value_Y_Monetary_Low = s.ValueYMonetaryLow,
                            ValueY_Monetary_Normal = s.ValueYMonetaryNormal,
                            Rate_Low = s.RateLow,
                            Rate_Normal = s.RateNormal,
                            s.CorrectionFactor
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
