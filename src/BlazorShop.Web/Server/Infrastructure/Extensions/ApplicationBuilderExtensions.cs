namespace BlazorShop.Web.Server.Infrastructure.Extensions
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    using Data;
    using Data.Models;

    using static Shared.Constants;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
            => app.SeedDataAsync().GetAwaiter().GetResult();

        public static IApplicationBuilder UseExceptionHandling(
            this IApplicationBuilder app,
            IWebHostEnvironment env)
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

            return app;
        }

        public static IApplicationBuilder UseEndpoints(this IApplicationBuilder app)
            => app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });

        public static async Task<IApplicationBuilder> SeedDataAsync(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.CreateScope();

            var services = serviceScope.ServiceProvider;
            var dbContext = services.GetService<ApplicationDbContext>();

            await dbContext.Database.MigrateAsync();

            var roleManager = services.GetService<RoleManager<ApplicationRole>>();
            var existingRole = await roleManager.FindByNameAsync(AdminRoleName);
            if (existingRole != null)
            {
                return app;
            }

            var adminRole = new ApplicationRole(AdminRoleName);

            await roleManager.CreateAsync(adminRole);

            var adminUser = new ApplicationUser
            {
                Email = "admin@blazorshop.com",
                UserName = "admin@blazorshop.com",
                SecurityStamp = "RandomSecurityStamp"
            };

            var userManager = services.GetService<UserManager<ApplicationUser>>();

            await userManager.CreateAsync(adminUser, "admin123456");
            await userManager.AddToRoleAsync(adminUser, AdminRoleName);

            if (await dbContext.Categories.AnyAsync())
            {
                return app;
            }

            var categories = new List<Category>
            {
                new Category { Name = "Men" },
                new Category { Name = "Women" },
                new Category { Name = "Kids" }
            };

            await dbContext.Categories.AddRangeAsync(categories);

            if (await dbContext.Products.AnyAsync())
            {
                return app;
            }

            var products = new List<Product>
            {
                new Product
                {
                    Name = "Cool T-Shirt",
                    Description = "The Cool T-Shirt is made from soft cotton and features a clean print.",
                    ImageSource = "https://gorilla.bg/userfiles/productlargeimages/product_256.jpg",
                    Price = 19.99m,
                    Quantity = 10,
                    CategoryId = 1
                },
                new Product
                {
                    Name = "Super Hero Marvel Sweatshirt",
                    Description = "The Super Hero Marvel Sweatshirt is made from soft cotton.",
                    ImageSource = "https://cdn11.bigcommerce.com/s-pkla4xn3/images/stencil/1280x1280/products/7404/67088/New-Super-Hero-Marvel-Sweatshirts-Fashion-Cotton-Men-Hoodies-Marvel-Cool-Printed-Sweatshirts-Men-Clothing-Free__23706.1527569968.jpg?c=2&imbypass=on",
                    Price = 50,
                    Quantity = 30,
                    CategoryId = 1
                },
                new Product
                {
                    Name = "Boardwalk Shorts",
                    Description = "The Boardwalk Shorts are soft and comfortable making walking for hours a breeze.",
                    ImageSource = "https://photos.cdn-outlet.com/photos/options/8172804-48426-1A-zoomin.jpg",
                    Price = 37.99m,
                    Quantity = 10,
                    CategoryId = 3
                }
            };

            await dbContext.Products.AddRangeAsync(products);

            await dbContext.SaveChangesAsync();

            return app;
        }
    }
}
