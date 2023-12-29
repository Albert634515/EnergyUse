using EnergyUse.Common.Libs;
using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class Rate : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoRate RateRepo;
        public RepoEnergyType EnergyTypeRepo;
        public RepoCostCategories CostCategoryRepo;
        public RepoTariffGroup TarifGroupRepo;
        public RepoStaffel StaffelRepo;
        public RepoAdditionalCategoryAndGroupInfo AdditionalCategoryAndGroupInfoRepo;

        public List<Models.Rate> RateList = new();

        public Rate(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            RateRepo = new RepoRate(_context);
            EnergyTypeRepo = new RepoEnergyType(_context);
            CostCategoryRepo = new RepoCostCategories(_context);
            TarifGroupRepo = new RepoTariffGroup(_context);
            StaffelRepo = new RepoStaffel(_context);
            AdditionalCategoryAndGroupInfoRepo = new RepoAdditionalCategoryAndGroupInfo(_context);
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
            CostCategoryRepo.RejectChanges();
            TarifGroupRepo.RejectChanges();
            RateRepo.RejectChanges();
            StaffelRepo.RejectChanges();
            AdditionalCategoryAndGroupInfoRepo.RejectChanges();
        }

        public void Delete(Models.Rate rate)
        {
            if (rate.Id > 0)
            {
                RateRepo.Remove(rate);
                Complete();
            }

            RateList.Remove(rate);
        }

        public Models.Rate AddDefaultEntity(long energyTypeId, long costCategoryId, long tarifGroupId)
        {
            var entity = new Models.Rate();
            entity.RateTypeId = (int)Common.Enums.RateType.FixedPrice;
            entity.CostCategoryId = costCategoryId;
            entity.EnergyTypeId = energyTypeId;
            entity.TariffGroupId = tarifGroupId;
            entity.StartRate = DateTime.Now.Date;
            entity.EndRate = DateTime.Now.Date;
            entity.RateValue = 0;

            Models.Rate? lastEntity = RateRepo.SelectLastRate(energyTypeId, costCategoryId, tarifGroupId);
            if (lastEntity != null)
            {
                entity.StartRate = lastEntity.EndRate.AddDays(1);

                var monthDiff = Manager.LibGeneral.MonthDiff(entity.StartRate, entity.EndRate);
                if (monthDiff <= 0)
                    monthDiff = 1;

                entity.EndRate = entity.StartRate.AddMonths(monthDiff);
            }

            RateRepo.Add(entity);
            RateList.Add(entity);

            SetListSorted();

            return entity;
        }

        public void SetListSorted()
        {
            RateList = RateList.OrderByDescending(o => o.StartRate).ToList();
        }

        public int GetPosition(Models.Rate rate)
        {
            return RateList.IndexOf(rate);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
