namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.ShoppingCarts;
    using Services.ShoppingCart;

    [Authorize]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartService shoppingCart;

        public ShoppingCartsController(IShoppingCartService shoppingCart)
            => this.shoppingCart = shoppingCart;

        [HttpGet]
        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> ByUser()
            => await this.shoppingCart.ByCurrentUserAsync();

        [HttpPost(Id)]
        public async Task<ActionResult> AddProduct(
            int id, ShoppingCartRequestModel model)
            => await this.shoppingCart
                .AddProductAsync(id, model.Quantity)
                .ToActionResult();

        [HttpPut(Id)]
        public async Task<ActionResult> UpdateProduct(
            int id, ShoppingCartRequestModel model)
            => await this.shoppingCart
                .UpdateProductAsync(id, model.Quantity)
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> RemoveProduct(
            int id)
            => await this.shoppingCart
                .RemoveProductAsync(id)
                .ToActionResult();
    }
}
