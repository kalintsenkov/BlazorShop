namespace BlazorShop.Web.Client.Infrastructure.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Extensions;
    using Models;
    using Models.Wishlists;

    public class WishlistsService : IWishlistsService
    {
        private readonly HttpClient http;

        private const string WishlistsPath = "api/wishlists";

        public WishlistsService(HttpClient http) => this.http = http;

        public async Task<Result> AddProduct(int id)
            => await this.http
                .PostAsJsonAsync($"{WishlistsPath}/{nameof(this.AddProduct)}/{id}", id)
                .ToResult();

        public async Task<Result> RemoveProduct(int id)
            => await this.http
                .DeleteAsync($"{WishlistsPath}/{nameof(this.RemoveProduct)}/{id}")
                .ToResult();

        public async Task<IEnumerable<WishlistsProductsResponseModel>> Mine()
            => await this.http.GetFromJsonAsync<IEnumerable<WishlistsProductsResponseModel>>(WishlistsPath);
    }
}
