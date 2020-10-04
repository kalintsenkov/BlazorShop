namespace BlazorShop.Web.Client.Shared.Common
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Components;

    public partial class ErrorsList
    {
        [Parameter]
        public bool ShowErrors { get; set; }

        [Parameter]
        public IEnumerable<string> Errors { get; set; }

        private void Reset() => this.ShowErrors = !this.ShowErrors;
    }
}
