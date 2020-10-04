namespace BlazorShop.Web.Client.Shared.Common
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models.Categories;

    public partial class Header
    {
        private IEnumerable<CategoriesListingResponseModel> categories;

        protected override async Task OnInitializedAsync()
            => this.categories = await this.Http.GetFromJsonAsync<IEnumerable<CategoriesListingResponseModel>>("api/categories");
    }
}
