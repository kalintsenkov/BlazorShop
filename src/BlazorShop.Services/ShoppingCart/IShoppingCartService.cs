namespace BlazorShop.Services.ShoppingCart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartService
    {
        Task<Result> AddAsync(int productId, int quantity, string userId);

        Task<Result> UpdateAsync(int productId, int quantity, string userId);

        Task<Result> RemoveAsync(int productId, string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> ByUserIdAsync(string userId);
    }
}