namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Contracts;

    public class Address : BaseDeletableModel<int>
    {
        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Description { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public ICollection<Order> Orders { get; } = new HashSet<Order>();
    }
}