namespace BlazorShop.Web.Client.Pages
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models.ShoppingCarts;
    using Models.Wishlists;

    public partial class Wishlist
    {
        private IEnumerable<WishlistsProductsResponseModel> products;

        protected override async Task OnInitializedAsync() => await this.LoadDataAsync();

        private async Task LoadDataAsync()
            => this.products = await this.Http.GetFromJsonAsync<IEnumerable<WishlistsProductsResponseModel>>("api/Wishlists");

        private async Task OnSubmitAsync(int id)
        {
            var cartRequest = new ShoppingCartRequestModel
            {
                ProductId = id,
                Quantity = 1
            };

            await this.Http.PostAsJsonAsync("api/shoppingcarts/AddProduct", cartRequest);
            this.NavigationManager.NavigateTo("/cart", forceLoad: true);
        }

        private async Task OnRemoveAsync(int id)
        {
            await this.Http.DeleteAsync($"api/wishlists/RemoveProduct/{id}");
            await this.LoadDataAsync();
        }
    }
}
