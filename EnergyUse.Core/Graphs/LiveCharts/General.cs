using EnergyUse.Common.Enums;
using EnergyUse.Common.Extensions;
using LiveChartsCore;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.Painting.Effects;
using LiveChartsCore.SkiaSharpView.VisualElements;
using SkiaSharp;
using Color = System.Drawing.Color;

namespace EnergyUse.Core.Graphs.LiveCharts;

public class General
{
    #region Lines

    public static ISeries GetLineDateSerie(ChartSeriesType serieName, int scalesYAtint)
    {
        var lineSerie = GetDefaultLineDateSeries(scalesYAtint);
        lineSerie.Name = serieName.GetDescription();

        return lineSerie;
    }

    public static ISeries GetDefaultLineDateSeries(int scalesYAt, int strokeThickness = 1, bool addSTroke = true)
    {
        var ln = new LineSeries<DateTimePoint>
        {
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null,
            LineSmoothness = 0,
            ScalesYAt = scalesYAt
        };

        if (addSTroke)
        {
            ln.Stroke = new SolidColorPaint
            {
                Color = SKColors.CornflowerBlue,
                StrokeCap = SKStrokeCap.Round,
                StrokeThickness = strokeThickness
            };
        }

        return ln;
    }

    public static ISeries GetLineSerieAvgDate(ChartSeriesType serieName, Color color, int scalesYAt)
    {
        var strokeThickness = 1;
        var strokeDashArray = new float[] { 3 * strokeThickness, 2 * strokeThickness };
        var effect = new DashEffect(strokeDashArray);
        var fillColor = (uint)color.ToArgb();

        return new LineSeries<DateTimePoint>
        {
            Name = serieName.GetDescription(),
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null,
            Stroke = new SolidColorPaint
            {
                Color = fillColor,
                StrokeCap = SKStrokeCap.Round,
                StrokeThickness = strokeThickness,
                PathEffect = effect
            },
            ScalesYAt = scalesYAt
        };
    }

    public static ISeries GetLineDataSerie(ChartSeriesType serieName, Color color, int scalesYAt)
    {
        var strokeThickness = 1;
        var fillColor = (uint)color.ToArgb();

        return new LineSeries<ObservablePoint>
        {
            Name = serieName.GetDescription(),
            Fill = null,
            GeometryFill = null,
            GeometryStroke = null,
            Stroke = new SolidColorPaint
            {
                Color = fillColor,
                StrokeCap = SKStrokeCap.Round,
                StrokeThickness = strokeThickness
            },
            ScalesYAt = scalesYAt
        };
    }

    #endregion

    #region StackedColumns

    public static ISeries GetStackedColumnDateSerie(ChartSeriesType serieName, long energyTypeId, int stackGroupId, Color color, int scalesYAt)
    {
        var fillColor = (uint)color.ToArgb();
        var serie = new StackedColumnSeries<DateTimePoint>
        {
            Name = serieName.GetDescription(),
            Stroke = new SolidColorPaint(fillColor) { StrokeThickness = 0 },
            DataLabelsPaint = new SolidColorPaint(SKColors.Black),
            DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Middle,
            StackGroup = (int)(stackGroupId + energyTypeId),
            ScalesYAt = scalesYAt
        };
        
        if (color != Color.Empty)
            serie.Fill = new SolidColorPaint(fillColor, 0);

        return serie;
    }

    #endregion

    #region Column

    public static ISeries GetColumnDateSerie(ChartSeriesType serieName, Color color, int scalesYAt)
    {
        var fillColor = (uint)color.ToArgb();
        var serie = new ColumnSeries<DateTimePoint>
        {
            Name = serieName.GetDescription(),
            DataLabelsPaint = new SolidColorPaint(SKColors.Black),
            ScalesYAt = scalesYAt
        };

        if (color != Color.Empty)
            serie.Fill = new SolidColorPaint(fillColor, 0);

        return serie;
    }

    #endregion

    #region Other

    public static LabelVisual GetTitle(string title)
    {
        var labelVisual = new LabelVisual
        {
            Text = title,
            TextSize = 15,
            Padding = new LiveChartsCore.Drawing.Padding(2),
            Paint = new SolidColorPaint(SKColors.DarkSlateGray)
        };

        return labelVisual;
    }

    #endregion
}