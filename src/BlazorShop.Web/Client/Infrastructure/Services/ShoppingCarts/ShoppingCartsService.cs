namespace BlazorShop.Web.Client.Infrastructure.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public class ShoppingCartsService : IShoppingCartsService
    {
        private readonly HttpClient http;

        private const string ShoppingCartsPath = "api/shoppingcarts";

        public ShoppingCartsService(HttpClient http) => this.http = http;

        public async Task<Result> AddProduct(ShoppingCartRequestModel model)
        {
            var path = $"{ShoppingCartsPath}/{nameof(this.AddProduct)}";
            var response = await this.http.PostAsJsonAsync(path, model);

            if (!response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<string[]>();

                return Result.Failure(errors);
            }

            return Result.Success;
        }

        public async Task<Result> UpdateProduct(ShoppingCartRequestModel model)
        {
            var path = $"{ShoppingCartsPath}/{nameof(this.UpdateProduct)}";
            var response = await this.http.PutAsJsonAsync(path, model);

            if (!response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<string[]>();

                return Result.Failure(errors);
            }

            return Result.Success;
        }

        public async Task<Result> RemoveProduct(int id)
        {
            var path = $"{ShoppingCartsPath}/{nameof(this.RemoveProduct)}/{id}";
            var response = await this.http.DeleteAsync(path);

            if (!response.IsSuccessStatusCode)
            {
                var errors = await response.Content.ReadFromJsonAsync<string[]>();

                return Result.Failure(errors);
            }

            return Result.Success;
        }

        public async Task<int> TotalProducts()
            => await this.http.GetFromJsonAsync<int>($"{ShoppingCartsPath}/{nameof(this.TotalProducts)}");

        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> Mine()
            => await this.http.GetFromJsonAsync<IEnumerable<ShoppingCartProductsResponseModel>>(ShoppingCartsPath);
    }
}
