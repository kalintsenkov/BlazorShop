namespace BlazorShop.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Products;
    using Services.Products;

    using static Common.Constants;

    public class ProductsController : ApiController
    {
        private readonly IProductsService products;

        public ProductsController(IProductsService products)
            => this.products = products;

        [HttpGet(Id)]
        public async Task<ProductsDetailsResponseModel> Details(
            int id)
            => await this.products.DetailsAsync(id);

        [HttpPost]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Create(
            ProductsRequestModel model)
        {
            var id = await this.products.CreateAsync(model);

            return Created(nameof(this.Create), id);
        }

        [HttpPut(Id)]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Update(
            int id, ProductsRequestModel model)
            => await this.products
                .UpdateAsync(id, model)
                .ToActionResult();

        [HttpDelete(Id)]
        [Authorize(Roles = AdministratorRole)]
        public async Task<ActionResult> Delete(
            int id)
            => await this.products
                .DeleteAsync(id)
                .ToActionResult();
    }
}
