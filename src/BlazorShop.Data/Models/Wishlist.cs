namespace BlazorShop.Data.Models
{
    using Contracts;

    public class Wishlist : BaseDeletableModel
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
