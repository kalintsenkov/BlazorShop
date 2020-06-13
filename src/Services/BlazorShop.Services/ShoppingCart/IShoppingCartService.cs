namespace BlazorShop.Services.ShoppingCart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.ShoppingCart;

    public interface IShoppingCartService
    {
        Task AddAsync(int productId, string userId, int quantity);

        Task<bool> UpdateAsync(int productId, string userId, int quantity);

        Task<bool> RemoveAsync(int productId, string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> GetByUserIdAsync(string userId);
    }
}