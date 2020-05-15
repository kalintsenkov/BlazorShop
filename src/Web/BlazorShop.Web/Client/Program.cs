namespace BlazorShop.Web.Client
{
    using System.Threading.Tasks;
    using Infrastructure.Extensions;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

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
