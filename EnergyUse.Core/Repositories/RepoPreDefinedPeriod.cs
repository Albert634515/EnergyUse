using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories
{
    public class RepoPreDefinedPeriod : RepoGeneral<Models.PreDefinedPeriod>
    {
        private readonly EnergyUseContext _context;

        public RepoPreDefinedPeriod(EnergyUseContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }
    }
}
