namespace BlazorShop.Web.Server.Pages
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [ResponseCache(
        Duration = 0, 
        Location = ResponseCacheLocation.None, 
        NoStore = true)]
    public class ErrorModel : PageModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet() => this.RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
    }
}
