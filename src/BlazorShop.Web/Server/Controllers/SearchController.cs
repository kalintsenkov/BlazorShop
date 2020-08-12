namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Models.Products;
    using Services.Search;

    public class SearchController : ApiController
    {
        private readonly ISearchService search;

        public SearchController(ISearchService search)
            => this.search = search;

        [HttpGet(nameof(Products))]
        public async Task<IEnumerable<ProductsListingResponseModel>> Products(
            string query,
            int page = 1)
            => await this.search.Products(query, page);
    }
}
