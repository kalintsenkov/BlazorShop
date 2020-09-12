namespace BlazorShop.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Products;
    using Services.Products;

    using static Common.Constants;

    [Authorize(Roles = AdministratorRole)]
    public class ProductsController : ApiController
    {
        private readonly IProductsService products;

        public ProductsController(IProductsService products)
            => this.products = products;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ProductsSearchResponseModel> Search(
            [FromQuery] ProductsSearchRequestModel model)
            => await this.products.SearchAsync(model);

        [HttpGet(Id)]
        [AllowAnonymous]
        public async Task<ProductsDetailsResponseModel> Details(
            int id)
            => await this.products.DetailsAsync(id);

        [HttpPost]
        public async Task<ActionResult> Create(
            ProductsRequestModel model)
        {
            var id = await this.products.CreateAsync(model);

            return Created(nameof(this.Create), id);
        }

        [HttpPut(Id)]
        public async Task<ActionResult> Update(
            int id, ProductsRequestModel model)
            => await this.products
                .UpdateAsync(id, model)
                .ToActionResult();

        [HttpDelete(Id)]
        public async Task<ActionResult> Delete(
            int id)
            => await this.products
                .DeleteAsync(id)
                .ToActionResult();
    }
}
