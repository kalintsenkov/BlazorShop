namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;

    using Data.Models;

    public interface IJwtGeneratorService
    {
        Task<string> GenerateJwtAsync(ApplicationUser user);
    }
}
