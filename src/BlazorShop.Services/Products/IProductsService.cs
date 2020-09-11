namespace BlazorShop.Services.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models;
    using Models.Products;

    public interface IProductsService
    {
        Task<int> CreateAsync(ProductsRequestModel model);

        Task<Result> UpdateAsync(int id, ProductsRequestModel model);

        Task<Result> DeleteAsync(int id);

        Task<ProductsDetailsResponseModel> DetailsAsync(int id);

        Task<IEnumerable<ProductsListingResponseModel>> SearchAsync(ProductsSearchRequestModel model);
    }
}