namespace BlazorShop.Services.Identity
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;

    using Data.Models;
    using DateTime;

    using static Common.GlobalConstants;

    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDateTimeProvider dateTimeProvider;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            IDateTimeProvider dateTimeProvider)
        {
            this.userManager = userManager;
            this.dateTimeProvider = dateTimeProvider;
        }

        public async Task<IdentityResult> CreateAsync(
            string userName,
            string email,
            string password)
        {
            var user = new ApplicationUser
            {
                Email = email,
                UserName = userName,
                CreatedOn = this.dateTimeProvider.Now()
            };

            return await this.userManager.CreateAsync(user, password);
        }

        public async Task<IdentityResult> ChangePassword(
            string userId,
            string password,
            string newPassword)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            return await this.userManager.ChangePasswordAsync(user, password, newPassword);
        }

        public async Task<string> GenerateJwtAsync(
            string userId,
            string userName,
            string key,
            string issuer,
            string audience)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, userName)
            };

            var isInAdminRole = await this.IsInAdminRole(userId);
            if (isInAdminRole)
            {
                claims.Add(new Claim(ClaimTypes.Role, AdminRoleName));
            }

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: new SigningCredentials(
                    securityKey,
                    SecurityAlgorithms.HmacSha256));

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(token);
        }

        private async Task<bool> IsInAdminRole(string userId)
        {
            var user = await this.userManager.FindByIdAsync(userId);

            return await this.userManager.IsInRoleAsync(user, AdminRoleName);
        }
    }
}
