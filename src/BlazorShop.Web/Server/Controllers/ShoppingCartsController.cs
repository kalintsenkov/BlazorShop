namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.ShoppingCart;
    using Shared.Models.ShoppingCarts;

    [Authorize]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartService shoppingCartService;

        public ShoppingCartsController(IShoppingCartService shoppingCartService)
            => this.shoppingCartService = shoppingCartService;

        [HttpGet]
        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> All()
            => await this.shoppingCartService.GetByUserIdAsync(this.User.GetId());

        [HttpPost(Id)]
        public async Task<ActionResult> Add(int id, ShoppingCartRequestModel model)
        {
            await this.shoppingCartService.AddAsync(id, model.Quantity, this.User.GetId());

            return Ok();
        }

        [HttpPut(Id)]
        public async Task<ActionResult> Update(int id, ShoppingCartRequestModel model)
        {
            var updated = await this.shoppingCartService.UpdateAsync(
                id,
                model.Quantity,
                this.User.GetId());

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete(Id)]
        public async Task<ActionResult> Remove(int id)
        {
            var removed = await this.shoppingCartService.RemoveAsync(id, this.User.GetId());
            if (!removed)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}