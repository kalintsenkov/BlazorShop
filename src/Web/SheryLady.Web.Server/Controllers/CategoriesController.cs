namespace SheryLady.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Models.Categories;
    using Services.Categories;
    using Services.Models.Categories;

    using static Infrastructure.WebConstants;

    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService) 
            => this.categoriesService = categoriesService;

        [HttpGet]
        public async Task<IEnumerable<CategoriesListingServiceModel>> All()
            => await this.categoriesService.GetAll();

        [HttpGet(RouteId)]
        public async Task<ActionResult<CategoriesDetailsServiceModel>> Details(int id)
            => await this.categoriesService.Details(id);

        [HttpPost]
        public async Task<ActionResult> Create(CategoriesCreateRequestModel model)
        {
            var id = await this.categoriesService.Create(model.Name);

            return this.Created(nameof(this.Create), id);
        }

        [HttpPut(RouteId)]
        public async Task<ActionResult> Update(CategoriesUpdateRequestModel model)
        {
            var updated = await this.categoriesService.Update(model.Id, model.Name);
            if (!updated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpDelete(RouteId)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await this.categoriesService.Delete(id);
            if (!deleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}