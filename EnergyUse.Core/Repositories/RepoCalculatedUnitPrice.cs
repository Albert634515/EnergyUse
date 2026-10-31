using EnergyUse.Core.Context;
using EnergyUse.Core.Manager;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoCalculatedUnitPrice : RepoGeneral<Models.CalculatedUnitPrice>
{
    private readonly EnergyUseContext _context;

    public RepoCalculatedUnitPrice(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.CalculatedUnitPrice?> GetByYear(int year, long energyTypeId, long? tariffGroupId = null)
    {
        var prices = await _context.Set<Models.CalculatedUnitPrice>()
            .Where(p => p.Year == year && p.EnergyTypeId == energyTypeId).ToListAsync();
        ValidateYearPrices(prices);
        return prices.FirstOrDefault();
    }

    public async Task<decimal> GetByAverage(long energyTypeId, long? tariffGroupId = null)
    {
        var prices = await _context.Set<Models.CalculatedUnitPrice>()
            .Where(p => p.EnergyTypeId == energyTypeId).AsNoTracking().ToListAsync();
        ValidateYearPrices(prices);
        // Legacy rows for different tariff groups count only once per year.
        return prices.Count == 0 ? -1m : prices.GroupBy(p => p.Year).Average(g => g.First().Price);
    }

    private static void ValidateYearPrices(IEnumerable<Models.CalculatedUnitPrice> prices)
    {
        foreach (var year in prices.GroupBy(p => p.Year))
            if (year.Select(p => p.Price).Distinct().Skip(1).Any())
                throw new RateValidationException($"Multiple unit prices exist for energy type {year.First().EnergyTypeId} in {year.Key}. Set one yearly price in Calculated Unit Price.");
    }

    public async Task<IEnumerable<Models.CalculatedUnitPrice>> SelectByEnergyType(long energyTypeId)
    {
        return await _context.Set<Models.CalculatedUnitPrice>()
            .Where(p => p.EnergyTypeId == energyTypeId).ToListAsync();
    }

    public async Task<IEnumerable<Models.CalculatedUnitPrice>> SelectByEnergyTypeAndTarifGroup(long energyTypeId, long? tariffGroupId = null)
    {
        return await _context.Set<Models.CalculatedUnitPrice>()
                             .Include(e => e.EnergyType)
                             .Where(w => w.EnergyTypeId == energyTypeId)
                             .ToListAsync();
    }

    public async Task<Models.CalculatedUnitPrice?> SelectLastYear(long energyTypeId, long? tariffGroupId = null)
    {
        return await _context.Set<Models.CalculatedUnitPrice>()
                             .Include(e => e.EnergyType)
                             .Where(w => w.EnergyTypeId == energyTypeId)
                             .OrderByDescending(o => o.Year)
                             .FirstOrDefaultAsync();
    }
}
