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

        var series = ConvertSeries(chart.GetSeries());

        var xAxis = new Axis
        {
            Labeler = value => new DateTime((long)value).ToString("dd-MM-yyyy")
        };
        var yAxis = CreateYAxis(energyType.Unit.Description, !energyType.HasEnergyReturn);

        return new RatesChartResult(chart, series, new List<Axis> { xAxis }, new List<Axis> { yAxis });
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
