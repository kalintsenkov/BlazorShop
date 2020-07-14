namespace BlazorShop.Models.Orders
{
    using System.Globalization;

    using AutoMapper;

    using Common.Mapping;
    using Data.Models;

    public class OrderListingResponseModel : IMapFrom<Order>, IMapExplicitly
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string UserFirstName { get; set; }

        public string UserLastName { get; set; }

        public virtual void RegisterMappings(IProfileExpression profile)
            => profile
                .CreateMap<Order, OrderListingResponseModel>()
                .ForMember(m => m.CreatedOn, m => m
                    .MapFrom(o => o.CreatedOn.ToString(CultureInfo.InvariantCulture)));
    }
}
