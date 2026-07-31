using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoCorrectionFactor : RepoGeneral<Models.CorrectionFactor>
{
    private readonly EnergyUseContext _context;

    public RepoCorrectionFactor(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<Models.CorrectionFactor>> SelectByEnergyType(long energyTypeId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CorrectionFactor>()
                             .Include(s => s.EnergyType)
                             .Where(w => w.EnergyTypeId == energyTypeId)
                             .ToListAsync(cancellationToken)
                             .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.CorrectionFactor>> SelectByRange(DateTime startRange, DateTime endRange, long energyTypeId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CorrectionFactor>()
                       .Include(s => s.EnergyType)
                       .Where(w => w.EnergyTypeId == energyTypeId && (w.StartFactor.Date <= endRange.Date || w.EndFactor.Date <= startRange.Date))
                       .OrderBy(o => o.StartFactor)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<Models.CorrectionFactor?> SelectLastRow(long energyTypeId, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.CorrectionFactor>()
                       .Where(w => w.EnergyTypeId == energyTypeId)
                       .OrderByDescending(o => o.StartFactor)
                       .FirstOrDefaultAsync(cancellationToken)
                       .ConfigureAwait(false);
    }
}
