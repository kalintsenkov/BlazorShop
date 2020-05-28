namespace BlazorShop.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Models;

    internal class CategoriesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Categories.AnyAsync())
            {
                return;
            }

            var categories = new List<Category>
            {
                new Category { Name = "Men", CreatedOn = DateTime.UtcNow },
                new Category { Name = "Women", CreatedOn = DateTime.UtcNow },
                new Category { Name = "Kids", CreatedOn = DateTime.UtcNow }
            };

            await dbContext.Categories.AddRangeAsync(categories);
        }
    }
}
