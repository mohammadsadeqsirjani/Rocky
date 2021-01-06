using Rocky.Domain.Common;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Rocky.Domain.Interfaces.Common
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Select();
        IQueryable<TEntity> Select(Expression<Func<TEntity, bool>> expression, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order = null, Expression<Func<TEntity, object>>[] include = null, bool isTracking = true);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, object>>[] include = null, bool isTracking = true);
        TEntity FirstOrDefault(int id);
        TEntity Add(TEntity entity, bool saveAutomatically = true);
        void Update(TEntity entity, bool saveAutomatically = true);
        void Delete(TEntity entity, bool saveAutomatically = true);
        void Delete(int id, bool saveAutomatically = true);
        bool Exists(Expression<Func<TEntity, bool>> predicate);
        bool Exists(int id);
        void SaveChanges();
    }
}
