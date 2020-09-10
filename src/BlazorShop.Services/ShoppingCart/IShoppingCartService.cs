namespace BlazorShop.Services.ShoppingCart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartService
    {
        Task<Result> AddProductAsync(
            int productId,
            int quantity, 
            string userId);

        Task<Result> UpdateProductAsync(
            int productId,
            int quantity, 
            string userId);

        Task<Result> RemoveProductAsync(
            int productId, 
            string userId);

        Task<int> CountAsync(string userId);

        Task<IEnumerable<ShoppingCartProductsResponseModel>> ByUserAsync(string userId);
    }
}
