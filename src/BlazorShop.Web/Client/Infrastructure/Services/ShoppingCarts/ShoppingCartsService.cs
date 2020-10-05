namespace BlazorShop.Web.Client.Infrastructure.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Extensions;
    using Models;
    using Models.ShoppingCarts;

    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly HttpClient http;

        private const string ShoppingCartsPath = "api/shoppingcarts";

        public ShoppingCartsService(HttpClient http) => this.http = http;

        public async Task<Result> AddProduct(ShoppingCartRequestModel model)
            => await this.http
                .PostAsJsonAsync($"{ShoppingCartsPath}/{nameof(this.AddProduct)}", model)
                .ToResult();

        public async Task<Result> UpdateProduct(ShoppingCartRequestModel model)
            => await this.http
                .PutAsJsonAsync($"{ShoppingCartsPath}/{nameof(this.UpdateProduct)}", model)
                .ToResult();

        public async Task<Result> RemoveProduct(int id)
            => await this.http.DeleteAsync($"{ShoppingCartsPath}/{nameof(this.RemoveProduct)}/{id}").ToResult();

        public async Task<int> TotalProducts()
            => await this.http.GetFromJsonAsync<int>($"{ShoppingCartsPath}/{nameof(this.TotalProducts)}");

        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> Mine()
            => await this.http.GetFromJsonAsync<IEnumerable<ShoppingCartProductsResponseModel>>(ShoppingCartsPath);
    }
}
