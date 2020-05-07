namespace SheryLady.Services.Server.Identity
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(string userName, string email, string password);

        Task<string> GenerateJwtToken(string userId, string userName, string key, string issuer, string audience);
    }
}
