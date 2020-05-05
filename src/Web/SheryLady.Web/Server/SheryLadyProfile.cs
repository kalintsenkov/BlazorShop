namespace SheryLady.Web.Server
{
    using AutoMapper;

    using Data.Models;
    using Shared.Categories;
    using Shared.Products;

    public class SheryLadyProfile : Profile
    {
        public SheryLadyProfile()
        {
            this.CreateMap<Category, CategoriesListingResponseModel>();

            this.CreateMap<Product, ProductsListingResponseModel>();
            this.CreateMap<Product, ProductsDetailsResponseModel>();
        }
    }
}