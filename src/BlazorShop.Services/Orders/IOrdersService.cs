namespace BlazorShop.Services.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Orders;

    public interface IOrdersService
    {
        Task<string> PurchaseAsync(int deliveryAddressId, string userId);

        Task<OrdersDetailsResponseModel> DetailsAsync(string id);

        Task<IEnumerable<OrdersListingResponseModel>> ByUserIdAsync(string userId);
    }
}