namespace BlazorShop.Models.Orders
{
    using System.Collections.Generic;

    using AutoMapper;

    using Data.Models;
    using Products;

    public class OrdersDetailsResponseModel : OrdersListingResponseModel
    {
        public IEnumerable<ProductsListingResponseModel> Products { get; set; }

        public override void RegisterMappings(IProfileExpression profile)
            => profile
                .CreateMap<OrderProduct, ProductsListingResponseModel>()
                .ForMember(m => m.Id, m => m.MapFrom(op => op.Product.Id))
                .ForMember(m => m.Name, m => m.MapFrom(op => op.Product.Name))
                .ForMember(m => m.ImageSource, m => m.MapFrom(op => op.Product.ImageSource))
                .ForMember(m => m.Quantity, m => m.MapFrom(op => op.Quantity))
                .ForMember(m => m.Price, m => m.MapFrom(op => op.Product.Price));
    }
}
