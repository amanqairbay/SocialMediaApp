using System;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    /// <summary>
    /// Represents the entity repository implementation
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Public Methods

        /// <summary>
        /// Gets the entity entry by identifier
        /// </summary>
        /// <param name="id">Entity entry identifier</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry
        /// </returns>
        public async Task<T?> GetByIdAsync(long id) => await _context.Set<T>().FindAsync(id);

        /// <summary>
        /// Gets all entity entries
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entries
        /// </returns>
        public async Task<IReadOnlyList<T>> GetAllAsync() => await _context.Set<T>().ToListAsync();

        /// <summary>
        /// Gets entity entries with specification
        /// </summary>
        /// <param name="specification">Specification to get entry</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the entity entry with specification
        /// </returns>
        public async Task<T?> GetEntityWithSpecification(ISpecification<T> specification) =>
            await ApplySpecification(specification).FirstOrDefaultAsync();

        /// <summary>
        /// Gets list of entity entries with specification
        /// </summary>
        /// <param name="specification">Specification to get entries</param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the list of entity entries with specification
        /// </returns>
        public async Task<IReadOnlyList<T>> GetAllWithSpecificationAsync(ISpecification<T> specification) =>
            await ApplySpecification(specification).ToListAsync();

        /// <summary>
        /// Adds the entity
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity) => _context.Set<T>().Add(entity);

        /// <summary>
        /// Deletes the entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity) => _context.Remove(entity);

        /// <summary>
        /// Saves all entities
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains boolean value
        /// </returns>
        public async Task<bool> SaveAll() => await _context.SaveChangesAsync() > 0;

        /// <summary>
        /// Gets number of entities
        /// </summary>
        /// <param name="specification"></param>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result number of entity entries with specification
        /// </returns>
        public async Task<int> CountAsync(ISpecification<T> specification) =>
            await ApplySpecification(specification).CountAsync();

        #endregion Public Methods

        #region Private Methods

        /// <summary>
        /// Applies the specification to implement an extension for the entity framework in the interface application
        /// </summary>
        /// <param name="specification">Specification to get entries</param>
        /// <returns>The result includes all expression-based includes</returns>
        private IQueryable<T> ApplySpecification(ISpecification<T> specification) =>
            SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specification);

        #endregion Private Methods 
    }
}

