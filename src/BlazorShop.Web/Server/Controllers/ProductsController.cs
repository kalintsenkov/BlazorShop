namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Services;
    using Shared.Models.Products;

    using static Shared.Constants;

    public class ProductsController : ApiController
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
            => this.productsService = productsService;

        [HttpGet]
        public async Task<IEnumerable<ProductListingResponseModel>> All()
            => await this.productsService.GetAllAsync();

        [HttpGet(Id)]
        public async Task<ActionResult<ProductDetailsResponseModel>> Details(int id)
            => await this.productsService.DetailsAsync(id);

        [HttpGet(nameof(ByCategory) + Slash + Id)]
        public async Task<IEnumerable<ProductListingResponseModel>> ByCategory(int id)
            => await this.productsService.GetAllByCategoryIdAsync(id);

        [HttpPost]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Create(ProductRequestModel model)
        {
            var id = await this.productsService.CreateAsync(
                model.Name,
                model.Description,
                model.ImageSource,
                model.Quantity,
                model.Price,
                model.CategoryId);

            return Created(nameof(this.Create), id);
        }

        [HttpPut(Id)]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Update(int id, ProductRequestModel model)
        {
            var updated = await this.productsService.UpdateAsync(
                id,
                model.Name,
                model.Description,
                model.ImageSource,
                model.Quantity,
                model.Price,
                model.CategoryId);

            if (!updated)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete(Id)]
        [Authorize(Roles = AdminRoleName)]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await this.productsService.DeleteAsync(id);
            if (!deleted)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}