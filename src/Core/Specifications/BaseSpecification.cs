using System;
using System.Linq.Expressions;

namespace Core.Specifications
{
    /// <summary>
    /// Represents base specification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseSpecification<T> : ISpecification<T>
    {
        /// <summary>
        /// Criteria for obtaining data
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; } = default!;

        /// <summary>
        /// Includes collection to load related data
        /// </summary>
        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        /// <summary>
        /// Order entities
        /// </summary>
        public Expression<Func<T, object>> OrderBy { get; private set; } = default!;

        #region Constructors

        protected BaseSpecification()
        {

        }

        protected BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        #endregion Constructors

        /// <summary>
        /// Adds includes
        /// </summary>
        /// <param name="includeExpression">A function to include entities by expression</param>
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        /// <summary>
        /// Adds order
        /// </summary>
        /// <param name="orderByExpression">A function orders entities by expression</param>
        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
    }
}

