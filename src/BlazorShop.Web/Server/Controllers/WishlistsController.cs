namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services;
    using Shared.Models.Products;

    [Authorize]
    public class WishlistsController : ApiController
    {
        private readonly IWishlistsService wishlistsService;

        public WishlistsController(IWishlistsService wishlistsService)
            => this.wishlistsService = wishlistsService;

        [HttpGet]
        public async Task<IEnumerable<ProductListingResponseModel>> All()
            => await this.wishlistsService.GetByUserIdAsync(this.User.GetId());

        [HttpPost(Id)]
        public async Task<ActionResult> Add(int id)
        {
            await this.wishlistsService.AddAsync(id, this.User.GetId());

            return Ok();
        }

        [HttpDelete(Id)]
        public async Task<ActionResult> Remove(int id)
        {
            var removed = await this.wishlistsService.RemoveAsync(id, this.User.GetId());
            if (!removed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
