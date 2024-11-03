using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoPredefinedPeriodDate : RepoGeneral<Models.PreDefinedPeriodDate>
{
    private readonly EnergyUseContext _context;

    public RepoPredefinedPeriodDate(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public IEnumerable<Models.PreDefinedPeriodDate> GetByPeriodId(long periodId)
    {
        return _context.PreDefinedPeriodDates
            .Include(e => e.EnergyType)
            .Include(t => t.TariffGroup)
            .Include(p => p.PreDefinedPeriod)
            .Where(a => a.PreDefinedPeriod.Id == periodId);
    }
}