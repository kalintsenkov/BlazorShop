namespace SheryLady.Web.Client
{
    using System;
    using System.Net.Http;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure;

    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddAuthorizationCore();
            
            builder.Services.AddScoped<TokenAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider
                .GetRequiredService<TokenAuthenticationStateProvider>());
            builder.Services.AddTransient(sp => 
                new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            await builder.Build().RunAsync();
        }
    }
}
