using EnergyUse.Core.Context;

using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoTariffGroup : RepoGeneral<Models.TariffGroup>
{
    private readonly EnergyUseContext _context;

    public RepoTariffGroup(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.TariffGroup?> SelectByDescription(string description, CancellationToken cancellationToken = default)
    {
        return await _context.TariffGroups
                             .FirstOrDefaultAsync(s => s.Description == description, cancellationToken)
                             .ConfigureAwait(false);
    }

    public async Task<Models.TariffGroup?> SelectById(long id, CancellationToken cancellationToken = default)
    {
        return await _context.TariffGroups
                             .FirstOrDefaultAsync(s => s.Id == id, cancellationToken)
                             .ConfigureAwait(false);
    }
}
