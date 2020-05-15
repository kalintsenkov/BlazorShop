namespace BlazorShop.Data.Models
{
    using System;
    using System.Collections.Generic;

    using Interfaces;

    public class Category : IAuditInfo, IDeletableEntity
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public ICollection<Product> Products { get; }
    }
}