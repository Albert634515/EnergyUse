using EnergyUse.Core.Context;

namespace EnergyUse.Core.Repositories;

public class RepoSettings : RepoGeneral<Models.Setting>
{
    private readonly EnergyUseContext _context;

    public RepoSettings(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.Setting? GetByKey(string key)
    {
        return _context.Set<Models.Setting>()
                       .Where(s => s.Key == key)
                       .FirstOrDefault();
    }
}