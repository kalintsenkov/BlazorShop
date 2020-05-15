namespace BlazorShop.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    using Data.Models;
    using Infrastructure.Extensions;
    using Services.Identity;
    using Shared.Identity;

    public class IdentityController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IIdentityService identityService;
        private readonly IConfiguration configuration;

        public IdentityController(
            UserManager<ApplicationUser> userManager,
            IIdentityService identityService,
            IConfiguration configuration)
        {
            this.userManager = userManager;
            this.identityService = identityService;
            this.configuration = configuration;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var result = await this.identityService.CreateAsync(
                model.Username, 
                model.Email, 
                model.Password);

            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors);
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<LoginResponseModel>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return this.Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return this.Unauthorized();
            }

            var token = await this.identityService.GenerateJwtAsync(
                user.Id,
                user.UserName,
                this.configuration.GetJwtKey(),
                this.configuration.GetJwtIssuer(),
                this.configuration.GetJwtAudience());

            return new LoginResponseModel { Token = token };
        }
    }
}