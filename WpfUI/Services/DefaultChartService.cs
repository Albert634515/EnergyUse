using EnergyUse.Common.Enums;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using WpfUI.Managers;

namespace WpfUI.Services;

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

        var series = ConvertSeries(chart.GetSeries());
        var labels = chart.GetResultLabelsPerPeriod(energyTypes.First());

        var xAxis = CreateXAxis(labels.Keys.ToList());

        var yAxes = new List<Axis>();
        foreach (var et in energyTypes)
        {
            yAxes.Add(CreateYAxis(et.Unit.Description, !et.HasEnergyReturn));
        }

        return new DefaultChartResult(series, new List<Axis> { xAxis }, yAxes, labels);
    }

    public void ExportToExcel(EnergyType energyType)
    {
        // Excel export logic
    }
}

public record DefaultChartResult(
    List<ISeries> Series,
    List<Axis> XAxes,
    List<Axis> YAxes,
    Dictionary<string, ResultLabel> Labels);