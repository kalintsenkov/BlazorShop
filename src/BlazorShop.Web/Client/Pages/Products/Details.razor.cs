namespace BlazorShop.Web.Client.Pages.Products
{
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    using Models.Products;

    public partial class Details
    {
        private ProductsDetailsResponseModel product;

        [Parameter]
        public int Id { get; set; }

        [Parameter]
        public string ProductName { get; set; }

        protected override async Task OnInitializedAsync()
            => this.product = await this.Http.GetFromJsonAsync<ProductsDetailsResponseModel>($"api/products/{this.Id}");
    }
}
