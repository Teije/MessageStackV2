using MessageStack.Context;
using MessageStack.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MessageStack.Repositories
{
    /// <summary>
    /// Base Repository to define basic functionality for all repositories
    /// </summary>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Adds an object to the database table
        /// </summary>
        /// <param name="entity">The object to be add to the database table</param>
        /// <returns>The object that has been added to the database table</returns>
        Task<T> Add(T entity);

        /// <summary>
        /// Gets the first or default object based on the predicate
        /// </summary>
        /// <param name="predicate">The condition(s) that the object must meet</param>
        /// <returns>Returns the first object that meets the specified condition(s)</returns>
        Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get all objects from the database table
        /// </summary>
        /// <returns>An enumerator containing all objects from the database table</returns>
        Task<IEnumerable<T>> GetAll();

        /// <summary>
        /// Get an object by Id
        /// </summary>
        /// <param name="id">The Id of the object</param>
        /// <returns>Returns the object with the specified Id</returns>
        Task<T> GetById(Guid id);

        /// <summary>
        /// Get all objects from the database table based on the specified condition
        /// </summary>
        /// <param name="predicate">The condition(s) that the object(s) must meet</param>
        /// <returns>An enumerator containing all objects, that meet the condition specified, from the database table</returns>
        Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Remove an object from the database table
        /// </summary>
        /// <param name="id">The id of the object to be removed from the database table</param>
        /// <returns>The object that has been removed from the database table</returns>
        Task<T> Remove(Guid id);

        /// <summary>
        /// Updates an object in the database table
        /// </summary>
        /// <param name="entity">The object to be updated to the database table</param>
        /// <returns>The object that has been updated to the database table</returns>
        Task<T> Update(T entity);
    }

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected MessageStackContext _databaseContext = null;

        protected BaseRepository(MessageStackContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region CRUD Methods

        public async Task<T> Add(T entity)
        {
            _databaseContext.Set<T>().Add(entity);
            await _databaseContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> FirstOrDefault(Expression<Func<T, bool>> predicate) => await _databaseContext.Set<T>().FirstOrDefaultAsync(predicate);

        public async Task<IEnumerable<T>> GetAll() => await _databaseContext.Set<T>().ToListAsync();

        public async Task<T> GetById(Guid id) => await _databaseContext.Set<T>().FirstOrDefaultAsync(entity => entity.Id == id);

        public async Task<IEnumerable<T>> GetWhere(Expression<Func<T, bool>> predicate) => await _databaseContext.Set<T>().Where(predicate).ToListAsync();

        public async Task<T> Update(T entity)
        {
            var newEntry = _databaseContext.Set<T>().First(t => t.Id == entity.Id);
            _databaseContext.Entry(newEntry).CurrentValues.SetValues(entity);
            await _databaseContext.SaveChangesAsync();
            return entity;
        }

        public async Task<T> Remove(Guid id)
        {
            var entity = _databaseContext.Set<T>().First(t => t.Id == id);

            _databaseContext.Set<T>().Remove(entity);
            await _databaseContext.SaveChangesAsync();

            return entity;
        }

        #endregion CRUD Methods
    }
}