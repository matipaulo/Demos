using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Basket.Api.Models;

namespace Basket.Api.Infrastructure
{
    public class CatalogApiClient
    {
        private readonly HttpClient _httpClient;

        public CatalogApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ProductDto?> GetProduct(int productId)
        {
            return await _httpClient.GetFromJsonAsync<ProductDto>($"api/Catalog/{productId}");
        }
    }
}