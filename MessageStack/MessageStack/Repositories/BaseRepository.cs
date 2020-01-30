using MessageStack.Context;
using MessageStack.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
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
        T Add(T entity);

        /// <summary>
        /// Gets the first or default object based on the predicate
        /// </summary>
        /// <param name="predicate">The condition(s) that the object must meet</param>
        /// <returns>Returns the first object that meets the specified condition(s)</returns>
        T FirstOrDefault(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Get all objects from the database table
        /// </summary>
        /// <returns>An enumerator containing all objects from the database table</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Get an object by Id
        /// </summary>
        /// <param name="id">The Id of the object</param>
        /// <returns>Returns the object with the specified Id</returns>
        T GetById(Guid id);

        /// <summary>
        /// Get all objects from the database table based on the specified condition
        /// </summary>
        /// <param name="predicate">The condition(s) that the object(s) must meet</param>
        /// <returns>An enumerator containing all objects, that meet the condition specified, from the database table</returns>
        IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Remove an object from the database table
        /// </summary>
        /// <param name="id">The id of the object to be removed from the database table</param>
        /// <returns>The object that has been removed from the database table</returns>
        T Remove(Guid id);

        /// <summary>
        /// Updates an object in the database table
        /// </summary>
        /// <param name="entity">The object to be updated to the database table</param>
        /// <returns>The object that has been updated to the database table</returns>
        T Update(T entity);
    }

    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        protected MessageStackContext _databaseContext = null;

        protected BaseRepository(MessageStackContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        #region CRUD Methods

        public T Add(T entity)
        {
            entity.Id = Guid.NewGuid();
            try
            {
                _databaseContext.Set<T>().Attach(entity);
            }
            catch
            {
                _databaseContext.Entry(entity).State = EntityState.Added;
                _databaseContext.Set<T>().Attach(entity);
            }

            _databaseContext.Set<T>().Add(entity);
            _databaseContext.SaveChanges();
            return entity;
        }

        public T FirstOrDefault(Expression<Func<T, bool>> predicate) => _databaseContext.Set<T>().AsNoTracking().FirstOrDefault(predicate);

        public IEnumerable<T> GetAll() => _databaseContext.Set<T>().AsNoTracking().ToList();

        public T GetById(Guid id) => _databaseContext.Set<T>().AsNoTracking().FirstOrDefault(entity => entity.Id == id);

        public IEnumerable<T> GetWhere(Expression<Func<T, bool>> predicate) => _databaseContext.Set<T>().AsNoTracking().Where(predicate).ToList();

        public T Update(T entity)
        {
            var dbEntry = _databaseContext.Set<T>().Find(entity.Id);

            _databaseContext.Set<T>().Attach(dbEntry);
            _databaseContext.Entry(dbEntry).CurrentValues.SetValues(entity);
            _databaseContext.Set<T>().AddOrUpdate(dbEntry);
            _databaseContext.SaveChanges();
            return dbEntry;
        }

        public T Remove(Guid id)
        {
            var entity = _databaseContext.Set<T>().First(t => t.Id == id);
            _databaseContext.Set<T>().Remove(entity);
            Detach(entity);
            _databaseContext.SaveChanges();

            return entity;
        }
        void Detach(T entity)
        {
            _databaseContext.Entry(entity).State = EntityState.Detached;
        }

        #endregion CRUD Methods
    }
}