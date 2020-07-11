namespace BlazorShop.Web.Client.Services
{
    using System.Threading.Tasks;

    using BlazorShop.Shared.Models.Identity;

    public interface IAuthService
    {
        Task<LoginResponseModel> Login(LoginRequestModel model);

        Task<RegisterResponseModel> Register(RegisterRequestModel model);

        Task Logout();
    }
}
