namespace BlazorShop.Services.Products.Specifications
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    internal class ProductByPriceSpecification : Specification<Product>
    {
        private readonly decimal minPrice;
        private readonly decimal maxPrice;

        internal ProductByPriceSpecification(
            decimal? minPrice = default,
            decimal? maxPrice = decimal.MaxValue)
        {
            this.minPrice = minPrice ?? default;
            this.maxPrice = maxPrice ?? decimal.MaxValue;
        }

        public override Expression<Func<Product, bool>> ToExpression()
            => product => this.minPrice < product.Price && this.maxPrice > product.Price;
    }
}
