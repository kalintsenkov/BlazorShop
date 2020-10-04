namespace BlazorShop.Web.Client.Shared.Common
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Models.Categories;

    public partial class Header
    {
        private IEnumerable<CategoriesListingResponseModel> categories;

        protected override async Task OnInitializedAsync()
            => this.categories = await this.CategoriesService.All();
    }
}
