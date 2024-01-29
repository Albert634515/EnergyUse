using System.Linq.Expressions;

namespace EnergyUse.Core.Interfaces
{
    internal interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get<T>(T id);
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}
