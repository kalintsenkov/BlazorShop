namespace BlazorShop.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.Orders;

    public interface IOrdersService
    {
        Task PurchaseAsync(string userId, int deliveryAddressId);

        Task<IEnumerable<OrdersListingResponseModel>> GetAllByUserIdAsync(string userId);
    }
}