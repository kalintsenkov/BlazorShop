namespace BlazorShop.Shared.Models.Categories
{
    using Data.Models;
    using Mapping;

    public class CategoryListingResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}