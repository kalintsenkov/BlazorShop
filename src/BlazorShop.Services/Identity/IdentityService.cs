namespace BlazorShop.Services.Identity
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    using Data.Models;
    using Models;
    using Models.Identity;

    public class IdentityService : IIdentityService
    {
        private const string InvalidErrorMessage = "Invalid username or password.";

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IJwtGeneratorService jwtGenerator;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IJwtGeneratorService jwtGenerator)
        {
            this.userManager = userManager;
            this.jwtGenerator = jwtGenerator;
        }

        public async Task<Result> RegisterAsync(RegisterRequestModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var identityResult = await this.userManager.CreateAsync(user, model.Password);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result.Success
                : Result.Failure(errors);
        }

        public async Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return InvalidErrorMessage;
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return InvalidErrorMessage;
            }

            var token = await this.jwtGenerator.GenerateJwtAsync(user);

            return new LoginResponseModel { Token = token };
        }

        public async Task<Result> ChangeSettingsAsync(ChangeSettingsRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return InvalidErrorMessage;
            }

            user.UserName = model.Username;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;

            var identityResult = await this.userManager.UpdateAsync(user);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result.Success
                : Result.Failure(errors);
        }

        public async Task<Result> ChangePasswordAsync(ChangePasswordRequestModel model)
        {
            var user = await this.userManager.FindByIdAsync(model.UserId);
            if (user == null)
            {
                return InvalidErrorMessage;
            }

            var identityResult = await this.userManager.ChangePasswordAsync(
                user,
                model.Password,
                model.NewPassword);

            var errors = identityResult.Errors.Select(e => e.Description);

            return identityResult.Succeeded
                ? Result.Success
                : Result.Failure(errors);
        }
    }
}
