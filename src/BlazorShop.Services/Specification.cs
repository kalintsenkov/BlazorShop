namespace BlazorShop.Services
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    internal abstract class Specification<T>
        where T : class
    {
        private static readonly ConcurrentDictionary<string, Func<T, bool>> DelegateCache
            = new ConcurrentDictionary<string, Func<T, bool>>();

        private readonly List<string> cacheKey;

        protected Specification()
            => this.cacheKey = new List<string> { this.GetType().Name };

        protected virtual bool Include => true;

        public virtual bool IsSatisfiedBy(T value)
        {
            if (!this.Include)
            {
                return true;
            }

            var func = DelegateCache.GetOrAdd(
                string.Join(string.Empty, this.cacheKey),
                _ => this.ToExpression().Compile());

            return func(value);
        }

        public Specification<T> And(Specification<T> specification)
        {
            if (!specification.Include)
            {
                return this;
            }

            this.cacheKey.Add($"{nameof(this.And)}{specification.GetType()}");

            return new BinarySpecification(this, specification, true);
        }

        public Specification<T> Or(Specification<T> specification)
        {
            if (!specification.Include)
            {
                return this;
            }

            this.cacheKey.Add($"{nameof(this.Or)}{specification.GetType()}");

            return new BinarySpecification(this, specification, false);
        }

        public abstract Expression<Func<T, bool>> ToExpression();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
            => specification.Include
                ? specification.ToExpression()
                : value => true;

        private class BinarySpecification : Specification<T>
        {
            private readonly Specification<T> left;
            private readonly Specification<T> right;
            private readonly bool andOperator;

            public BinarySpecification(Specification<T> left, Specification<T> right, bool andOperator)
            {
                this.right = right;
                this.left = left;
                this.andOperator = andOperator;
            }

            public override Expression<Func<T, bool>> ToExpression()
            {
                Expression<Func<T, bool>> leftExpression = this.left;
                Expression<Func<T, bool>> rightExpression = this.right;

                Expression body = this.andOperator
                    ? Expression.AndAlso(leftExpression.Body, rightExpression.Body)
                    : Expression.OrElse(leftExpression.Body, rightExpression.Body);

                var parameter = Expression.Parameter(typeof(T));
                body = (BinaryExpression)new ParameterReplacer(parameter).Visit(body);

                body = body ?? throw new InvalidOperationException("Binary expression cannot be null.");

                return Expression.Lambda<Func<T, bool>>(body, parameter);
            }
        }

        private class ParameterReplacer : ExpressionVisitor
        {
            private readonly ParameterExpression parameter;

            protected override Expression VisitParameter(ParameterExpression node)
                => base.VisitParameter(this.parameter);

            internal ParameterReplacer(ParameterExpression parameter)
                => this.parameter = parameter;
        }
    }
}
