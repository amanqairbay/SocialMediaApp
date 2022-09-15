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

        /// <summary>
        /// Order entities by descending
        /// </summary>
        public Expression<Func<T, object>> OrderByDescending { get; private set; } = default!;

        /// <summary>
        /// Takes only the required amount of data, set by page size
        /// </summary>
        public int Take { get; private set; }

        /// <summary>
        /// Skips a certain set of records, by the page number * page size
        /// </summary>
        public int Skip { get; private set; }

        /// <summary>
        /// A boolean value that determines whether paging is enabled for the scroll view
        /// </summary>
        public bool IsPagingEnabled { get; private set; }

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

        /// <summary>
        /// Adds descending ordering
        /// </summary>
        /// <param name="orderByExpression">A function orders entities by expression</param>
        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }

        /// <summary>
        /// Apply paging
        /// </summary>
        /// <param name="skip">Skips a certain set of records, by the page number * page size</param>
        /// <param name="take">Takes only the required amount of data, set by page size</param>
        protected void ApplyPaging(int skip, int take)
        {
            Skip = skip;
            Take = take;

            // determines whether paging is enabled for the scroll view
            IsPagingEnabled = true;
        }
    }
}

