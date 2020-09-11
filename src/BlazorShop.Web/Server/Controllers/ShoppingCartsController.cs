namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.ShoppingCarts;
    using Services.Identity;
    using Services.ShoppingCarts;

    [Authorize]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartsService shoppingCarts;
        private readonly ICurrentUserService currentUser;

        public ShoppingCartsController(
            IShoppingCartsService shoppingCarts,
            ICurrentUserService currentUser)
        {
            this.shoppingCarts = shoppingCarts;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> Mine()
            => await this.shoppingCarts.ByUserAsync(this.currentUser.UserId);

        [HttpGet(nameof(Count))]
        public async Task<ActionResult<int>> Count()
            => await this.shoppingCarts.CountAsync(this.currentUser.UserId);

        [HttpPost]
        public async Task<ActionResult> Add(
            ShoppingCartRequestModel model)
            => await this.shoppingCarts
                .AddAsync(this.currentUser.UserId, model)
                .ToActionResult();

        [HttpPut]
        public async Task<ActionResult> Update(
            ShoppingCartRequestModel model)
            => await this.shoppingCarts
                .UpdateAsync(this.currentUser.UserId, model)
                .ToActionResult();

        [HttpDelete]
        public async Task<ActionResult> Remove(
            ShoppingCartRequestModel model)
            => await this.shoppingCarts
                .RemoveAsync(this.currentUser.UserId, model)
                .ToActionResult();
    }
}
