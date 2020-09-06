namespace BlazorShop.Web.Client.Services
{
    using System.Threading.Tasks;

    using Models.Categories;

    public interface ICategoriesService
    {
        Task<CategoriesListingResponseModel[]> AllAsync();
    }
}
