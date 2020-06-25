namespace BlazorShop.Shared.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Identity;

    public class ChangePasswordRequestModel
    {
        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [MinLength(MinPasswordLength)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
    }
}
