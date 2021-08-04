using System;
using System.Threading;
using System.Threading.Tasks;
using CleanArchitecture.Application.Wrappers;
using CleanArchitecture.Core;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ITransactional 
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IDbContext _dbContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger, IDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response = default;
            try
            {
                await _dbContext.RetryOnExceptionAsync(async () =>
                {
                    _logger.LogInformation($"Begin transaction: {typeof(TRequest).Name}.");
                    await _dbContext.BeginTransactionAsync(cancellationToken);

                    response = await next();

                    await _dbContext.CommitTransactionAsync(cancellationToken);
                    _logger.LogInformation($"End transaction: {typeof(TRequest).Name}.");
                });
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Rollback transaction executed {typeof(TRequest).Name}.");
                await _dbContext.RollbackTransactionAsync(cancellationToken);
                _logger.LogError(e.Message, e.StackTrace);
                
                throw;
            }

            return response;
        }
    }
}