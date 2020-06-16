namespace BlazorShop.Web.Server
{
    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

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
                .AddAutoMapper(this.GetType())
                .AddApplicationServices()
                .AddApiControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseExceptionHandling(env)
                .UseHttpsRedirection()
                .UseBlazorFrameworkFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints()
                .SeedData();
    }
}
