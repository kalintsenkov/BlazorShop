namespace BlazorShop.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common;
    using Models;
    using Models.Wishlists;

    public interface IWishlistsService : IService
    {
        Task<Result> AddProductAsync(int productId, string userId);

        Task<Result> RemoveProductAsync(int productId, string userId);

        Task<IEnumerable<WishlistsProductsResponseModel>> ByUserAsync(string userId);
    }
}
