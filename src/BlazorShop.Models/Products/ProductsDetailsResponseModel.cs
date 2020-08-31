namespace BlazorShop.Models.Products
{
    public class ProductsDetailsResponseModel : ProductsListingResponseModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}
