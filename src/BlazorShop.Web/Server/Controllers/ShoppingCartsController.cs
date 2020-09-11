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

        [HttpPost(Id)]
        public async Task<ActionResult> AddProduct(int id, ShoppingCartRequestModel model)
            => await this.shoppingCarts
                .AddProductAsync(id, model.Quantity, this.currentUser.UserId)
                .ToActionResult();

        [HttpPut(Id)]
        public async Task<ActionResult> UpdateProduct(int id, ShoppingCartRequestModel model)
            => await this.shoppingCarts
                .UpdateProductAsync(id, model.Quantity, this.currentUser.UserId)
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> RemoveProduct(int id)
            => await this.shoppingCarts
                .RemoveProductAsync(id, this.currentUser.UserId)
                .ToActionResult();
    }
}
