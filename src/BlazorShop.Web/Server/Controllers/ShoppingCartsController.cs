namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.ShoppingCarts;
    using Services.Identity;
    using Services.ShoppingCart;

    [Authorize]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartService shoppingCart;
        private readonly ICurrentUserService currentUser;

        public ShoppingCartsController(
            IShoppingCartService shoppingCart, 
            ICurrentUserService currentUser)
        {
            this.shoppingCart = shoppingCart;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> Mine()
            => await this.shoppingCart.ByUserAsync(this.currentUser.UserId);

        [HttpGet(nameof(Count))]
        public async Task<ActionResult<int>> Count()
            => await this.shoppingCart.CountAsync(this.currentUser.UserId);

        [HttpPost(Id)]
        public async Task<ActionResult> AddProduct(
            int id, ShoppingCartRequestModel model)
            => await this.shoppingCart
                .AddProductAsync(id, model.Quantity, this.currentUser.UserId)
                .ToActionResult();

        [HttpPut(Id)]
        public async Task<ActionResult> UpdateProduct(
            int id, ShoppingCartRequestModel model)
            => await this.shoppingCart
                .UpdateProductAsync(id, model.Quantity, this.currentUser.UserId)
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> RemoveProduct(
            int id)
            => await this.shoppingCart
                .RemoveProductAsync(id, this.currentUser.UserId)
                .ToActionResult();
    }
}
