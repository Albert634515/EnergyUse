using EnergyUse.Core.Context;

using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoSettings : RepoGeneral<Models.Setting>
{
    private readonly EnergyUseContext _context;

    public RepoSettings(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.Setting?> GetByKey(string key, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.Setting>()
                             .FirstOrDefaultAsync(s => s.Key == key, cancellationToken)
                             .ConfigureAwait(false);
    }
}
