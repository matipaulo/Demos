using MediatR;
using Ordering.Api.Commands;
using Ordering.Api.Data;

namespace Ordering.Api.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckoutCommand, int>
    {
        private readonly ILogger<CheckoutHandler> _logger;
        private readonly OrderContext _context;

        public CheckoutHandler(ILogger<CheckoutHandler> logger, OrderContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<int> Handle(CheckoutCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Create and save the order.");

            var savedOrder = await _context.Orders.AddAsync(new Order
            {
                Address = request.Address,
                CardName = request.CardName,
                CardNumber = request.CardNumber,
                CVV = request.CVV,
                Email = request.Email,
                Expiration = request.Expiration,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Total = request.Total,
                Items = request.Items.Select(x => new Data.OrderItem
                {
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList()
            });

            await _context.SaveChangesAsync();

            return savedOrder.Entity.Id;
        }
    }
}