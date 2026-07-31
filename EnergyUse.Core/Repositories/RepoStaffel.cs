using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoStaffel : RepoGeneral<Models.Staffel>
{
    private readonly EnergyUseContext _context;

    public RepoStaffel(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<Models.Staffel>> SelectByRateId(long rateId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Staffel>()
                       .Include(p => p.Rate)
                       .Where(w => w.RateId == rateId)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.Staffel>> SelectByRateIdAndRange(long rateId, long maxRange, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Staffel>()
                       .Include(p => p.Rate)
                       .Where(w => w.RateId == rateId 
                                && w.ValueFrom <= maxRange
                                && w.ValueTill >= maxRange)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<int> DeleteByRateId(long rateId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Staffel>()
                       .Include(p => p.Rate)
                       .Where(w => w.RateId == rateId)
                       .ExecuteDeleteAsync(cancellationToken)
                       .ConfigureAwait(false);
    }
}
