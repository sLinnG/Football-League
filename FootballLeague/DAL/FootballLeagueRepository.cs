namespace DAL
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;
    using Models.DBModels;
    using Services.Interfaces;

    public class FootballLeagueRepository : IRepository
    {
        private readonly FootballLeagueDbContext context;

        public FootballLeagueRepository(FootballLeagueDbContext context)
        {
            this.context = context;
        }

        public virtual List<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
            where TEntity : class
        {
            IQueryable<TEntity> query = context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties
                                                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual TEntity GetByID<TEntity>(int id, params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class, IIdentifiable
        {
            IQueryable<TEntity> dbSet = ApplyIncludes(includes, context.Set<TEntity>());
            return dbSet.FirstOrDefault(e => e.Id == id);
        }

        public virtual List<TEntity> GetAll<TEntity>(params Expression<Func<TEntity, object>>[] includes)
            where TEntity : class
            => ApplyIncludes(includes, context.Set<TEntity>()).ToList();

        public virtual void Insert<TEntity>(TEntity entity)
            where TEntity : class
            => context.Set<TEntity>().Add(entity);

        public virtual void Delete<TEntity>(object id)
            where TEntity : class
            => Delete(context.Set<TEntity>().Find(id));

        public virtual void Delete<TEntity>(TEntity entityToDelete)
            where TEntity : class
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                context.Set<TEntity>().Attach(entityToDelete);
            }

            context.Set<TEntity>().Remove(entityToDelete);
        }

        public virtual void Update<TEntity>(TEntity entityToUpdate)
            where TEntity : class
        {
            context.Set<TEntity>().Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private static IQueryable<TEntity> ApplyIncludes<TEntity>(Expression<Func<TEntity, object>>[] includes, IQueryable<TEntity> dbSet) where TEntity : class
        {
            if (includes != null)
            {
                foreach (var includeFunc in includes)
                {
                    if (includeFunc != null)
                    {
                        dbSet = dbSet.Include(includeFunc);
                    }
                }
            }

            return dbSet;
        }
    }
}
