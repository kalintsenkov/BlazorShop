namespace JewelleryShop.Web.Client.Infrastructure
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.Products;

    public interface IProductsClient
    {
        Task<int> AddAsync(ProductsCreateRequestModel model);

        Task UpdateAsync(ProductsUpdateRequestModel model);

        Task DeleteAsync(int id);

        Task<ProductsDetailsResponseModel> DetailsAsync(int id);

        Task<IEnumerable<ProductsListingResponseModel>> GetAllAsync();
    }
}
