using System;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    /// <summary>
    /// Represents the entity repository
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Get the entity entry
        /// </summary>
        /// <param name="id">Entity entry identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry
        /// </returns>
        Task<T?> GetByIdAsync(long id);

        /// <summary>
        /// Get all entity entries
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        Task<IReadOnlyList<T>> GetAllAsync();

        /// <summary>
        /// Get entity entry with specification
        /// </summary>
        /// <param name="specification">Specification to get entry</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry with specification
        /// </returns>
        Task<T?> GetEntityWithSpecification(ISpecification<T> specification);

        /// <summary>
        /// Get a list entity entries with specification
        /// </summary>
        /// <param name="specification">Specification to get entries</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of entity entries with specification
        /// </returns>
        Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification);

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <param name="entity">Entity type</param>
        void Delete(T entity);

        /// <summary>
        /// Saves all entities
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains boolean value
        /// </returns>
        Task<bool> SaveAll();

        Task<int> CountAsync(ISpecification<T> specification);
    }
}

