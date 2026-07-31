using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoPayment : RepoGeneral<Models.Payment>
{
    private readonly EnergyUseContext _context;

    public RepoPayment(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.Payment?> Get(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Payment>()
                       .Include(s => s.PreDefinedPeriod)
                       .FirstOrDefaultAsync(s => s.Id == id, cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.Payment>> SelectByAddressAndPeriod(long addressId, long periodId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Payment>()
                       .Include(p => p.PreDefinedPeriod)
                       .Include(a => a.Address)
                       .Where(w => w.AddressId == addressId && w.PreDefinedPeriodId == periodId)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.Payment>> SelectByAddressAndRange(long addressId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Payment>()
                       .Include(p => p.PreDefinedPeriod)
                       .Include(a => a.Address)
                       .Where(w => w.AddressId == addressId && w.PayDate >= startDate && w.PayDate <= endDate)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }
}
