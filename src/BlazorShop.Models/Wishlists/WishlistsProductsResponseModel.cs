namespace BlazorShop.Models.Wishlists
{
    using AutoMapper;

    using Common.Mapping;
    using Data.Models;

    public class WishlistsProductsResponseModel : IMapFrom<WishlistProduct>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageSource { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public void Mapping(Profile mapper)
            => mapper
                .CreateMap<WishlistProduct, WishlistsProductsResponseModel>()
                .ForMember(m => m.Id, m => m
                    .MapFrom(c => c.Product.Id))
                .ForMember(m => m.Name, m => m
                    .MapFrom(c => c.Product.Name))
                .ForMember(m => m.Description, m => m
                    .MapFrom(c => c.Product.Description))
                .ForMember(m => m.ImageSource, m => m
                    .MapFrom(c => c.Product.ImageSource))
                .ForMember(m => m.Price, m => m
                    .MapFrom(c => c.Product.Price));

    }
}
