namespace BlazorShop.Web.Client.Infrastructure.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Categories;

    public interface ICategoriesService
    {
        Task<IEnumerable<CategoriesListingResponseModel>> All();
    }
}
