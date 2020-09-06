namespace BlazorShop.Web.Client.Services
{
    using System.Threading.Tasks;

    using Models;
    using Models.Products;

    public interface IWishlistsService
    {
        Task<Result> AddProductAsync(int productId);

        Task<Result> RemoveProductAsync(int productId);

        Task<ProductsListingResponseModel[]> MineAsync();
    }
}
