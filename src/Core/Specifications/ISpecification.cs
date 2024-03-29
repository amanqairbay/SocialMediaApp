﻿using System;
using System.Linq.Expressions;

namespace Core.Specifications
{
    /// <summary>
    /// Represents the specification
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Criteria expression
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }

        /// <summary>
        /// Includes collection to load related data
        /// </summary>
        List<Expression<Func<T, object>>> Includes { get; }

        /// <summary>
        /// Order entities
        /// </summary>
        Expression<Func<T, object>> OrderBy { get; }

        // <summary>
        /// Order entities by descending
        /// </summary>
        Expression<Func<T, object>> OrderByDescending { get; }

        /// <summary>
        /// Takes only the required amount of data, set by page size.
        /// </summary>
        int Take { get; }

        /// <summary>
        /// Skips a certain set of records, by the page number * page size.
        /// </summary>
        int Skip { get; }

        /// <summary>
        /// A boolean value that determines whether paging is enabled for the scroll view.
        /// </summary>
        bool IsPagingEnabled { get; }
    }
}

