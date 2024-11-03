using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class VatTarif : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoVatTarif VatTarifRepo;
    public RepoEnergyType EnergyTypeRepo;
    public RepoCostCategories CostCategoryRepo;

    public List<Models.VatTarif> VatTarifs = new();

    public VatTarif(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        VatTarifRepo = new RepoVatTarif(_context);
        EnergyTypeRepo = new RepoEnergyType(_context);
        CostCategoryRepo = new RepoCostCategories(_context);
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
        VatTarifRepo.RejectChanges();
        EnergyTypeRepo.RejectChanges();
        CostCategoryRepo.RejectChanges();
    }

    public void Delete(Models.VatTarif entity)
    {
        VatTarifRepo.Remove(entity);
        VatTarifs.Remove(entity);
    }

    public Models.VatTarif AddDefaultEntity(long costCategoryId)
    {
        var entity = new Models.VatTarif();
        entity.CostCategoryId = costCategoryId;
        entity.StartDate = DateTime.Now.Date;
        entity.EndDate = DateTime.Now.Date;

        VatTarifRepo.Add(entity);
        VatTarifs.Add(entity);

        SetListSorted();

        return entity;
    }

    public void SetListSorted()
    {
        VatTarifs = VatTarifs.OrderByDescending(o => o.StartDate).ToList();
    }

    public int GetPosition(Models.VatTarif entity)
    {
       return VatTarifs.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}