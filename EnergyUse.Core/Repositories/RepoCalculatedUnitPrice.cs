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

    public Models.CalculatedUnitPrice? GetByYear(int year, long energyTypeId, long tariffGroupId)
    {
        return _context.Set<Models.CalculatedUnitPrice>()
                       .Include(e => e.EnergyType)
                       .Include(t => t.TariffGroup)
                       .Where(w => w.Year == year && w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId).FirstOrDefault();
    }

    public decimal GetByAverage(long energyTypeId, long tariffGroupId)
    {
        return (decimal)_context.Set<Models.CalculatedUnitPrice>()
                       .Where(w => w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId)
                       .Average(a => (double)a.Price);
    }

    public IEnumerable<Models.CalculatedUnitPrice> SelectByEnergyTypeAndTarifGroup(long energyTypeId, long tariffGroupId)
    {
        return _context.Set<Models.CalculatedUnitPrice>()
                       .Include(e => e.EnergyType)
                       .Include(t => t.TariffGroup)
                       .Where(w => w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId);
    }

    public Models.CalculatedUnitPrice? SelectLastYear(long energyTypeId, long tariffGroupId)
    {
        return _context.Set<Models.CalculatedUnitPrice>()
                       .Include(e => e.EnergyType)
                       .Include(t => t.TariffGroup)
                       .Where(w => w.EnergyTypeId == energyTypeId && w.TariffGroupId == tariffGroupId)
                       .OrderByDescending(o => o.Year)
                       .FirstOrDefault();
    }
}