using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class MeterReading : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoMeterReading MeterReadingRepo;
    public RepoMeter MeterRepo;
    public RepoEnergyType EnergyTypeRepo;

    public List<Models.MeterReading> MeterReadings = new();

    public MeterReading(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        MeterReadingRepo = new RepoMeterReading(_context);
        MeterRepo = new RepoMeter(_context);
        EnergyTypeRepo = new RepoEnergyType(_context);
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
        MeterReadingRepo.RejectChanges();
        MeterRepo.RejectChanges();
    }

    public void Delete(Models.MeterReading meterReading)
    {
        MeterReadingRepo.Remove(meterReading);
        Complete();

        MeterReadings.Remove(meterReading);
    }

    public async Task<Models.MeterReading> AddDefaultEntity(long addressId, long energyTypeId)
    {
        var defaultMeter = await MeterRepo.SelectDefaultMeterByAddress(addressId, energyTypeId);
        var energyType = EnergyTypeRepo.Get(energyTypeId);
        var entity = MeterReadingRepo.GetDefaultReading(energyType, defaultMeter);

        MeterReadingRepo.Add(entity);
        MeterReadings.Add(entity);

        return entity;
    }

    public int GetPosition(Models.MeterReading meterReading)
    {
        return MeterReadings.IndexOf(meterReading);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}