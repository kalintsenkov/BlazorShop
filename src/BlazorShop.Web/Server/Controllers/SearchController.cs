namespace BlazorShop.Web.Server.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    using Models.Products;
    using Services.Search;

    public class SearchController : ApiController
    {
        private readonly ISearchService searchService;

        public SearchController(ISearchService searchService)
            => this.searchService = searchService;

        [HttpGet(nameof(Products))]
        public async Task<IEnumerable<ProductsListingResponseModel>> Products(string query)
            => await this.searchService.Products(query);
    }
}
