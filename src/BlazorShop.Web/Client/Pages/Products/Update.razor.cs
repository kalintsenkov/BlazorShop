namespace BlazorShop.Web.Client.Pages.Products
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Components;

    using Models.Categories;
    using Models.Products;

    public partial class Update
    {
        private ProductsRequestModel model;
        private IEnumerable<CategoriesListingResponseModel> categories;

        [Parameter]
        public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.model = await this.Http.GetFromJsonAsync<ProductsRequestModel>($"api/products/{this.Id}");
            this.categories = await this.Http.GetFromJsonAsync<IEnumerable<CategoriesListingResponseModel>>("api/categories");
        }

        private async Task SubmitAsync()
        {
            var result = await this.ProductsService.UpdateAsync(this.Id, this.model);

            if (result.Succeeded)
            {
                this.NavigationManager.NavigateTo($"/products/{this.Id}/{this.model.Name}");
            }
        }
    }
}
