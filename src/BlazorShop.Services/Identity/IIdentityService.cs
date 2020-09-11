namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;

    using Models;
    using Models.Identity;

    public interface IIdentityService
    {
        Task<Result> RegisterAsync(RegisterRequestModel model);

        Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model);

        Task<Result> ChangeSettingsAsync(ChangeSettingsRequestModel model, string userId);

        Task<Result> ChangePasswordAsync(ChangePasswordRequestModel model, string userId);
    }
}
