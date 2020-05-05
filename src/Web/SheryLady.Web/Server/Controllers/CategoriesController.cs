namespace SheryLady.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Categories;
    using Services.Products;
    using Shared.Categories;
    using Shared.Products;

    using static Common.GlobalConstants;
    using static Infrastructure.WebConstants;

    [Authorize]
    public class CategoriesController : ApiController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public CategoriesController(
            ICategoriesService categoriesService, 
            IProductsService productsService)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoriesListingResponseModel>> All()
            => await this.categoriesService.GetAllAsync();

        [HttpGet(RouteId)]
        public async Task<IEnumerable<ProductsListingResponseModel>> Details(int id)
            => await this.productsService.GetAllByCategoryIdAsync(id);

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Create(CategoriesCreateRequestModel model)
        {
            var id = await this.categoriesService.CreateAsync(model.Name);

            return this.Created(nameof(this.Create), id);
        }

        [HttpPut]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Update(CategoriesUpdateRequestModel model)
        {
            var updated = await this.categoriesService.UpdateAsync(model.Id, model.Name);
            if (!updated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpDelete(RouteId)]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await this.categoriesService.DeleteAsync(id);
            if (!deleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}