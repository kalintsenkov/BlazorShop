namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models.Orders;
    using Services.Orders;

    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IOrdersService orders;

        public OrdersController(IOrdersService orders)
            => this.orders = orders;

        [HttpGet]
        public async Task<IEnumerable<OrdersListingResponseModel>> ByUser()
            => await this.orders.ByCurrentUserAsync();

        [HttpGet(Id)]
        public async Task<ActionResult<OrdersDetailsResponseModel>> Details(
            string id)
            => await this.orders.DetailsAsync(id);

        [HttpPost]
        public async Task<ActionResult> Purchase(
            OrdersRequestModel model)
        {
            var id = await this.orders.PurchaseAsync(model);

            return Created(nameof(this.Purchase), id);
        }
    }
}
