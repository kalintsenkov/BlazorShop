namespace SheryLady.Web.Client.Infrastructure.Clients
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Authorization;

    using Web.Shared.Identity;

    public class UsersClient : IUsersClient
    {
        private readonly HttpClient httpClient;
        private readonly AuthenticationStateProvider authenticationProvider;

        public UsersClient(
            HttpClient httpClient,
            AuthenticationStateProvider authenticationProvider)
        {
            this.httpClient = httpClient;
            this.authenticationProvider = authenticationProvider;
        }

        public async Task<bool> LoginAsync(LoginRequestModel model)
        {
            var result = await httpClient.PostAsJsonAsync("api/identity/login", model);
            var response = await result.Content.ReadFromJsonAsync<LoginResponseModel>();
            var token = response.Token;

            if (token != null)
            {
                await ((TokenAuthenticationStateProvider)this.authenticationProvider).SetTokenAsync(token);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task LogoutAsync()
            => await ((TokenAuthenticationStateProvider)this.authenticationProvider).SetTokenAsync(null);
    }
}
