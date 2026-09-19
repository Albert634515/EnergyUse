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
        Staffels.Remove(entity);
    }

    public Models.Staffel AddDefaultEntity(long rateId)
    {
        var valueFrom = Staffels.Count == 0 ? 0 : Staffels.Max(x => x.ValueTill);
        var entity = new Models.Staffel
        {
            RateId = rateId,
            ValueFrom = valueFrom,
            ValueTill = valueFrom + 1
        };

        StaffelRepo.Add(entity);
        Staffels.Add(entity);

        return entity;
    }
    public void SetListSorted()
    {
        Staffels = Staffels.OrderBy(o => o.ValueFrom).ToList();
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
