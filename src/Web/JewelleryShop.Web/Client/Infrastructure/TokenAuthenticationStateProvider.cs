namespace JewelleryShop.Web.Client.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.JSInterop;

    using Extensions;

    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime;

        public TokenAuthenticationStateProvider(IJSRuntime jsRuntime) => this.jsRuntime = jsRuntime;

        public async Task SetTokenAsync(string token)
        {
            if (token == null)
            {
                await this.jsRuntime.RemoveAsync("authToken");
                await this.jsRuntime.RemoveAsync("authTokenExpiry");
            }
            else
            {
                await this.jsRuntime.SetAsync("authToken", token);
                await this.jsRuntime.SetAsync("authTokenExpiry", DateTime.UtcNow.AddDays(7));
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> GetTokenAsync()
        {
            var expiry = await this.jsRuntime.GetAsync("authTokenExpiry");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry) > DateTime.UtcNow)
                {
                    return await this.jsRuntime.GetAsync("authToken");
                }

                await this.SetTokenAsync(null);
            }

            return null;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await this.GetTokenAsync();
            var identity = string.IsNullOrEmpty(token)
                ? new ClaimsIdentity()
                : new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }

            return Convert.FromBase64String(base64);
        }
    }
}
