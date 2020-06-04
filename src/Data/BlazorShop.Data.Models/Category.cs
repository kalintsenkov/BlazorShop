namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Interfaces;

    public class Category : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public ICollection<Product> Products { get; } = new HashSet<Product>();
    }
}