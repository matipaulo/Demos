using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CancellationTokenSource.Api.Handlers
{
    public class LongTaskRequest : IRequest
    {
        public LongTaskRequest(string name)
        {
            Name = name;
        }

        public string Name { get; }
    }

    public class LongTaskHandler : IRequestHandler<LongTaskRequest>
    {
        /// <inheritdoc />
        public async Task<Unit> Handle(LongTaskRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(80000, cancellationToken);
            Console.WriteLine($"Name received: {request.Name}.");

            return Unit.Value;
        }
    }
}