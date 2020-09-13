namespace BlazorShop.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Identity;

    public class LoginRequestModel
    {
        [Required]
        [EmailAddress]
        [MinLength(MinEmailLength)]
        [MaxLength(MaxEmailLength)]
        public string Email { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
