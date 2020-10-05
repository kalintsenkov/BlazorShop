namespace BlazorShop.Web.Client.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.ShoppingCarts;
    using Models.Wishlists;

    public partial class Wishlist
    {
        private IEnumerable<WishlistsProductsResponseModel> products;

        protected override async Task OnInitializedAsync() => await this.LoadDataAsync();

        private async Task LoadDataAsync() => this.products = await this.WishlistsService.Mine();

        private async Task OnSubmitAsync(int id)
        {
            var cartRequest = new ShoppingCartRequestModel
            {
                ProductId = id,
                Quantity = 1
            };

            await this.ShoppingCartsService.AddProduct(cartRequest);

            this.NavigationManager.NavigateTo("/cart", forceLoad: true);
        }

        private async Task OnRemoveAsync(int id)
        {
            var result = await this.WishlistsService.RemoveProduct(id);

            if (result.Succeeded)
            {
                await this.LoadDataAsync();
            }
        }
    }
}
