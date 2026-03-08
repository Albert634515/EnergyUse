using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;

namespace EnergyUse.Core.Extensions;

public static class AxisExtensions
{
    public static Axis CloneAxis(this Axis axis)
    {
        if (axis == null) return new Axis();

        // Als het een DateTimeAxis is, maak een nieuwe DateTimeAxis en kopieer relevante properties
        if (axis is DateTimeAxis dtAxis)
        {
            var clone = new DateTimeAxis(TimeSpan.FromTicks((long)dtAxis.UnitWidth), dt => dt.ToString("dd/MM/yyyy"))
            {
                Labels = dtAxis.Labels,
                LabelsRotation = dtAxis.LabelsRotation,
                Position = dtAxis.Position,
                MinStep = dtAxis.MinStep,
                UnitWidth = dtAxis.UnitWidth,
                MinLimit = dtAxis.MinLimit,
                MaxLimit = dtAxis.MaxLimit
            };
            return clone;
        }

        // Anders: gewone Axis
        return new Axis
        {
            Labels = axis.Labels,
            LabelsRotation = axis.LabelsRotation,
            Position = axis.Position,
            MinStep = axis.MinStep,
            UnitWidth = axis.UnitWidth,
            MinLimit = axis.MinLimit,
            MaxLimit = axis.MaxLimit
        };
    }
}