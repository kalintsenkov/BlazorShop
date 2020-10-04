namespace BlazorShop.Web.Client.Pages.Products
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Categories;
    using Models.Products;

    public partial class Add
    {
        private readonly ProductsRequestModel model = new ProductsRequestModel();

        private IEnumerable<CategoriesListingResponseModel> categories;

        protected override async Task OnInitializedAsync()
            => this.categories = await this.CategoriesService.All();

        private async Task SubmitAsync()
        {
            var id = await this.ProductsService.CreateAsync(this.model);

            this.NavigationManager.NavigateTo($"/products/{id}/{this.model.Name}");
        }
    }
}
