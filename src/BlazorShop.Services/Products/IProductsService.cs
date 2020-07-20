namespace BlazorShop.Services.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Products;

    public interface IProductsService
    {
        Task<int> CreateAsync(
            string name,
            string description,
            string imageSource,
            int quantity,
            decimal price,
            int categoryId);

        Task<Result> UpdateAsync(
            int id,
            string name,
            string description,
            string imageSource,
            int quantity,
            decimal price,
            int categoryId);

        Task<Result> DeleteAsync(int id);

        Task<ProductsDetailsResponseModel> DetailsAsync(int id);

        Task<IEnumerable<ProductsListingResponseModel>> AllAsync(int page = 1);
    }
}