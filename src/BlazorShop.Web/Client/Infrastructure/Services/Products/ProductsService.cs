namespace BlazorShop.Web.Client.Infrastructure.Services.Products
{
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Extensions;
    using Models;
    using Models.Products;

    public class ProductsService : IProductsService
    {
        private readonly HttpClient http;

        private const string ProductsPath = "api/products";
        private const string ProductsPathWithSlash = ProductsPath + "/";
        private const string ProductsSearchPath = ProductsPath + "?category={0}&minPrice={1}&maxPrice={2}&query={3}&page={4}";

        public ProductsService(HttpClient http) => this.http = http;

        public async Task<int> CreateAsync(
            ProductsRequestModel model)
        {
            var response = await this.http.PostAsJsonAsync(ProductsPath, model);
            var id = await response.Content.ReadFromJsonAsync<int>();

            return id;
        }

        public async Task<Result> UpdateAsync(
            int id, ProductsRequestModel model)
            => await this.http
                .PutAsJsonAsync(ProductsPathWithSlash + id, model)
                .ToResult();

        public async Task<Result> DeleteAsync(
            int id)
            => await this.http
                .DeleteAsync(ProductsPathWithSlash + id)
                .ToResult();

        public async Task<TModel> DetailsAsync<TModel>(
            int id)
            where TModel : class
            => await this.http.GetFromJsonAsync<TModel>(ProductsPathWithSlash + id);

        public async Task<ProductsSearchResponseModel> SearchAsync(
            ProductsSearchRequestModel model)
            => await this.http.GetFromJsonAsync<ProductsSearchResponseModel>(
                string.Format(
                    ProductsSearchPath,
                    model.Category,
                    model.MinPrice,
                    model.MaxPrice,
                    model.Query,
                    model.Page));
    }
}