namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;

    using Common;
    using Models;
    using Models.Identity;

    public interface IIdentityService : IService
    {
        Task<Result> RegisterAsync(RegisterRequestModel model);

        Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model);

        Task<Result> ChangeSettingsAsync(ChangeSettingsRequestModel model, string userId);

        Task<Result> ChangePasswordAsync(ChangePasswordRequestModel model, string userId);
    }
}
