using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class Staffel : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoStaffel StaffelRepo;
    public List<Models.Staffel> Staffels = new();

    public Staffel(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        StaffelRepo = new RepoStaffel(_context);
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public bool HasChanges()
    {
        return _context.ChangeTracker.HasChanges();
    }

    public void CancelChanges()
    {
        StaffelRepo.RejectChanges();
    }

    public void Delete(Models.Staffel entity)
    {
        StaffelRepo.Remove(entity);
    }

    public Models.Staffel AddDefaultEntity(long rateId)
    {
        var entity = new Models.Staffel();
        entity.RateId = rateId;

        StaffelRepo.Add(entity);

        return entity;
    }
    public void SetListSorted()
    {
        Staffels = Staffels.OrderByDescending(o => o.StaffelValue).ToList();
    }

    public int GetPosition(Models.Staffel entity)
    {
        return Staffels.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}