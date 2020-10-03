namespace BlazorShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Contracts;

    public class Order : BaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string UserId { get; set; }

        public BlazorShopUser User { get; set; }

        public int DeliveryAddressId { get; set; }

        public Address DeliveryAddress { get; set; }

        public ICollection<OrderProduct> Products { get; } = new HashSet<OrderProduct>();
    }
}
