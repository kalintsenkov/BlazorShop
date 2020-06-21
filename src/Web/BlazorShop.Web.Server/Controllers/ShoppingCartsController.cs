namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.ShoppingCarts;
    using Shared.ShoppingCarts;

    [Authorize]
    public class ShoppingCartsController : ApiController
    {
        private readonly IShoppingCartsService shoppingCartService;

        public ShoppingCartsController(IShoppingCartsService shoppingCartService)
            => this.shoppingCartService = shoppingCartService;

        [HttpGet]
        public async Task<IEnumerable<ShoppingCartProductsResponseModel>> All()
            => await this.shoppingCartService.GetByUserIdAsync(this.User.GetId());

        [HttpPost(Id)]
        public async Task<ActionResult> Add(int id, ShoppingCartAddRequestModel request)
        {
            await this.shoppingCartService.AddAsync(id, this.User.GetId(), request.Quantity);

            return Ok();
        }

        [HttpPut(Id)]
        public async Task<ActionResult> Update(int id, ShoppingCartUpdateRequestModel request)
        {
            var updated = await this.shoppingCartService.UpdateAsync(
                id, 
                this.User.GetId(), 
                request.Quantity);

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