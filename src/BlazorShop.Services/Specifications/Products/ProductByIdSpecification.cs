namespace BlazorShop.Services.Specifications.Products
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    internal class ProductByIdSpecification : Specification<Product>
    {
        private readonly int id;

        internal ProductByIdSpecification(int id)
            => this.id = id;

        public override Expression<Func<Product, bool>> ToExpression()
            => product => product.Id == this.id;
    }
}
