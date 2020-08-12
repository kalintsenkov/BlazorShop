namespace BlazorShop.Services.Search
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Products;

    public interface ISearchService
    {
        Task<IEnumerable<ProductsListingResponseModel>> Products(
            string query, 
            int page = 1);
    }
}