namespace BlazorShop.Web.Client.Services.Orders
{
    using System.Threading.Tasks;

    using Infrastructure;
    using Models.Orders;

    public class OrderService : IOrdersService
    {
        private const string OrdersPath = "api/orders";

        private readonly IApiClient apiClient;

        public OrderService(IApiClient apiClient) 
            => this.apiClient = apiClient;

        public async Task<string> PurchaseAsync(OrdersRequestModel model)
            => await this.apiClient.PostJson<OrdersRequestModel, string>(OrdersPath, model);

        public async Task<OrdersDetailsResponseModel> DetailsAsync(string id)
            => await this.apiClient.GetJson<OrdersDetailsResponseModel>($"{OrdersPath}/{id}");

        public async Task<OrdersListingResponseModel[]> MineAsync()
            => await this.apiClient.GetJson<OrdersListingResponseModel[]>(OrdersPath);
    }
}
