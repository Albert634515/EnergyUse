using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories
{
    public class RepoMeter : RepoGeneral<Models.Meter>
    {
        private readonly EnergyUseContext _context;

        public RepoMeter(EnergyUseContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public new IEnumerable<Models.Meter> GetAll()
        {
            return _context.Set<Models.Meter>()
                           .Include(e => e.EnergyType)
                           .Include(a => a.Address);
        }

        public IEnumerable<Models.Meter> SelectByAddressAndEnergyType(long addressId, long energyTypeId)
        {
            return _context.Meters
                .Include(e => e.EnergyType)
                .Include(a => a.Address)
                .Where(n => n.EnergyType.Id == energyTypeId && n.Address.Id == addressId);
        }

        public Models.Meter SelectDefaultMeterByAddress(long addressId, long energyTypeId)
        {
            return _context.Meters
                .Include(e => e.EnergyType)
                .Include(a => a.Address)
                .Where(n => n.EnergyType.Id == energyTypeId && n.Address.Id == addressId && n.Active == true).FirstOrDefault();
        }
    }
}
