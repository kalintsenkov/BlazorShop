namespace SheryLady.Services.Identity
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public interface IIdentityService
    {
        Task<IdentityResult> CreateAsync(
            string firstName, 
            string lastName, 
            string userName, 
            string email, 
            string password);

        string GenerateJwtToken(string userId, string userName, string secret);
    }
}
