namespace BlazorShop.Web.Shared.Identity
{
    using System.ComponentModel.DataAnnotations;

    using static Common.ModelConstants.User;

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
