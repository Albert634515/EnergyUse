using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class TariffGroup : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoTariffGroup TariffGroupRepo;

    public List<Models.TariffGroup> TariffGroups = new();

    public TariffGroup(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        TariffGroupRepo = new RepoTariffGroup(_context);
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
        TariffGroupRepo.RejectChanges();
    }

    public void Delete(Models.TariffGroup entity)
    {
        TariffGroupRepo.Remove(entity);
        TariffGroups.Remove(entity);
    }

    public Models.TariffGroup AddDefaultEntity(string defaultDescription)
    {
        var entity = new Models.TariffGroup();
        entity.Description = defaultDescription;

        TariffGroupRepo.Add(entity);
        TariffGroups.Add(entity);

        return entity;
    }

    public int GetPosition(Models.TariffGroup entity)
    {
        return TariffGroups.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}