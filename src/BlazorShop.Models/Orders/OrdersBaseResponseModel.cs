namespace BlazorShop.Models.Orders
{
    using System.Globalization;

    using AutoMapper;

    using Common.Mapping;
    using Data.Models;

    public class OrdersBaseResponseModel : IMapFrom<Order>
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public virtual void Mapping(Profile mapper)
            => mapper
                .CreateMap<Order, OrdersDetailsResponseModel>()
                .ForMember(m => m.Id, m => m
                    .MapFrom(o => o.Id))
                .ForMember(m => m.CreatedOn, m => m
                    .MapFrom(o => o.CreatedOn.ToString(CultureInfo.InvariantCulture)));
    }
}
