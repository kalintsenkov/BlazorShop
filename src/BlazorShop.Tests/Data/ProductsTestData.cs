namespace BlazorShop.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BlazorShop.Data.Models;

    public static class ProductsTestData
    {
        public static List<Product> GetProducts(int count)
            => Enumerable
                .Range(1, count)
                .Select(i => new Product
                {
                    Name = $"Product {i}",
                    Description = $"Description {i}",
                    ImageSource = $"Image {i}",
                    Quantity = i,
                    Price = i + 0.5m,
                    CategoryId = i
                })
                .ToList();
    }
}
