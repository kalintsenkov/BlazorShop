namespace BlazorShop.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Wishlists;

    public interface IWishlistsService
    {
        Task<Result> AddAsync(WishlistsRequestModel model, string userId);

        Task<Result> RemoveAsync(WishlistsRequestModel model, string userId);

        Task<IEnumerable<WishlistsProductsResponseModel>> ByUserAsync(string userId);
    }
}
