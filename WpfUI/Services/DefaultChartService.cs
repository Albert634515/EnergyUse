using EnergyUse.Common.Enums;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
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

                if (sm.IsLine)
                {
                    var line = new LineSeries<DateTimePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        ScalesYAt = sm.ScalesYAt,
                        LineSmoothness = 0,
                        GeometrySize = 12,
                        GeometryFill = new SolidColorPaint(SKColors.White),
                        Fill = null,
                    };

                    // Een lege kleur betekent in WinForms: gebruik de standaard lijnkleur,
                    // niet een volledig transparante lijn.
                    var color = sm.Color.IsEmpty || sm.Color.A == 0
                        ? GetFallbackLineColor(sm.SeriesKey)
                        : new SKColor(sm.Color.R, sm.Color.G, sm.Color.B, sm.Color.A);
                    var paint = new SolidColorPaint(color) { StrokeThickness = 2 };
                    line.Stroke = paint;
                    line.GeometryStroke = paint;

                    result.Add(line);
                }
                else if (sm.IsStacked)
                {
                    result.Add(new StackedColumnSeries<DateTimePoint>
                    {
                        Values = points,
                        Name = sm.Name,
                        ScalesYAt = sm.ScalesYAt,
                        StackGroup = 0,
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
                        Fill = new SolidColorPaint((uint)sm.Color.ToArgb())
                    });
                }
            }

            return result;
        }

        private static SKColor GetFallbackLineColor(string seriesKey)
        {
            if (seriesKey.StartsWith("GrossValue", StringComparison.Ordinal))
                return SKColors.SlateGray;
            if (seriesKey.StartsWith("ReturnAvg", StringComparison.Ordinal))
                return seriesKey.Contains("Low", StringComparison.Ordinal) ? SKColors.YellowGreen : SKColors.DarkCyan;
            if (seriesKey.StartsWith("Avg", StringComparison.Ordinal))
                return seriesKey.Contains("Low", StringComparison.Ordinal) ? SKColors.Turquoise : SKColors.DodgerBlue;

            return SKColors.SlateGray;
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

            var axisPosition = AxisPosition.End;
            foreach (var et in energyTypes)
            {
                axisPosition = axisPosition == AxisPosition.End ? AxisPosition.Start : AxisPosition.End;

                axes.Add(new Axis
                {
                    Name = et.Unit.Description,
                    MinLimit = et.HasEnergyReturn ? null : 0,
                    Position = axisPosition,
                    TextSize = 15
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
