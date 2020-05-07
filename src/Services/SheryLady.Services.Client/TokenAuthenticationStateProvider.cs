namespace SheryLady.Services.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.JSInterop;

    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IJSRuntime jsRuntime;

        public TokenAuthenticationStateProvider(IJSRuntime jsRuntime) => this.jsRuntime = jsRuntime;

        public async Task SetTokenAsync(string token)
        {
            if (token == null)
            {
                await this.jsRuntime.RemoveItem("authToken");
                await this.jsRuntime.RemoveItem("authTokenExpiry");
            }
            else
            {
                await this.jsRuntime.SetItem("authToken", token);
                await this.jsRuntime.SetItem("authTokenExpiry", DateTime.UtcNow.AddDays(7));
            }

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task<string> GetTokenAsync()
        {
            var expiry = await this.jsRuntime.GetItem("authTokenExpiry");
            if (expiry != null)
            {
                if (DateTime.Parse(expiry) > DateTime.Now)
                {
                    return await this.jsRuntime.GetItem("authToken");
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
