using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoEnergySubType : RepoGeneral<Models.EnergySubType>
{
    private readonly EnergyUseContext _context;

    public RepoEnergySubType(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.EnergySubType? SelectByDescription(string description)
    {
        return _context.EnergySubTypes
                       .Where(s => s.Description == description)
                       .AsNoTracking()
                       .FirstOrDefault();
    }
}