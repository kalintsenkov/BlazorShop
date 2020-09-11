namespace BlazorShop.Data.Models
{
    using Contracts;

    public class ShoppingCartProduct : BaseModel
    {
        public int ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
