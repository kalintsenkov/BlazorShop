namespace BlazorShop.Services.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Shared.Models.Orders;

    public interface IOrdersService
    {
        Task<string> PurchaseAsync(int deliveryAddressId, string userId);

        Task<OrderDetailsResponseModel> DetailsAsync(string id);

        Task<IEnumerable<OrderListingResponseModel>> GetAllByUserIdAsync(string userId);
    }
}