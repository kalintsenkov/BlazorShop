namespace SheryLady.Services.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models.Products;

    public interface IProductsService
    {
        Task<int> CreateAsync(
            string name, 
            string description, 
            string image, 
            int quantity, 
            decimal price, 
            int categoryId);

        Task<bool> UpdateAsync(
            int id,
            string name,
            string description,
            string image,
            int quantity,
            decimal price,
            int categoryId);

        Task<bool> DeleteAsync(int id);

        Task<ProductsDetailsServiceModel> DetailsAsync(int id);

        Task<IEnumerable<ProductsListingServiceModel>> GetAllAsync();

        Task<IEnumerable<ProductsListingServiceModel>> GetAllByCategoryIdAsync(int categoryId);
    }
}