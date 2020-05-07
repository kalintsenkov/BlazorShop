namespace SheryLady.Web.Client.Infrastructure.Extensions
{
    using System;
    using System.Net.Http;

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using Clients;

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
                .AddScoped<TokenAuthenticationStateProvider>()
                .AddScoped<AuthenticationStateProvider>(provider => provider
                    .GetRequiredService<TokenAuthenticationStateProvider>())
                .AddTransient<IUsersClient, UsersClient>()
                .AddTransient(sp => new HttpClient 
                { 
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
                });

            return builder;
        }
    }
}
