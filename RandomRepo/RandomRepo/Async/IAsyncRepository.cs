using System.Linq.Expressions;

namespace RandomRepo.Async
{
    public interface IAsyncRepository<TEntity>
        where TEntity : class
    {
        public Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token);

        public Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token);

        public Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> filter, CancellationToken token);
        public Task<TEntity> AddAsync(TEntity entity, CancellationToken token);

        public Task<TEntity[]> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken token);

        public bool Update(TEntity entity, CancellationToken token);

        public bool UpdateRange(IEnumerable<TEntity> entities, CancellationToken token);

        public bool Remove(TEntity entity, CancellationToken token);

        public bool RemoveRange(IEnumerable<TEntity> entities, CancellationToken token);
    }
}
