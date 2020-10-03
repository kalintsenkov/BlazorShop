namespace BlazorShop.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using Contracts;

    public class BlazorShopRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public BlazorShopRole()
            : this(null)
        {
        }

        public BlazorShopRole(string name)
            : base(name)
            => this.Id = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}