namespace BlazorShop.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Wishlists;

    public interface IWishlistsService
    {
        Task<Result> AddProductAsync(int productId, string userId);

        Task<Result> RemoveProductAsync(int productId, string userId);

        Task<IEnumerable<WishlistsProductsResponseModel>> ByUserAsync(string userId);
    }
}
