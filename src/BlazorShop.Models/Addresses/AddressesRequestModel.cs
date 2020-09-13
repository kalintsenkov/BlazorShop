namespace BlazorShop.Models.Addresses
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Address;

    public class AddressesRequestModel
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

        [Required]
        [MaxLength(MaxPostalCodeLength)]
        public string PostalCode { get; set; }

        [Required]
        [MinLength(MinPhoneNumberLength)]
        [MaxLength(MaxPhoneNumberLength)]
        [RegularExpression(PhoneNumberRegularExpression)]
        public string PhoneNumber { get; set; }
    }
}
