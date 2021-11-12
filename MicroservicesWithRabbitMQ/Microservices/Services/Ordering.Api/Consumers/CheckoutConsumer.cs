using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Api.Commands;

namespace Ordering.Api.Consumers
{
    public class CheckoutConsumer : IConsumer<CheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CheckoutConsumer> _logger;

        public CheckoutConsumer(IMediator mediator, ILogger<CheckoutConsumer> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<CheckoutEvent> context)
        {
            var command = new CheckoutCommand
            {
                Address = context.Message.Address,
                CardName = context.Message.CardName,
                CardNumber = context.Message.CardNumber,
                CVV = context.Message.CVV,
                Email = context.Message.Email,
                Expiration = context.Message.Expiration,
                FirstName = context.Message.FirstName,
                LastName = context.Message.LastName,
                Total = context.Message.Total,
                Items = context.Message.Items.Select(x => new OrderItem
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList()
            };

            var orderId = await _mediator.Send(command);
            _logger.LogInformation($"Order created: {orderId}");
        }
    }
}