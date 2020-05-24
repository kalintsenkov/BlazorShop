// ReSharper disable VirtualMemberCallInConstructor
namespace BlazorShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Identity;

    using Interfaces;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Orders = new HashSet<Order>();
            this.Wishlists = new HashSet<Wishlist>();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Order> Orders { get; }

        public ICollection<Wishlist> Wishlists { get; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; }
    }
}