using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoCalculatedUnitPrice : RepoGeneral<Models.CalculatedUnitPrice>
{
    private readonly EnergyUseContext _context;

    public RepoCalculatedUnitPrice(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.CalculatedUnitPrice?> GetByYear(int year, long energyTypeId, long tariffGroupId)
    {
        return await _context.Set<Models.CalculatedUnitPrice>()
                             .Include(e => e.EnergyType)
                             .Include(t => t.TariffGroup)
                             .Where(w => w.Year == year && w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId)
                             .FirstOrDefaultAsync();
    }

    public async Task<decimal> GetByAverage(long energyTypeId, long tariffGroupId)
    {
        return (decimal)await _context.Set<Models.CalculatedUnitPrice>()
                                      .Where(w => w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId)
                                      .AsNoTracking()
                                      .AverageAsync(a => (double)a.Price);
    }

    public async Task<IEnumerable<Models.CalculatedUnitPrice>> SelectByEnergyTypeAndTarifGroup(long energyTypeId, long tariffGroupId)
    {
        return await _context.Set<Models.CalculatedUnitPrice>()
                             .Include(e => e.EnergyType)
                             .Include(t => t.TariffGroup)
                             .Where(w => w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId)
                             .ToListAsync();
    }

    public async Task<Models.CalculatedUnitPrice?> SelectLastYear(long energyTypeId, long tariffGroupId)
    {
        return await _context.Set<Models.CalculatedUnitPrice>()
                             .Include(e => e.EnergyType)
                             .Include(t => t.TariffGroup)
                             .Where(w => w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId)
                             .OrderByDescending(o => o.Year)
                             .FirstOrDefaultAsync();
    }
}