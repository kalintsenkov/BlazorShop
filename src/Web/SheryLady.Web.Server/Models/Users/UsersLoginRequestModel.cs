namespace SheryLady.Web.Server.Models.Users
{
    using System.ComponentModel.DataAnnotations;

    public class UsersLoginRequestModel
    {
        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
