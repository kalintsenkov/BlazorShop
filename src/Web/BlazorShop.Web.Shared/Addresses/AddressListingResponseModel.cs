namespace BlazorShop.Web.Shared.Addresses
{
    using Data.Models;
    using Services.Mapping;

    public class AddressListingResponseModel : IMapFrom<Address>
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Description { get; set; }

        public int PostalCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
