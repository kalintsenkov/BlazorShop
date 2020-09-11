namespace BlazorShop.Data.Models
{
    using Contracts;

    public class OrderProduct : BaseModel
    {
        public string OrderId { get; set; }

        public Order Order { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
