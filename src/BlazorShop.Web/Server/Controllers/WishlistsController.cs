namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Products;
    using Services.Wishlist;

    [Authorize]
    public class WishlistsController : ApiController
    {
        private readonly IWishlistService wishlist;

        public WishlistsController(IWishlistService wishlist)
            => this.wishlist = wishlist;

        [HttpGet]
        public async Task<IEnumerable<ProductsListingResponseModel>> Mine()
            => await this.wishlist.MineAsync();

        [HttpPost(Id)]
        public async Task<ActionResult> AddProduct(
            int id)
            => await this.wishlist
                .AddProductAsync(id)
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> RemoveProduct(
            int id)
            => await this.wishlist
                .RemoveProductAsync(id)
                .ToActionResult();
    }
}
