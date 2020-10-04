namespace BlazorShop.Web.Client.Shared.Navigation
{
    using Models.Products;

    public partial class NavMobileSearch
    {
        private readonly ProductsSearchRequestModel searchModel = new ProductsSearchRequestModel();

        private void Search() 
            => this.NavigationManager.NavigateTo($"/products/search/{this.searchModel.Query}/page/1");
    }
}
