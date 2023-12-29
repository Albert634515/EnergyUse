using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class Import : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoEnergyType EnergyTypeRepo;
        public RepoAddress AddressRepo;
        public RepoMeter MeterRepo;
        public RepoMeterReading MeterReadingRepo;

        public List<Models.MeterReading>? meterReadings = null;

        public Import(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            EnergyTypeRepo = new RepoEnergyType(_context);
            AddressRepo = new RepoAddress(_context);
            MeterRepo = new RepoMeter(_context);
            MeterReadingRepo = new RepoMeterReading(_context);
        }

        /// <summary>
        /// Save changes to db context
        /// </summary>
        /// <returns>Number of updates</returns>
        public int Complete()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// Check if there are changes in the db context
        /// </summary>
        /// <returns>Number of changes</returns>
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }

        public void CancelChanges()
        {
            EnergyTypeRepo.RejectChanges();
            AddressRepo.RejectChanges();
            MeterRepo.RejectChanges();
            MeterReadingRepo.RejectChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
