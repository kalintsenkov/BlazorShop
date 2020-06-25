namespace BlazorShop.Shared.Models.Products
{
    using Data.Models;
    using Mapping;

    public class ProductsListingResponseModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
