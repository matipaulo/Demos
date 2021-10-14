using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;

namespace DistributedCache.API.BackgroundServices
{
    public class Subscriber : BackgroundService
    {
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public Subscriber(IConnectionMultiplexer connectionMultiplexer)
        {
            _connectionMultiplexer = connectionMultiplexer;
        }

        /// <inheritdoc />
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var subscriber = _connectionMultiplexer.GetSubscriber();
            await subscriber.SubscribeAsync("myChannel", (channel, value) =>
            {
                Console.WriteLine($"Received: {value}");
            });
        }
    }
}