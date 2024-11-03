using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class RatingReport : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoCostCategories CostCategoriesRepo;
    public RepoRate RateRepo;
    public RepoTariffGroup TariffGroupRepo;

    public RatingReport(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        CostCategoriesRepo = new RepoCostCategories(_context);
        RateRepo = new RepoRate(_context);
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
        CostCategoriesRepo.RejectChanges();
        RateRepo.RejectChanges();
        TariffGroupRepo.RejectChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}