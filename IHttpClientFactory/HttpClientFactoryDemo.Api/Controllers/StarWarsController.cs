using System.Net.Http;
using System.Threading.Tasks;
using HttpClientFactoryDemo.ApiClient;
using Microsoft.AspNetCore.Mvc;

namespace HttpClientFactoryDemo.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StarWarsController : ControllerBase
    {
        private readonly StarWarsApiClient _starWarsApiClient;
        private readonly IHttpClientFactory _httpClientFactory;

        public StarWarsController(StarWarsApiClient starWarsApiClient, IHttpClientFactory httpClientFactory)
        {
            _starWarsApiClient = starWarsApiClient;
            _httpClientFactory = httpClientFactory;
        }
        
        [HttpGet("GetStarWarsCharacter1")]
        public async Task<ActionResult> GetStarWarsCharacter1()
        {
            string response;
            using (var httpClient = new HttpClient())
            {
                var apiResponse = await httpClient.GetAsync("https://swapi.dev/api/people/1");
                response = await apiResponse.Content.ReadAsStringAsync();
            }
        
            return Ok(response);
        }
        
        [HttpGet("GetStarWarsCharacter2")]
        public async Task<ActionResult> GetStarWarsCharacter2()
        {
            var httpClient = _httpClientFactory.CreateClient("StarWars");
            var apiResponse = await httpClient.GetAsync("people/1");
            var response = await apiResponse.Content.ReadAsStringAsync();
        
            return Ok(response);
        }
        
        [HttpGet("GetStarWarsCharacter3")]
        public async Task<ActionResult> GetStarWarsCharacter3()
        {
            var response = await _starWarsApiClient.GetCharacter();
            
            return Ok(response);
        }

        

        
    }
}