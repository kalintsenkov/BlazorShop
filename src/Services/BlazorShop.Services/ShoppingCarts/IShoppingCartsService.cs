namespace BlazorShop.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task AddAsync(int productId, string userId, int quantity);

        Task<bool> UpdateAsync(int productId, string userId, int quantity);

        Task<bool> RemoveAsync(int productId, string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> GetByUserIdAsync(string userId);
    }
}