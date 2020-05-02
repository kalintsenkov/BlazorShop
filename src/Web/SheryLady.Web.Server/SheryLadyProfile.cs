namespace SheryLady.Web.Server
{
    using AutoMapper;

    using Data.Models;
    using Services.Models.Categories;

    public class SheryLadyProfile : Profile
    {
        public SheryLadyProfile()
        {
            this.CreateMap<Category, CategoriesListingServiceModel>();
            this.CreateMap<Category, CategoriesDetailsServiceModel>();
        }
    }
}