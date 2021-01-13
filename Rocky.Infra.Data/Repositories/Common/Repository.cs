using Microsoft.EntityFrameworkCore;
using Rocky.Domain.Common;
using Rocky.Domain.Interfaces.Common;
using Rocky.Infra.Data.Persistence;
using Rocky.Infra.Data.Scrutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rocky.Infra.Data.Repositories.Common
{
    public class Repository<TEntity, TKey> : IScoped, IRepository<TEntity, TKey> where TEntity : class, IBaseEntity<TKey>
    {
        protected readonly ApplicationDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ApplicationDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Select() => DbSet.AsNoTracking();

        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression) => Select(expression, null, false);
        public virtual IEnumerable<TEntity> Select(params Expression<Func<TEntity, object>>[] includes) => Select(null, null, false, includes);
        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order) => Select(expression, order, false);
        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, bool isTracking) => Select(expression, order, isTracking, null);
        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, params Expression<Func<TEntity, object>>[] includes) => Select(expression, order, false, includes);
        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes) => Select(expression, null, false, includes);
        public virtual IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, bool isTracking = true, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (order != null)
                query = order(query);

            query = isTracking ? query : query.AsNoTracking();

            return query.ToList();
        }

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression) => FirstOrDefault(expression, false);
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking) => FirstOrDefault(expression, isTracking, null);
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes) => FirstOrDefault(expression, false, includes);
        public virtual TEntity FirstOrDefault(params Expression<Func<TEntity, object>>[] includes) => FirstOrDefault(null, false, includes);

        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking, params Expression<Func<TEntity, object>>[] includes)
        {
            IQueryable<TEntity> query = DbSet;

            if (expression != null)
                query = query.Where(expression);

            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            query = isTracking ? query : query.AsNoTracking();

            return query.FirstOrDefault();
        }

        public virtual TEntity FirstOrDefault(TKey id) => DbSet.FirstOrDefault(p => Equals(p.Id, id));

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

        public virtual void Delete(TKey id, bool saveAutomatically = true)
        {
            var entity = FirstOrDefault(id);

            Delete(entity);
        }

        public virtual void Delete(Expression<Func<TEntity, bool>> expression, bool saveAutomatically = true)
        {
            var entities = Select(expression);

            foreach (var entity in entities) Delete(entity, false);

            if (saveAutomatically)
                SaveChanges();
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate) => DbSet.Any(predicate);

        public virtual bool Exists(TKey id) => Exists(p => Equals(p.Id, id));

        public virtual void SaveChanges() => Db.SaveChanges();
    }
}
