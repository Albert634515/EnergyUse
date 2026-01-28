using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.VisualElements;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp;
using SkiaSharp.Views.Desktop;
using System.Collections.ObjectModel;

namespace WinFormsEF.Managers;

public class LiveCharts
{
    // Cache LibSettings so we don't construct it repeatedly.
    // Lazy ensures thread-safe, on-demand creation.
    private static readonly Lazy<EnergyUse.Core.Manager.LibSettings> _libSettings =
        new(() => new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName()));

    public static List<ISeries> ConvertSeriesModelsToISeries(List<SeriesModel> models)
    {
        var result = new List<ISeries>();

        foreach (var sm in models)
        {
            if (sm == null) continue;

            // build date points
            var datePoints = new List<DateTimePoint>();
            foreach (var dp in sm.Points ?? Enumerable.Empty<DatePoint>())
                datePoints.Add(new DateTimePoint(dp.DateTime, dp.Value));

            // determine chart series type from SeriesKey ("ChartSeriesType_energyId")
            ChartSeriesType? chartSeriesType = null;
            var keyParts = (sm.SeriesKey ?? "").Split('_');
            if (keyParts.Length > 0 && Enum.TryParse<ChartSeriesType>(keyParts[0], out var parsed))
                chartSeriesType = parsed;

            bool isLine = sm.IsLine;

            // create series instance
            if (isLine)
            {
                var line = new LineSeries<DateTimePoint>
                {
                    Values = new ObservableCollection<DateTimePoint>(datePoints),
                    Name = sm.Name,
                    ScalesYAt = sm.ScalesYAt,
                    LineSmoothness = 0,
                    Fill = null,
                    //GeometryFill = null,
                    //GeometryStroke = null
                };

                if (sm.Color != System.Drawing.Color.Empty)
                    line.Stroke = new SolidColorPaint((uint)sm.Color.ToArgb()) { StrokeThickness = 2 };

                result.Add(line);
            }
            else
            {
                // column or stacked column
                if (sm.IsStacked)
                {
                    var stacked = new StackedColumnSeries<DateTimePoint>
                    {
                        Values = new ObservableCollection<DateTimePoint>(datePoints),
                        Name = sm.Name,
                        ScalesYAt = sm.ScalesYAt,
                        StackGroup = 0
                    };

                    if (sm.Color != System.Drawing.Color.Empty)
                        stacked.Fill = new SolidColorPaint((uint)sm.Color.ToArgb());
                    result.Add(stacked);
                }
                else
                {
                    var column = new ColumnSeries<DateTimePoint>
                    {
                        Values = new ObservableCollection<DateTimePoint>(datePoints),
                        Name = sm.Name,
                        ScalesYAt = sm.ScalesYAt
                    };

                    if (sm.Color != System.Drawing.Color.Empty)
                        column.Fill = new SolidColorPaint((uint)sm.Color.ToArgb());
                    result.Add(column);
                }
            }
        }

        return result;
    }

    #region Lines

    public static ISeries GetLineSerieAvgDate(ChartSeriesType serieName, Color color, int scalesYAt)
    {
        var strokeThickness = 1;
        var strokeDashArray = new float[] { 3 * strokeThickness, 2 * strokeThickness };
        var effect = new DashEffect(strokeDashArray);
        var fillColor = (uint)color.ToArgb();

        return new LineSeries<DateTimePoint>
        {
            Name = serieName.GetDescription(),
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null,
            Stroke = new SolidColorPaint
            {
                Color = fillColor,
                StrokeCap = SKStrokeCap.Round,
                StrokeThickness = strokeThickness,
                PathEffect = effect
            },
            ScalesYAt = scalesYAt
        };
    }

    public static ISeries GetLineDataSerie(ChartSeriesType serieName, Color color, int scalesYAt)
    {
        var strokeThickness = 1;
        var fillColor = (uint)color.ToArgb();

        return new LineSeries<ObservablePoint>
        {
            Name = serieName.GetDescription(),
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null,
            Stroke = new SolidColorPaint
            {
                Color = fillColor,
                StrokeCap = SKStrokeCap.Round,
                StrokeThickness = strokeThickness
            },
            ScalesYAt = scalesYAt
        };
    }



    public static ISeries GetLineDateSerie(ChartSeriesType serieName, int scalesYAtint)
    {
        var lineSerie = GetDefaultLineDateSeries(scalesYAtint);
        lineSerie.Name = serieName.GetDescription();

        return lineSerie;
    }

    public static ISeries GetDefaultLineDateSeries(int scalesYAt, int strokeThickness = 1, bool addSTroke = true)
    {
        var ln = new LineSeries<DateTimePoint>
        {
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null,
            LineSmoothness = 0,
            ScalesYAt = scalesYAt
        };

        if (addSTroke)
        {
            ln.Stroke = new SolidColorPaint
            {
                Color = SKColors.CornflowerBlue,
                StrokeCap = SKStrokeCap.Round,
                StrokeThickness = strokeThickness
            };
        }

        return ln;
    }

    #endregion

    #region Column

    public static ISeries GetColumnDateSerie(ChartSeriesType serieName, Color color, int scalesYAt)
    {
        var fillColor = (uint)color.ToArgb();
        var serie = new ColumnSeries<DateTimePoint>
        {
            Name = serieName.GetDescription(),
            DataLabelsPaint = new SolidColorPaint(SKColors.Black),
            ScalesYAt = scalesYAt
        };

        if (color != Color.Empty)
            serie.Fill = new SolidColorPaint(fillColor, 0);

        return serie;
    }

    #endregion

    #region StackedColumns

    public static ISeries GetStackedColumnDateSerie(ChartSeriesType serieName, long energyTypeId, int stackGroupId, Color color, int scalesYAt)
    {
        var fillColor = (uint)color.ToArgb();
        var serie = new StackedColumnSeries<DateTimePoint>
        {
            Name = serieName.GetDescription(),
            Stroke = new SolidColorPaint(fillColor) { StrokeThickness = 0 },
            DataLabelsPaint = new SolidColorPaint(SKColors.Black),
            DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Middle,
            StackGroup = (int)(stackGroupId + energyTypeId),
            ScalesYAt = scalesYAt
        };

        if (color != Color.Empty)
            serie.Fill = new SolidColorPaint(fillColor, 0);

        return serie;
    }

    #endregion

    #region Other

    public static LabelVisual GetTitle(string title)
    {
        var labelVisual = new LabelVisual
        {
            Text = title,
            TextSize = 15,
            Padding = new LiveChartsCore.Drawing.Padding(2),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
        };

        return labelVisual;
    }

    #endregion

    public static string GetYaxisLabel(ShowType showType, EnergyUse.Models.EnergyType energyType)
    {
        var yAxisLabel = string.Empty;

        switch (showType)
        {
            case ShowType.Rate:
                yAxisLabel = energyType.Unit.Description;
                break;
            case ShowType.Value:
                var libSettings = _libSettings.Value;
                var currency = libSettings.GetSetting("Currency");
                yAxisLabel = currency.KeyValue;
                break;
            case ShowType.AvgRate:
                yAxisLabel = energyType.Unit.Description;
                break;
            case ShowType.AvgValue:
                yAxisLabel = energyType.Unit.Description;
                break;
            case ShowType.Efficiency:
                yAxisLabel = "%";
                break;
            case ShowType.Unit:
                yAxisLabel = energyType.Unit.Description;
                break;
            case ShowType.Unknown:
                yAxisLabel = string.Empty;
                break;
            default:
                break;
        }

        return yAxisLabel;
    }

    public static CartesianChart GetDefaultChart(Period periodType, List<ISeries> serieslist, string title, bool setMinLimitZero, string label = null)
    {
        var cartesianChart = GetChart(serieslist, title, setMinLimitZero, label);
        cartesianChart.XAxes = GetAxisList(periodType);

        return cartesianChart;
    }

    public static CartesianChart GetDefaultChart(Period periodType, List<ISeries> serieslist, List<Axis> axislist, string title, bool setMinLimitZero, string label = null)
    {
        var cartesianChart = GetChart(serieslist, title, setMinLimitZero, label);
        cartesianChart.XAxes = GetAxisList(periodType);
        cartesianChart.YAxes = axislist;

        return cartesianChart;
    }

    public static List<Axis> GetAxisList(Period periodType)
    {
        var axisList = new List<Axis>();
        axisList.Add(getAxis(periodType));

        return axisList;
    }

    private static Axis getAxis(Period periodType)
    {
        Axis axis;

        if (periodType == Period.Day)
            axis = ChartAxis.GetDateAxis();
        else if (periodType == Period.Year)
            axis = ChartAxis.GetYearAxis();
        else if (periodType == Period.Month)
            axis = ChartAxis.GetMonthAxis();
        else if (periodType == Period.Week)
            axis = ChartAxis.GetWeekAxis();
        else
            axis = ChartAxis.GetDataAxis();

        return axis;
    }

    public static CartesianChart GetCompareChart(Period periodType, List<ISeries> serieslist, string title, string label, bool setMinLimitZero)
    {
        var cartesianChart = GetChart(serieslist, title, setMinLimitZero, label);
    
        if (periodType == Period.Day)
            cartesianChart.XAxes = ChartAxis.GetDateAxisList(ChartAxis.defaultYearSpan);
        else if (periodType == Period.Year)
            cartesianChart.XAxes = ChartAxis.GetYearAxisList();
        else if (periodType == Period.Month)
            cartesianChart.XAxes = ChartAxis.GetMonthAxisList(ChartAxis.defaultYearSpan);
        else if (periodType == Period.Week)
            cartesianChart.XAxes = ChartAxis.GetWeekAxisList(ChartAxis.defaultYearSpan);
        else
            cartesianChart.XAxes = ChartAxis.GetDataAxisList();

        return cartesianChart;
    }

    public static CartesianChart GetChart(List<ISeries> serieslist, string title, bool setMinLimitZero, string label)
    {
        var libSettings = _libSettings.Value;

        var cartesianChart = new CartesianChart
        {
            Visible = true,
            Dock = DockStyle.Fill,
            Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom
        };

        cartesianChart.YAxes = GetYAxisList(setMinLimitZero, label);
        cartesianChart.Dock = DockStyle.Fill;            
        cartesianChart.BackColor = libSettings.GetChartColor("BackgroundColorChart");
        cartesianChart.ForeColor = libSettings.GetChartColor("ForeColorChart");
        cartesianChart.Title = GetTitle(title);
        cartesianChart.Series = new ObservableCollection<ISeries>(serieslist ?? new List<ISeries>());

        return cartesianChart;
    }

    public static List<Axis> GetYAxisList(bool setMinLimitZero, string label = null)
    {
        return new List<Axis> { GetYAxis(setMinLimitZero, label) };
    }

    public static Axis GetYAxis(bool setMinLimitZero, string label = null, AxisPosition axisPosition = AxisPosition.Start)
    {
        var libSettings = _libSettings.Value;
        var lineColor = libSettings.GetColorSetting("LineColorChart", Color.LightGray).ToSKColor();
        var labelColor = libSettings.GetColorSetting("LabelsYColorChart", Color.Black).ToSKColor();

        var axis = new Axis
        {
            TextSize = 15,
            LabelsPaint = new SolidColorPaint { Color = labelColor },
            SeparatorsPaint = new SolidColorPaint { Color = lineColor, StrokeThickness = 2 },
            Position = axisPosition,
            Name = label
        };

        if (setMinLimitZero)
            axis.MinLimit = 0;

        return  axis;
    }
}