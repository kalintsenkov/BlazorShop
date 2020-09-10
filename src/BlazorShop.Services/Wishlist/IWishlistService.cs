namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Products;

    public interface IWishlistService
    {
        Task<Result> AddProductAsync(int productId, string userId);

        Task<Result> RemoveProductAsync(int productId, string userId);

        Task<IEnumerable<ProductsListingResponseModel>> ByUserAsync(string userId);
    }
}
