namespace SheryLady.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    using Common;
    using Models;

    internal class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();

            var isExisting = await userManager.Users.AnyAsync(u => u.UserName == GlobalConstants.AdminRoleName);
            if (!isExisting)
            {
                var admin = new ApplicationUser
                {
                    FirstName = GlobalConstants.AdminRoleName,
                    LastName = GlobalConstants.AdminRoleName,
                    UserName = GlobalConstants.AdminRoleName,
                    Email = GlobalConstants.AdminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(admin, GlobalConstants.AdminPassword);
                if (!result.Succeeded)
                {
                    throw new Exception(string.Join(Environment.NewLine, result.Errors.Select(e => e.Description)));
                }

                var isRoleExists = await roleManager.RoleExistsAsync(GlobalConstants.AdminRoleName);
                if (isRoleExists)
                {
                    await userManager.AddToRoleAsync(admin, GlobalConstants.AdminRoleName);
                }
            }
        }
    }
}
