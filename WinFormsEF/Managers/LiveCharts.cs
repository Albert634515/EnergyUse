using EnergyUse.Common.Enums;
using EnergyUse.Core.Graphs.LiveCharts;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using SkiaSharp.Views.Desktop;
using System.Collections.ObjectModel;

namespace WinFormsEF.Managers;

public class LiveCharts
{
    public static string GetYaxisLabel(ShowType showType, EnergyUse.Models.EnergyType energyType)
    {
        var yAxisLabel = string.Empty;

        switch (showType)
        {
            case ShowType.Rate:
                yAxisLabel = energyType.Unit.Description;
                break;
            case ShowType.Value:
                var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
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
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());

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
        cartesianChart.Title = EnergyUse.Core.Graphs.LiveCharts.General.GetTitle(title);
        cartesianChart.Series = new ObservableCollection<ISeries>(serieslist);

        return cartesianChart;
    }

    public static List<Axis> GetYAxisList(bool setMinLimitZero, string label = null)
    {
        return new List<Axis> { GetYAxis(setMinLimitZero, label) };
    }

    public static Axis GetYAxis(bool setMinLimitZero, string label = null, AxisPosition axisPosition = AxisPosition.Start)
    {
        var libSettings = new EnergyUse.Core.Manager.LibSettings(Managers.Config.GetDbFileName());
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