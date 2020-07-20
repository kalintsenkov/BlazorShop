namespace BlazorShop.Services.Categories
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Categories;
    using Models.Products;

    public interface ICategoriesService
    {
        Task<int> CreateAsync(string name);

        Task<Result> UpdateAsync(int id, string name);

        Task<Result> DeleteAsync(int id);

        Task<IEnumerable<ProductsListingResponseModel>> DetailsAsync(int id);

        Task<IEnumerable<CategoriesListingResponseModel>> AllAsync();
    }
}