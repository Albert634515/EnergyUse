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

    public Models.EnergyType? Get(int id)
    {
        return _context.Set<Models.EnergyType>().Include(s => s.Unit).Where(s => s.Id == id).FirstOrDefault();
    }

    public async Task<IEnumerable<Models.EnergyType>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _context.Set<Models.EnergyType>()
                       .Include(s => s.Unit)
                       .ToListAsync(cancellationToken)
                       .ConfigureAwait(false); 
    }

    public Models.EnergyType? SelectByName(string energyTypeName)
    {
        return _context.EnergyTypes
                       .Where(s => s.Name == energyTypeName)
                       .AsNoTracking()
                       .FirstOrDefault();
    }

    public IEnumerable<Models.EnergyType> SelectByAddressId(long addressId)
    {
        var energyTypes = _context.Meters
                       .Include(a => a.Address)
                       .Where(m => m.Address.Id == addressId)
                       .Select(s => s.EnergyType.Id)
                       .ToList();

        if (energyTypes.Count == 0)
            energyTypes.Add(0);

        return _context.Set<Models.EnergyType>()
                       .Include(s => s.Unit)
                       .Where(x => energyTypes.Contains(x.Id));
    }
}