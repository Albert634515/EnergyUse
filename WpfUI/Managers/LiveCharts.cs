using EnergyUse.Core.Graphs.LiveCharts;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace WpfUI.Managers;

public static class LiveChartsManager
{
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