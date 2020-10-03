namespace BlazorShop.Services.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Common;
    using Models.Orders;

    public interface IOrdersService : IService
    {
        Task<string> PurchaseAsync(OrdersRequestModel model, string userId);

        Task<OrdersDetailsResponseModel> DetailsAsync(string id);

        Task<IEnumerable<OrdersListingResponseModel>> ByUserAsync(string userId);
    }
}