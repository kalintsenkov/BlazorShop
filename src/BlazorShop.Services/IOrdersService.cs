namespace BlazorShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Shared.Models.Orders;

    public interface IOrdersService
    {
        Task<string> PurchaseAsync(int deliveryAddressId, string userId);

        Task<IEnumerable<OrdersListingResponseModel>> GetAllByUserIdAsync(string userId);
    }
}