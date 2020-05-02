namespace SheryLady.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Categories;

    public interface ICategoriesService
    {
        Task<int> Create(string name);

        Task<bool> Update(int id, string name);

        Task<bool> Delete(int id);

        Task<CategoriesDetailsServiceModel> Details(int id);

        Task<IEnumerable<CategoriesListingServiceModel>> GetAll();
    }
}