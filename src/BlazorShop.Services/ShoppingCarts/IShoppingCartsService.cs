namespace BlazorShop.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common;
    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartsService : IService
    {
        Task<Result> AddProductAsync(ShoppingCartRequestModel model, string userId);

        Task<Result> UpdateProductAsync(ShoppingCartRequestModel model, string userId);

        Task<Result> RemoveProductAsync(int productId, string userId);

        Task<int> TotalAsync(string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> ByUserAsync(string userId);
    }
}
