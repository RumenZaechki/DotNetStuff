using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace RandomRepo.Async
{
    public abstract class AsyncRepository<TEntity, TContext> :
        IAsyncRepository<TEntity>
        where TEntity : class
        where TContext : DbContext
    {
        protected readonly TContext Context;

        protected AsyncRepository(TContext dbContext)
        {
            Context = dbContext;
        }
        public async Task<TEntity> AddAsync(TEntity entity, CancellationToken token)
        {
            await Context
                .Set<TEntity>()
                .AddAsync(entity, token);
            await Context.SaveChangesAsync(token);

            return entity;
        }

        public async Task<TEntity[]> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token)
        {
            await Context
                .Set<TEntity>()
                .AddRangeAsync(entities, token);
            await Context.SaveChangesAsync(token);

            return entities.ToArray();
        }

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            return await Context
                          .Set<TEntity>()
                          .Where(filter)
                          .ToListAsync(token);
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            return await Context
                          .Set<TEntity>()
                          .FirstAsync(filter, token);
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token)
        {
            return await Context.Set<TEntity>()
                          .FirstOrDefaultAsync(filter, token);
        }

        public async Task<bool> RemoveAsync(TEntity entity, CancellationToken token)
        {
            Context
                .Set<TEntity>()
                .Remove(entity);

            return await Context.SaveChangesAsync(token) > 0;
        }

        public async Task<bool> RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken token)
        {
            Context
                .Set<TEntity>()
                .RemoveRange(entities);

            return await Context.SaveChangesAsync(token) > 0;
        }

        public async Task<bool> UpdateAsync(TEntity entity, CancellationToken token)
        {
            Context
                .Set<TEntity>()
                .Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
            return await Context.SaveChangesAsync(token) > 0;
        }

        public async Task<bool> UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken token)
        {
            Context
                .Set<TEntity>()
                .AttachRange(entities);
            foreach (TEntity entity in entities)
            {
                Context.Entry(entity).State = EntityState.Modified;
            }
            return await Context.SaveChangesAsync(token) > 0;
        }
    }
}
