namespace BlazorShop.Web.Server
{
    using System.Reflection;

    using AutoMapper;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Infrastructure.Extensions;
    using Middleware;

    public class Startup
    {
        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddDatabase(this.Configuration)
                .AddIdentity()
                .AddJwtAuthentication(services.GetApplicationSettings(this.Configuration))
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddApplicationServices()
                .AddApiControllers();

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
            => app
                .UseExceptionHandling(env)
                .UseValidationExceptionHandler()
                .UseHttpsRedirection()
                .UseBlazorFrameworkFiles()
                .UseStaticFiles()
                .UseRouting()
                .UseAuthentication()
                .UseAuthorization()
                .UseEndpoints()
                .Initialize();
    }
}
