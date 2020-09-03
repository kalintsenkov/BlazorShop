namespace BlazorShop.Services.Specifications.Categories
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    internal class CategoryByIdSpecification : Specification<Category>
    {
        private readonly int id;

        public CategoryByIdSpecification(int id)
            => this.id = id;

        public override Expression<Func<Category, bool>> ToExpression()
            => category => category.Id == this.id;
    }
}
