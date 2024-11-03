using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class PreDefinedPeriod : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoPreDefinedPeriod PreDefinedPeriodRepo;

    public List<Models.PreDefinedPeriod> PreDefinedPeriods = new();

    public PreDefinedPeriod(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        PreDefinedPeriodRepo = new RepoPreDefinedPeriod(_context);
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
        PreDefinedPeriodRepo.RejectChanges();
    }

    public void Delete(Models.PreDefinedPeriod entity)
    {
        PreDefinedPeriodRepo.Remove(entity);
        PreDefinedPeriods.Remove(entity);
    }

    public Models.PreDefinedPeriod AddDefaultEntity(string defaultDescription)
    {
        var entity = new Models.PreDefinedPeriod();
        entity.Description = defaultDescription;

        PreDefinedPeriodRepo.Add(entity);
        PreDefinedPeriods.Add(entity);

        return entity;
    }

    public int GetPosition(Models.PreDefinedPeriod entity)
    {
        return PreDefinedPeriods.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}