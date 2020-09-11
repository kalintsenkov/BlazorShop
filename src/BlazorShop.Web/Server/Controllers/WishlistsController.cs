namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Wishlists;
    using Services.Identity;
    using Services.Wishlists;

    [Authorize]
    public class WishlistsController : ApiController
    {
        private readonly IWishlistsService wishlists;
        private readonly ICurrentUserService currentUser;

        public WishlistsController(
            IWishlistsService wishlists,
            ICurrentUserService currentUser)
        {
            this.wishlists = wishlists;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IEnumerable<WishlistsProductsResponseModel>> Mine()
            => await this.wishlists.ByUserAsync(this.currentUser.UserId);

        [HttpPost]
        public async Task<ActionResult> Add(
            WishlistsRequestModel model)
            => await this.wishlists
                .AddAsync(model, this.currentUser.UserId)
                .ToActionResult();

        [HttpDelete]
        public async Task<ActionResult> Remove(
            WishlistsRequestModel model)
            => await this.wishlists
                .RemoveAsync(model, this.currentUser.UserId)
                .ToActionResult();
    }
}
