namespace BlazorShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Shared.Models.Orders;

    public interface IOrdersService
    {
        Task PurchaseAsync(string userId, int deliveryAddressId);

        Task<IEnumerable<OrdersListingResponseModel>> GetAllByUserIdAsync(string userId);
    }
}