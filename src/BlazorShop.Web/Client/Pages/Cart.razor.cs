namespace BlazorShop.Web.Client.Pages
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models.ShoppingCarts;

    public partial class Cart
    {
        private readonly ShoppingCartRequestModel model = new ShoppingCartRequestModel();

        private decimal totalPrice;
        private IEnumerable<ShoppingCartProductsResponseModel> cartProducts;

        protected override async Task OnInitializedAsync() => await this.LoadDataAsync();

        private async Task LoadDataAsync()
        {
            this.cartProducts = await this.Http.GetFromJsonAsync<IEnumerable<ShoppingCartProductsResponseModel>>("api/ShoppingCarts");
            this.totalPrice = this.cartProducts.Sum(p => p.Price * p.Quantity);
        }

        private async Task OnRemoveAsync(int productId)
        {
            await this.Http.DeleteAsync($"api/shoppingcarts/RemoveProduct/{productId}");

            this.NavigationManager.NavigateTo("/cart", forceLoad: true);
        }

        private async Task IncrementQuantity(int productId, int quantity, int stockQuantity)
        {
            this.model.ProductId = productId;
            this.model.Quantity = quantity;

            if (this.model.Quantity + 1 <= stockQuantity)
            {
                this.model.Quantity++;

                await this.Http.PutAsJsonAsync("api/shoppingcarts/UpdateProduct", this.model);
                await this.LoadDataAsync();
            }
        }

        private async Task DecrementQuantity(int productId, int quantity)
        {
            this.model.ProductId = productId;
            this.model.Quantity = quantity;

            if (this.model.Quantity - 1 > 0)
            {
                this.model.Quantity--;

                await this.Http.PutAsJsonAsync("api/shoppingcarts/UpdateProduct", this.model);
                await this.LoadDataAsync();
            }
        }
    }
}
