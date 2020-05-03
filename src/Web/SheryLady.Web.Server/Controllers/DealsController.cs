namespace SheryLady.Web.Server.Controllers
{
    using Microsoft.AspNetCore.Authorization;

    using Services.Deals;

    using static Common.GlobalConstants;

    [Authorize(Roles = AdminRoleName)]
    public class DealsController : ApiController
    {
        private readonly IDealsService dealsService;

        public DealsController(IDealsService dealsService) 
            => this.dealsService = dealsService;
    }
}