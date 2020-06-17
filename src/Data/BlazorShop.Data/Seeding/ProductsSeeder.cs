namespace BlazorShop.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.EntityFrameworkCore;

    using Models;

    internal class ProductsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (await dbContext.Products.AnyAsync())
            {
                return;
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
                    CategoryId = 1,
                    CreatedOn = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Super Hero Marvel Sweatshirt",
                    Description = "The Super Hero Marvel Sweatshirt is made from soft cotton.",
                    ImageSource = "https://cdn11.bigcommerce.com/s-pkla4xn3/images/stencil/1280x1280/products/7404/67088/New-Super-Hero-Marvel-Sweatshirts-Fashion-Cotton-Men-Hoodies-Marvel-Cool-Printed-Sweatshirts-Men-Clothing-Free__23706.1527569968.jpg?c=2&imbypass=on",
                    Price = 50,
                    Quantity = 30,
                    CategoryId = 1,
                    CreatedOn = DateTime.UtcNow
                },
                new Product
                {
                    Name = "Rip Curl Boys' Mirage Phase Boardwalk Shorts",
                    Description = "The Rip Curl Boys' Mirage Phase Boardwalk Shorts are soft and comfortable making walking for hours a breeze.",
                    ImageSource = "https://photos.cdn-outlet.com/photos/options/8172804-48426-1A-zoomin.jpg",
                    Price = 37.99m,
                    Quantity = 10,
                    CategoryId = 3,
                    CreatedOn = DateTime.UtcNow
                }
            };

            await dbContext.Products.AddRangeAsync(products);
        }
    }
}
