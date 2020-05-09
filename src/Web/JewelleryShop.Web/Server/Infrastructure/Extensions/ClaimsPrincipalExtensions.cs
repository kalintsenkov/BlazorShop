namespace JewelleryShop.Web.Server.Infrastructure.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
            => claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
