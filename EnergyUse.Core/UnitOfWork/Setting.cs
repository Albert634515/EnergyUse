using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class Setting : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoSettings SettingsRepo;

        public Setting(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            SettingsRepo = new RepoSettings(_context);
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
            SettingsRepo.RejectChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
