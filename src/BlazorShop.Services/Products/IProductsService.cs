namespace BlazorShop.Services.Products
{
    using System.Threading.Tasks;

    using Common;
    using Models;
    using Models.Products;

    public interface IProductsService : IService
    {
        Task<int> CreateAsync(ProductsRequestModel model);

        Task<Result> UpdateAsync(int id, ProductsRequestModel model);

        Task<Result> DeleteAsync(int id);

        Task<ProductsDetailsResponseModel> DetailsAsync(int id);

        Task<ProductsSearchResponseModel> SearchAsync(ProductsSearchRequestModel model);
    }
}