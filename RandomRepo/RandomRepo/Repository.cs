using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RandomRepo
{
    public abstract class Repository<TEntity, TContext> :
        IRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext Context;

        protected Repository(TContext dbContext)
        {
            Context = dbContext;
        }


        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> filter)
        {
            return Context
                          .Set<TEntity>()
                          .Where(filter)
                          .ToList();
        }

        public virtual TEntity GetSingle(Expression<Func<TEntity, bool>> filter)
        {
            return Context.Set<TEntity>()
                          .FirstOrDefault(filter);
        }

        public TEntity GetFirst(Expression<Func<TEntity, bool>> filter)
        {
            return Context
                          .Set<TEntity>()
                          .First(filter);
        }

        public virtual TEntity Add(TEntity entity)
        {
            Context
                .Set<TEntity>()
                .Add(entity);
            Context.SaveChanges();

            return entity;
        }

        public virtual TEntity[] AddRange(IEnumerable<TEntity> entities)
        {
            Context
                .Set<TEntity>()
                .AddRange(entities);
            Context.SaveChanges();

            return entities.ToArray();
        }

        public virtual bool Update(TEntity entity)
        {
            Context
                .Set<TEntity>()
                .Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChanges() > 0;
        }

        public virtual bool UpdateRange(IEnumerable<TEntity> entities)
        {
            Context
                .Set<TEntity>()
                .AttachRange(entities);
            foreach (TEntity entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            return Context.SaveChanges() > 0;
        }

        public virtual bool Remove(TEntity entity)
        {
            Context
                .Set<TEntity>()
                .Remove(entity);

            return Context.SaveChanges() > 0;
        }

        public virtual bool RemoveRange(IEnumerable<TEntity> entities)
        {
            Context
                .Set<TEntity>()
                .RemoveRange(entities);

            return Context.SaveChanges() > 0;
        }
    }
}
