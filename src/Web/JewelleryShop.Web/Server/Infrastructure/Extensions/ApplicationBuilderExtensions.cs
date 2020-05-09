namespace JewelleryShop.Web.Server.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Data;
    using Data.Seeding;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();
            using var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();

            dbContext.Database.Migrate();

            return app;
        }

        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();
            using var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            new ApplicationDbContextSeeder()
                .SeedAsync(dbContext, serviceScope.ServiceProvider)
                .GetAwaiter()
                .GetResult();

            return app;
        }
    }
}
