namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.Wishlist;
    using Models.Products;

    [Authorize]
    public class WishlistsController : ApiController
    {
        private readonly IWishlistService wishlistService;

        public WishlistsController(IWishlistService wishlistsService)
            => this.wishlistService = wishlistsService;

        [HttpGet]
        public async Task<IEnumerable<ProductListingResponseModel>> All()
            => await this.wishlistService.GetByUserIdAsync(this.User.GetId());

        [HttpPost(Id)]
        public async Task<ActionResult> Add(int id)
        {
            await this.wishlistService.AddAsync(id, this.User.GetId());

            return Ok();
        }

        [HttpDelete(Id)]
        public async Task<ActionResult> Remove(int id)
        {
            var removed = await this.wishlistService.RemoveAsync(id, this.User.GetId());
            if (!removed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
