namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(
            string userName, 
            string email, 
            string password);

        Task<IdentityResult> ChangePassword(
            string userId, 
            string password, 
            string newPassword);

        Task<string> GenerateJwtAsync(
            string userId,
            string userName,
            string key,
            string issuer,
            string audience);
    }
}
