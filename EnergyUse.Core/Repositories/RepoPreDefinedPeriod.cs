using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoPreDefinedPeriod : RepoGeneral<Models.PreDefinedPeriod>
{
    private readonly EnergyUseContext _context;

    public RepoPreDefinedPeriod(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<Models.PreDefinedPeriod>> GetAllWithDates(CancellationToken cancellationToken = default)
    {
        return await _context.PreDefinedPeriods
            .Include(period => period.PreDefinedPeriodDates)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
    }
}
