namespace BlazorShop.Web.Client.Services.ShoppingCart
{
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartService
    {
        Task<Result> AddProductAsync(int productId, ShoppingCartRequestModel model);

        Task<Result> UpdateProductAsync(int productId, ShoppingCartRequestModel model);

        Task<Result> RemoveProductAsync(int productId);

        Task<int> CountAsync();

        Task<ShoppingCartProductsResponseModel[]> MineAsync();
    }
}
