namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Products;

    public interface IWishlistService
    {
        Task<Result> AddProductAsync(int productId);

        Task<Result> RemoveProductAsync(int productId);

        Task<IEnumerable<ProductsListingResponseModel>> MineAsync();
    }
}
