using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;
using static iText.IO.Util.IntHashtable;

namespace EnergyUse.Core.Repositories
{
    public class RepoStaffel : RepoGeneral<Models.Staffel>
    {
        private readonly EnergyUseContext _context;

        public RepoStaffel(EnergyUseContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public IEnumerable<Models.Staffel> SelectByRateId(long rateId)
        {
            return _context.Set<Models.Staffel>()
                           .Include(p => p.Rate)
                           .Where(w => w.RateId == rateId);
        }

        public void DeleteByRateId(long rateId)
        {
            _context.Set<Models.Staffel>()
                           .Include(p => p.Rate)
                           .Where(w => w.RateId == rateId).ExecuteDelete();
        }
    }
}