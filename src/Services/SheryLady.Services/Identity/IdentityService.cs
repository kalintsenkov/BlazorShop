namespace SheryLady.Services.Identity
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.IdentityModel.Tokens;

    using Data.Models;

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

        public async Task<IdentityResult> Create(
            string firstName, 
            string lastName, 
            string userName, 
            string email, 
            string password)
        {
            var user = new ApplicationUser
            {
                FirstName = firstName,
                LastName = lastName,
                Email = userName,
                UserName = email,
                CreatedOn = this.dateTimeProvider.Now()
            };

            return await this.userManager.CreateAsync(user, password);
        }

        public string GenerateJwtToken(string userId, string userName, string secret)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), 
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }
    }
}
