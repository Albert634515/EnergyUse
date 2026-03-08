using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace WpfUI.Models;

public class CompareChartResult
{
    public List<ISeries> Series { get; set; } = new();
    public List<Axis> XAxes { get; set; } = new();
    public List<Axis> YAxes { get; set; } = new();

    public Dictionary<string, EnergyUse.Models.Common.ResultLabel> Labels { get; set; } = new();

    public List<PeriodicData> ExportData { get; set; } = new();
}