namespace Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Models.DBModels;

    public interface IRepository
    {
        void Delete<TEntity>(object id)
            where TEntity : class;

        void Delete<TEntity>(TEntity entityToDelete)
            where TEntity : class;

        List<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
            where TEntity : class;

        TEntity GetByID<TEntity>(int id, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IIdentifiable;

        List<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class;

        void Insert<TEntity>(TEntity entity)
            where TEntity : class;

        void Update<TEntity>(TEntity entityToUpdate)
            where TEntity : class;

        void Save();
    }
}
