using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class Export : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoEnergyType EnergyTypeRepo;
    public RepoGeneral<Models.Address> AddressRepo;

    public Export(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        EnergyTypeRepo = new RepoEnergyType(_context);
        AddressRepo = new RepoGeneral<Models.Address>(_context);
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
        EnergyTypeRepo.RejectChanges();
        AddressRepo.RejectChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}