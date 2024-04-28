using EnergyUse.Models.Common;
using OfficeOpenXml;
using System.Globalization;

namespace EnergyUse.Core.Manager
{
    public class LibExport
    {
        public static void ExportDataDefaultLiveChartToExcel(string exportFileName, EnergyUse.Models.EnergyType energyType, List<PeriodicData> dataList)
        {

            if (dataList.Count == 0)
                return;

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

                ToExcel(exportResult, exportFileName, "DefaultLiveChart");
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
                ToExcel(exportResult, exportFileName, "DefaultLiveChart");
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
                ToExcel(exportResult, exportFileName, "DefaultLiveChart");
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
                ToExcel(exportResult, exportFileName, "DefaultLiveChart");
            }
            else
            {
                var exportResult = dataList;
                ToExcel(exportResult, exportFileName, "DefaultLiveChart");
            }
        }

        public static void ExportCompareChartToExcel(string exportFileName, EnergyUse.Models.EnergyType energyType, List<PeriodicData> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
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
                    ToExcel(exportResult, exportFileName, "ChartCompareData");
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
                    ToExcel(exportResult, exportFileName, "ChartCompareData");
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
                    ToExcel(exportResult, exportFileName, "ChartCompareData");
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
                    ToExcel(exportResult, exportFileName, "ChartCompareData");
                }
                else
                {
                    var exportResult = dataList;
                    ToExcel(exportResult, exportFileName, "ChartCompareData");
                }
            }
        }

        public static void ExportChartRatesToExcel(string exportFileName, EnergyUse.Models.EnergyType energyType, List<PeriodicData> dataList)
        {
            if (dataList != null && dataList.Count > 0)
            {
                var exportResult = dataList
                .Select(s => new
                {
                    Value_X = s.ValueXString,
                    Value_X_Date = s.ValueXDate.Date,
                    Value_Y_Value = s.ValueY2
                });
                ToExcel(exportResult, exportFileName, "ChartRatesData");
            }
        }

        private static void ToExcel<T>(IEnumerable<T> exportResult, string exportFileName, string sheetName)
        {
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet energyExport = excelPackage.Workbook.Worksheets.Add(sheetName);
                energyExport.Cells[1, 1].LoadFromCollection(exportResult, true);
                                
                var firstResult = exportResult.FirstOrDefault();
                List<System.Reflection.PropertyInfo> properties = firstResult.GetType().GetProperties().ToList();

                // Clean up column headers
                var totalCols = energyExport.Dimension.End.Column;
                for (int i = 1; i <= totalCols; i++)
                {
                    if (!string.IsNullOrWhiteSpace(energyExport.Cells[1, i].Value.ToString()))
                        energyExport.Cells[1, i].Value = energyExport.Cells[1, i].Value.ToString().Replace("_", " ");

                    // Format columns
                    var property = properties[i-1];
                    if (property.PropertyType == typeof(DateTime))
                        energyExport.Column(i).Style.Numberformat.Format = DateTimeFormatInfo.CurrentInfo.ShortDatePattern;

                }

                energyExport.Cells[energyExport.Dimension.Address].AutoFitColumns();
                excelPackage.SaveAs(exportFileName);

                EnergyUse.Common.Libs.LibGeneral.OpenCreatedFile(exportFileName);
            }
        }
    }
}
