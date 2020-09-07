namespace BlazorShop.Web.Client.Services.Categories
{
    using System.Threading.Tasks;

    using Models.Categories;

    public interface ICategoriesService
    {
        Task<CategoriesListingResponseModel[]> AllAsync();
    }
}
