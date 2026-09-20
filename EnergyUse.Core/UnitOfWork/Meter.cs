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
        closePreviousMeters();
        return _context.SaveChanges();
    }

    private void closePreviousMeters()
    {
        var newMeters = _context.ChangeTracker
            .Entries<Models.Meter>()
            .Where(entry => entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
            .Select(entry => entry.Entity)
            .OrderBy(meter => meter.ActiveFrom)
            .ToList();

        foreach (var newMeter in newMeters)
        {
            if (!newMeter.AddressId.HasValue ||
                !newMeter.EnergyTypeId.HasValue ||
                newMeter.ActiveFrom == DateTime.MinValue)
            {
                continue;
            }

            var previousMeter = _context.Meters
                .Where(meter => meter.AddressId == newMeter.AddressId
                             && meter.EnergyTypeId == newMeter.EnergyTypeId
                             && meter.ActiveFrom < newMeter.ActiveFrom)
                .OrderByDescending(meter => meter.ActiveFrom)
                .FirstOrDefault();

            if (previousMeter == null)
                continue;

            previousMeter.ActiveTill = newMeter.ActiveFrom.Date.AddDays(-1);
            previousMeter.Active = false;
        }
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
