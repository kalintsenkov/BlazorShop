namespace BlazorShop.Services.Identity
{
    using System.Threading.Tasks;

    public interface IIdentityService
    {
        Task<string> GenerateJwtAsync(string userId, string userName, string secret);
    }
}
