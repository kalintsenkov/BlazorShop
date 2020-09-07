namespace BlazorShop.Web.Client.Services.Authentication
{
    using System.Threading.Tasks;

    using Models;
    using Models.Identity;

    public interface IAuthService
    {
        Task<Result> Register(RegisterRequestModel model);

        Task<Result> Login(LoginRequestModel model);

        Task Logout();
    }
}
