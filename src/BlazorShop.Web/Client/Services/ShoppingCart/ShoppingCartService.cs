namespace BlazorShop.Web.Client.Services.ShoppingCart
{
    using System.Threading.Tasks;

    using Infrastructure;
    using Models;
    using Models.ShoppingCarts;

    public class ShoppingCartService : IShoppingCartService
    {
        private const string ShoppingCartPath = "api/shoppingcarts";

        private readonly IApiClient apiClient;

        public ShoppingCartService(IApiClient apiClient) 
            => this.apiClient = apiClient;

        public async Task<Result> AddProductAsync(int productId, ShoppingCartRequestModel model) 
            => await this.apiClient.PostJson($"{ShoppingCartPath}/{productId}", model);

        public async Task<Result> RemoveProductAsync(int productId)
            => await this.apiClient.Delete($"{ShoppingCartPath}/{productId}");

        public async Task<Result> UpdateProductAsync(int productId, ShoppingCartRequestModel model)
            => await this.apiClient.PutJson($"{ShoppingCartPath}/{productId}", model);

        public async Task<int> CountAsync()
            => await this.apiClient.GetJson<int>($"{ShoppingCartPath}/count");

        public async Task<ShoppingCartProductsResponseModel[]> MineAsync()
            => await this.apiClient.GetJson<ShoppingCartProductsResponseModel[]>(ShoppingCartPath);
    }
}
