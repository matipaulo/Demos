using Bogus;
using Catalog.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private static readonly ICollection<Product> _products = new Faker<Product>()
                .RuleFor(x => x.Id, y => y.IndexFaker)
                .RuleFor(x => x.Name, y => y.Commerce.ProductName())
                .RuleFor(x => x.Price, y => decimal.Parse(y.Commerce.Price()))
                .Generate(10);

        // GET: api/<CatalogController>
        [HttpGet]
        public ActionResult<ICollection<Product>> Get()
        {
            return Ok(_products);
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public ActionResult<Product> Get(int id)
        {
            var product = _products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
                return NoContent();

            return Ok(product);
        }

        // POST api/<CatalogController>
        [HttpPost]
        public ActionResult Post([FromBody] string productName)
        {
            _products.Add(new Product
            {
                Name = productName,
                Id = _products.MaxBy(x => x.Id).Id++
            });

            return Ok();
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string productName)
        {
            var product = _products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
                return BadRequest();

            product.Name = productName;

            return Ok();
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = _products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
                return BadRequest();

            _products.Remove(product);

            return Ok();
        }
    }
}