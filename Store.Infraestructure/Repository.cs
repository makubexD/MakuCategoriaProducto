using Store.Common.Expresions;
using Store.Repository;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Infraestructure
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public DbContext Db;
        internal DbSet<TEntity> DbSet;

        public Repository(DbContext context)
        {
            Db = context;
            DbSet = context.Set<TEntity>();
        }

        public virtual TEntity GetByKey(params object[] keyValues)
        {
            return DbSet.Find(keyValues);
        }

        public virtual Task<TEntity> GetByKeyAsync(params object[] keyValues)
        {
            return DbSet.FindAsync(keyValues);
        }

        public async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> whereExpression, OrderExpression<TEntity>[] orderExpression = null, string[] includes = null)
        {
            DbQuery<TEntity> baseQuery = Db.Set<TEntity>();
            if (includes != null && includes.Any())
            {
                baseQuery = includes.Aggregate(baseQuery, (current, include) => current.Include(include));
            }

            var includedExpression = baseQuery
                .Where(whereExpression);

            if (orderExpression != null && orderExpression.Any())
            {
                if (orderExpression.Length > 1)
                {
                    var ordered = ApplyOrderExpression(includedExpression, orderExpression.First());
                    ordered = orderExpression.Skip(1).Aggregate(ordered, ApplyOrderExpression);
                    return await ordered.FirstOrDefaultAsync();
                }
                else
                {
                    var ordered = ApplyOrderExpression(includedExpression, orderExpression.First());
                    return await ordered.FirstOrDefaultAsync();
                }
            }
            return await includedExpression.FirstOrDefaultAsync();
        }

        protected virtual IQueryable<TEntity> ApplyOrderExpression(IQueryable<TEntity> query, OrderExpression<TEntity> orderExpression)
        {
            if (orderExpression.Type == OrderType.Asc)
            {
                return query.OrderBy(orderExpression.Expression);
            }
            else
            {
                return query.OrderByDescending(orderExpression.Expression);
            }
        }

        protected virtual IQueryable<TEntity> ApplyOrderExpression(IOrderedQueryable<TEntity> query, OrderExpression<TEntity> orderExpression)
        {
            if (orderExpression.Type == OrderType.Asc)
            {
                return query.ThenBy(orderExpression.Expression);
            }
            return query.ThenByDescending(orderExpression.Expression);
        }
        

        public virtual void Insert(TEntity entity)
        {
            DbSet.Add(entity);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            if (Db.Entry(entityToUpdate).State == EntityState.Detached)
                DbSet.Attach(entityToUpdate);

            Db.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual void Delete(params object[] keyValues)
        {
            TEntity entityToDelete = GetByKey(keyValues);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (Db.Entry(entityToDelete).State == EntityState.Detached)
                DbSet.Attach(entityToDelete);

            Db.Entry(entityToDelete).State = EntityState.Deleted;
            DbSet.Remove(entityToDelete);
        }

        public virtual TEntity FindByAltKey(params object[] keys)
        {
            return GetByKey(keys);
        }
        
        public void Dispose()
        {
            Db.Dispose();
            GC.SuppressFinalize(this);
        }

    }
}
