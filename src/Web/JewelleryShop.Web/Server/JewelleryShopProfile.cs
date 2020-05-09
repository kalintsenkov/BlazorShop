namespace JewelleryShop.Web.Server
{
    using AutoMapper;

    using Data.Models;
    using Shared.Categories;
    using Shared.Products;

    public class JewelleryShopProfile : Profile
    {
        public JewelleryShopProfile()
        {
            this.CreateMap<Category, CategoriesListingResponseModel>();

            this.CreateMap<Product, ProductsListingResponseModel>();
            this.CreateMap<Product, ProductsDetailsResponseModel>();
        }
    }
}