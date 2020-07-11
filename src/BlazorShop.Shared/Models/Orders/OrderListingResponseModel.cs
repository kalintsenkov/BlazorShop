namespace BlazorShop.Shared.Models.Orders
{
    using System.Globalization;

    using AutoMapper;

    using Data.Models;
    using Mapping;

    public class OrderListingResponseModel : IMapFrom<Order>, IMapExplicitly
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public int ProductsCount { get; set; }

        public virtual void RegisterMappings(IProfileExpression profile)
            => profile
                .CreateMap<Order, OrderListingResponseModel>()
                .ForMember(m => m.CreatedOn, m => m
                    .MapFrom(o => o.CreatedOn.ToString(CultureInfo.InvariantCulture)));
    }
}
