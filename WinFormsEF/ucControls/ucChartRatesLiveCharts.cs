using System.Data;
using EnergyUse.Common.Enums;
using EnergyUse.Common.Libs;
using EnergyUse.Core.UnitOfWork;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView.WinForms;
using OfficeOpenXml;

namespace WinFormsEF.ucControls
{
    public partial class ucChartRatesLiveCharts : UserControl
    {
        #region ChartProperties

        private bool initChart;
        private EnergyUse.Core.Graphs.LiveCharts.Rates _chartCompare { get; set; }
        private EnergyUse.Core.UnitOfWork.Graphs _unitOfWork { get; set; }
        private EnergyUse.Models.Address CurrentAddress { get; set; }
        private EnergyUse.Models.EnergyType CurrentEnergyType { get; set; }

        private CartesianChart cartesianChart;

        #endregion

        public ucChartRatesLiveCharts(EnergyUse.Models.Address selectedAddress, EnergyUse.Models.EnergyType selectedEnergyType)
        {
            InitializeComponent();

            initChart = true;
            _unitOfWork = new EnergyUse.Core.UnitOfWork.Graphs(Managers.Config.GetDbFileName());
            CurrentAddress = selectedAddress;
            CurrentEnergyType = selectedEnergyType;

            ResetCostCategory(CurrentEnergyType);
            initChart = false;

            ResetChart();
        }

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
            ResetChart();
        }

        private void chkListCostCategory_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (initChart == false)
            {
                CheckedListBox clb = (CheckedListBox)sender;
                // Switch off event handler
                clb.ItemCheck -= chkListCostCategory_ItemCheck;
                clb.SetItemCheckState(e.Index, e.NewValue);
                // Switch on event handler
                clb.ItemCheck += chkListCostCategory_ItemCheck;

                LoadChart();
            }
        }

        private void rbType_CheckedChanged(object sender, EventArgs e)
        {
            initChart = true;
            ResetCostCategory(CurrentEnergyType);
            initChart = false;

            LoadChart();
        }

        #endregion

        #region Methods

        public void LoadChart()
        {
            if (CurrentEnergyType != null)
            {
                ParameterGraph graphParameter = new ParameterGraph();
                graphParameter.Address = CurrentAddress;
                graphParameter.EnergyTypeList = new() { CurrentEnergyType };
                graphParameter.DbName = Managers.Config.GetDbFileName();
                graphParameter.ShowType = GetShowType();
                graphParameter.From = dtpFrom.Value;
                graphParameter.Till = dtpTill.Value;
                graphParameter.CostCategoryList = GetSelecteCostCategories();
                graphParameter.TarifGroupId = 1;

                Cursor = Cursors.WaitCursor;
                if (rbUnit.Checked)
                    GetChartSeriesPerCostCategoryAndUnit(graphParameter);
                else
                    GetChartSeriesPerCostCategory(graphParameter);
                Cursor = Cursors.Default;
            }
        }

        private void AddChart(Period periodType, List<ISeries> serieslist)
        {
            if (serieslist.Count == 0)
                return;

            string title = Managers.Languages.GetResourceString("ChartRatesTitle", "Rates");

            panel1.Controls.Clear();
            cartesianChart = Managers.LiveCharts.GetDefaultChart(periodType, serieslist, title, !CurrentEnergyType.HasEnergyReturn);

            panel1.Controls.Add(cartesianChart);
        }

        private void GetChartSeriesPerCostCategory(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Rates(graphParameter);

            AddChart(Period.Day, _chartCompare.GetSeries());
        }

        private void GetChartSeriesPerCostCategoryAndUnit(ParameterGraph graphParameter)
        {
            _chartCompare = new EnergyUse.Core.Graphs.LiveCharts.Rates(graphParameter);

            AddChart(Period.Day, _chartCompare.GetSeries());
        }

        private List<EnergyUse.Models.CostCategory> GetSelecteCostCategories()
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
            CurrentAddress = address;
            CurrentEnergyType = energyType;
            ResetChart();
        }

        public void ResetCurrentEnergyType(EnergyUse.Models.EnergyType energyType)
        {
            CurrentEnergyType = energyType;
            ResetCostCategory(CurrentEnergyType);
            ResetChart();
        }

        private void ResetCostCategory(EnergyUse.Models.EnergyType energyType)
        {
            List<EnergyUse.Models.CostCategory> costCategories = new();
            if (energyType != null)
                costCategories = _unitOfWork.CostCategoryRepo.SelectByEnergyTypeId(energyType.Id).ToList();

            if (rbUnit.Checked)
                costCategories = costCategories.Where(x => x.EnergySubType.Id >= 1 && x.EnergySubType.Id <= 4).ToList();

            chkListCostCategory.Items.Clear();
            foreach (var item in costCategories)
            {
                chkListCostCategory.Items.Add(item, true);
            }

            ((ListBox)chkListCostCategory).DisplayMember = "Name";
        }

        public void ResetChart()
        {
            if (initChart)
                return;

            dtpFrom.Value = new DateTime(DateTime.Now.AddYears(-4).Year, 1, 1); ;
            dtpTill.Value = new DateTime(DateTime.Now.Year, 12, 31);

            LoadChart();
        }

        private void ExportToExcel(EnergyUse.Models.EnergyType energyType)
        {
            string exportFileName, exportDirectory, message;
            int totalCols, totalRows;

            exportFileName = $"ChartRates_{DateTime.Now:yyyyMMddHHmmss}_{energyType.Name}.xlsx";
            exportDirectory = string.Empty;

            var dataList = _chartCompare.GetDataList();
            if (dataList != null && dataList.Count == 0)
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
                    ExcelWorksheet energieExport = excelPackage.Workbook.Worksheets.Add("ChartRatesData");

                    var exportResult = dataList
                    .Select(s => new
                    {
                        Value_X = s.ValueXString,
                        Value_X_Date = s.ValueXDate.ToShortDateString(),
                        Value_Y_Value = s.ValueY2
                    });
                    energieExport.Cells[1, 1].LoadFromCollection(exportResult, true);

                    // Clean up column headers
                    totalCols = energieExport.Dimension.End.Column;
                    for (int i = 1; i <= totalCols; i++)
                        energieExport.Cells[1, i].Value = energieExport.Cells[1, i].Value.ToString().Replace("_", " ");

                    totalRows = dataList.Count + 1;
                    for (int i = 1; i <= totalRows; i++)
                        energieExport.Cells[$"B{i}"].Style.Numberformat.Format = "yyyy-mm-dd";

                    energieExport.Cells[energieExport.Dimension.Address].AutoFitColumns();
                    excelPackage.SaveAs(f);

                    LibGeneral.OpenCreatedFile(Path.Combine(exportDirectory, exportFileName));
                }
            }
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

        #endregion
    }
}