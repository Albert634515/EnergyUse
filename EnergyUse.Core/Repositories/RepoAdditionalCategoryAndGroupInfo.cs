using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoAdditionalCategoryAndGroupInfo : RepoGeneral<Models.AdditionalCategoryAndGroupInfo>
{
    private readonly EnergyUseContext _context;

    public RepoAdditionalCategoryAndGroupInfo(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.AdditionalCategoryAndGroupInfo? SelectByPrimaryKey(long energyId, long categoryId, long groupId)
    {
        return _context.Set<Models.AdditionalCategoryAndGroupInfo>()
                       .Include(e => e.EnergyType)
                       .Include(c => c.CostCategory)
                       .Include(s => s.TariffGroup)
                       .Where(w => w.EnergyTypeId == energyId && w.CostCategoryId == categoryId && w.TariffGroupId == groupId).FirstOrDefault();
    }
}