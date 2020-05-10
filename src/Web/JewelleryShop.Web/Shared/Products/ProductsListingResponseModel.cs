namespace JewelleryShop.Web.Shared.Products
{
    using Data.Models;
    using Services.Mapping;

    public class ProductsListingResponseModel : IMapFrom<Product>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public decimal Price { get; set; }
    }
}
