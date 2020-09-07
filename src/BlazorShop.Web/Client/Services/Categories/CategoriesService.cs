namespace BlazorShop.Web.Client.Services.Categories
{
    using System.Threading.Tasks;

    using Infrastructure;
    using Models.Categories;

    public class CategoriesService : ICategoriesService
    {
        private const string CategoriesPath = "api/categories";

        private readonly IApiClient apiClient;

        public CategoriesService(IApiClient apiClient)
            => this.apiClient = apiClient;

        public async Task<CategoriesListingResponseModel[]> AllAsync()
            => await this.apiClient.GetJson<CategoriesListingResponseModel[]>(CategoriesPath);
    }
}
