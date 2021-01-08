using Rocky.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Rocky.Domain.Interfaces.Common
{
    public interface IRepository<TEntity, in TKey> where TEntity : IBaseEntity<TKey>
    {
        IEnumerable<TEntity> Select();
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> Select(params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order);
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, bool isTracking);
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order, bool isTracking, params Expression<Func<TEntity, object>>[] includes);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking);
        TEntity FirstOrDefault(params Expression<Func<TEntity, object>>[] includes);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, bool isTracking = true, params Expression<Func<TEntity, object>>[] includes);
        TEntity FirstOrDefault(TKey id);
        TEntity Add(TEntity entity, bool saveAutomatically = true);
        void Update(TEntity entity, bool saveAutomatically = true);
        void Delete(TEntity entity, bool saveAutomatically = true);
        void Delete(TKey id, bool saveAutomatically = true);
        void Delete(Expression<Func<TEntity, bool>> expression, bool saveAutomatically = true);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        bool Exists(TKey id);
        void SaveChanges();
    }
}
