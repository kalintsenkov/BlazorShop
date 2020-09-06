namespace BlazorShop.Services.ShoppingCart
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartService
    {
        Task<Result> AddProductAsync(int productId, int quantity);

        Task<Result> UpdateProductAsync(int productId, int quantity);

        Task<Result> RemoveProductAsync(int productId);

        Task<int> CountAsync();

        Task<IEnumerable<ShoppingCartProductsResponseModel>> MineAsync();
    }
}