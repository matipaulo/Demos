using Basket.Api.Infrastructure;
using Basket.Api.Models;
using EventBus.Messages.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Basket.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private static readonly ConcurrentDictionary<string, ShoppingCart> _basket = new ConcurrentDictionary<string, ShoppingCart>();

        private readonly CatalogApiClient _catalogApiClient;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketController(CatalogApiClient catalogApiClient, IPublishEndpoint publishEndpoint)
        {
            _catalogApiClient = catalogApiClient;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet("{basketId}")]
        public ActionResult<ShoppingCart> GetBasket([FromRoute] string basketId)
        {
            var basket = _basket[basketId];
            if (basket == null)
                return NoContent();

            return Ok(basket);
        }

        [HttpPost]
        public ActionResult<ShoppingCart> Create()
        {
            var basket = new ShoppingCart();
            _basket.TryAdd(basket.Id.ToString(), basket);

            return Ok(_basket[basket.Id.ToString()]);
        }

        [HttpPost("{basketId}/items")]
        public async Task<ActionResult> AddItem([FromRoute] string basketId, [FromBody] AddItem request)
        {
            if (!_basket.ContainsKey(basketId))
                return BadRequest();
            
            var product = await _catalogApiClient.GetProduct(request.Id);
            if (product is null)
                return BadRequest();

            _basket[basketId].Items.Add(new ShoppingCartItem
            {
                Id = request.Id,
                Name = product.Name,
                Price = product!.Price,
                Quantity = request.Quantity
            });

            return Ok();
        }

        [HttpPost("{basketId}/checkout")]
        public async Task<IActionResult> Checkout([FromRoute] string basketId, [FromBody] BasketCheckout basketCheckout)
        {
            var basket = _basket[basketId];
            if (basket == null)
                return BadRequest();

            var eventMessage = new CheckoutEvent
            {
                Address = basketCheckout.Address,
                CardName = basketCheckout.CardName,
                CardNumber = basketCheckout.CardNumber,
                CVV = basketCheckout.CVV,
                Email = basketCheckout.Email,
                Expiration = basketCheckout.Expiration,
                FirstName = basketCheckout.FirstName,
                LastName = basketCheckout.LastName,
                Total = _basket[basketId].Total,
                Items = _basket[basketId].Items.Select(x => new Item
                {
                    Id = x.Id,
                    Name = x.Name,
                    Price = x.Price,
                    Quantity = x.Quantity
                }).ToList()
            };

            await _publishEndpoint.Publish<CheckoutEvent>(eventMessage);

            _basket.TryRemove(basketId, out _);

            return Accepted();
        }
    }
}