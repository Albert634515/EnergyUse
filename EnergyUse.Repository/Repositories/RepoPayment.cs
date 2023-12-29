using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories
{
    public class RepoPayment : RepoGeneral<Models.Payment>
    {
        private readonly EnergyUseContext _context;

        public RepoPayment(EnergyUseContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public Models.Payment? Get(int id)
        {
            return _context.Set<Models.Payment>()
                           .Include(s => s.PreDefinedPeriod)
                           .Where(s => s.Id == id).FirstOrDefault();
        }

        public IEnumerable<Models.Payment> SelectByAddressAndPeriod(long addressId, long periodId)
        {
            return _context.Set<Models.Payment>()
                           .Include(p => p.PreDefinedPeriod)
                           .Include(a => a.Address)
                           .Where(w => w.AddressId == addressId && w.PreDefinedPeriodId == periodId);
        }

        public IEnumerable<Models.Payment> SelectByAddressAndRange(long addressId, DateTime startDate, DateTime endDate)
        {
            return _context.Set<Models.Payment>()
                           .Include(p => p.PreDefinedPeriod)
                           .Include(a => a.Address)
                           .Where(w => w.AddressId == addressId && (w.PayDate >= startDate || w.PayDate <= endDate));
        }
    }
}