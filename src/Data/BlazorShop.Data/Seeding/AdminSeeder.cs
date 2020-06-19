namespace BlazorShop.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Models;

    internal class AdminSeeder : ISeeder
    {
        private const string AdminName = "Admin";
        private const string AdminEmail = "admin@blazorshop.bg";
        private const string AdminPassword = "admin123456";
        private const string AdminRoleName = "Admin";

        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();

            var isExisting = await userManager.Users.AnyAsync(u => u.UserName == AdminName);
            if (!isExisting)
            {
                var admin = new ApplicationUser
                {
                    FirstName = AdminName,
                    LastName = AdminName,
                    UserName = AdminName,
                    Email = AdminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, AdminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                var isRoleExists = await roleManager.RoleExistsAsync(AdminRoleName);
                if (isRoleExists)
                {
                    await userManager.AddToRoleAsync(admin, AdminRoleName);
                }
            }
        }
    }
}
