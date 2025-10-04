using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoRate : RepoGeneral<Models.Rate>
{
    private readonly EnergyUseContext _context;

    public RepoRate(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.Rate? SelectById(long rateId)
    {
        return _context.Rates
                       .Include(c => c.CostCategory)
                       .Include(t => t.TariffGroup)
                       .Include(e => e.EnergyType)
                       .Where(x => x.Id == rateId)
                       .FirstOrDefault();
    }

    public IEnumerable<Models.Rate> SelectByCostCategoryAndEnergyTypeAndTarifGroup(long costCategoryId, long energyTypeId, long tarifGroupId)
    {
        return _context.Rates
                       .Include(c => c.CostCategory)
                       .Include(t => t.TariffGroup)
                       .Include(e => e.EnergyType)
                       .Where(x => x.CostCategoryId == costCategoryId 
                                && x.EnergyTypeId == energyTypeId 
                                && x.TariffGroupId == tarifGroupId)
                       .OrderBy(o => o.StartRate);
    }

    public IEnumerable<Models.Rate> SelectByCostCategoryAndDate(long energyTypeId, long costCategoryId, DateTime startDate, DateTime endDate, long tarifGroupId)
    {
        return _context.Rates
            .Include(c => c.CostCategory)
            .Include(t => t.TariffGroup)
            .Include(e => e.EnergyType)
            .Where(x => x.EnergyTypeId == energyTypeId 
                     && x.CostCategoryId == costCategoryId 
                     && x.TariffGroupId == tarifGroupId 
                     && (x.StartRate.Date <= endDate.Date && x.EndRate.Date >= startDate.Date));
    }

    public Models.Rate? SelectLastRateByDate(long energyTypeId, long costCategoryId, DateTime lastDate, long tarifGroupId)
    {
        return _context.Rates
            .Include(c => c.CostCategory)
            .Include(t => t.TariffGroup)
            .Include(e => e.EnergyType)
            .Where(x => x.CostCategoryId == costCategoryId 
                     && x.TariffGroupId == tarifGroupId
                     && x.EnergyTypeId == energyTypeId
                     && x.StartRate.Date <= lastDate.Date)
             .OrderByDescending(o=> o.StartRate).FirstOrDefault();
    }

    public Models.Rate? SelectLastRate(long energyTypeId, long costCategoryId, long tarifGroupId)
    {
        return _context.Rates
            .Include(c => c.CostCategory)
            .Include(t => t.TariffGroup)
            .Include(e => e.EnergyType)
            .Where(x => x.CostCategoryId == costCategoryId
                     && x.TariffGroupId == tarifGroupId
                     && x.EnergyTypeId == energyTypeId)
             .OrderByDescending(o => o.StartRate).FirstOrDefault();
    }

}