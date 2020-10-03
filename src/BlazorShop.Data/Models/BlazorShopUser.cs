namespace BlazorShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using Contracts;

    public class BlazorShopUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public BlazorShopUser() => this.Id = Guid.NewGuid().ToString();

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Address> Addresses { get; } = new HashSet<Address>();

        public ICollection<Order> Orders { get; } = new HashSet<Order>();

        public ICollection<Wishlist> Wishlists { get; } = new HashSet<Wishlist>();

        public ICollection<ShoppingCart> ShoppingCarts { get; } = new HashSet<ShoppingCart>();
    }
}