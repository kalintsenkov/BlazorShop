namespace SheryLady.Web.Server
{
    using AutoMapper;

    using Data.Models;
    using Services.Models.Categories;
    using Services.Models.Products;

    public class SheryLadyProfile : Profile
    {
        public SheryLadyProfile()
        {
            this.CreateMap<Category, CategoriesListingServiceModel>();

            this.CreateMap<Product, ProductsListingServiceModel>();
            this.CreateMap<Product, ProductsDetailsServiceModel>();
        }
    }
}