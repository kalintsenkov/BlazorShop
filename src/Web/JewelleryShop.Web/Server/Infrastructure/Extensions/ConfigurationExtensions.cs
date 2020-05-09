namespace JewelleryShop.Web.Server.Infrastructure.Extensions
{
    using Microsoft.Extensions.Configuration;

    public static class ConfigurationExtensions
    {
        public static string GetDefaultConnectionString(this IConfiguration configuration)
            => configuration.GetConnectionString("DefaultConnection");

        public static string GetJwtKey(this IConfiguration configuration)
            => configuration["Jwt:Key"];

        public static string GetJwtIssuer(this IConfiguration configuration)
            => configuration["Jwt:Issuer"];

        public static string GetJwtAudience(this IConfiguration configuration)
            => configuration["Jwt:Audience"];
    }
}
