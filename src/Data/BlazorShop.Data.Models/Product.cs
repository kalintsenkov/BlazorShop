namespace BlazorShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class Product : IAuditInfo, IDeletableEntity
    {
        public Product()
        {
            this.Orders = new HashSet<OrderProduct>();
            this.Wishlists = new HashSet<Wishlist>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageSource { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<OrderProduct> Orders { get; }

        public ICollection<Wishlist> Wishlists { get; set; }
    }
}