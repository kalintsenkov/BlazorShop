namespace BlazorShop.Services.Products.Specifications
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    internal class ProductByCategorySpecification : Specification<Product>
    {
        private readonly int? category;

        internal ProductByCategorySpecification(int? category)
            => this.category = category;

        protected override bool Include => this.category != null;

        public override Expression<Func<Product, bool>> ToExpression()
            => product => product.Category.Id == this.category;
    }
}