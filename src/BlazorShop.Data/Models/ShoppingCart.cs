namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Contracts;

    public class ShoppingCart : BaseModel
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<ShoppingCartProduct> Products { get; } = new HashSet<ShoppingCartProduct>();
    }
}
