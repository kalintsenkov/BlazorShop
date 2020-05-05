namespace SheryLady.Web.Shared.Products
{
    public class ProductsDetailsResponseModel : ProductsListingResponseModel
    {
        public string Description { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CreatedOn { get; set; }
    }
}
