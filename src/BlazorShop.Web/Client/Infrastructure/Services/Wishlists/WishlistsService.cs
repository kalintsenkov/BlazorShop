namespace BlazorShop.Web.Client.Infrastructure.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models;
    using Models.Wishlists;

    public class WishlistsService : IWishlistsService
    {
        private readonly HttpClient http;

        private const string WishlistsPath = "api/wishlists";

        public WishlistsService(HttpClient http) => this.http = http;

        public async Task<Result> AddProduct(int id)
        {
            var path = $"{WishlistsPath}/{nameof(this.AddProduct)}/{id}";

            var response = await this.http.PostAsJsonAsync(path, id);

            return response.IsSuccessStatusCode;
        }

        public async Task<Result> RemoveProduct(int id)
        {
            var path = $"{WishlistsPath}/{nameof(this.RemoveProduct)}/{id}";

            var response = await this.http.DeleteAsync(path);

            return response.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<WishlistsProductsResponseModel>> Mine()
            => await this.http.GetFromJsonAsync<IEnumerable<WishlistsProductsResponseModel>>(WishlistsPath);
    }
}
