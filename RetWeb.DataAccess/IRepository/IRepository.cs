﻿using System.Linq.Expressions;

namespace Cartopia.DataAccess.IRepository
{
    public interface IRepository<T> where T : class  // since we are working with generic interface and do not know what the class Type will be we will give it a Type <T>
    {                                               //T- Category or any other generic model which we want to perform the operation or want to interact with the dbContext
                                                    // this will be a generic Interface Repository for the app
        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="includeProperties"></param>
        /// <returns> list of all entities</returns>
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null, bool tracked = false);


        /// <summary>
        ///  Get a single category
        /// </summary>
        ///<param name="includeProperties"></param>
        /// <returns>single category</returns>
        /// // here we pass a function that returns a boolean.
        // we choose this to be more flexible in what parameter we choose to pass for an individual record.
        // think about it like the same syntax as FirstOrDefault
        //the tracked stops EF from tracking items retrieved from the db, so we can either update it or not.
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked= false);

        /// <summary>
        /// Add method
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// Delete Category
        /// </summary>
        /// <param name="entity"></param>
        void Remove(T entity);

        /// <summary>
        /// Delete a collection of entities
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<T> entities);

    }
}
