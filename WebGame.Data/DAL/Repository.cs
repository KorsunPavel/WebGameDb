using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebGame.Data.DAL
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        internal DbContext Context;
        internal DbSet<TEntity> DbSet;
        

        public Repository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual IQueryable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = DbSet;

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (filter != null)
            {
                query = query.Where(filter);
            }

            var sql = query.ToString();
            return orderBy != null ? orderBy(query) : query;
        }

        public virtual TEntity GetById(object id)
        {
            return DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetByPropertyValue(Expression<Func<TEntity, bool>> filter)
        {
            return Get(filter);
        }

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public virtual void Delete(object id)
        {
            var entityToDelete = DbSet.Find(id);
            if (entityToDelete != null)
            {
                Delete(entityToDelete);
            }
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Context.Entry(entityToDelete).State == EntityState.Detached)
            {
                DbSet.Attach(entityToDelete);
            }
            DbSet.Remove(entityToDelete);
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// Returns Entity of class T
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual TEntity GetEntityByStoredProcedure(string storedProcedureName, params object[] parameters)
        {
            return Context.Database.SqlQuery<TEntity>(storedProcedureName, parameters).SingleOrDefault();
        }

        /// <summary>
        /// Returns Entities of class T
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public virtual List<TEntity> GetEntitiesByStoredProcedure(string storedProcedureName, params object[] parameters)
        {
            // simply pass parameters as part of storedProcedureName
            return Context.Database.SqlQuery<TEntity>(storedProcedureName, parameters).ToList();
        }

        public virtual void ExecStoredProcedure(string storedProcedureName, params object[] parameters)
        {
            Context.Database.ExecuteSqlCommand(storedProcedureName, parameters);
        }
    }
}
