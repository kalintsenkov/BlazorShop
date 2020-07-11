namespace BlazorShop.Web.Client.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;

    using Infrastructure;
    using Models.Identity;

    public class AuthService : IAuthService
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationStateProvider authenticationStateProvider;

        public AuthService(
            HttpClient httpClient,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            this.authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<RegisterResponseModel> Register(RegisterRequestModel model)
        {
            var response = await this.httpClient.PostAsJsonAsync("api/identity/register", model);
            var result = await response.Content.ReadFromJsonAsync<RegisterResponseModel>();

            return result;
        }

        public async Task<LoginResponseModel> Login(LoginRequestModel model)
        {
            var loginAsJson = JsonSerializer.Serialize(model);

            var content = new StringContent(loginAsJson, Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync("api/identity/login", content);

            var json = await response.Content.ReadAsStringAsync();

            var loginResult = JsonSerializer.Deserialize<LoginResponseModel>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await this.localStorage.SetItemAsync("authToken", loginResult.Token);

            ((ApiAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsAuthenticated(model.Username);

            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await this.localStorage.RemoveItemAsync("authToken");

            ((ApiAuthenticationStateProvider)this.authenticationStateProvider).MarkUserAsLoggedOut();

            this.httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
