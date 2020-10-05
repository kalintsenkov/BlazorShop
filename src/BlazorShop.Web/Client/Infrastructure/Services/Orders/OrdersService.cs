namespace BlazorShop.Web.Client.Infrastructure.Services.Orders
{
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models.Orders;

    public class OrdersService : IOrdersService
    {
        private readonly HttpClient http;

        private const string OrdersPath = "api/orders";
        private const string OrdersPathWithSlash = OrdersPath + "/";

        public OrdersService(HttpClient http) => this.http = http;

        public async Task<string> Purchase(OrdersRequestModel model)
        {
            var orderResponse = await this.http.PostAsJsonAsync(OrdersPath, model);
            var orderId = await orderResponse.Content.ReadAsStringAsync();

            return orderId;
        }

        public async Task<OrdersDetailsResponseModel> Details(string id)
            => await this.http.GetFromJsonAsync<OrdersDetailsResponseModel>(OrdersPathWithSlash + id);

        public async Task<IEnumerable<OrdersListingResponseModel>> Mine()
            => await this.http.GetFromJsonAsync<IEnumerable<OrdersListingResponseModel>>(OrdersPath);
    }
}
