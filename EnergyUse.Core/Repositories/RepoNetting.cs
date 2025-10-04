using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoNetting : RepoGeneral<Models.Netting>
{
    private readonly EnergyUseContext _context;

    public RepoNetting(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public IEnumerable<Models.Netting> SelectByEnergyType(long energyTypeId)
    {
        return _context.Nettings.Include(e => e.EnergyType).Where(n => n.EnergyTypeId == energyTypeId).OrderBy(o => o.StartDate)
                                .ToList();
    }

    public Models.Netting? SelectByEnergyTypeAndDate(long energyTypeId, DateTime nettingDate)
    {
        return _context.Nettings
                       .Include(e => e.EnergyType)
                       .Where(x => x.EnergyTypeId == energyTypeId && x.StartDate.Date <= nettingDate.Date && x.EndDate.Date >= nettingDate.Date)
                       .FirstOrDefault();
    }
}