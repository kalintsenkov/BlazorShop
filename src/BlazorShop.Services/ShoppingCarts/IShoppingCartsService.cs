namespace BlazorShop.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task<Result> AddAsync(ShoppingCartRequestModel model, string userId);

        Task<Result> UpdateAsync(ShoppingCartRequestModel model, string userId);

        Task<Result> RemoveAsync(ShoppingCartRequestModel model, string userId);

        Task<int> TotalByUserAsync(string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> ByUserAsync(string userId);
    }
}
