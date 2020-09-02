namespace BlazorShop.Data.Models
{
    using Contracts;

    public class ShoppingCart : BaseModel
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
