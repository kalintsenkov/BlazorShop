namespace BlazorShop.Data.Models
{
    using System;

    using Contracts;

    public class Wishlist : IAuditInfo, IDeletableEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
