namespace BlazorShop.Services.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Web.Shared.Orders;

    public interface IOrdersService
    {
        Task PurchaseAsync(string userId);

        Task<IEnumerable<OrdersListingResponseModel>> GetAllByUserIdAsync(string userId);
    }
}