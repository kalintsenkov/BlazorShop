namespace BlazorShop.Web.Client.Infrastructure
{
    using System.Threading.Tasks;

    using Web.Shared.Identity;

    public interface IAuthClient
    {
        Task<bool> LoginAsync(LoginRequestModel model);

        Task RegisterAsync(RegisterRequestModel model);

        Task LogoutAsync();
    }
}
