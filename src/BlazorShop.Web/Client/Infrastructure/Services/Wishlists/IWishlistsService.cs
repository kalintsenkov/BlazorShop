namespace BlazorShop.Web.Client.Infrastructure.Services.Wishlists
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Wishlists;

    public interface IWishlistsService
    {
        Task<Result> AddProduct(int id);

        Task<Result> RemoveProduct(int id);

        Task<IEnumerable<WishlistsProductsResponseModel>> Mine();
    }
}
