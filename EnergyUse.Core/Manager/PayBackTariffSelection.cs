using EnergyUse.Models;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Manager;

internal static class PayBackTariffSelection
{
    internal static List<TariffPeriod> Select(DateTime start, DateTime end, long energyTypeId,
        long defaultGroupId, IEnumerable<PreDefinedPeriodDate> dates, IEnumerable<TariffGroup> groups)
    {
        start = start.Date;
        end = end.Date;
        var contracts = dates.Where(date => date.EnergyTypeId == energyTypeId && date.TariffGroupId > 0).ToList();
        var relevant = contracts.Where(date => date.StartDate.Date <= end && date.EndDate.Date >= start).ToList();
        var latestEnd = contracts.Count == 0 ? (DateTime?)null : contracts.Max(date => date.EndDate.Date);
        // Split at contract boundaries, including both end dates.
        var boundaries = new SortedSet<DateTime> { start, end.AddDays(1) };
        foreach (var date in relevant)
        {
            boundaries.Add(date.StartDate.Date < start ? start : date.StartDate.Date);
            boundaries.Add(date.EndDate.Date > end ? end.AddDays(1) : date.EndDate.Date.AddDays(1));
        }
        if (latestEnd.HasValue && latestEnd.Value >= start && latestEnd.Value < end)
            boundaries.Add(latestEnd.Value.AddDays(1));

        var names = groups.ToDictionary(group => group.Id, group => group.Description);
        var points = boundaries.ToList();
        var result = new List<TariffPeriod>();
        for (var i = 0; i < points.Count - 1; i++)
        {
            var ids = relevant.Where(date => date.StartDate.Date <= points[i] && date.EndDate.Date >= points[i])
                .Select(date => date.TariffGroupId!.Value).Distinct().ToList();
            // Only project beyond the last contract; historical gaps still use the address default.
            if (ids.Count == 0 && latestEnd.HasValue && points[i] > latestEnd.Value)
                ids = contracts.Where(date => date.EndDate.Date == latestEnd.Value)
                    .Select(date => date.TariffGroupId!.Value).Distinct().ToList();
            if (ids.Count > 1)
                throw new RateValidationException($"Conflicting predefined tariff groups for energy type {energyTypeId} on {points[i]:dd-MM-yyyy}.");
            var id = ids.SingleOrDefault(defaultGroupId);
            var periodEnd = points[i + 1].AddDays(-1);
            if (result.Count > 0 && result[^1].TariffGroupId == id)
                result[^1] = result[^1] with { EndDate = periodEnd };
            else
                result.Add(new TariffPeriod(points[i], periodEnd, id,
                    names.GetValueOrDefault(id, id.ToString())));
        }
        return result;
    }
}
