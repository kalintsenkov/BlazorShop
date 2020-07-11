namespace BlazorShop.Services.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Shared.Models.Products;

    public interface IProductsService
    {
        Task<int> CreateAsync(
            string name, 
            string description, 
            string imageSource, 
            int quantity, 
            decimal price, 
            int categoryId);

        Task<bool> UpdateAsync(
            int id,
            string name,
            string description,
            string imageSource,
            int quantity,
            decimal price,
            int categoryId);

        Task<bool> DeleteAsync(int id);

        Task<ProductDetailsResponseModel> DetailsAsync(int id);

        Task<IEnumerable<ProductListingResponseModel>> GetAllAsync();

        Task<IEnumerable<ProductListingResponseModel>> GetAllByCategoryIdAsync(int categoryId);
    }
}