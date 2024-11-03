using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoVatTarif : RepoGeneral<Models.VatTarif>
{
    private readonly EnergyUseContext _context;

    public RepoVatTarif(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.VatTarif Get(int id)
    {
        return _context.Set<Models.VatTarif>()
                       .Include(s => s.CostCategory)
                       .Where(s => s.Id == id).FirstOrDefault();
    }

    public new IEnumerable<Models.VatTarif> GetAll()
    {
        return _context.Set<Models.VatTarif>()
                       .Include(s => s.CostCategory);
    }

    public IEnumerable<Models.VatTarif> GetByCostCategoryId(long costCategoryId)
    {
        return _context.Set<Models.VatTarif>()
                       .Include(s => s.CostCategory)
                       .Where(x => x.CostCategoryId == costCategoryId);
    }

    public Models.VatTarif? GetByCostCategoryIdAndDate(long costCategoryId, DateTime date)
    {
        return _context.Set<Models.VatTarif>()
                       .Include(s => s.CostCategory)
                       .Where(x => x.CostCategoryId == costCategoryId && x.StartDate.Date <= date.Date && x.EndDate.Date >= date.Date)
                       .FirstOrDefault();
    }

    public Models.VatTarif? GetLastTarif(long costCategoryId, DateTime lastDate)
    {
        return _context.Set<Models.VatTarif>()
                       .Include(c => c.CostCategory)
                       .Where(x => x.CostCategory.Id == costCategoryId && x.StartDate.Date <= lastDate.Date)
                       .OrderByDescending(o => o.StartDate).FirstOrDefault();
    }
}