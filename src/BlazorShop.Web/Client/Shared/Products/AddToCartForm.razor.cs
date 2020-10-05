namespace BlazorShop.Web.Client.Shared.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    using Models.ShoppingCarts;

    public partial class AddToCartForm
    {
        private readonly ShoppingCartRequestModel model = new ShoppingCartRequestModel();

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        [Parameter]
        public int ProductId { get; set; }

        [Parameter]
        public string ProductName { get; set; }

        [Parameter]
        public int ProductQuantity { get; set; }

        [Inject]
        protected IJSRuntime JsRuntime { get; set; }

        private async Task OnSubmitAsync()
        {
            this.model.ProductId = this.ProductId;

            var result = await this.ShoppingCartsService.AddProduct(this.model);

            if (!result.Succeeded)
            {
                this.Errors = result.Errors;
                this.ShowErrors = true;
            }
            else
            {
                this.ShowErrors = false;
                this.NavigationManager.NavigateTo("/cart", forceLoad: true);
            }
        }

        private async Task OnDeleteAsync()
        {
            var confirmed = await this.JsRuntime.InvokeAsync<bool>(
                "confirm",
                "Are you sure you want to delete this item?");

            if (confirmed)
            {
                var result = await this.ProductsService.DeleteAsync(this.ProductId);

                if (result.Succeeded)
                {
                    this.ToastService.ShowSuccess($"{this.ProductName} has been deleted successfully.");
                    this.NavigationManager.NavigateTo("/products/page/1");
                }
                else
                {
                    this.Errors = result.Errors;
                    this.ShowErrors = true;
                }
            }
        }

        private async Task AddToWishlist()
        {
            var result = await this.WishlistsService.AddProduct(this.ProductId);

            if (result.Succeeded)
            {
                this.ToastService.ShowSuccess($"{this.ProductName} has been added to your wishlist.");
            }
            else
            {
                this.Errors = result.Errors;
                this.ShowErrors = true;
            }
        }

        private void IncrementQuantity()
        {
            if (this.model.Quantity < this.ProductQuantity)
            {
                this.model.Quantity++;
                this.ShowErrors = false;
            }
        }

        private void DecrementQuantity()
        {
            if (this.model.Quantity > 1)
            {
                this.model.Quantity--;
                this.ShowErrors = false;
            }
        }
    }
}
