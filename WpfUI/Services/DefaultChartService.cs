using EnergyUse.Common.Enums;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using WpfUI.Managers;
using WpfUI.Models;

namespace WpfUI.Services
{
    public class DefaultChartService : ChartBaseService
    {
        public DefaultChartResult BuildChart(
            Address address,
            IEnumerable<EnergyType> energyTypes,
            Period period,
            DateTime from,
            DateTime till,
            bool showStacked,
            bool showAvg,
            bool predictMissing,
            ShowBy showBy,
            ShowType showType)
        {
            var p = new ParameterGraph
            {
                Address = address,
                EnergyTypeList = energyTypes.ToList(),
                DbName = Config.GetDbFileName(),
                From = from,
                Till = till,
                ShowStacked = showStacked,
                ShowAvg = showAvg,
                PredictMissingData = predictMissing,
                PeriodType = period,
                ShowBy = showBy,
                ShowType = showType
            };

            var chart = new Default(p);

            var seriesModels = chart.GetSeries();
            var series = ConvertDefaultChartSeries(seriesModels);

            var labels = chart.GetResultLabelsPerPeriod(energyTypes.First());

            var xAxis = CreateDateTimeAxis(period, seriesModels);
            var yAxes = CreateYAxes(energyTypes);

            return new DefaultChartResult(series, new List<Axis> { xAxis }, yAxes, labels);
        }

        private List<ISeries> ConvertDefaultChartSeries(List<SeriesModel> models)
        {
            var result = new List<ISeries>();

            foreach (var sm in models)
            {
                if (sm?.Points == null || sm.Points.Count == 0)
                    continue;

                var points = sm.Points
                    .Select(dp => new DateTimePoint(dp.DateTime, dp.Value))
                    .ToList();

                bool isLine =
                    sm.SeriesKey.StartsWith("GrossValue") ||
                    sm.SeriesKey.Contains("Avg");

                if (isLine)
                {
                    result.Add(new LineSeries<DateTimePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        ScalesYAt = sm.ScalesYAt,
                        LineSmoothness = 0,
                        GeometryFill = null,
                        GeometryStroke = null,
                        Fill = null,
                        Stroke = new SolidColorPaint((uint)sm.Color.ToArgb())
                        {
                            StrokeThickness = 2
                        }
                    });
                }
                else if (sm.IsStacked)
                {
                    result.Add(new StackedColumnSeries<DateTimePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        ScalesYAt = sm.ScalesYAt,
                        MaxBarWidth = 40,
                        Padding = 0,
                        Fill = new SolidColorPaint((uint)sm.Color.ToArgb())
                    });
                }
                else
                {
                    result.Add(new ColumnSeries<DateTimePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        ScalesYAt = sm.ScalesYAt,
                        MaxBarWidth = 40,
                        Padding = 0,
                        Fill = new SolidColorPaint((uint)sm.Color.ToArgb())
                    });
                }
            }

            return result;
        }

        private Axis CreateDateTimeAxis(Period period, List<SeriesModel> models)
        {
            return period switch
            {
                Period.Day => ChartAxis.GetDateAxis(),
                Period.Week => ChartAxis.GetWeekAxis(),
                Period.Month => ChartAxis.GetMonthAxis(),
                Period.Year => ChartAxis.GetYearAxis(),
                _ => ChartAxis.GetDateAxis()
            };
        }


        private List<Axis> CreateYAxes(IEnumerable<EnergyType> energyTypes)
        {
            var axes = new List<Axis>();

            foreach (var et in energyTypes)
            {
                axes.Add(new Axis
                {
                    Name = et.Unit.Description,
                    Labeler = value =>
                    {
                        if (value > 1000)
                            return (Math.Round(value / 1000) * 1000).ToString("N0");
                        return (Math.Round(value / 100) * 100).ToString("N0");
                    },
                    MinLimit = et.HasEnergyReturn ? null : 0,
                    TextSize = 12
                });
            }

            return axes;
        }

        public void ExportToExcel(EnergyType energyType)
        {
            // placeholder zodat bestaande aanroep compileert
        }
    }
}