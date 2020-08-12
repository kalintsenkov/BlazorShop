namespace BlazorShop.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models.Identity;
    using Services.Identity;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identity;

        public IdentityController(IIdentityService identity)
            => this.identity = identity;

        [HttpPost(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
            => await this.identity
                .RegisterAsync(model)
                .ToActionResult();

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
            => await this.identity
                .LoginAsync(model)
                .ToActionResult();

        [Authorize]
        [HttpPut(nameof(ChangeSettings))]
        public async Task<ActionResult> ChangeSettings(ChangeSettingsRequestModel model)
            => await this.identity
                .ChangeSettingsAsync(model)
                .ToActionResult();

        [Authorize]
        [HttpPut(nameof(ChangePassword))]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequestModel model)
            => await this.identity
                .ChangePasswordAsync(new ChangePasswordRequestModel
                {
                    UserId = this.User.GetId(),
                    Password = model.Password,
                    NewPassword = model.NewPassword,
                    ConfirmNewPassword = model.ConfirmNewPassword
                })
                .ToActionResult();
    }
}
