using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class Netting : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoEnergyType EnergyTypeRepo;
        public RepoNetting NettingRepo;

        public List<Models.Netting> Nettings = new();

        public Netting(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            EnergyTypeRepo = new RepoEnergyType(_context);
            NettingRepo = new RepoNetting(_context);
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
            NettingRepo.RejectChanges();
        }

        public void Delete(Models.Netting entity)
        {
            NettingRepo.Remove(entity);
            Nettings.Remove(entity);
        }

        public Models.Netting AddDefaultEntity(long energyTypeId)
        {
            var entity = new Models.Netting();
            entity.EnergyTypeId = energyTypeId;
            entity.Rate = 0;

            NettingRepo.Add(entity);
            Nettings.Add(entity);

            SetListSorted();

            return entity;
        }

        public void SetListSorted()
        {
            Nettings = Nettings.OrderByDescending(o => o.StartDate).ToList();
        }

        public int GetPosition(Models.Netting entity)
        {
            return Nettings.IndexOf(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
