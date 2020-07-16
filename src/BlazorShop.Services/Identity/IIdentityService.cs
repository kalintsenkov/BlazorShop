namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;

    using Data.Models;
    using Models;
    using Models.Identity;

    public interface IIdentityService
    {
        Task<Result<ApplicationUser>> RegisterAsync(RegisterRequestModel model);

        Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model);

        Task<Result> ChangePasswordAsync(ChangePasswordRequestModel model);
    }
}
