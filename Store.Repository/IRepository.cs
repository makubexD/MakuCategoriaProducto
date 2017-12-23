using Store.Common.Expresions;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity GetByKey(params object[] keyValues);

        Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> whereExpression, OrderExpression<TEntity>[] orderExpression = null, string[] includes = null);

        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
        void Delete(params object[] keyValues);
        void Delete(TEntity entityToDelete);
    }
}
