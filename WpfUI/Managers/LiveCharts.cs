using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using System.Collections.ObjectModel;

using EnergyUse.Core.Manager;

namespace WpfUI.Managers;

public static class LiveChartsManager
{
    private static readonly Lazy<LibSettings> _settings =
        new(() => new LibSettings(Config.GetDbFileName()));

    public static Axis ApplyYAxisStyle(Axis axis)
    {
        var lineColor = _settings.Value.GetColorSetting("LineColorChart", System.Drawing.Color.LightGray);
        var backgroundColor = _settings.Value.GetColorSetting("BackgroundColorChart", System.Drawing.Color.White);
        var labelColor = _settings.Value.GetColorSetting("LabelsYColorChart", System.Drawing.Color.Black);
        lineColor = ensureVisibleLineColor(lineColor, backgroundColor);

        axis.LabelsPaint = new SolidColorPaint((uint)labelColor.ToArgb());
        axis.SeparatorsPaint = new SolidColorPaint((uint)lineColor.ToArgb()) { StrokeThickness = 2 };
        return axis;
    }

    private static System.Drawing.Color ensureVisibleLineColor(
        System.Drawing.Color lineColor,
        System.Drawing.Color backgroundColor)
    {
        var sameAsBackground = lineColor.R == backgroundColor.R &&
                               lineColor.G == backgroundColor.G &&
                               lineColor.B == backgroundColor.B;
        if (!lineColor.IsEmpty && lineColor.A > 0 && !sameAsBackground)
            return lineColor;

        return backgroundColor.GetBrightness() < 0.5f
            ? System.Drawing.Color.LightGray
            : System.Drawing.Color.DarkGray;
    }

    public static List<ISeries> ConvertSeriesModelsToISeries(List<SeriesModel> models)
    {
        var result = new List<ISeries>();

        foreach (var sm in models)
        {
            var points = new ObservableCollection<DateTimePoint>();
            foreach (var dp in sm.Points)
                points.Add(new DateTimePoint(dp.DateTime, dp.Value));

            if (sm.IsLine)
            {
                var line = new LineSeries<DateTimePoint>
                {
                    Values = points,
                    Name = sm.Name,
                    ScalesYAt = sm.ScalesYAt,
                    LineSmoothness = 0,
                    Fill = null
                };

                if (sm.Color != System.Drawing.Color.Empty)
                    line.Stroke = new SolidColorPaint((uint)sm.Color.ToArgb()) { StrokeThickness = 2 };

                result.Add(line);
            }
            else if (sm.IsStacked)
            {
                var stacked = new StackedColumnSeries<DateTimePoint>
                {
                    Values = points,
                    Name = sm.Name,
                    ScalesYAt = sm.ScalesYAt,
                    StackGroup = 0
                };

                if (sm.Color != System.Drawing.Color.Empty)
                    stacked.Fill = new SolidColorPaint((uint)sm.Color.ToArgb());

                result.Add(stacked);
            }
            else
            {
                var col = new ColumnSeries<DateTimePoint>
                {
                    Values = points,
                    Name = sm.Name,
                    ScalesYAt = sm.ScalesYAt
                };

                if (sm.Color != System.Drawing.Color.Empty)
                    col.Fill = new SolidColorPaint((uint)sm.Color.ToArgb());

                result.Add(col);
            }
        }

        return result;
    }
}
