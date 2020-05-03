namespace SheryLady.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models.Deals;
    using Services.Deals;

    using static Common.GlobalConstants;
    using static Infrastructure.WebConstants;

    [Authorize(Roles = AdminRoleName)]
    public class DealsController : ApiController
    {
        private readonly IDealsService dealsService;

        public DealsController(IDealsService dealsService) 
            => this.dealsService = dealsService;

        [HttpPost]
        public async Task<ActionResult> Start(DealsStartRequestModel model)
        {
            var id = await this.dealsService.StartAsync(
                model.StartDate, 
                model.EndDate, 
                model.Discount, 
                model.ProductId);

            return this.Created(nameof(this.Start), id);
        }

        [HttpPut]
        public async Task<ActionResult> Update(DealsUpdateRequestModel model)
        {
            var updated = await this.dealsService.UpdateAsync(
                model.Id,
                model.StartDate,
                model.EndDate,
                model.Discount);

            if (!updated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpDelete(RouteId)]
        public async Task<ActionResult> End(int id)
        {
            var ended = await this.dealsService.EndAsync(id);
            if (!ended)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}