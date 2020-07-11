namespace BlazorShop.Models.Orders
{
    using System.Globalization;

    using AutoMapper;

    using Data.Models;

    public class OrderListingResponseModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public int ProductsCount { get; set; }

        public virtual void Mapping(Profile profile)
            => profile
                .CreateMap<Order, OrderListingResponseModel>()
                .ForMember(m => m.CreatedOn, m => m
                    .MapFrom(o => o.CreatedOn.ToString(CultureInfo.InvariantCulture)));
    }
}
