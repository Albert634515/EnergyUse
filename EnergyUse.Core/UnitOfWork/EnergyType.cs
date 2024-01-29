using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class EnergyType : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoGeneral<Models.EnergyType> EnergyTypeRepo;
        public RepoGeneral<Models.Unit> UnitRepo;

        public List<Models.EnergyType> EnergyTypes = new();
        public List<Models.Unit> Units = new();

        public EnergyType(string dbFileName)
        {
            _context = new EnergyUseContext(dbFileName);

            EnergyTypeRepo = new RepoEnergyType(_context);
            UnitRepo = new RepoGeneral<Models.Unit>(_context);
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
            UnitRepo.RejectChanges();
        }

        public void Delete(Models.EnergyType entity)
        {
            EnergyTypeRepo.Remove(entity);
            EnergyTypes.Remove(entity);
        }

        public Models.EnergyType AddDefaultEntity(string defaultDescription)
        {
            var entity = new Models.EnergyType();
            entity.Name = defaultDescription;

            EnergyTypeRepo.Add(entity);
            EnergyTypes.Add(entity);

            return entity;
        }

        public int GetPosition(Models.EnergyType entity)
        {
            return EnergyTypes.IndexOf(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
