namespace BlazorShop.Web.Server.Infrastructure.Extensions
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
    using Models;
    using Services.Addresses;
    using Services.Categories;
    using Services.Identity;
    using Services.Orders;
    using Services.Products;
    using Services.Search;
    using Services.ShoppingCart;
    using Services.Wishlist;

    using static Data.ModelConstants.Identity;

    public static class ServiceCollectionExtensions
    {
        public static ApplicationSettings GetApplicationSettings(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var applicationSettingsConfiguration = configuration.GetSection(nameof(ApplicationSettings));
            services.Configure<ApplicationSettings>(applicationSettingsConfiguration);
            return applicationSettingsConfiguration.Get<ApplicationSettings>();
        }

        public static IServiceCollection AddDatabase(
            this IServiceCollection services,
            IConfiguration configuration)
            => services
                .AddDbContext<ApplicationDbContext>(options => options
                    .UseSqlServer(configuration.GetDefaultConnectionString()));

        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<ApplicationUser, ApplicationRole>(options =>
                {
                    options.Password.RequiredLength = MinPasswordLength;
                    options.Password.RequireDigit = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = false;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            ApplicationSettings applicationSettings)
        {
            var key = Encoding.ASCII.GetBytes(applicationSettings.Secret);

            services
                .AddAuthentication(authentication =>
                {
                    authentication.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    authentication.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearer =>
                {
                    bearer.RequireHttpsMetadata = false;
                    bearer.SaveToken = true;
                    bearer.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
            => services
                .AddTransient<IAddressesService, AddressesService>()
                .AddTransient<ICategoriesService, CategoriesService>()
                .AddTransient<IIdentityService, IdentityService>()
                .AddTransient<IJwtGeneratorService, JwtGeneratorService>()
                .AddTransient<IOrdersService, OrdersService>()
                .AddTransient<IProductsService, ProductsService>()
                .AddTransient<ISearchService, SearchService>()
                .AddTransient<IShoppingCartService, ShoppingCartService>()
                .AddTransient<IWishlistService, WishlistService>();

        public static IServiceCollection AddApiControllers(this IServiceCollection services)
        {
            services
                .AddControllers(options => options
                      .Filters
                      .Add<ModelOrNotFoundActionFilter>());

            services.AddRazorPages();

            return services;
        }
    }
}
