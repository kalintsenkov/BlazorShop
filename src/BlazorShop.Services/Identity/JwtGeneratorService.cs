namespace BlazorShop.Services.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.Extensions.Options;
    using Microsoft.IdentityModel.Tokens;

    using Data.Models;
    using Models;

    using static BlazorShop.Common.Constants;

    public class JwtGeneratorService : IJwtGeneratorService
    {
        private readonly UserManager<BlazorShopUser> userManager;
        private readonly ApplicationSettings applicationSettings;

        public JwtGeneratorService(
            UserManager<BlazorShopUser> userManager,
            IOptions<ApplicationSettings> applicationSettings)
        {
            this.userManager = userManager;
            this.applicationSettings = applicationSettings.Value;
        }

        public async Task<string> GenerateJwtAsync(BlazorShopUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName)
            };

            var isAdministrator = await this.userManager.IsInRoleAsync(user, AdministratorRole);

            if (isAdministrator)
            {
                claims.Add(new Claim(ClaimTypes.Role, AdministratorRole));
            }

            var secret = Encoding.UTF8.GetBytes(this.applicationSettings.Secret);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(secret),
                    SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
