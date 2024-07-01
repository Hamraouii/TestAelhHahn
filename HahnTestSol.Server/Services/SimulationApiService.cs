
using HahnTestSol.Server.Models;

namespace HahnTestSol.Server.Services
{
    public class SimulationApiService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public SimulationApiService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<Grid> GetGridAsync()
        {
            var response = await _httpClient.GetAsync($"{_configuration["ApiBaseUrl"]}/Grid/Get");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Grid>();
        }

        // Implement other methods for API interactions
    }
}
