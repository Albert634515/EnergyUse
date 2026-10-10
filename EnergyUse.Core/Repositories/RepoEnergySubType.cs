using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoEnergySubType : RepoGeneral<Models.EnergySubType>
{
    private readonly EnergyUseContext _context;

    public RepoEnergySubType(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.EnergySubType?> SelectByDescription(string description, CancellationToken cancellationToken = default)
    {
        return await _context.EnergySubTypes
                             .AsNoTracking()
                             .FirstOrDefaultAsync(s => s.Description == description, cancellationToken)
                             .ConfigureAwait(false);
    }
}
