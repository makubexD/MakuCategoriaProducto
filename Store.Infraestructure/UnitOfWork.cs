using Store.Repository;
using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

namespace Store.Infraestructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _context;
        private bool _disposed;

        public UnitOfWork(DbContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
                return await Task.FromResult(true);
            }
            catch (DbEntityValidationException e)
            {
                var errors = e.EntityValidationErrors;
                throw e;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _context.Dispose();
            }

            _disposed = true;
        }

        #endregion
    }
}
