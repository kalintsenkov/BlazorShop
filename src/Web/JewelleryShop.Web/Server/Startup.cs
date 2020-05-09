namespace JewelleryShop.Web.Server
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Infrastructure.Extensions;

    public class Startup
    {
        public Startup(IConfiguration configuration) => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) 
            => services
                .AddDatabase(this.Configuration)
                .AddIdentity()
                .AddJwtAuthentication(this.Configuration)
                .AddAutoMapper(typeof(JewelleryShopProfile).Assembly)
                .AddApplicationServices()
                .AddApiControllers()
                .AddRazorPages();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app
                .UseHttpsRedirection()
                .UseBlazorFrameworkFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapRazorPages();
                    endpoints.MapControllers();
                    endpoints.MapFallbackToFile("index.html");
                })
                .ApplyMigrations()
                .SeedData();
        }
    }
}
