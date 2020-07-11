namespace BlazorShop.Shared.Models.Identity
{
    public class LoginResponseModel
    {
        public bool Successful { get; set; }

        public string Error { get; set; }

        public string Token { get; set; }
    }
}
