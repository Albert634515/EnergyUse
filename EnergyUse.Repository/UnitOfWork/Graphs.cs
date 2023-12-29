using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class Graphs : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoAddress AddressRepo;
        public RepoRate RateRepo;
        public RepoCostCategories CostCategoryRepo;

        public Graphs(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            AddressRepo = new RepoAddress(_context);
            RateRepo = new RepoRate(_context);
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
            AddressRepo.RejectChanges();
            RateRepo.RejectChanges();
            CostCategoryRepo.RejectChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
