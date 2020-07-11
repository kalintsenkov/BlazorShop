namespace BlazorShop.Models
{
    internal class ErrorMessages
    {
        public const string StringLengthErrorMessage 
            = "The {0} must be at least {2} and at max {1} characters long.";

        public const string PasswordsDoNotMatchErrorMessage 
            = "The password and confirmation password do not match.";
    }
}