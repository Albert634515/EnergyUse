using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class CostCategory : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoEnergyType EnergyTypeRepo;
    public RepoEnergySubType EnergySubTypeRepo;
    public RepoCostCategories CostCategoryRepo;
    public RepoCalculationType CalculationTypeRepo;
    public RepoUnit UnitRepo;
    public RepoVatTarif VatTarifRepo;

    public List<Models.CostCategory> CostCategories = new();

    public CostCategory(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        EnergyTypeRepo = new RepoEnergyType(_context);
        EnergySubTypeRepo = new RepoEnergySubType(_context);
        CostCategoryRepo = new RepoCostCategories(_context);
        CalculationTypeRepo = new RepoCalculationType(_context);
        UnitRepo = new RepoUnit(_context);
        VatTarifRepo = new RepoVatTarif(_context);
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
        EnergySubTypeRepo.RejectChanges();
        CostCategoryRepo.RejectChanges();
        CalculationTypeRepo.RejectChanges();
        UnitRepo.RejectChanges();
        VatTarifRepo.RejectChanges();
    }

    public void Delete(Models.CostCategory entity)
    {
        CostCategoryRepo.Remove(entity);
        CostCategories.Remove(entity);
    }

    public Models.CostCategory AddDefaultEntity(string defaultDescription, long energyTypeId)
    {
        var defaultCalculationType = _context.CalculationTypes.Find(1L)
            ?? throw new InvalidOperationException("Default calculation type 'Per Unit' was not found.");
        var defaultEnergySubType = _context.EnergySubTypes.Find(5L)
            ?? throw new InvalidOperationException("Default energy subtype 'Other' was not found.");

        var entity = new Models.CostCategory
        {
            Name = defaultDescription,
            CalculationTypeId = defaultCalculationType.Id,
            CalculationType = defaultCalculationType,
            EnergyTypeId = energyTypeId,
            EnergySubTypeId = defaultEnergySubType.Id,
            EnergySubType = defaultEnergySubType,
            TariffGroupTypeId = (int)Common.Enums.TariffGroupType.EnergyCosts,
            Start = DateTime.Now.Date,
            End = DateTime.Now.AddYears(100).Date
        };

        CostCategoryRepo.Add(entity);
        CostCategories.Add(entity);

        return entity;
    }

    public int GetPosition(Models.CostCategory entity)
    {
        return CostCategories.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
