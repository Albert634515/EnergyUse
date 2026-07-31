using System.Linq.Expressions;
using EnergyUse.Core.Context;
using EnergyUse.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EnergyUse.Core.Repositories;

public class RepoGeneral<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly EnergyUseContext _context;

    public RepoGeneral(EnergyUseContext context)
    {
        _context = context;
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _context.AddRange(entities);
    }

    public async Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
                             .Where(predicate)
                             .ToListAsync(cancellationToken)
                             .ConfigureAwait(false);
    }

    public async Task<TEntity?> Get<T>(T id, CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
                             .FindAsync([id], cancellationToken)
                             .ConfigureAwait(false);
    }

    public async Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _context.Set<TEntity>()
                             .ToListAsync(cancellationToken)
                             .ConfigureAwait(false);
    }

    public void Remove(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
        _context.Entry(entity).State = EntityState.Deleted;
    }

    public void RemoveRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public void RejectChanges()
    {
        foreach (var entry in _context.ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                case EntityState.Deleted:
                    entry.State = EntityState.Modified; //Revert changes made to deleted entity.
                    entry.State = EntityState.Unchanged;
                    break;
                case EntityState.Added:
                    entry.State = EntityState.Detached;
                    break;
            }
        }
    }
}
