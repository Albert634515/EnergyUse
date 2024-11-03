using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class SelectParameter : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoAddress AddressRepo;
    public RepoEnergyType EnergyTypeRepo;
    public RepoTariffGroup TarifGroupRepo;
    public RepoPreDefinedPeriod PreDefinedPeriodRepo;
    public RepoPredefinedPeriodDate PreDefinedPeriodDateRepo;

    public SelectParameter(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        AddressRepo = new RepoAddress(_context);
        EnergyTypeRepo = new RepoEnergyType(_context);
        TarifGroupRepo = new RepoTariffGroup(_context);
        PreDefinedPeriodRepo = new RepoPreDefinedPeriod(_context);
        PreDefinedPeriodDateRepo = new RepoPredefinedPeriodDate(_context);
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
        AddressRepo.RejectChanges();
        EnergyTypeRepo.RejectChanges();
        TarifGroupRepo.RejectChanges();
        PreDefinedPeriodRepo.RejectChanges();
        PreDefinedPeriodDateRepo.RejectChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}