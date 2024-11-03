using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class PredefinedPeriodDate : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoTariffGroup TarifGroupRepo;
    public RepoEnergyType EnergyTypeRepo;
    public RepoPredefinedPeriodDate PreDefinedPeriodDateRepo;

    public PredefinedPeriodDate(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName); 

        TarifGroupRepo = new RepoTariffGroup(_context);
        EnergyTypeRepo = new RepoEnergyType(_context);
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
        TarifGroupRepo.RejectChanges();
        EnergyTypeRepo.RejectChanges();
        PreDefinedPeriodDateRepo.RejectChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}