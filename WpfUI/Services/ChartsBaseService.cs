using System.Collections.Generic;
using EnergyUse.Common.Enums;
using EnergyUse.Models;
using EnergyUse.Models.Common;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using WpfUI.Managers;

namespace WpfUI.Services;

public abstract class ChartBaseService
{
    protected List<ISeries> ConvertSeries(List<SeriesModel> models)
        => LiveChartsManager.ConvertSeriesModelsToISeries(models);

    protected Axis CreateXAxis(List<string> labels)
        => new Axis { Labels = labels };

    protected Axis CreateYAxis(string label, bool minZero = false)
    {
        var axis = new Axis
        {
            Name = label,
            Labeler = value => value.ToString("N0")
        };

        if (minZero)
            axis.MinLimit = 0;

        return axis;
    }

    protected ShowBy ResolveShowBy(bool cat, bool sub, bool tot)
        => cat ? ShowBy.Category :
           sub ? ShowBy.SubCategory :
           ShowBy.Total;

    protected ShowType ResolveShowType(bool rate, bool value, bool eff)
        => rate ? ShowType.Rate :
           value ? ShowType.Value :
           eff ? ShowType.Efficiency :
           ShowType.Unknown;
}