namespace BlazorShop.Shared.Models.Addresses
{
    using Data.Models;
    using Mapping;

    public class AddressListingResponseModel : IMapFrom<Address>
    {
        public int Id { get; set; }

        public string Country { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string Description { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }
    }
}
