namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Products;
    using Services.WishList;

    [Authorize]
    public class WishListsController : ApiController
    {
        private readonly IWishListService wishList;

        public WishListsController(IWishListService wishList)
            => this.wishList = wishList;

        [HttpGet]
        public async Task<IEnumerable<ProductsListingResponseModel>> ByUser()
            => await this.wishList.ByUserIdAsync(this.User.GetId());

        [HttpPost(Id)]
        public async Task<ActionResult> AddProduct(int id)
            => await this.wishList
                .AddProductAsync(id, this.User.GetId())
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> RemoveProduct(int id)
            => await this.wishList
                .RemoveProductAsync(id, this.User.GetId())
                .ToActionResult();
    }
}
