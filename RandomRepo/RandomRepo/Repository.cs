using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RandomRepo
{
    public abstract class Repository<TEntity, TContext> :
        IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly DbSet<TEntity> _entity;
        protected readonly TContext Context;

        protected Repository(TContext dbContext)
        {
            Context = dbContext;
            _entity = Context.Set<TEntity>();
        }


        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return _entity
                          .Where(filter)
                          .ToList();
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return _entity.FirstOrDefault(filter);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter)
        {
            return _entity.First(filter);
        }

        public virtual TEntity Add(TEntity entity)
        {
            _entity.Add(entity);
            return entity;
        }

        public virtual TEntity[] AddRange(IEnumerable<TEntity> entities)
        {
            _entity.AddRange(entities);
            return entities.ToArray();
        }

        public virtual bool Update(TEntity entity)
        {
            _entity.Update(entity);
            return Context.Entry(entity).State == EntityState.Modified;
        }

        public virtual bool UpdateRange(IEnumerable<TEntity> entities)
        {
            _entity.UpdateRange(entities);
            return entities.Any(e => Context.Entry(e).State == EntityState.Modified);
        }

        public virtual bool Remove(TEntity entity)
        {
            _entity.Remove(entity);
            return Context.Entry(entity).State == EntityState.Deleted;
        }

        public virtual bool RemoveRange(IEnumerable<TEntity> entities)
        {
            _entity.RemoveRange(entities);
            return entities.Any(e => Context.Entry(e).State == EntityState.Deleted);
        }
    }
}
