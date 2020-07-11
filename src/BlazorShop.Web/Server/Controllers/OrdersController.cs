namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Services.Orders;
    using Models.Orders;

    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IOrdersService ordersService;

        public OrdersController(IOrdersService ordersService)
            => this.ordersService = ordersService;

        [HttpGet]
        public async Task<IEnumerable<OrderListingResponseModel>> All()
            => await this.ordersService.GetAllByUserIdAsync(this.User.GetId());

        [HttpGet(Id)]
        public async Task<ActionResult<OrderDetailsResponseModel>> Details(string id)
            => await this.ordersService.DetailsAsync(id);

        [HttpPost]
        public async Task<ActionResult> Purchase([FromBody] int addressId)
        {
            var id = await this.ordersService.PurchaseAsync(addressId, this.User.GetId());

            return Created(nameof(this.Purchase), id);
        }
    }
}
