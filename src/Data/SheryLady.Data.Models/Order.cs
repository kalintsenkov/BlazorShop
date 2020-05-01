namespace SheryLady.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class Order : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<OrderProduct> Products { get; } = new HashSet<OrderProduct>();
    }
}
