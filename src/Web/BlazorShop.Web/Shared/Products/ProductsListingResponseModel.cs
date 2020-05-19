namespace BlazorShop.Web.Shared.Products
{
    using Data.Models;
    using Services.Mapping;

    public class ProductsListingResponseModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
