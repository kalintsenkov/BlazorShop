namespace BlazorShop.Services.Specifications.Addresses
{
    using System;
    using System.Linq.Expressions;

    using Data.Models;

    internal class AddressByIdSpecification : Specification<Address>
    {
        private readonly int id;

        internal AddressByIdSpecification(int id)
            => this.id = id;

        public override Expression<Func<Address, bool>> ToExpression()
            => address => address.Id == this.id;
    }
}
