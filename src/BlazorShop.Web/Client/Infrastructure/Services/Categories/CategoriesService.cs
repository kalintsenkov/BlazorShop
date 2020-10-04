namespace BlazorShop.Web.Client.Infrastructure.Services.Categories
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models.Categories;

    public class CategoriesService : ICategoriesService
    {
        private readonly HttpClient http;

        private const string CategoriesPath = "api/categories";

        public CategoriesService(HttpClient http) => this.http = http;

        public async Task<IEnumerable<CategoriesListingResponseModel>> All()
            => await this.http.GetFromJsonAsync<IEnumerable<CategoriesListingResponseModel>>(CategoriesPath);
    }
}
