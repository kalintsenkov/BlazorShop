namespace BlazorShop.Web.Server.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Identity;

    using static Data.ModelConstants.Identity;

    public static class IdentityOptionsExtensions
    {
        public static IdentityOptions SetIdentityOptions(this IdentityOptions options)
        {
            options.Password.RequiredLength = MinPasswordLength;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;

            return options;
        }
    }
}
