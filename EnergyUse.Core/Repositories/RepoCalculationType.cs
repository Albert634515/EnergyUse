using EnergyUse.Core.Context;

using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoCalculationType : RepoGeneral<Models.CalculationType>
{
    private readonly EnergyUseContext _context;

    public RepoCalculationType(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<Models.CalculationType>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _context.CalculationTypes
                             .ToListAsync(cancellationToken)
                             .ConfigureAwait(false);
    }

    public async Task<Models.CalculationType?> SelectByDescription(string description, CancellationToken cancellationToken = default)
    {
        return await _context.CalculationTypes
                             .FirstOrDefaultAsync(w => w.Description == description, cancellationToken)
                             .ConfigureAwait(false);
    }
}
