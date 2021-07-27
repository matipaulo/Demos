using System.Net.Http;
using System.Threading.Tasks;

namespace HttpClientFactoryDemo.ApiClient
{
    public class StarWarsApiClient
    {
        private readonly HttpClient _httpClient;
        
        public StarWarsApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<string> GetCharacter()
        {
            var apiResponse = await _httpClient.GetAsync("people/1");
            return await apiResponse.Content.ReadAsStringAsync();
        }
    }
}