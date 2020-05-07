namespace SheryLady.Web.Client.Infrastructure.Clients
{
    using System.Threading.Tasks;

    using Web.Shared.Identity;

    public interface IUsersClient
    {
        Task<bool> LoginAsync(LoginRequestModel model);

        Task LogoutAsync();
    }
}
