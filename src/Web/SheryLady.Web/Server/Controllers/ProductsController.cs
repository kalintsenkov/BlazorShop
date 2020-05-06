namespace SheryLady.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services.Products;
    using Shared.Products;

    using static Common.GlobalConstants;
    using static Infrastructure.WebConstants;

    public class ProductsController : ApiController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService) 
            => this.productsService = productsService;

        [HttpGet]
        public async Task<IEnumerable<ProductsListingResponseModel>> All()
            => await this.productsService.GetAllAsync();

        [HttpGet(Id)]
        public async Task<ActionResult<ProductsDetailsResponseModel>> Details(int id)
            => await this.productsService.DetailsAsync(id);

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Create(ProductsCreateRequestModel model)
        {
            var id = await this.productsService.CreateAsync(
                model.Name,
                model.Description,
                model.Image,
                model.Quantity,
                model.Price,
                model.CategoryId);

            return this.Created(nameof(this.Create), id);
        }

        [HttpPut]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Update(ProductsUpdateRequestModel model)
        {
            var updated = await this.productsService.UpdateAsync(
                model.Id,
                model.Name,
                model.Description,
                model.Image,
                model.Quantity,
                model.Price,
                model.CategoryId);

            if (!updated)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }

        [HttpDelete(Id)]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await this.productsService.DeleteAsync(id);
            if (!deleted)
            {
                return this.BadRequest();
            }

            return this.Ok();
        }
    }
}