namespace BlazorShop.Data.Models
{
    public class OrderProduct
    {
        public string OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
