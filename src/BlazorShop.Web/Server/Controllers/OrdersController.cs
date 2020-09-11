namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models.Orders;
    using Services.Identity;
    using Services.Orders;

    [Authorize]
    public class OrdersController : ApiController
    {
        private readonly IOrdersService orders;
        private readonly ICurrentUserService currentUser;

        public OrdersController(
            IOrdersService orders,
            ICurrentUserService currentUser)
        {
            this.orders = orders;
            this.currentUser = currentUser;
        }

        [HttpGet]
        public async Task<IEnumerable<OrdersListingResponseModel>> Mine()
            => await this.orders.ByUserAsync(this.currentUser.UserId);

        [HttpGet(Id)]
        public async Task<OrdersDetailsResponseModel> Details(string id)
            => await this.orders.DetailsAsync(id);

        [HttpPost]
        public async Task<ActionResult> Purchase(OrdersRequestModel model)
        {
            var userId = this.currentUser.UserId;

            var id = await this.orders.PurchaseAsync(userId, model);

            return Created(nameof(this.Purchase), id);
        }
    }
}
