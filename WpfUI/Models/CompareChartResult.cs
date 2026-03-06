using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace WpfUI.Models
{
    public class CompareChartResult
    {
        public List<ISeries> Series { get; } = new();
        public List<Axis> XAxes { get; } = new();
        public List<Axis> YAxes { get; } = new();
        public Dictionary<string, ResultLabel> Labels { get; } = new();
    }
}