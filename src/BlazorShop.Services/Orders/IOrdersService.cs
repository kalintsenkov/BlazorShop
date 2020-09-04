namespace BlazorShop.Services.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Orders;

    public interface IOrdersService
    {
        Task<string> PurchaseAsync(OrdersRequestModel model);

        Task<OrdersDetailsResponseModel> DetailsAsync(string id);

        Task<IEnumerable<OrdersListingResponseModel>> MineAsync();
    }
}