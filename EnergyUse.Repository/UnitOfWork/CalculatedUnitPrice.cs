using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class CalculatedUnitPrice : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoCalculatedUnitPrice CalculatedUnitPriceRepo;
        public RepoEnergyType EnergyTypeRepo;
        public RepoTariffGroup TarifGroupRepo;

        public List<Models.CalculatedUnitPrice> CalculatedUnitPrices = new();

        public CalculatedUnitPrice(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            CalculatedUnitPriceRepo = new RepoCalculatedUnitPrice(_context);
            EnergyTypeRepo = new RepoEnergyType(_context);
            TarifGroupRepo = new RepoTariffGroup(_context);
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
            CalculatedUnitPriceRepo.RejectChanges();
        }

        public void Delete(Models.CalculatedUnitPrice entity)
        {
            CalculatedUnitPriceRepo.Remove(entity);
            CalculatedUnitPriceRepo.Remove(entity);
        }

        public Models.CalculatedUnitPrice AddDefaultEntity(long energyTypeId, long tariffGroupId)
        {
            var entity = new Models.CalculatedUnitPrice();
            entity.EnergyTypeId = energyTypeId;
            entity.TariffGroupId = tariffGroupId;
            entity.Year = GetDefaultYear(energyTypeId, tariffGroupId);
            entity.Price = 0;

            CalculatedUnitPriceRepo.Add(entity);
            CalculatedUnitPrices.Add(entity);

            return entity;
        }

        public int GetDefaultYear(long energyTypeId, long tariffGroupId)
        {
           var lastCalculatedUnitPrice = CalculatedUnitPriceRepo.SelectLastYear(energyTypeId, tariffGroupId);
            if (lastCalculatedUnitPrice != null)
                return lastCalculatedUnitPrice.Year + 1;
            else
                return DateTime.Now.Year;
        }
        public void SetListSorted()
        {
            CalculatedUnitPrices = CalculatedUnitPrices.OrderByDescending(o => o.Year).ToList();
        }

        public int GetPosition(Models.CalculatedUnitPrice entity)
        {
            return CalculatedUnitPrices.IndexOf(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
