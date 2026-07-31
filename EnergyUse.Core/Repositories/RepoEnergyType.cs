using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoEnergyType : RepoGeneral<Models.EnergyType>
{ 
    private readonly EnergyUseContext _context;

    public RepoEnergyType(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public async Task<Models.EnergyType?> Get(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.EnergyType>()
                             .Include(s => s.Unit)
                             .FirstOrDefaultAsync(s => s.Id == id, cancellationToken)
                             .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.EnergyType>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.EnergyType>()
                       .Include(s => s.Unit)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false); 
    }

    public async Task<Models.EnergyType?> SelectByName(string energyTypeName, CancellationToken cancellationToken = default)
    {
        return await _context.EnergyTypes
                       .Where(s => s.Name == energyTypeName)
                       .AsNoTracking()
                       .FirstOrDefaultAsync(cancellationToken)
                       .ConfigureAwait(false);
    }

    public async Task<IEnumerable<Models.EnergyType>> SelectByAddressId(long addressId, CancellationToken cancellationToken = default)
    {
        var energyTypes = await _context.Meters
                       .Include(a => a.Address)
                       .Where(m => m.Address.Id == addressId)
                       .Select(s => s.EnergyType.Id)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);

        if (energyTypes.Count == 0)
            energyTypes.Add(0);

        return await _context.Set<Models.EnergyType>()
                       .Include(s => s.Unit)
                       .Where(x => energyTypes.Contains(x.Id))
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false);
    }
}
