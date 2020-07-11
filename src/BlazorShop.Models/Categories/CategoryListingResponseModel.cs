namespace BlazorShop.Models.Categories
{
    using Data.Models;

    public class CategoryListingResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}