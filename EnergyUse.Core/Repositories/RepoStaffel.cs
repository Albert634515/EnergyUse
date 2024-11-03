using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

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

    public IEnumerable<Models.Staffel> SelectByRateIdAndRange(long rateId, long maxRange)
    {
        return _context.Set<Models.Staffel>()
                       .Include(p => p.Rate)
                       .Where(w => w.RateId == rateId 
                                && w.ValueFrom <= maxRange
                                && w.ValueTill >= maxRange);
    }

    public void DeleteByRateId(long rateId)
    {
        _context.Set<Models.Staffel>()
                       .Include(p => p.Rate)
                       .Where(w => w.RateId == rateId).ExecuteDelete();
    }
}