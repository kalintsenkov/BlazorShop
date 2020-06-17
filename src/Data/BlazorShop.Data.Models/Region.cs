namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    using Contracts;

    public class Region : BaseModel<int>
    {
        public string Name { get; set; }

        public ICollection<City> Cities { get; } = new HashSet<City>();
    }
}
