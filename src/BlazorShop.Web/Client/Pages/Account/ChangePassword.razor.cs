namespace BlazorShop.Web.Client.Pages.Account
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Models.Identity;

    public partial class ChangePassword
    {
        private readonly ChangePasswordRequestModel model = new ChangePasswordRequestModel();

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        private async Task SubmitAsync()
        {
            var response = await this.Http.PutAsJsonAsync("api/identity/changepassword", this.model);

            if (response.IsSuccessStatusCode)
            {
                this.ShowErrors = false;

                this.model.Password = null;
                this.model.NewPassword = null;
                this.model.ConfirmNewPassword = null;

                await this.AuthService.Logout();

                this.ToastService.ShowSuccess("Your password has been changed successfully.\n Please login.");
                this.NavigationManager.NavigateTo("/account/login");
            }
            else
            {
                this.Errors = await response.Content.ReadFromJsonAsync<string[]>();
                this.ShowErrors = true;
            }
        }
    }
}
