namespace BlazorShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common;
    using Models;
    using Models.Categories;

    public interface ICategoriesService : IService
    {
        Task<int> CreateAsync(CategoriesRequestModel model);

        Task<Result> UpdateAsync(int id, CategoriesRequestModel model);

        Task<Result> DeleteAsync(int id);

        Task<IEnumerable<CategoriesListingResponseModel>> AllAsync();
    }
}