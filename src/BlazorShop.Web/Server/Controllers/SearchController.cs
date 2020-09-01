namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Models.Products;
    using Models.Search;
    using Services.Search;

    public class SearchController : ApiController
    {
        private readonly ISearchService search;

        public SearchController(ISearchService search)
            => this.search = search;

        [HttpGet(nameof(Products))]
        public async Task<IEnumerable<ProductsListingResponseModel>> Products(
            [FromQuery] SearchRequestModel model)
            => await this.search.Products(model);
    }
}
