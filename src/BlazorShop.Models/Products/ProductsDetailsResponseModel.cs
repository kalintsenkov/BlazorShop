namespace BlazorShop.Models.Products
{
    public class ProductsDetailsResponseModel : ProductsListingResponseModel
    {
        public int Quantity { get; set; }

        public string CategoryName { get; set; }
    }
}
