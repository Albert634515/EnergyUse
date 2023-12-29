using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class Payment : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoPayment PaymentRepo;
        public RepoAddress AddressRepo;
        public RepoPreDefinedPeriod PreDefinedPeriodRepo;

        public List<Models.Payment> Payments = new();

        public Payment(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            PaymentRepo = new RepoPayment(_context);
            AddressRepo = new RepoAddress(_context);
            PreDefinedPeriodRepo = new RepoPreDefinedPeriod(_context);
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
            PaymentRepo.RejectChanges();
        }

        public void Delete(Models.Payment entity)
        {
            PaymentRepo.Remove(entity);
        }

        public Models.Payment AddDefaultEntity(string defaultDescription, long addressId, long periodId)
        {
            var entity = new Models.Payment();
            entity.Description = defaultDescription;
            entity.AddressId = addressId;
            entity.PreDefinedPeriodId = periodId;
            entity.PayDate = DateTime.Now;

            PaymentRepo.Add(entity);
            Payments.Add(entity);

            return entity;
        }
        public void SetListSorted()
        {
            Payments = Payments.OrderByDescending(o => o.PayDate).ToList();
        }

        public int GetPosition(Models.Payment entity)
        {
            return Payments.IndexOf(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}