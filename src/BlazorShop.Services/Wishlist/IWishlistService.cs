namespace BlazorShop.Services.WishList
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Products;

    public interface IWishListService
    {
        Task<Result> AddProductAsync(int productId, string userId);

        Task<Result> RemoveProductAsync(int productId, string userId);

        Task<IEnumerable<ProductsListingResponseModel>> ByUserIdAsync(string userId);
    }
}
