namespace BlazorShop.Models.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static Data.ModelConstants.Identity;

    public class ChangePasswordRequestModel
    {
        [Required]
        public string UserId { get; set; }

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
