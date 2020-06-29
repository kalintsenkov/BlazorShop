namespace BlazorShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Shared.Models.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task AddAsync(int productId, int quantity, string userId);

        Task<bool> UpdateAsync(int productId, int quantity, string userId);

        Task<bool> RemoveAsync(int productId, string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> GetByUserIdAsync(string userId);
    }
}