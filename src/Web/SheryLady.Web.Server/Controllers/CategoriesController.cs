namespace SheryLady.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Models.Categories;
    using Services;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ICategoriesService categoriesService) 
            => this.categoriesService = categoriesService;

        [HttpPost]
        public async Task<ActionResult> Create(CategoriesCreateRequestModel model)
        {
            var id = await this.categoriesService.Create(model.Name);

            return this.Created(nameof(this.Create), id);
        }
    }
}