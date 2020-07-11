namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Categories;
    using Models.Categories;

    using static Common.Constants;

    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService) 
            => this.categoriesService = categoriesService;

        [HttpGet]
        public async Task<IEnumerable<CategoryListingResponseModel>> All()
            => await this.categoriesService.GetAllAsync();

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Create(CategoryRequestModel model)
        {
            var id = await this.categoriesService.CreateAsync(model.Name);

            return Created(nameof(this.Create), id);
        }

        [HttpPut(Id)]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Update(int id, CategoryRequestModel model)
        {
            var updated = await this.categoriesService.UpdateAsync(id, model.Name);
            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete(Id)]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await this.categoriesService.DeleteAsync(id);
            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}