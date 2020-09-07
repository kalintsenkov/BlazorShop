namespace BlazorShop.Web.Client.Infrastructure
{
    using System;
    using System.Net.Http;

    using Blazored.LocalStorage;
    using Blazored.Toast;
    using Microsoft.AspNetCore.Components.Authorization;
    using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
    using Microsoft.Extensions.DependencyInjection;

    using Services.Addresses;
    using Services.Authentication;
    using Services.Categories;
    using Services.Orders;
    using Services.Products;
    using Services.Search;
    using Services.ShoppingCart;
    using Services.Wishlists;

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
                .AddTransient<IApiClient, ApiClient>()
                .AddTransient<IAddressesService, AddressesService>()
                .AddTransient<IAuthService, AuthService>()
                .AddTransient<ICategoriesService, CategoriesService>()
                .AddTransient<IOrdersService, OrderService>()
                .AddTransient<IProductsService, ProductsService>()
                .AddTransient<ISearchService, SearchService>()
                .AddTransient<IShoppingCartService, ShoppingCartService>()
                .AddTransient<IWishlistsService, WishlistService>()
                .AddTransient(sp => new HttpClient
                {
                    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
                });

            return builder;
        }
    }
}
