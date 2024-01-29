using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class Settlement : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoAddress AddressRepo;
        public RepoCostCategories CostCategoriesRepo;
        public RepoVatTarif VatTarifRepo;
        public RepoMeterReading MeterReadingRepo;
        public RepoPayment PaymentRepo;

        public Settlement(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            AddressRepo = new RepoAddress(_context);
            CostCategoriesRepo = new RepoCostCategories(_context);
            VatTarifRepo = new RepoVatTarif(_context);
            MeterReadingRepo = new RepoMeterReading(_context);
            PaymentRepo = new RepoPayment(_context);
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
            CostCategoriesRepo.RejectChanges();
            VatTarifRepo.RejectChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
