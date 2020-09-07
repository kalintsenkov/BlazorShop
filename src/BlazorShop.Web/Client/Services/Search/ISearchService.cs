namespace BlazorShop.Web.Client.Services.Search
{
    using System.Threading.Tasks;
    
    using Models.Products;
    using Models.Search;

    public interface ISearchService
    {
        Task<ProductsListingResponseModel[]> Products(SearchRequestModel model = null);
    }
}
