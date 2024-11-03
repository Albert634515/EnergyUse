using EnergyUse.Core.Context;

namespace EnergyUse.Core.Repositories;

public class RepoCalculationType : RepoGeneral<Models.CalculationType>
{
    private readonly EnergyUseContext _context;

    public RepoCalculationType(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.CalculationType? SelectByDescription(string description)
    {
        return _context.CalculationTypes
                       .Where(w => w.Description == description).FirstOrDefault();
    }
}