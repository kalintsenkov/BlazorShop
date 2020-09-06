namespace BlazorShop.Web.Client.Services
{
    using System.Threading.Tasks;

    using Infrastructure;
    using Models;
    using Models.Products;

    public class WishlistService : IWishlistsService
    {
        private const string WishlistsPath = "api/wishlists";

        private readonly IApiClient apiClient;

        public WishlistService(IApiClient apiClient) 
            => this.apiClient = apiClient;

        public async Task<Result> AddProductAsync(int productId)
            => await this.apiClient.PostJson($"{WishlistsPath}/{productId}", productId);

        public async Task<Result> RemoveProductAsync(int productId) 
            => await this.apiClient.Delete($"{WishlistsPath}/{productId}");

        public async Task<ProductsListingResponseModel[]> MineAsync()
            => await this.apiClient.GetJson<ProductsListingResponseModel[]>(WishlistsPath);
    }
}
