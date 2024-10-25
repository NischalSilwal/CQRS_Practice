using ConsumeAPI.Models;

namespace ConsumeAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> AuthenticateAsync(UserLogin loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("https://localhost:7114/login", loginModel);

            response.EnsureSuccessStatusCode();

            var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

            return tokenResponse?.Token;
        }

    }
}
