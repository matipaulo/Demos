using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core
{
    public interface IDbContext
    {
        Task BeginTransactionAsync(CancellationToken cancellationToken);

        Task CommitTransactionAsync(CancellationToken cancellationToken);

        Task RollbackTransactionAsync(CancellationToken cancellationToken);

        Task RetryOnExceptionAsync(Func<Task> func);
    }
}