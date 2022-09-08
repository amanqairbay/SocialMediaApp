using System;
using Core.Entities;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Represents the entity specification evaluator
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        /// <summary>
        /// Implements an extension for entity framework within applying interface.
        /// /// </summary>
        /// <param name="inputQuery">Input query to get entry</param>
        /// <param name="specification">Specification to get entry</param>
        /// <returns>The static method result includes all expression-based includes</returns>
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria is not null)
                query = query.Where(specification.Criteria);

            // applies the order if expressions are specified
            if (specification.OrderBy is not null)
                query = query.OrderBy(specification.OrderBy);

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}

