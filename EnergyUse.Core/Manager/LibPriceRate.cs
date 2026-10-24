using EnergyUse.Core.Context;
using EnergyUse.Models.Common;

namespace EnergyUse.Core.Manager;

public sealed class LibPriceRate : IDisposable
{
    private readonly EnergyUseContext _context;
    private readonly Repositories.RepoRate _rateRepo;
    private readonly Repositories.RepoCostCategories _costCategoryRepo;
    private readonly Dictionary<(long EnergyTypeId, long CostCategoryId, long TariffGroupId), List<Models.Rate>> _rates = new();

    public LibPriceRate(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);
        _rateRepo = new Repositories.RepoRate(_context);
        _costCategoryRepo = new Repositories.RepoCostCategories(_context);
    }

    public async Task<PriceRate> GetCalculatedRate(long energyTypeId, DateTime costDate,
        Common.Enums.SubEnergyType subEnergyType, long tarifGroupId)
    {
        var categories = (await _costCategoryRepo.SelectByTariffGroup(tarifGroupId, energyTypeId))
            .Where(category => category.EnergySubTypeId == (long)subEnergyType && category.CalculationTypeId == 1
                && (!category.Start.HasValue || category.Start.Value.Date <= costDate.Date)
                && (!category.End.HasValue || category.End.Value.Date >= costDate.Date))
            .ToList();
        if (categories.Count == 0)
            throw new RateValidationException($"No cost category for energy type {energyTypeId}, subtype {subEnergyType}, on {costDate:dd-MM-yyyy}.");

        var total = new PriceRate();
        foreach (var category in categories)
        {
            var rate = await GetRate(energyTypeId, category, costDate, tarifGroupId);
            total.Rate += rate.Rate;
            total.LastRateUsed |= rate.LastRateUsed;
        }
        return total;
    }

    public Task<PriceRate> GetRate(long energyTypeId, Models.CostCategory costCategory,
        PeriodicDataPerDay periodicData, long tarifGroupId)
        => GetRate(energyTypeId, costCategory, periodicData.ValueX, tarifGroupId);

    public async Task<PriceRate> GetRate(long energyTypeId, Models.CostCategory costCategory,
        DateTime costDate, long tarifGroupId)
    {
        var key = (energyTypeId, costCategory.Id, tarifGroupId);
        if (!_rates.TryGetValue(key, out var rates))
        {
            rates = (await _rateRepo.SelectByCostCategoryAndEnergyTypeAndTarifGroup(
                costCategory.Id, energyTypeId, tarifGroupId)).ToList();
            _rates.Add(key, rates);
        }

        return RateSelection.Select(rates, energyTypeId, costCategory.Id, costCategory.Name, costDate, tarifGroupId);
    }

    public void Dispose() => _context.Dispose();
}
