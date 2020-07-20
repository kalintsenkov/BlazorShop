namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Products;

    public interface IWishlistService
    {
        Task AddAsync(int productId, string userId);

        Task<Result> RemoveAsync(int productId, string userId);

        Task<IEnumerable<ProductsListingResponseModel>> ByUserIdAsync(string userId);
    }
}
