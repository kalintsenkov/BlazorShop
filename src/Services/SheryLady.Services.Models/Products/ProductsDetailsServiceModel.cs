namespace SheryLady.Services.Models.Products
{
    public class ProductsDetailsServiceModel : ProductsListingServiceModel
    {
        public string Description { get; set; }

        public int Quantity { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string CreatedOn { get; set; }
    }
}
