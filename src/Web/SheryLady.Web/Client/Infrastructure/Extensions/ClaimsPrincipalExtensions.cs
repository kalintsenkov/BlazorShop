namespace SheryLady.Web.Client.Infrastructure.Extensions
{
    using System.Security.Claims;

    using Common;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public static bool IsAdministrator(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.IsInRole(GlobalConstants.AdminRoleName);
    }
}
