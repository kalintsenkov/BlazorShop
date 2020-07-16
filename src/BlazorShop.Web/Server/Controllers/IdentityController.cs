namespace BlazorShop.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using Infrastructure.Extensions;
    using Models;
    using Models.Identity;
    using Services.Identity;

    public class IdentityController : ApiController
    {
        private readonly IIdentityService identityService;

        public IdentityController(IIdentityService identityService)
            => this.identityService = identityService;

        [HttpPost(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var result = await this.identityService.RegisterAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok();
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var result = await this.identityService.LoginAsync(model);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return result.Data;
        }

        [HttpPut(nameof(ChangePassword))]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordRequestModel model)
            => await this
                .identityService
                .ChangePasswordAsync(new ChangePasswordRequestModel
                {
                    UserId = this.User.GetId(),
                    Password = model.Password,
                    NewPassword = model.NewPassword
                })
                .ToActionResult();
    }
}