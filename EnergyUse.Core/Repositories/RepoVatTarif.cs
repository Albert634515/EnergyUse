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

    public async Task<Models.VatTarif?> Get(int id)
    {
        return await _context.Set<Models.VatTarif>()
                             .Include(s => s.CostCategory)
                             .Where(s => s.Id == id)
                             .FirstOrDefaultAsync();
    }

    public async Task<List<Models.VatTarif>> GetAll()
    {
        return await _context.Set<Models.VatTarif>()
                             .Include(s => s.CostCategory)
                             .ToListAsync();
    }

    public async Task<List<Models.VatTarif>> GetByCostCategoryId(long costCategoryId)
    {
        return await _context.Set<Models.VatTarif>()
                             .Include(s => s.CostCategory)
                             .Where(x => x.CostCategoryId == costCategoryId)
                             .ToListAsync();
    }

    public async Task<Models.VatTarif?> GetByCostCategoryIdAndDate(long costCategoryId, DateTime date)
    {
        return await _context.Set<Models.VatTarif>()
                             .Include(s => s.CostCategory)
                             .Where(x => x.CostCategoryId == costCategoryId && x.StartDate.Date <= date.Date && x.EndDate.Date >= date.Date)
                             .FirstOrDefaultAsync();
    }

    public async Task<Models.VatTarif?> GetLastTarif(long costCategoryId, DateTime lastDate)
    {
        return await _context.Set<Models.VatTarif>()
                             .Include(c => c.CostCategory)
                             .Where(x => x.CostCategoryId == costCategoryId && x.StartDate.Date <= lastDate.Date)
                             .OrderByDescending(o => o.StartDate)
                             .FirstOrDefaultAsync();
    }
}