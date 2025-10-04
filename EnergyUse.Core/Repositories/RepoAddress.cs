using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace EnergyUse.Core.Repositories;

public class RepoAddress : RepoGeneral<Models.Address>
{
    private readonly EnergyUseContext _context;

    public RepoAddress(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.Address?> Get(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Address>()        
                             .Include(a => a.TariffGroup)
                             .FirstOrDefaultAsync(a => a.Id == id, cancellationToken)
                             .ConfigureAwait(false);
    }

    public async Task<Models.Address?> GetByDescription(string description, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Address>()
                       .AsNoTracking()
                       .Include(i => i.TariffGroup)
                       .Where(w => w.Description == description)
                       .FirstOrDefaultAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.Address>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Address>()
                       .Include(a => a.TariffGroup)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<int> GetExistsByDescription(string description, long addressId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Address>()
                       .AsNoTracking()
                       .Where(w=> w.Description == description && w.Id != addressId)
                       .CountAsync()
                       .ConfigureAwait(false);
    }
}