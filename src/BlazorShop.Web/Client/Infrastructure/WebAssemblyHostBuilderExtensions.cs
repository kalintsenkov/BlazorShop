namespace BlazorShop.Web.Client.Infrastructure
{
    using System;
    using System.Net.Http;

    using Blazored.LocalStorage;
    using Blazored.Toast;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using Services;

    public static class WebAssemblyHostBuilderExtensions
    {
        public static WebAssemblyHostBuilder AddRootComponents(this WebAssemblyHostBuilder builder)
        {
            builder.RootComponents.Add<App>("app");
            return builder;
        }

        public static WebAssemblyHostBuilder AddClientServices(this WebAssemblyHostBuilder builder)
        {
            builder
                .Services
                .AddAuthorizationCore()
                .AddBlazoredToast()
                .AddBlazoredLocalStorage()
                .AddScoped<ApiAuthenticationStateProvider>()
                .AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>()
                .AddTransient<IAuthService, AuthService>()
                .AddTransient(sp => new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });

            return builder;
        }
    }
}
