namespace BlazorShop.Web.Client.Clients
{
    using System.Threading.Tasks;

    using BlazorShop.Shared.Models.Identity;

    public interface IAuthClient
    {
        Task<bool> LoginAsync(LoginRequestModel model);

        Task RegisterAsync(RegisterRequestModel model);

        Task LogoutAsync();
    }
}
