using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class SetupNewFile : IUnitOfWork
    {
        private readonly EnergyUseContext _context;
                
        
        public RepoCalculationType CalculationTypeRepo;
        public RepoEnergyType EnergyTypeRepo;
        public RepoEnergySubType EnergySubTypeRepo;
        public RepoTariffGroup TarifGroupRepo;        
        public RepoCostCategories CostCategoriesRepo;
        public RepoAddress AddressRepo;
        public RepoMeter MeterRepo;

        public SetupNewFile(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);
                                   
            CalculationTypeRepo = new RepoCalculationType(_context);
            EnergyTypeRepo = new RepoEnergyType(_context);
            EnergySubTypeRepo = new RepoEnergySubType(_context);
            TarifGroupRepo = new RepoTariffGroup(_context);            
            CostCategoriesRepo = new RepoCostCategories(_context);
            AddressRepo = new RepoAddress(_context);
            MeterRepo = new RepoMeter(_context);
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
}
