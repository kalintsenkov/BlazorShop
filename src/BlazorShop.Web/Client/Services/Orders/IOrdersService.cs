namespace BlazorShop.Web.Client.Services.Orders
{
    using System.Threading.Tasks;

    using Models.Orders;

    public interface IOrdersService
    {
        Task<string> PurchaseAsync(OrdersRequestModel model);

        Task<OrdersDetailsResponseModel> DetailsAsync(string id);

        Task<OrdersListingResponseModel[]> MineAsync();
    }
}
