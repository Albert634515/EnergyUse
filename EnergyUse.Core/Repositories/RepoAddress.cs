using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoAddress : RepoGeneral<Models.Address>
{
    private readonly EnergyUseContext _context;

    public RepoAddress(EnergyUseContext dbContext) : base(dbContext)
    {
        _context = dbContext;
    }

    public Models.Address? Get(int id)
    {
        return _context.Set<Models.Address>()
                       .Include(t => t.TariffGroup)
                       .Where(w => w.Id == id).FirstOrDefault();
    }

    public Models.Address? GetByDescription(string description)
    {
        return _context.Set<Models.Address>()
                       .Include(t => t.TariffGroup)
                       .Where(w => w.Description == description).FirstOrDefault();
    }

    public new IEnumerable<Models.Address> GetAll()
    {
        return _context.Set<Models.Address>()
                       .Include(t => t.TariffGroup);
    }

    public int GetExistsByDescription(string description, long addressId)
    {
        return _context.Set<Models.Address>()
                       .Include(t => t.TariffGroup)
                       .Where(w=> w.Description == description && w.Id != addressId).Count();
    }
}