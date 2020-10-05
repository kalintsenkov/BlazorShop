namespace BlazorShop.Web.Client.Infrastructure.Services.ShoppingCarts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.ShoppingCarts;

    public interface IShoppingCartsService
    {
        Task<Result> AddProduct(ShoppingCartRequestModel model);

        Task<Result> UpdateProduct(ShoppingCartRequestModel model);

        Task<Result> RemoveProduct(int id);

        Task<int> TotalProducts();

        Task<IEnumerable<ShoppingCartProductsResponseModel>> Mine();
    }
}
