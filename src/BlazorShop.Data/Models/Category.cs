namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Contracts;

    public class Category : BaseDeletableModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Product> Products { get; } = new HashSet<Product>();
    }
}