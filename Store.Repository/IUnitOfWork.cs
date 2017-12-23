using System.Threading;
using System.Threading.Tasks;

namespace Store.Repository
{
    public interface IUnitOfWork
    {
        void Commit();

        Task<bool> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
