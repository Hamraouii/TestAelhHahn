namespace HahnTestSol.Server.Services.Legacy
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;
    using HahnTestSol.Server.Models.DTO;

    public class LegacyAuthService
    {
        private readonly HttpClient _httpClient;

        public LegacyAuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateAsync(string username)
        {
            var userAuthDto = new UserAuthenticateDto { Username = username, Password = "Hahn" };
            var response = await _httpClient.PostAsJsonAsync("/User/Login", userAuthDto);

            response.EnsureSuccessStatusCode();

            var authResponse = await response.Content.ReadFromJsonAsync<LegacyAuthResponseDto>();
            return authResponse.Token;
        }
    }

    public class LegacyAuthResponseDto
    {
        public string Token { get; set; }
    }

}
