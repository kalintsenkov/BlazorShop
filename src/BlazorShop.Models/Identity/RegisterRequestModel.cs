namespace BlazorShop.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static ErrorMessages;
    using static Data.ModelConstants.Common;
    using static Data.ModelConstants.Identity;

    public class RegisterRequestModel : LoginRequestModel
    {
        [Required]
        [StringLength(
            MaxNameLength,
            ErrorMessage = StringLengthErrorMessage,
            MinimumLength = MinNameLength)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(
            MaxNameLength,
            ErrorMessage = StringLengthErrorMessage,
            MinimumLength = MinNameLength)]
        public string LastName { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = PasswordsDoNotMatchErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
