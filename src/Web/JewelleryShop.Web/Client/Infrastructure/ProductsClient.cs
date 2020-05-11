namespace JewelleryShop.Web.Client.Infrastructure
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Web.Shared.Products;

    public class ProductsClient : IProductsClient
    {
        private readonly HttpClient httpClient;
        private readonly TokenAuthenticationStateProvider tokenProvider;

        public ProductsClient(
            HttpClient httpClient,
            TokenAuthenticationStateProvider tokenProvider)
        {
            this.httpClient = httpClient;
            this.tokenProvider = tokenProvider;
        }

        public async Task<int> AddAsync(ProductsCreateRequestModel model)
        {
            await this.SetAuthorizationRequestHeader();
            var response = await this.httpClient.PostAsJsonAsync("api/products", model);
            var id = await response.Content.ReadFromJsonAsync<int>();
            return id;
        }

        public async Task UpdateAsync(ProductsUpdateRequestModel model)
        {
            await this.SetAuthorizationRequestHeader();
            await this.httpClient.PutAsJsonAsync("api/products", model);
        }

        public async Task DeleteAsync(int id)
        {
            await this.SetAuthorizationRequestHeader();
            await this.httpClient.DeleteAsync($"api/products/{id}");
        }

        public async Task<ProductsDetailsResponseModel> DetailsAsync(int id)
            => await this.httpClient.GetFromJsonAsync<ProductsDetailsResponseModel>($"api/products/{id}");

        public async Task<IEnumerable<ProductsListingResponseModel>> GetAllAsync()
            => await this.httpClient.GetFromJsonAsync<List<ProductsListingResponseModel>>("api/products");

        private async Task SetAuthorizationRequestHeader()
        {
            var token = await this.tokenProvider.GetTokenAsync();
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
