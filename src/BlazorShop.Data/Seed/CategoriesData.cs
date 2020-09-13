namespace BlazorShop.Data.Seed
{
    using System;
    using System.Collections.Generic;

    using Contracts;
    using Models;

    public class CategoriesData : IInitialData
    {
        public Type EntityType => typeof(Category);

        public IEnumerable<object> GetData()
            => new List<Category>
            {
                new Category { Name = "Fashion" },
                new Category { Name = "Electronics" },
                new Category { Name = "Books, Movies & Music" }
            };
    }
}
