namespace BlazorShop.Web.Server.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    using Data.Models;
    using Infrastructure.Extensions;
    using Services.Identity;
    using Shared.Models.Identity;

    public class IdentityController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IIdentityService identityService;
        private readonly ApplicationSettings appSettings;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            IIdentityService identityService,
            IOptions<ApplicationSettings> appSettings)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.appSettings = appSettings.Value;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<RegisterResponseModel>> Register(RegisterRequestModel model)
        {
            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Username,
                FirstName = model.FirstName,
                LastName = model.LastName
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                return new RegisterResponseModel { Successful = true };
            }

            var errors = result.Errors.Select(x => x.Description);

            return new RegisterResponseModel { Successful = false, Errors = errors };
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return Unauthorized(new LoginResponseModel { Successful = false, Error = "Invalid username or password." });
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return Unauthorized(new LoginResponseModel { Successful = false, Error = "Invalid username or password." });
            }

            var token = await this.identityService.GenerateJwtAsync(
                user.Id,
                user.UserName,
                this.appSettings.Secret);

            return new LoginResponseModel { Successful = true, Token = token };
        }

        [HttpPost(nameof(ChangePassword))]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequestModel model)
        {
            var user = await this.userManager.FindByIdAsync(this.User.GetId());

            var result = await this.userManager.ChangePasswordAsync(
                user,
                model.Password,
                model.NewPassword);

            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}