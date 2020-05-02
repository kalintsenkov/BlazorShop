// ReSharper disable VirtualMemberCallInConstructor
namespace SheryLady.Data.Models
{
    using System;

    using Microsoft.AspNetCore.Identity;

    using Interfaces;

    public class ApplicationRole : IdentityRole, IAuditInfo, IDeletableEntity
    {
        public ApplicationRole()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}