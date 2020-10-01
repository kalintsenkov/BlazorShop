namespace BlazorShop.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using Contracts;

    public sealed class ApplicationRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole()
            : this(null)
        {
        }

        public ApplicationRole(string name)
            : base(name)
            => Id = Guid.NewGuid().ToString();

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}