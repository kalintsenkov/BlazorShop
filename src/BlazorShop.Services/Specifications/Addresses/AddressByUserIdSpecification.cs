namespace BlazorShop.Services.Specifications.Addresses
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    internal class AddressByUserIdSpecification : Specification<Address>
    {
        private readonly string userId;

        internal AddressByUserIdSpecification(string userId)
            => this.userId = userId;

        public override Expression<Func<Address, bool>> ToExpression()
            => address => address.UserId == this.userId;
    }
}
