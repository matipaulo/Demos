using CQRS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CQRS.Core.Data
{
    public class SchoolContext : DbContext, IDbContext
    {
        private IDbContextTransaction _currentTransaction;
        public DbSet<Student> Students { get; set; }

        public SchoolContext(DbContextOptions options) : base(options)
        {
        }

        /// <inheritdoc />
        public async Task BeginTransactionAsync(CancellationToken cancellationToken)
        {
            _currentTransaction ??= await Database.BeginTransactionAsync(cancellationToken);
        }

        /// <inheritdoc />
        public async Task CommitTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await SaveChangesAsync(cancellationToken);
                _currentTransaction?.CommitAsync(cancellationToken);
            }
            catch
            {
                await RollbackTransactionAsync(cancellationToken);
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <inheritdoc />
        public async Task RollbackTransactionAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _currentTransaction?.RollbackAsync(cancellationToken);
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        /// <inheritdoc />
        public async Task RetryOnExceptionAsync(Func<Task> func)
        {
            await Database.CreateExecutionStrategy().ExecuteAsync(func);
        }
    }
}