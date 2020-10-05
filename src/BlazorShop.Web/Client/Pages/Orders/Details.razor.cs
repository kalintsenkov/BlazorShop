namespace BlazorShop.Web.Client.Pages.Orders
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    using Models.Orders;

    public partial class Details
    {
        private OrdersDetailsResponseModel order;
        private decimal totalPrice;

        [Parameter]
        public string Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.order = await this.OrdersService.Details(this.Id);
            this.totalPrice = this.order.Products.Sum(p => p.Price * p.Quantity);
        }
    }
}
