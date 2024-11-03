using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class Address : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoAddress AddressRepo;

    public List<Models.Address> Addresses = new();

    public Address(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

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
        AddressRepo.RejectChanges();
    }

    public void Delete(Models.Address entity)
    {
        AddressRepo.Remove(entity);
        Addresses.Remove(entity);
    }

    public Models.Address AddDefaultEntity(string defaultDescription)
    {
        var entity = new Models.Address();
        entity.Description = defaultDescription;

        AddressRepo.Add(entity);
        Addresses.Add(entity);

        return entity;
    }

    public int GetPosition(Models.Address entity)
    {
        return Addresses.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}