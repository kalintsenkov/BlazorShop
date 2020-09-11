namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Categories;
    using Services.Categories;

    using static Common.Constants;

    [Authorize(Roles = AdministratorRole)]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categories;

        public CategoriesController(ICategoriesService categories)
            => this.categories = categories;

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<CategoriesListingResponseModel>> All()
            => await this.categories.AllAsync();

        [HttpPost]
        public async Task<ActionResult> Create(
            CategoriesRequestModel model)
        {
            var id = await this.categories.CreateAsync(model);

            return Created(nameof(this.Create), id);
        }

        [HttpPut(Id)]
        public async Task<ActionResult> Update(
            int id, CategoriesRequestModel model)
            => await this.categories
                .UpdateAsync(id, model)
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> Delete(
            int id)
            => await this.categories
                .DeleteAsync(id)
                .ToActionResult();
    }
}
