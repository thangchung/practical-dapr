using System.Threading;
using System.Threading.Tasks;

namespace N8T.Domain
{
    public interface IUnitOfWork
    {
        Task BeginTransactionAsync(CancellationToken token);
        Task CommitTransactionAsync(CancellationToken token);
        Task RollbackTransaction(CancellationToken token);
    }
}