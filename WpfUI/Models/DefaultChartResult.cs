using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;

namespace WpfUI.Models
{
    public record DefaultChartResult(
      List<ISeries> Series,
      List<Axis> XAxes,
      List<Axis> YAxes,
      Dictionary<string, ResultLabel> Labels);
}
