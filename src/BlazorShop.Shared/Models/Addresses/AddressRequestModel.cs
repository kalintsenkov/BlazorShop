namespace BlazorShop.Shared.Models.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Address;

    public class AddressRequestModel
    {
        [Required]
        [MaxLength(MaxCountryLength)]
        public string Country { get; set; }

        [Required]
        [MaxLength(MaxStateLength)]
        public string State { get; set; }

        [Required]
        [MaxLength(MaxCityLength)]
        public string City { get; set; }

        [Required]
        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        public string PostalCode { get; set; }

        [Required]
        [Phone]
        [MaxLength(MaxPhoneNumberLength)]
        public string PhoneNumber { get; set; }
    }
}
