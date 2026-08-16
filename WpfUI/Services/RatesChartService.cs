using EnergyUse.Common.Enums;
using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Core.Manager;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using System;
using System.Collections.Generic;
using System.Linq;
using WpfUI.Managers;

namespace WpfUI.Services;

public class RatesChartService : ChartBaseService
{
    public async Task<RatesChartResult> BuildChartAsync(
        Address address,
        EnergyType energyType,
        IEnumerable<CostCategory> costCategories,
        DateTime from,
        DateTime till,
        ShowType showType,
        bool showMonthlyDataPoints)
    {
        var p = new ParameterGraph
        {
            Address = address,
            EnergyTypeList = new() { energyType },
            DbName = Config.GetDbFileName(),
            From = from,
            Till = till,
            CostCategoryList = costCategories.ToList(),
            ShowType = showType,
            ShowMonthlyDataPoints = showMonthlyDataPoints,
            TarifGroupId = address?.TariffGroup?.Id ?? 1
        };

        var chart = await Rates.CreateAsync(p);

        var seriesModels = chart.GetSeries();
        var yAxes = CreateRatesYAxes(energyType.Unit.Description, seriesModels);
        var series = ConvertSeries(seriesModels);

        var xAxis = new Axis
        {
            Labeler = value => new DateTime((long)value).ToString("dd-MM-yyyy")
        };
        return new RatesChartResult(chart, series, new List<Axis> { xAxis }, yAxes);
    }

    private static List<Axis> CreateRatesYAxes(string label, List<SeriesModel> seriesModels)
    {
        foreach (var series in seriesModels)
            series.ScalesYAt = 0;

        var seriesByMagnitude = seriesModels
            .Select(series => new
            {
                Series = series,
                Magnitude = series.Points
                    .Select(point => Math.Abs(point.Value))
                    .Where(value => double.IsFinite(value) && value > 0)
                    .DefaultIfEmpty(0)
                    .Average()
            })
            .Where(item => item.Magnitude > 0)
            .OrderBy(item => item.Magnitude)
            .ToList();

        var splitIndex = -1;
        var largestFactor = 1d;
        for (var index = 1; index < seriesByMagnitude.Count; index++)
        {
            var factor = seriesByMagnitude[index].Magnitude / seriesByMagnitude[index - 1].Magnitude;
            if (factor > largestFactor)
            {
                largestFactor = factor;
                splitIndex = index;
            }
        }

        const double secondaryAxisThreshold = 10d;
        if (splitIndex < 0 || largestFactor < secondaryAxisThreshold)
        {
            return new List<Axis>
            {
                CreateRatesYAxis(label, seriesModels, AxisPosition.Start)
            };
        }

        var highMagnitudeSeries = seriesByMagnitude
            .Skip(splitIndex)
            .Select(item => item.Series)
            .ToHashSet();

        foreach (var series in highMagnitudeSeries)
            series.ScalesYAt = 1;

        return new List<Axis>
        {
            CreateRatesYAxis($"{label} (laag)", seriesModels.Where(series => series.ScalesYAt == 0), AxisPosition.Start),
            CreateRatesYAxis($"{label} (hoog)", seriesModels.Where(series => series.ScalesYAt == 1), AxisPosition.End)
        };
    }

    private static Axis CreateRatesYAxis(
        string label,
        IEnumerable<SeriesModel> seriesModels,
        AxisPosition position)
    {
        var values = seriesModels
            .SelectMany(series => series.Points)
            .Select(point => point.Value)
            .Where(double.IsFinite)
            .ToList();

        var axis = new Axis
        {
            Name = label,
            Position = position
        };
        if (values.Count == 0)
            return LiveChartsManager.ApplyYAxisStyle(axis);

        var minimum = values.Min();
        var maximum = values.Max();
        var range = maximum - minimum;
        var padding = range > 0
            ? range * 0.1
            : Math.Max(Math.Abs(minimum) * 0.1, 0.01);

        axis.MinLimit = minimum >= 0 ? Math.Max(0, minimum - padding) : minimum - padding;
        axis.MaxLimit = maximum + padding;

        var visibleRange = axis.MaxLimit.Value - axis.MinLimit.Value;
        axis.Labeler = visibleRange switch
        {
            < 1 => value => value.ToString("N3"),
            < 10 => value => value.ToString("N2"),
            _ => value => value.ToString("N0")
        };

        return LiveChartsManager.ApplyYAxisStyle(axis);
    }

    public void ExportToExcel(EnergyType energyType, Rates chart)
    {
        var dataList = chart.GetDataList();
        if (dataList.Count == 0)
            return;

        var dlg = new Microsoft.Win32.SaveFileDialog
        {
            Filter = "Excel files (*.xlsx)|*.xlsx",
            FileName = $"ChartRates_{energyType.Name}.xlsx"
        };

        if (dlg.ShowDialog() == true)
        {
            LibExport.ExportChartRatesToExcel(dlg.FileName, energyType, dataList);
        }
    }
}

public record RatesChartResult(
    Rates Chart,
    List<ISeries> Series,
    List<Axis> XAxes,
    List<Axis> YAxes);
