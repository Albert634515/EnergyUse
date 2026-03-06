using EnergyUse.Common.Enums;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using System.Collections.Generic;
using WpfUI.Models;

namespace WpfUI.ViewModels;

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
        var result = new CompareChartResult();

        // TODO: hier later jouw echte CompareChart backend koppelen
        // Voor nu dummy data zodat alles werkt
        var valuesDelivery = new double[] { 3000, 2500, 2800, 2979 };
        var valuesReturn = new double[] { 2800, 2400, 2600, 2690 };
        var labels = new[] { "2021", "2022", "2023", "2024" };

        ISeries serieDelivery;
        ISeries serieReturn;

        if (showStacked)
        {
            serieDelivery = new StackedColumnSeries<double>
            {
                Values = valuesDelivery,
                Name = "Delivery",
                Fill = new SolidColorPaint(SKColors.Orange),
                Stroke = new SolidColorPaint(SKColors.Transparent)
            };

            serieReturn = new StackedColumnSeries<double>
            {
                Values = valuesReturn,
                Name = "Return delivery",
                Fill = new SolidColorPaint(SKColors.Green),
                Stroke = new SolidColorPaint(SKColors.Transparent)
            };
        }
        else
        {
            serieDelivery = new ColumnSeries<double>
            {
                Values = valuesDelivery,
                Name = "Delivery",
                Fill = new SolidColorPaint(SKColors.Orange),
                Stroke = new SolidColorPaint(SKColors.Transparent)
            };

            serieReturn = new ColumnSeries<double>
            {
                Values = valuesReturn,
                Name = "Return delivery",
                Fill = new SolidColorPaint(SKColors.Green),
                Stroke = new SolidColorPaint(SKColors.Transparent)
            };
        }

        result.Series.Add(serieDelivery);
        result.Series.Add(serieReturn);

        var xAxis = new Axis
        {
            Labels = labels,
            LabelsRotation = 0
        };

        var yAxis = new Axis
        {
            Labeler = value => value.ToString("N0")
        };

        result.XAxes.Add(xAxis);
        result.YAxes.Add(yAxis);

        return result;
    }

    public void ExportToExcel()
    {
        // TODO: hier jouw bestaande Excel export koppelen
    }
}