namespace BlazorShop.Web.Client
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

    using Infrastructure.Extensions;

    public class Program
    {
        public static async Task Main(string[] args) 
            => await WebAssemblyHostBuilder
                .CreateDefault(args)
                .AddRootComponents()
                .AddClientServices()
                .Build()
                .RunAsync();
    }
}
