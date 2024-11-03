using EnergyUse.Core.Context;

namespace EnergyUse.Core.Repositories;

public class RepoTariffGroup : RepoGeneral<Models.TariffGroup>
{
    private readonly EnergyUseContext _context;

    public RepoTariffGroup(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.TariffGroup? SelectByDescription(string description)
    {
        return _context.TariffGroups
                       .Where(s => s.Description == description).FirstOrDefault();
    }
}