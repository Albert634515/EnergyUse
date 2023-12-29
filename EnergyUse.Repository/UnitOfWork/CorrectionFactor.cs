using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork
{
    public class CorrectionFactor : IUnitOfWork
    {
        private readonly EnergyUseContext _context;

        public RepoEnergyType EnergyTypeRepo;
        public RepoCorrectionFactor CorrectionFactorRepo;

        public List<Models.CorrectionFactor> CorrectionFactors = new();

        public CorrectionFactor(string dbFileName)
        {
            _context = _context = new EnergyUseContext(dbFileName);

            EnergyTypeRepo = new RepoEnergyType(_context);
            CorrectionFactorRepo = new RepoCorrectionFactor(_context);
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
            CorrectionFactorRepo.RejectChanges();
        }

        public void Delete(Models.CorrectionFactor entity)
        {
            CorrectionFactorRepo.Remove(entity);
            CorrectionFactors.Remove(entity);
        }

        public Models.CorrectionFactor AddDefaultEntity(long energyTypeId)
        {
            var entity = new Models.CorrectionFactor();
            var lastRow = CorrectionFactorRepo.SelectLastRow(energyTypeId);

            entity.Factor = 0;
            entity.EnergyTypeId = energyTypeId;
            entity.StartFactor = DateTime.Now.Date;
            entity.EndFactor = DateTime.Now.AddYears(1).Date;

            if (lastRow != null)
            {
                entity.StartFactor = lastRow.StartFactor.AddYears(1);
                entity.EndFactor = lastRow.EndFactor.AddYears(1);
            }

            CorrectionFactorRepo.Add(entity);
            CorrectionFactors.Add(entity);

            return entity;
        }

        public int GetPosition(Models.CorrectionFactor entity)
        {
            return CorrectionFactors.IndexOf(entity);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
