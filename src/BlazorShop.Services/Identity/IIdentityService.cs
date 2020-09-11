namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;

    using Models;
    using Models.Identity;

    public interface IIdentityService
    {
        Task<Result> RegisterAsync(RegisterRequestModel model);

        Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model);

        Task<Result> ChangeSettingsAsync(string userId, ChangeSettingsRequestModel model);

        Task<Result> ChangePasswordAsync(string userId, ChangePasswordRequestModel model);
    }
}
