namespace BlazorShop.Web.Client.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Infrastructure.Extensions;
    using Models.Addresses;
    using Models.Orders;
    using Models.ShoppingCarts;

    public partial class Checkout
    {
        private readonly AddressesRequestModel address = new AddressesRequestModel();
        private readonly OrdersRequestModel order = new OrdersRequestModel();

        private string email;
        private decimal totalPrice;
        private IEnumerable<ShoppingCartProductsResponseModel> cartProducts;

        protected override async Task OnInitializedAsync()
        {
            var state = await this.AuthState.GetAuthenticationStateAsync();
            var user = state.User;

            this.email = user.GetEmail();

            this.cartProducts = await this.ShoppingCartsService.Mine();
            this.totalPrice = this.cartProducts.Sum(p => p.Price * p.Quantity);
        }

        private async Task SubmitAsync()
        {
            var addressId = await this.AddressesService.CreateAsync(this.address);

            this.order.AddressId = addressId;

            var orderId = await this.OrdersService.Purchase(this.order);

            this.NavigationManager.NavigateTo($"/order/confirmed/{orderId}", forceLoad: true);
        }
    }
}
