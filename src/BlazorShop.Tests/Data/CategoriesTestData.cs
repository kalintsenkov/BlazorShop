namespace BlazorShop.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using BlazorShop.Data.Models;

    public static class CategoriesTestData
    {
        public static List<Category> GetCategories(int count)
            => Enumerable
                .Range(1, count)
                .Select(i => new Category
                {
                    Id = i,
                    Name = $"Category {i}"
                })
                .ToList();
    }
}
