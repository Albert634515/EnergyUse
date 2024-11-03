using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class Meter : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoMeter MeterRepo;
    public RepoEnergyType EnergyTypeRepo;
    public RepoAddress AddressRepo;

    public List<Models.Meter> Meters = new();

    public Meter(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        MeterRepo = new RepoMeter(_context);
        EnergyTypeRepo = new RepoEnergyType(_context);
        AddressRepo = new RepoAddress(_context);
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
        MeterRepo.RejectChanges();
        EnergyTypeRepo.RejectChanges();
        AddressRepo.RejectChanges();
    }

    public void Delete(Models.Meter entity)
    {
        MeterRepo.Remove(entity);
        Meters.Remove(entity);
    }

    public Models.Meter AddDefaultEntity(string defaultDescription)
    {
        var entity = new Models.Meter();
        entity.Description = defaultDescription;
        entity.ActiveFrom = DateTime.Now.Date;

        MeterRepo.Add(entity);
        Meters.Add(entity);

        return entity;
    }

    public int GetPosition(Models.Meter entity)
    {
        return Meters.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}