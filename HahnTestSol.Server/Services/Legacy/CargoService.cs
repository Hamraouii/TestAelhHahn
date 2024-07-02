using HahnTestSol.Server.Models;

namespace HahnTestSol.Server.Services.Legacy
{
    public class CargoService
    {
        private readonly HttpClient _httpClient;

        public CargoService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("AuthenticatedClient");
        }

        public async Task<List<Node>> GetNodesAsync()
        {
            var response = await _httpClient.GetAsync("/Grid/Get");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<List<Node>>();
        }

        // Other methods to interact with the API...
    }

}
