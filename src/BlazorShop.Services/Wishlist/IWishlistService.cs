namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Products;

    public interface IWishlistService
    {
        Task AddAsync(int productId, string userId);

        Task<bool> RemoveAsync(int productId, string userId);

        Task<IEnumerable<ProductListingResponseModel>> GetByUserIdAsync(string userId);
    }
}
