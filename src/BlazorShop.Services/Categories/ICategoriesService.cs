namespace BlazorShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Categories;
    using Models.Products;

    public interface ICategoriesService
    {
        Task<int> CreateAsync(string name);

        Task<bool> UpdateAsync(int id, string name);

        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<ProductListingResponseModel>> DetailsAsync(int id);

        Task<IEnumerable<CategoryListingResponseModel>> GetAllAsync();
    }
}