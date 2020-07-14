namespace BlazorShop.Models.Categories
{
    using Common.Mapping;
    using Data.Models;

    public class CategoryListingResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}