namespace BlazorShop.Shared.Models.Categories
{
    using Data.Models;
    using Mapping;

    public class CategoriesListingResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}