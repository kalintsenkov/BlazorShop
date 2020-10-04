namespace BlazorShop.Web.Client.Shared.Navigation
{
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    public partial class NavCart
    {
        private int? cartProductsCount;

        protected override async Task OnInitializedAsync()
            => this.cartProductsCount = await this.Http.GetFromJsonAsync<int>("api/shoppingcarts/TotalProducts");
    }
}
