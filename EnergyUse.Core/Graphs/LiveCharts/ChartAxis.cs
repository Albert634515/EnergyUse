using EnergyUse.Common.Extensions;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;

namespace EnergyUse.Core.Graphs.LiveCharts;

public class ChartAxis
{
    public const double defaultYearSpan = 365.5;

    public static List<Axis> GetDataAxisList()
    {
        return new List<Axis> { GetDataAxis() };
    }

    public static Axis GetDataAxis()
    {
        return new Axis
        {
            LabelsRotation = 15,
            Position = AxisPosition.Start,
            MinStep = 1,
        };
    }

    public static List<Axis> GetDateAxisList(double value = 1)
    {
        return new List<Axis> { GetDateAxis(value) };
    }

    public static Axis GetDateAxis(double value = 1)
    {
        return new DateTimeAxis(TimeSpan.FromDays(value), date => date.ToString("dd/MM/yyyy"))
        {
            LabelsRotation = 20,
            Position = AxisPosition.Start,
            MinStep = TimeSpan.FromDays(value).Ticks,
            UnitWidth = TimeSpan.FromDays(value).Ticks
        };
    }

    public static List<Axis> GetWeekAxisList(double value = (defaultYearSpan / 53))
    {
        return new List<Axis> { GetWeekAxis(value) };
    }

    public static Axis GetWeekAxis(double value = (defaultYearSpan / 53))
    {
        return new DateTimeAxis(TimeSpan.FromDays(value), date => date.YearWeekFormat())
        {
            LabelsRotation = 20,
            Position = AxisPosition.Start,
            MinStep = TimeSpan.FromDays(value).Ticks,
            UnitWidth = TimeSpan.FromDays(value).Ticks
        };
    }

    public static List<Axis> GetMonthAxisList(double value = (defaultYearSpan / 12))
    {
        return new List<Axis> { GetMonthAxis(value) };
    }

    public static Axis GetMonthAxis(double value = (defaultYearSpan / 12))
    {
        return new DateTimeAxis(TimeSpan.FromDays(value), date => date.ToString("yyyyMM"))
        {
            LabelsRotation = 20,
            Position = AxisPosition.Start,
            MinStep = TimeSpan.FromDays(value).Ticks,
            UnitWidth = TimeSpan.FromDays(value).Ticks
        };
    }

    public static List<Axis> GetYearAxisList(double value = defaultYearSpan, string label = "Year")
    {
        return new List<Axis> { GetYearAxis(value) };
    }

    public static Axis GetYearAxis(double value = defaultYearSpan, string label = "Year")
    {
        return new DateTimeAxis(TimeSpan.FromDays(value), date => date.ToString("yyyy"))
        {
            LabelsRotation = 20,
            Position = AxisPosition.Start,
            MinStep = TimeSpan.FromDays(value).Ticks,
            UnitWidth = TimeSpan.FromDays(value).Ticks
        };
    }
}