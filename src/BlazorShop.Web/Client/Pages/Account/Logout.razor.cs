namespace BlazorShop.Web.Client.Pages.Account
{
    using System.Threading.Tasks;

    public partial class Logout
    {
        private async Task Submit()
        {
            this.ToastService.ShowSuccess("You have successfully logged out.");
            await this.AuthService.Logout();
        }
    }
}
