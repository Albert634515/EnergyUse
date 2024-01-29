using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;

namespace EnergyUse.Core.Manager
{
    public class LibGraphGeneral
    {
        public static string GetColorKey(ChartSeriesType chartSeriesType, long energytypeId)
        {
            string colorKey;

            string chartSeriesName = chartSeriesType.ToString();

            chartSeriesName = chartSeriesName.Replace("Predicted", "").Replace("Avg", "").Replace("Value", "");

            if (chartSeriesType == ChartSeriesType.GrossValue)
                colorKey = $"Color{chartSeriesName}";
            else if (chartSeriesType == ChartSeriesType.Produced)
                colorKey = $"Color{ChartSeriesType.ReturnNormal}{energytypeId}";
            else if (chartSeriesType == ChartSeriesType.Consumed
                        || chartSeriesType == ChartSeriesType.Total
                        || chartSeriesType == ChartSeriesType.AvgNormal
                        || chartSeriesType == ChartSeriesType.AvgValueNormal
                        )
                colorKey = $"Color{ChartSeriesType.Normal}{energytypeId}";
            else if (chartSeriesType == ChartSeriesType.AvgLow || chartSeriesType == ChartSeriesType.AvgValueLow)
                colorKey = $"Color{ChartSeriesType.Low}{energytypeId}";
            else
                colorKey = $"Color{chartSeriesName}{energytypeId}";

            return colorKey;
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

        public static int GetStartPeriod(Period periodType)
        {
            int startPeriod;

            switch (periodType)
            {
                case Period.Day:
                    startPeriod = DateTime.Now.Year;
                    break;
                case Period.Week:
                    startPeriod = DateTime.Now.Year;
                    break;
                case Period.Month:
                    startPeriod = DateTime.Now.Year;
                    break;
                case Period.Year:
                    startPeriod = DateTime.Now.Year - 5;
                    break;
                default:
                    startPeriod = DateTime.Now.Year - 5;
                    break;
            }

            return startPeriod;
        }

        public static int GetEndPeriod(Period periodType)
        {
            int startPeriod;

            switch (periodType)
            {
                case Period.Day:
                    startPeriod = DateTime.Now.Year;
                    break;
                case Period.Month:
                    startPeriod = DateTime.Now.Year;
                    break;
                case Period.Year:
                    startPeriod = DateTime.Now.Year;
                    break;
                default:
                    startPeriod = DateTime.Now.Year;
                    break;
            }

            return startPeriod;
        }

        public static Period GetPeriodType(string item)
        {
            if (item.ToUpper() == "DAY")
                return Period.Day;
            else if (item.ToUpper() == "WEEK")
                return Period.Week;
            else if (item.ToUpper() == "MONTH")
                return Period.Month;
            else if (item.ToUpper() == "YEAR")
                return Period.Year;
            else
                return Period.Unknown;
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
                    //chartGroup = ChartGroup.Return;
                    chartGroup = ChartGroup.Consumed;
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
                    //chartGroup = ChartGroup.Return;
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.ReturnLow:
                    ///chartGroup = ChartGroup.Return;
                    chartGroup = ChartGroup.Consumed;
                    break;
                case ChartSeriesType.ReturnAvgLowDelivery:
                    chartGroup = ChartGroup.Return;
                    break;
                case ChartSeriesType.ReturnNormal:
                    //chartGroup = ChartGroup.Return;
                    chartGroup = ChartGroup.Consumed;
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