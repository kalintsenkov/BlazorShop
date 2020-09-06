namespace BlazorShop.Web.Client.Services
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;

    using Infrastructure;
    using Models;
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

        public async Task<Result> Register(RegisterRequestModel model)
        {
            var response = await httpClient.PostAsJsonAsync("api/identity/register", model);

            if (!response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<string[]>();

                return Result.Failure(errors);
            }

            return Result.Success;
        }

        public async Task<Result> Login(LoginRequestModel model)
        {
            var response = await httpClient.PostAsJsonAsync("api/identity/login", model);

            if (!response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<string[]>();

                return Result.Failure(errors);
            }

            var responseAsString = await response.Content.ReadAsStringAsync();

            var responseObject = JsonSerializer.Deserialize<LoginResponseModel>(responseAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            var token = responseObject.Token;

            await localStorage.SetItemAsync("authToken", token);

            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsAuthenticated(model.Email);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return Result.Success;
        }

        public async Task Logout()
        {
            await localStorage.RemoveItemAsync("authToken");

            ((ApiAuthenticationStateProvider)authenticationStateProvider).MarkUserAsLoggedOut();

            httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
