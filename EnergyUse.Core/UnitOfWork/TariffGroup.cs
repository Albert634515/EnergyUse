using EnergyUse.Core.Context;
using Microsoft.EntityFrameworkCore;
using EnergyUse.Core.Interfaces;
using EnergyUse.Core.Repositories;

namespace EnergyUse.Core.UnitOfWork;

public class TariffGroup : IUnitOfWork
{
    private readonly EnergyUseContext _context;

    public RepoTariffGroup TariffGroupRepo;
    public List<Models.CostCategory> CostCategories { get; }
    private List<Models.TariffGroupCostCategory> _categoryLinks;

    public bool IsCostCategoryAllowed(Models.TariffGroup group, long categoryId) =>
        _categoryLinks.Any(link => ReferenceEquals(link.TariffGroup, group) && link.CostCategoryId == categoryId);

    public void SetCostCategoryAllowed(Models.TariffGroup group, long categoryId, bool allowed)
    {
        var link = _categoryLinks.FirstOrDefault(item => ReferenceEquals(item.TariffGroup, group)
            && item.CostCategoryId == categoryId);
        if (allowed && link == null)
        {
            var deleted = _context.ChangeTracker.Entries<Models.TariffGroupCostCategory>()
                .FirstOrDefault(entry => entry.State == EntityState.Deleted
                    && ReferenceEquals(entry.Entity.TariffGroup, group) && entry.Entity.CostCategoryId == categoryId);
            if (deleted != null)
            {
                deleted.State = EntityState.Unchanged;
                link = deleted.Entity;
            }
            else
            {
                link = new Models.TariffGroupCostCategory { TariffGroup = group, CostCategoryId = categoryId };
                _context.TariffGroupCostCategories.Add(link);
            }
            _categoryLinks.Add(link);
        }
        else if (!allowed && link != null)
        {
            _context.TariffGroupCostCategories.Remove(link);
            _categoryLinks.Remove(link);
        }
    }

    public List<Models.TariffGroup> TariffGroups = new();

    public TariffGroup(string dbFileName)
    {
        _context = new EnergyUseContext(dbFileName);

        TariffGroupRepo = new RepoTariffGroup(_context);
        CostCategories = _context.CostCategories.AsNoTracking().Include(category => category.EnergyType).OrderBy(category => category.EnergyTypeId)
            .ThenBy(category => category.SortOrder).ToList();
        _categoryLinks = _context.TariffGroupCostCategories.Include(link => link.TariffGroup).ToList();
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
        _context.ChangeTracker.Clear();
        _categoryLinks = _context.TariffGroupCostCategories.Include(link => link.TariffGroup).ToList();
    }

    public void Delete(Models.TariffGroup entity)
    {
        TariffGroupRepo.Remove(entity);
        TariffGroups.Remove(entity);
    }

    public Models.TariffGroup SetDefaultEntity(string defaultDescription)
    {
        var entity = new Models.TariffGroup();
        entity.Description = defaultDescription;
        entity.TypeId = 2;

        TariffGroupRepo.Add(entity);
        TariffGroups.Add(entity);

        return entity;
    }

    public int GetPosition(Models.TariffGroup entity)
    {
        return TariffGroups.IndexOf(entity);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
