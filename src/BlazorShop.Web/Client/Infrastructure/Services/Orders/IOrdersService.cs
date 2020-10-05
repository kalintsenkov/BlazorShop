namespace BlazorShop.Web.Client.Infrastructure.Services.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Orders;

    public interface IOrdersService
    {
        Task<string> Purchase(OrdersRequestModel model);

        Task<OrdersDetailsResponseModel> Details(string id);

        Task<IEnumerable<OrdersListingResponseModel>> Mine();
    }
}