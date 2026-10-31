namespace EnergyUse.Core.Manager;

public static class VatRateSelection
{
    public static Models.VatTarif Select(IEnumerable<Models.VatTarif> rates,
        Models.CostCategory category, DateTime date)
    {
        var active = rates.Where(rate => rate.CostCategoryId == category.Id
            && rate.StartDate.Date <= date.Date && rate.EndDate.Date >= date.Date).ToList();
        var context = $"cost category '{category.Name}' (ID {category.Id}), on {date:dd-MM-yyyy}";
        if (active.Count == 0)
            throw new RateValidationException($"Missing VAT rate for {context}. Add a valid VAT rate before calculating.");
        if (active.Count > 1)
            throw new RateValidationException($"Overlapping VAT rates for {context}. VAT rate IDs: " +
                string.Join(", ", active.Select(rate => rate.Id)) + ". Correct the VAT periods before calculating.");
        return active[0];
    }
}
