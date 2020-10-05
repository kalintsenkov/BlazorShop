namespace BlazorShop.Web.Client.Pages.Orders
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Orders;

    public partial class Mine
    {
        private IEnumerable<OrdersListingResponseModel> orders;

        protected override async Task OnInitializedAsync()
            => this.orders = await this.OrdersService.Mine();
    }
}
