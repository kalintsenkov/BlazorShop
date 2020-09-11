namespace BlazorShop.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Wishlists;

    public interface IWishlistsService
    {
        Task<Result> AddAsync(string userId, WishlistsRequestModel model);

        Task<Result> RemoveAsync(string userId, WishlistsRequestModel model);

        Task<IEnumerable<WishlistsProductsResponseModel>> ByUserAsync(string userId);
    }
}
