namespace SheryLady.Services.Server.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.Categories;

    public interface ICategoriesService
    {
        Task<int> CreateAsync(string name);

        Task<bool> UpdateAsync(int id, string name);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<CategoriesListingResponseModel>> GetAllAsync();
    }
}