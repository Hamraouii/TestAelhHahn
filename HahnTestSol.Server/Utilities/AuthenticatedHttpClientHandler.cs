using HahnTestSol.Server.Services.Legacy;
namespace HahnTestSol.Server.Utilities
{
    public class AuthenticatedHttpClientHandler : DelegatingHandler
    {
        private readonly LegacyAuthService _authService;
        private readonly string _username;

        public AuthenticatedHttpClientHandler(LegacyAuthService authService, string username)
        {
            _authService = authService;
            _username = username;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var token = await _authService.AuthenticateAsync(_username);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
