using Rocky.Domain.Common;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rocky.Domain.Interfaces.Common
{
    public interface IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IQueryable<TEntity>> SelectAsync();
        Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, Expression<Func<TEntity, object>>[] includes = null, bool isTracking = true);
        Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includes = null, bool isTracking = true);
        Task<TEntity> FirstOrDefaultAsync(int id);
        Task<TEntity> AddAsync(TEntity entity, bool saveAutomatically = true);
        Task UpdateAsync(TEntity entity, bool saveAutomatically = true);
        Task DeleteAsync(TEntity entity, bool saveAutomatically = true);
        Task DeleteAsync(int id, bool saveAutomatically = true);
        Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);
        Task<bool> ExistsAsync(int id);
        Task SaveChangesAsync();
    }
}
