namespace SheryLady.Data.Models
{
    using System;

    using Interfaces;

    public class Deal : IAuditInfo, IDeletableEntity
    {
        public int Id { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Discount { get; set; }

        public int ProductId { get; set; }

        public Product Product { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}