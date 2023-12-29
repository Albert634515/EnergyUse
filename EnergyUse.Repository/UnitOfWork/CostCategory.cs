using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class CostCategory : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoEnergyType EnergyTypeRepo;
        public RepoEnergySubType EnergySubTypeRepo;
        public RepoCostCategories CostCategoryRepo;
        public RepoCalculationType CalculationTypeRepo;
        public RepoTariffGroup TariffGroupRepo;
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
            TariffGroupRepo = new RepoTariffGroup(_context);
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
            TariffGroupRepo.RejectChanges();
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
            var entity = new Models.CostCategory();
            entity.Name = defaultDescription;
            entity.EnergyTypeId = energyTypeId;
            entity.Start = DateTime.Now.Date;
            entity.End = DateTime.Now.AddYears(100).Date;

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
}
