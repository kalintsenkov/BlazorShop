namespace BlazorShop.Models.Products
{
    using System.Collections.Generic;

    public class ProductsSearchResponseModel
    {
        public IEnumerable<ProductsListingResponseModel> Products { get; set; }

        public int Page { get; set; }

        public int TotalPages { get; set; }
    }
}
