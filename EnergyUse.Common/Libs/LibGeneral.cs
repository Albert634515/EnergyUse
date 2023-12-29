using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;

namespace EnergyUse.Common.Libs
{
    public class LibGeneral
    {
        public static string GetDefaultDataFile(string currentFile)
        {
            string defaultFile;

            if (string.IsNullOrWhiteSpace(currentFile))
                throw new Exception("No file passed");

            string? directory = Path.GetDirectoryName(currentFile);
            if (directory == null || !Directory.Exists(directory))
                return "";

            if (!string.IsNullOrWhiteSpace(currentFile))
                defaultFile = Path.Combine(directory, "NewFile.db");
            else
                defaultFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "EnergyUse", "EnergyUse.db");

            return defaultFile;
        }

        public static Period GetPeriodType(string periodType)
        {
            if (periodType.ToUpper() == "DAY")
                return Period.Day;
            else if (periodType.ToUpper() == "WEEK")
                return Period.Week;
            else if (periodType.ToUpper() == "MONTH")
                return Period.Month;
            else if (periodType.ToUpper() == "YEAR")
                return Period.Year;
            else
                return Period.Unknown;
        }

        public static void OpenCreatedFile(string fileName)
        {
            FileInfo fi = new(fileName);
            if (fi.Exists)
            {
                System.Diagnostics.Process.Start($"{fileName}");
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// Quantity reduction cumulative
        /// </summary>
        /// <param name="quantityReductionPercentage"></param>
        /// <param name="years"></param>
        /// <returns>Quantity Reduction cumulativ</returns>
        public static decimal GetQuantityReduction(decimal quantityReductionPercentage, int years)
        {
            decimal quantityReduction = 0;

            if (quantityReductionPercentage != 0)
            {
                quantityReduction = 100;
                for (int j = 0; j < years; j++)
                    quantityReduction *= ((decimal)(100 - quantityReductionPercentage) / 100);
            }

            return Math.Round(quantityReduction, 2);
        }

        public static int GetMaxAvg(Period periodType, DateTime endDate, int periodCount)
        {
            int maxAvgValue;

            maxAvgValue = 0;

            switch (periodType)
            {
                case Period.Year:
                    maxAvgValue = endDate.Year;
                    if (endDate.Year == DateTime.Now.Year && endDate < new DateTime(DateTime.Now.Year, 12, 31) && periodCount > 1)
                        maxAvgValue = endDate.Year - 1;
                    break;
                case Period.Month:
                    maxAvgValue = int.Parse(endDate.ToString("yyyyMM"));
                    if (endDate.Year == DateTime.Now.Year && endDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)) && periodCount > 1)
                        maxAvgValue = int.Parse($"{endDate.Year - 1}{DateTime.Now.Month.ToString().PadLeft(2, '0')}");
                    break;
                case Period.Week:
                    maxAvgValue = int.Parse($"{endDate:yyyy}{endDate.GetWeekNumber().ToString().PadLeft(2, '0')}");
                    if (endDate.Year == DateTime.Now.Year && endDate.GetWeekNumber() < DateTime.Now.GetWeekNumber() && periodCount > 1)
                        maxAvgValue = int.Parse($"{endDate:yyyy}{DateTime.Now.GetWeekNumber().ToString().PadLeft(2, '0')}");
                    break;
                case Period.Day:
                    maxAvgValue = endDate.Day;
                    if (endDate.Day < DateTime.Now.Day && periodCount > 1)
                        maxAvgValue = DateTime.Now.Day;
                    break;
            }

            return maxAvgValue;
        }

        public static ChartGroup GetChartGroup(ChartSeriesType serieName)
        {
            var chartGroup = ChartGroup.Unknown;

            switch (serieName)
            {
                case ChartSeriesType.Low:
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.AvgLow:
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.AvgValueLow:
                    chartGroup = ChartGroup.Low;
                    break;
                case ChartSeriesType.LowPredicted:
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.ReturnLowPredicted:
                    chartGroup = ChartGroup.Return;
                    break;
                case ChartSeriesType.Normal:
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.AvgNormal:
                    chartGroup = ChartGroup.Normal;
                    break;
                case ChartSeriesType.AvgValueNormal:
                    break;
                case ChartSeriesType.NormalPredicted:
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.ReturnNormalPredicted:
                    chartGroup = ChartGroup.Return;
                    break;
                case ChartSeriesType.ReturnLow:
                    chartGroup = ChartGroup.Return;
                    break;
                case ChartSeriesType.ReturnAvgLowDelivery:
                    chartGroup = ChartGroup.Return;
                    break;
                case ChartSeriesType.ReturnNormal:
                    chartGroup = ChartGroup.Return;
                    break;
                case ChartSeriesType.ReturnAvgNormalDelivery:
                    chartGroup = ChartGroup.Return;
                    break;
                case ChartSeriesType.Consumed:
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.ConsumedPredicted:
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.Produced:
                    chartGroup = ChartGroup.Produced;
                    break;
                case ChartSeriesType.ProducedPredicted:
                    chartGroup = ChartGroup.Produced;
                    break;
                case ChartSeriesType.Total:
                    chartGroup = ChartGroup.Total;
                    break;
                case ChartSeriesType.TotalPredicted:
                    chartGroup = ChartGroup.Total;
                    break;
                default:
                    break;
            }

            return chartGroup;
        }
    }
}
