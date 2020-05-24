namespace BlazorShop.Data.Models
{
    using System.Collections.Generic;

    public class Region
    {
        public Region()
        {
            this.Cities = new HashSet<City>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<City> Cities { get; }
    }
}
