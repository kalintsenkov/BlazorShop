namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;
    using Common;
    using Data.Models;

    public interface IJwtGeneratorService : IService
    {
        Task<string> GenerateJwtAsync(BlazorShopUser user);
    }
}
