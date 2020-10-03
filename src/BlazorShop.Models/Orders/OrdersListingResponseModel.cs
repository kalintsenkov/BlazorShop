namespace BlazorShop.Models.Orders
{
    using AutoMapper;

    using Data.Models;

    public class OrdersListingResponseModel : OrdersBaseResponseModel
    {
        public string ProductName { get; set; }

        public string ProductImageSource { get; set; }

        public override void Mapping(Profile mapper)
            => mapper
                .CreateMap<OrderProduct, OrdersListingResponseModel>()
                .ForMember(m => m.Id, m => m
                    .MapFrom(op => op.Order.Id))
                .ForMember(m => m.CreatedOn, m => m
                    .MapFrom(op => op.Order.CreatedOn.ToShortDateString()))
                .ForMember(m => m.ProductName, m => m
                    .MapFrom(op => op.Product.Name))
                .ForMember(m => m.ProductImageSource, m => m
                    .MapFrom(o => o.Product.ImageSource));
    }
}
