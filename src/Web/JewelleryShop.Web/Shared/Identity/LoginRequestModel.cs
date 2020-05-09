namespace JewelleryShop.Web.Shared.Identity
{
    using System.ComponentModel.DataAnnotations;

    public class LoginRequestModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
