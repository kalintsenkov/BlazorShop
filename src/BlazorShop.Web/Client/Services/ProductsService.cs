namespace BlazorShop.Web.Client.Services
{
    using System.Threading.Tasks;

    using Infrastructure;
    using Models;
    using Models.Products;

    public class ProductsService : IProductsService
    {
        private const string ProductsPath = "api/products";

        private readonly IApiClient apiClient;

        public ProductsService(IApiClient apiClient)
            => this.apiClient = apiClient;

        public async Task<int> CreateAsync(ProductsRequestModel model)
            => await apiClient.PostJson<ProductsRequestModel, int>(ProductsPath, model);

        public async Task<Result> UpdateAsync(int id, ProductsRequestModel model)
            => await apiClient.PutJson($"{ProductsPath}/{id}", model);

        public async Task<Result> DeleteAsync(int id)
            => await apiClient.Delete($"{ProductsPath}/{id}");

        public async Task<ProductsDetailsResponseModel> DetailsAsync(int id)
            => await apiClient.GetJson<ProductsDetailsResponseModel>($"{ProductsPath}/{id}");
    }
}
