namespace BlazorShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.Products;

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

        Task<ProductsDetailsResponseModel> DetailsAsync(int id);

        Task<IEnumerable<ProductsListingResponseModel>> GetAllAsync();

        Task<IEnumerable<ProductsListingResponseModel>> GetAllByCategoryIdAsync(int categoryId);
    }
}