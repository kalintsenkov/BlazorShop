namespace SheryLady.Web.Server.Infrastructure.Extensions
{
    using System.Text;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    using Data;
    using Data.Models;
    using Filters;
    using Services.Categories;
    using Services.DateTime;
    using Services.Identity;
    using Services.Products;

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options => options
                    .SetIdentityOptions())
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var key = Encoding.UTF8.GetBytes(configuration.GetJwtKey());
            var issuer = configuration.GetJwtIssuer();
            var audience = configuration.GetJwtAudience();

            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = issuer,
                        ValidAudience = audience,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IProductsService, ProductsService>()
                .AddTransient<ICategoriesService, CategoriesService>()
                .AddTransient<IDateTimeProvider, DateTimeProvider>();

        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options => options
                      .Filters
                      .Add<ModelOrNotFoundActionFilter>());

            return services;
        }
    }
}
