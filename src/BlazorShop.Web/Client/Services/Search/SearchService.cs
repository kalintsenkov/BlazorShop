namespace BlazorShop.Web.Client.Services.Search
{
    using System.Threading.Tasks;

    using Infrastructure;
    using Models.Products;
    using Models.Search;

    public class SearchService : ISearchService
    {
        private readonly IApiClient apiClient;

        public SearchService(IApiClient apiClient)
            => this.apiClient = apiClient;

        public async Task<ProductsListingResponseModel[]> Products(SearchRequestModel model = null)
        {
            var requestUrl = model == null
                ? "api/search/products"
                : "api/search/products" +
                $"?category={model.Category}" +
                $"&minPrice={model.MinPrice}" +
                $"&maxPrice={model.MaxPrice}" +
                $"&query={model.Query}" +
                $"&page={model.Page}";

            return await this.apiClient.GetJson<ProductsListingResponseModel[]>(requestUrl);
        }
    }
}
