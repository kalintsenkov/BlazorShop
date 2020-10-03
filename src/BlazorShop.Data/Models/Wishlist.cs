namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Contracts;

    public class Wishlist : BaseModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public BlazorShopUser User { get; set; }

        public ICollection<WishlistProduct> Products { get; } = new HashSet<WishlistProduct>();
    }
}
