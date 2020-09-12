namespace BlazorShop.Services.Products.Specifications
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    internal class ProductByNameSpecification : Specification<Product>
    {
        private readonly string name;

        internal ProductByNameSpecification(string name) => this.name = name;

        protected override bool Include => this.name != null;

        public override Expression<Func<Product, bool>> ToExpression()
            => product => product.Name.ToLower().Contains(this.name.ToLower());
    }
}
