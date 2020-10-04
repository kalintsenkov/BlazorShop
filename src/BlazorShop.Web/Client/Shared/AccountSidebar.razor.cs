namespace BlazorShop.Web.Client.Shared
{
    using System.Threading.Tasks;

    public partial class AccountSidebar
    {
        private async Task Submit()
        {
            this.ToastService.ShowSuccess("You have successfully logged out.");
            await this.AuthService.Logout();
        }
    }
}
