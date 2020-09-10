namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Products;
    using Services.Identity;
    using Services.Wishlist;

    [Authorize]
    public class WishlistsController : ApiController
    {
        private readonly IWishlistService wishlist;
        private readonly ICurrentUserService currentUser;

        public WishlistsController(
            IWishlistService wishlist,
            ICurrentUserService currentUser)
        {
            this.wishlist = wishlist;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IEnumerable<ProductsListingResponseModel>> Mine()
            => await this.wishlist.ByUserAsync(this.currentUser.UserId);

        [HttpPost(Id)]
        public async Task<ActionResult> AddProduct(int id)
            => await this.wishlist
                .AddProductAsync(id, this.currentUser.UserId)
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> RemoveProduct(int id)
            => await this.wishlist
                .RemoveProductAsync(id, this.currentUser.UserId)
                .ToActionResult();
    }
}
