namespace BlazorShop.Models.Products
{
    using Common.Mapping;
    using Data.Models;

    public class ProductsListingResponseModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
    }
}
