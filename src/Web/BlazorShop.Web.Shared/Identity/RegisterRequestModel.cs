namespace BlazorShop.Web.Shared.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ErrorMessages;
    using static Common.ModelConstants.Common;
    using static Common.ModelConstants.User;

    public class RegisterRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(MaxNameLength, ErrorMessage = StringLengthErrorMessage, MinimumLength = MinNameLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = PasswordsDoNotMatchErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
