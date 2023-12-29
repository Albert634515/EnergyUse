using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class DemoData : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoGeneral<Models.Address> AddressRepo;
        public RepoEnergyType EnergyTypeRepo;

        public DemoData(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            AddressRepo = new RepoGeneral<Models.Address>(_context);
            EnergyTypeRepo = new RepoEnergyType(_context);
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
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
