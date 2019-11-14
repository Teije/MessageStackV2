using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using MessageStack.Models;

namespace MessageStack.Repositories
{
    public abstract class GenericRepository<T> where T : class
    {
        protected MessageStackContext Context;

        protected GenericRepository()
        {
            Context = new MessageStackContext();
        }

        /// <summary>
        /// Returns an entry from the database by id
        /// </summary>
        public virtual T Get(Guid id) => Context.Set<T>().Find(id);
        /// <summary>
        /// Returns an entry from the database by id
        /// </summary>
        public virtual T Get(string id) => Context.Set<T>().Find(id);

        /// <summary>
        /// Returns an entry from the database that matches the condition
        /// </summary>
        public virtual T Find(Expression<Func<T, bool>> match) => Context.Set<T>().SingleOrDefault(match);

        /// <summary>
        /// Returns all entries from the database that match the condition
        /// </summary>
        public virtual List<T> FindAll(Expression<Func<T, bool>> match)
        {
            var source = Context.Set<T>();
            var list = new List<T>(source);
            return list;
        }

        /// <summary>
        /// Adds an entry to the database
        /// </summary>
        public virtual T Add(T t)
        {
            Context.Set<T>().Add(t);
            Context.SaveChanges();
            return t;
        }

        /// <summary>
        /// Deletes the specified entry from the database
        /// </summary>
        public virtual void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
        }

        /// <summary>
        /// Updates the specified entry in the database
        /// </summary>
        public virtual T Update(T t, object key)
        {
            if (t == null)
                return null;
            var exist = Context.Set<T>().Find(key);
            if (exist == null) return null;
            Context.Entry(exist).CurrentValues.SetValues(t);
            Context.SaveChanges();

            return exist;
        }

        /// <summary>
        /// Saves all changes made in this context to the underlying database
        /// </summary>
        public virtual void Save() => Context.SaveChanges();
    }
}