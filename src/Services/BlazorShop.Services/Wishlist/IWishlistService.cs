namespace BlazorShop.Services.Wishlist
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.Products;

    public interface IWishlistService
    {
        Task AddProductAsync(int productId, string userId);

        Task<bool> RemoveProductAsync(int productId, string userId);

        Task<IEnumerable<ProductsListingResponseModel>> GetByUserIdAsync(string userId);
    }
}
