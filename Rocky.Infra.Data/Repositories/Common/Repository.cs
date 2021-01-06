using Microsoft.EntityFrameworkCore;
using Rocky.Domain.Common;
using Rocky.Domain.Interfaces.Common;
using Rocky.Infra.Data.Persistence;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocky.Infra.Data.Repositories.Common
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public IQueryable<TEntity> Select() => DbSet.AsNoTracking();

        public IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, Expression<Func<TEntity, object>>[] includes = null, bool isTracking = true)
        {
            var query = DbSet.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (order != null)
                query = order(query);

            query = isTracking ? query : query.AsNoTracking();

            return query;

        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] includes = null, bool isTracking = true)
        {
            var query = DbSet.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            query = isTracking ? query : query.AsNoTracking();

            return query.FirstOrDefault();
        }

        public TEntity FirstOrDefault(int id) => DbSet.FirstOrDefault(p => p.Id == id);

        public virtual TEntity Add(TEntity entity, bool saveAutomatically = true)
        {
            DbSet.Add(entity);

            if (saveAutomatically)
                SaveChanges();

            return entity;
        }

        public virtual void Update(TEntity entity, bool saveAutomatically = true)
        {
            DbSet.Update(entity);

            if (saveAutomatically)
                SaveChanges();
        }

        public virtual void Delete(TEntity entity, bool saveAutomatically = true)
        {
            DbSet.Remove(entity);

            if (saveAutomatically)
                SaveChanges();
        }

        public virtual void Delete(int id, bool saveAutomatically = true)
        {
            var entity = FirstOrDefault(id);

            Delete(entity);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate) => DbSet.Any(predicate);

        public virtual bool Exists(int id) => Exists(p => p.Id == id);

        public virtual void SaveChanges() => Db.SaveChanges();
    }
}
