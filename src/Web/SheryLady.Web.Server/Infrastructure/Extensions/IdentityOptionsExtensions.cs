namespace SheryLady.Web.Server.Infrastructure.Extensions
{
    using static Common.ModelConstants;

    using Microsoft.AspNetCore.Identity;

    public static class IdentityOptionsExtensions
    {
        public static IdentityOptions SetIdentityOptions(this IdentityOptions options)
        {
            options.Password.RequiredLength = UserPasswordMinLength;
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;

            return options;
        }
    }
}
