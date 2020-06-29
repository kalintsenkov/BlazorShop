namespace BlazorShop.Shared.Models.Products
{
    using Data.Models;
    using Mapping;

    public class ProductListingResponseModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
