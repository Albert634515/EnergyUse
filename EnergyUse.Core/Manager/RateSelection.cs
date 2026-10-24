using EnergyUse.Models.Common;

namespace EnergyUse.Core.Manager;

public static class RateSelection
{
    public static PriceRate Select(IEnumerable<Models.Rate> rates, long energyTypeId,
        long costCategoryId, string categoryName, DateTime costDate, long tariffGroupId)
    {
        var candidates = rates.Where(rate => rate.EnergyTypeId == energyTypeId
            && rate.CostCategoryId == costCategoryId && rate.TariffGroupId == tariffGroupId).ToList();
        var context = $"cost category '{categoryName}' (ID {costCategoryId}), energy type {energyTypeId}, " +
                      $"tariff group {tariffGroupId}, on {costDate:dd-MM-yyyy}";
        var active = candidates.Where(rate => rate.StartRate.Date <= costDate.Date
            && rate.EndRate.Date >= costDate.Date).ToList();
        if (active.Count > 1)
            throw new RateValidationException($"Overlapping rates for {context}. Rate IDs: {string.Join(", ", active.Select(rate => rate.Id))}.");

        var historical = active.Count == 0;
        var rate = active.SingleOrDefault();
        if (historical)
        {
            var previous = candidates.Where(item => item.EndRate.Date < costDate.Date
                && item.StartRate.Date <= item.EndRate.Date).OrderByDescending(item => item.EndRate.Date).ThenByDescending(item => item.StartRate.Date).ToList();
            rate = previous.FirstOrDefault();
            if (rate != null && previous.Count(item => item.EndRate.Date == rate.EndRate.Date) > 1)
                throw new RateValidationException($"Ambiguous historical rates for {context}.");
        }
        if (rate == null)
            throw new RateValidationException($"Missing rate for {context}. Add a valid rate before calculating.");

        var factor = historical && rate.ExpectedPriceChange != 0 ? 1m + rate.ExpectedPriceChange / 100m : 1m;
        return new PriceRate
        {
            Rate = rate.RateValue * factor,
            RateId = rate.Id,
            RateTypeId = rate.RateTypeId,
            StartRate = rate.StartRate,
            EndRate = rate.EndRate,
            LastRateUsed = historical,
            PriceAdjustmentFactor = factor == 1m ? 0m : factor
        };
    }
}
