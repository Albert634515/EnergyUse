using EnergyUse.Core.Context;

namespace EnergyUse.Core.Repositories;

public class RepoUnit : RepoGeneral<Models.Unit>
{
    private readonly EnergyUseContext _context;

    public RepoUnit(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }
}