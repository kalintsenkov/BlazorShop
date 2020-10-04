namespace BlazorShop.Web.Client.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Infrastructure.Extensions;
    using Models.Addresses;
    using Models.Orders;
    using Models.ShoppingCarts;

    public partial class Checkout
    {
        private readonly AddressesRequestModel address = new AddressesRequestModel();
        private readonly OrdersRequestModel order = new OrdersRequestModel();

        private IEnumerable<ShoppingCartProductsResponseModel> cartProducts;

        private string email;
        private decimal totalPrice;

        protected override async Task OnInitializedAsync()
        {
            var state = await this.AuthState.GetAuthenticationStateAsync();
            var user = state.User;

            this.cartProducts = await this.Http.GetFromJsonAsync<IEnumerable<ShoppingCartProductsResponseModel>>("api/shoppingcarts");

            this.email = user.GetEmail();
            this.totalPrice = this.cartProducts.Sum(p => p.Price * p.Quantity);
        }

        private async Task SubmitAsync()
        {
            var addressResponse = await this.Http.PostAsJsonAsync("api/addresses", this.address);
            var addressId = await addressResponse.Content.ReadFromJsonAsync<int>();

            this.order.AddressId = addressId;

            var orderResponse = await this.Http.PostAsJsonAsync("api/orders", this.order);
            var orderId = await orderResponse.Content.ReadAsStringAsync();

            this.NavigationManager.NavigateTo($"/order/confirmed/{orderId}", forceLoad: true);
        }
    }
}
