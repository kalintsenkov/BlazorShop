namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Contracts;

    public class Order : BaseModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<OrderProduct> Products { get; } = new HashSet<OrderProduct>();
    }
}
