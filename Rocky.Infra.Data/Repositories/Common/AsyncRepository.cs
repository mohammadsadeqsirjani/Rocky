using Microsoft.EntityFrameworkCore;
using Rocky.Domain.Common;
using Rocky.Domain.Interfaces.Common;
using Rocky.Infra.Data.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Rocky.Infra.Data.Repositories.Common
{
    public class AsyncRepository<TEntity> : IAsyncRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public AsyncRepository(ApplicationDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public Task<IQueryable<TEntity>> SelectAsync() => Task.FromResult(DbSet.AsNoTracking());

        public Task<IQueryable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, Expression<Func<TEntity, object>>[] includes = null, bool isTracking = true)
        {
            var query = DbSet.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (order != null)
                query = order(query);

            query = isTracking ? query : query.AsNoTracking();

            return Task.FromResult(query);

        }

        public async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includes = null, bool isTracking = true)
        {
            var query = DbSet.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            query = isTracking ? query : query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public async Task<TEntity> FirstOrDefaultAsync(int id) => await DbSet.FirstOrDefaultAsync(p => p.Id == id);

        public virtual async Task<TEntity> AddAsync(TEntity entity, bool saveAutomatically = true)
        {
            await DbSet.AddAsync(entity);

            if (saveAutomatically)
                await SaveChangesAsync();

            return entity;
        }

        public virtual async Task UpdateAsync(TEntity entity, bool saveAutomatically = true)
        {
            DbSet.Update(entity);

            if (saveAutomatically)
                await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(TEntity entity, bool saveAutomatically = true)
        {
            DbSet.Remove(entity);

            if (saveAutomatically)
                await SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(int id, bool saveAutomatically = true)
        {
            var entity = await FirstOrDefaultAsync(id);

            await DeleteAsync(entity);
        }

        public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate) => await DbSet.AnyAsync(predicate);

        public virtual async Task<bool> ExistsAsync(int id) => await ExistsAsync(p => p.Id == id);

        public virtual async Task SaveChangesAsync() => await Db.SaveChangesAsync();
    }
}
