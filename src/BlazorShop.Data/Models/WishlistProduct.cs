namespace BlazorShop.Data.Models
{
    using Contracts;

    public class WishlistProduct : BaseModel
    {
        public int WishlistId { get; set; }

        public Wishlist Wishlist { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }
    }
}
