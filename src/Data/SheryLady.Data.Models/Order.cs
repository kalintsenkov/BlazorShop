namespace SheryLady.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class Order : IAuditInfo
    {
        public Order()
        {
            this.Products = new HashSet<OrderProduct>();
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public ICollection<OrderProduct> Products { get; }
    }
}
