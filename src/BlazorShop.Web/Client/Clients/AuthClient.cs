namespace BlazorShop.Web.Client.Clients
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Authorization;

    using BlazorShop.Shared.Models.Identity;
    using Infrastructure;

    public class AuthClient : IAuthClient
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationProvider;

        public AuthClient(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationProvider)
        {
            this.httpClient = httpClient;
            this.authenticationProvider = authenticationProvider;
        }

        public async Task<bool> LoginAsync(LoginRequestModel model)
        {
            var result = await this.httpClient.PostAsJsonAsync("api/identity/login", model);
            var response = await result.Content.ReadFromJsonAsync<LoginResponseModel>();
            var token = response.Token;

            if (token != null)
            {
                await ((TokenAuthenticationStateProvider)this.authenticationProvider).SetTokenAsync(token);
                return true;
            }

            return false;
        }

        public async Task RegisterAsync(RegisterRequestModel model)
            => await this.httpClient.PostAsJsonAsync("api/identity/register", model);

        public async Task LogoutAsync()
            => await ((TokenAuthenticationStateProvider)this.authenticationProvider).SetTokenAsync(null);
    }
}
