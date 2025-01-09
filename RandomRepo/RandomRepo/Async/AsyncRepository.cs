using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RandomRepo.Async
{
    public abstract class AsyncRepository<TEntity, TContext> :
        IAsyncRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        private readonly DbSet<TEntity> _entity;
        protected readonly TContext Context;

        protected AsyncRepository(TContext dbContext)
        {
            Context = dbContext;
            _entity = Context.Set<TEntity>();
        }
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken token)
        {
            await _entity.AddAsync(entity, token);
            return entity;
        }

        public async Task<TEntity[]> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token)
        {
            await _entity.AddRangeAsync(entities, token);
            return entities.ToArray();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            return await _entity
                          .Where(filter)
                          .ToListAsync(token);
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            return await _entity.FirstAsync(filter, token);
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            return await _entity.FirstOrDefaultAsync(filter, token);
        }

        public bool Remove(TEntity entity, CancellationToken token)
        {
            _entity.Remove(entity);
            return Context.Entry(entity).State == EntityState.Deleted;
        }

        public bool RemoveRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            _entity.RemoveRange(entities);
            return entities.Any(e => Context.Entry(e).State == EntityState.Deleted);
        }

        public bool Update(TEntity entity, CancellationToken token)
        {
            _entity.Update(entity);
            return Context.Entry(entity).State == EntityState.Modified;
        }

        public bool UpdateRange(IEnumerable<TEntity> entities, CancellationToken token)
        {
            _entity.UpdateRange(entities);
            return entities.Any(e => Context.Entry(e).State == EntityState.Modified);
        }
    }
}
