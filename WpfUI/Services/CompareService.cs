using EnergyUse.Common.Enums;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfUI.Managers;

namespace WpfUI.Services;

public class CompareChartService
{
    public CompareChartResult BuildChart(
        Address address,
        EnergyType energyType,
        SelectionItem periodType,
        int startYear,
        int endYear,
        int number,
        int day,
        bool predictMissing,
        bool showStacked,
        ShowBy showBy,
        ShowType showType)
    {
        var p = new ParameterGraph
        {
            Address = address,
            EnergyTypeList = new() { energyType },
            DbName = Config.GetDbFileName(),
            PredictMissingData = predictMissing,
            ShowStacked = showStacked,
            YearStart = startYear,
            YearEnd = endYear,
            PeriodType = LibGraphGeneral.GetPeriodType(periodType.Key),
            ShowBy = showBy,
            ShowType = showType
        };

        var compare = new Compare(p);

        var series = LiveChartsManager.ConvertSeriesModelsToISeries(compare.GetSeries());
        var labels = compare.GetResultLabelsPerPeriod(energyType);

        var xAxis = new Axis
        {
            Labels = labels.Keys.ToList()
        };

        var yAxis = new Axis
        {
            Labeler = value => value.ToString("N0")
        };

        return new CompareChartResult(series, new List<Axis> { xAxis }, new List<Axis> { yAxis }, labels);
    }

    public void ExportToExcel(EnergyType energyType)
    {
        // Excel export logic (optioneel)
    }
}

public record CompareChartResult(
    List<ISeries> Series,
    List<Axis> XAxes,
    List<Axis> YAxes,
    Dictionary<string, ResultLabel> Labels);