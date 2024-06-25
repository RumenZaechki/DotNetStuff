using System.Linq.Expressions;

namespace RandomRepo
{
    public interface IRepository<TEntity>
        where TEntity : class
    {
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter);

        public TEntity GetSingle(Expression<Func<TEntity, bool>> filter);

        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter);
        public TEntity Add(TEntity entity);

        public TEntity[] AddRange(IEnumerable<TEntity> entities);

        public bool Update(TEntity entity);

        public bool UpdateRange(IEnumerable<TEntity> entities);

        public bool Remove(TEntity entity);

        public bool RemoveRange(IEnumerable<TEntity> entities);
    }
}
