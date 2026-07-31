using System.Linq.Expressions;

namespace EnergyUse.Core.Interfaces;

internal interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity?> Get<T>(T id, CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);

    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);
}
