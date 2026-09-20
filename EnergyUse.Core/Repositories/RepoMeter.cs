using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoMeter : RepoGeneral<Models.Meter>
{
    private readonly EnergyUseContext _context;

    public RepoMeter(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public new async Task<IEnumerable<Models.Meter>> GetAll()
    {
        return await _context.Set<Models.Meter>()
                             .Include(e => e.EnergyType)
                             .Include(a => a.Address)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Models.Meter>> SelectByAddressAndEnergyType(long addressId, long energyTypeId)
    {
        return await _context.Meters
                             .Include(e => e.EnergyType)
                             .Include(a => a.Address)
                             .Where(n => n.EnergyType.Id == energyTypeId && n.Address.Id == addressId)
                             .ToListAsync();
    }

    public async Task<IEnumerable<Models.Meter>> SelectOverlappingPeriod(long addressId, long energyTypeId, DateTime startDate, DateTime endDate)
    {
        var start = startDate.Date;
        var end = endDate.Date;

        return await _context.Meters
                             .Include(e => e.EnergyType)
                             .Include(a => a.Address)
                             .Where(m => m.AddressId == addressId
                                      && m.EnergyTypeId == energyTypeId
                                      && m.ActiveFrom <= end
                                      && (m.ActiveTill == null || m.ActiveTill >= start))
                             .OrderBy(m => m.ActiveFrom)
                             .ToListAsync();
    }

    public async Task<Models.Meter?> SelectDefaultMeterByAddress(long addressId, long energyTypeId)
    {
        var today = DateTime.Today;

        return await _context.Meters
                             .Include(e => e.EnergyType)
                             .Include(a => a.Address)
                             .Where(n => n.EnergyType.Id == energyTypeId
                                      && n.Address.Id == addressId
                                      && n.Active == true
                                      && n.ActiveFrom <= today
                                      && (n.ActiveTill == null || n.ActiveTill >= today))
                             .FirstOrDefaultAsync();
    }
}
