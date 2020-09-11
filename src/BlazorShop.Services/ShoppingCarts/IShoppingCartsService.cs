namespace BlazorShop.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task<Result> AddAsync(string userId, ShoppingCartRequestModel model);

        Task<Result> UpdateAsync(string userId, ShoppingCartRequestModel model);

        Task<Result> RemoveAsync(string userId, ShoppingCartRequestModel model);

        Task<int> CountAsync(string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> ByUserAsync(string userId);
    }
}
