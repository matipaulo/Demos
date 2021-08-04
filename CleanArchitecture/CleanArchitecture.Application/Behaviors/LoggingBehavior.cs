using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var requestName = request.GetType().Name;
            _logger.LogInformation($"[Start segment] {requestName}.");
            TResponse response;

            var stopwatch = Stopwatch.StartNew();
            try
            {
                try
                {
                    _logger.LogInformation($"[Properties] {requestName} {JsonSerializer.Serialize(request)}.");
                }
                catch (NotSupportedException)
                {
                    _logger.LogInformation($"[Serialization ERROR] {requestName} Could not serialize the request.");
                }

                response = await next();
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation(
                    $"[End segment] {requestName}; Execution time={stopwatch.ElapsedMilliseconds}ms.");
            }

            return response;
        }
    }
}