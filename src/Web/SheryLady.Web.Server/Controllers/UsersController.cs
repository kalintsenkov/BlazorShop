namespace SheryLady.Web.Server.Controllers
{
    using System.Threading.Tasks;

    using Data.Models;
    using Models.Users;
    using Services;
    using Services.Users;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Options;

    public class UsersController : ApiController
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IUsersService usersService;
        private readonly AppSettings appSettings;

        public UsersController(
            UserManager<ApplicationUser> userManager,
            IDateTimeProvider dateTimeProvider,
            IUsersService usersService,
            IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.dateTimeProvider = dateTimeProvider;
            this.usersService = usersService;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route(nameof(Register))]
        public async Task<ActionResult> Register(UsersRegisterRequestModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                CreatedOn = this.dateTimeProvider.Now()
            };

            var result = await this.userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                return this.Ok();
            }

            return this.BadRequest(result.Errors);
        }

        [HttpPost]
        [Route(nameof(Login))]
        public async Task<ActionResult<UsersLoginResponseModel>> Login(UsersLoginRequestModel model)
        {
            var user = await this.userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return this.Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordValid)
            {
                return this.Unauthorized();
            }

            var token = this.usersService.GenerateJwtToken(
                user.Id,
                user.UserName,
                this.appSettings.Secret);

            return new UsersLoginResponseModel
            {
                Token = token
            };
        }
    }
}