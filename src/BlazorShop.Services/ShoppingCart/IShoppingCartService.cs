namespace BlazorShop.Services.ShoppingCart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.ShoppingCarts;

    public interface IShoppingCartService
    {
        Task AddAsync(int productId, int quantity, string userId);

        Task<bool> UpdateAsync(int productId, int quantity, string userId);

        Task<bool> RemoveAsync(int productId, string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> GetByUserIdAsync(string userId);
    }
}