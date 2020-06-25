namespace BlazorShop.Web.Client.Clients
{
    using System.Net.Http;
    using System.Threading.Tasks;

    using BlazorShop.Shared.Models.Identity;

    public interface IAuthClient
    {
        Task<bool> LoginAsync(LoginRequestModel model);

        Task<HttpResponseMessage> RegisterAsync(RegisterRequestModel model);

        Task LogoutAsync();
    }
}
