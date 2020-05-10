namespace JewelleryShop.Web.Shared.Categories
{
    using Data.Models;
    using Services.Mapping;

    public class CategoriesListingResponseModel : IMapFrom<Category>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}