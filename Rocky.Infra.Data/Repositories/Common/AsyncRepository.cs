using Microsoft.EntityFrameworkCore;
using Rocky.Domain.Common;
using Rocky.Domain.Interfaces.Common;
using Rocky.Infra.Data.Persistence;
using System;
using System.Collections.Generic;
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

        public virtual Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression) => SelectAsync(expression, null, false);
        public virtual Task<IEnumerable<TEntity>> SelectAsync(params Expression<Func<TEntity, object>>[] includes) => SelectAsync(null, null, false, includes);
        public virtual Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order) => SelectAsync(expression, order, false);
        public virtual Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, bool isTracking) => SelectAsync(expression, order, isTracking, null);
        public virtual Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, params Expression<Func<TEntity, object>>[] includes) => SelectAsync(expression, order, false, includes);
        public virtual Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes) => SelectAsync(expression, null, false, includes);
        public virtual async Task<IEnumerable<TEntity>> SelectAsync() => await DbSet.AsNoTracking().ToListAsync();
        public virtual async Task<IEnumerable<TEntity>> SelectAsync(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, bool isTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (order != null)
                query = order(query);

            query = isTracking ? query : query.AsNoTracking();

            return await query.ToListAsync();

        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression) => FirstOrDefaultAsync(expression, false);
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool isTracking) => FirstOrDefaultAsync(expression, isTracking, null);
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes) => FirstOrDefaultAsync(expression, false, includes);

        public virtual async Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> expression, bool isTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            query = isTracking ? query : query.AsNoTracking();

            return await query.FirstOrDefaultAsync();
        }

        public virtual async Task<TEntity> FirstOrDefaultAsync(int id) => await DbSet.FirstOrDefaultAsync(p => p.Id == id);

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
