namespace BlazorShop.Services.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Products;
    using Models.Search;

    public interface ISearchService
    {
        Task<IEnumerable<ProductsListingResponseModel>> Products(SearchRequestModel model);
    }
}