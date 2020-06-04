namespace BlazorShop.Data.Models
{
    using Interfaces;

    public class City : BaseModel<int>
    {
        public string Name { get; set; }

        public int RegionId { get; set; }

        public Region Region { get; set; }
    }
}
