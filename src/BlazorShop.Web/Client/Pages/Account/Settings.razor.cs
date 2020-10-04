namespace BlazorShop.Web.Client.Pages.Account
{
    using System.Collections.Generic;
    using System.Net.Http.Json;
    using System.Threading.Tasks;

    using Infrastructure.Extensions;
    using Models.Identity;

    public partial class Settings
    {
        private readonly ChangeSettingsRequestModel model = new ChangeSettingsRequestModel();

        private string email;

        public bool ShowErrors { get; set; }

        public IEnumerable<string> Errors { get; set; }

        protected override async Task OnInitializedAsync() => await this.LoadDataAsync();

        private async Task SubmitAsync()
        {
            var response = await this.Http.PutAsJsonAsync("api/identity/changesettings", this.model);

            if (response.IsSuccessStatusCode)
            {
                this.ShowErrors = false;

                await this.AuthService.Logout();

                this.ToastService.ShowSuccess("Your account settings has been changed successfully.\n Please login.");
                this.NavigationManager.NavigateTo("/account/login");
            }
            else
            {
                this.Errors = await response.Content.ReadFromJsonAsync<string[]>();
                this.ShowErrors = true;
            }
        }

        private async Task LoadDataAsync()
        {
            var state = await this.AuthState.GetAuthenticationStateAsync();
            var user = state.User;

            this.email = user.GetEmail();
            this.model.FirstName = user.GetFirstName();
            this.model.LastName = user.GetLastName();
        }
    }
}
