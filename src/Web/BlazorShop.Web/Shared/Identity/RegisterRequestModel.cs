namespace BlazorShop.Web.Shared.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ErrorMessages;
    using static Common.ModelConstants;

    public class RegisterRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(UserUserNameMaxLength, ErrorMessage = StringLengthErrorMessage, MinimumLength = UserUserNameMinLength)]
        public string Username { get; set; }

        [Required]
        [MinLength(UserPasswordMinLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(UserPasswordMinLength)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = PasswordsDoNotMatchErrorMessage)]
        public string ConfirmPassword { get; set; }
    }
}
